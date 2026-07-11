import axios, { type AxiosError, type InternalAxiosRequestConfig } from 'axios'
import {
  clearAuthSession,
  getAccessToken,
  getRefreshToken,
  isTokenExpired,
  setTokens,
} from './auth'

export const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL || 'http://163.223.210.87:5000'

export const apiClient = axios.create({
  baseURL: API_BASE_URL,
  timeout: 30000,
})

let refreshPromise: Promise<string | null> | null = null

async function refreshAccessToken(): Promise<string | null> {
  const accessToken = getAccessToken()
  const refreshToken = getRefreshToken()

  if (!accessToken || !refreshToken) return null

  try {
    const { data } = await axios.post(`${API_BASE_URL}/api/identity/Auth/refresh`, {
      AccessToken: accessToken,
      RefreshToken: refreshToken,
    })

    const newAccessToken = data.accessToken || data.AccessToken
    const newRefreshToken = data.refreshToken || data.RefreshToken

    if (!newAccessToken || !newRefreshToken) return null

    setTokens(newAccessToken, newRefreshToken)
    return newAccessToken
  } catch {
    return null
  }
}

function getRefreshPromise(): Promise<string | null> {
  if (!refreshPromise) {
    refreshPromise = refreshAccessToken().finally(() => {
      refreshPromise = null
    })
  }

  return refreshPromise
}

function redirectToLogin(): void {
  clearAuthSession()
  if (!window.location.pathname.startsWith('/login')) {
    window.location.href = '/login'
  }
}

apiClient.interceptors.request.use(async (config: InternalAxiosRequestConfig) => {
  let token = getAccessToken()
  const isRefreshCall = config.url?.includes('/Auth/refresh')

  if (token && isTokenExpired(token) && !isRefreshCall) {
    token = await getRefreshPromise()
  }

  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }

  return config
})

apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as InternalAxiosRequestConfig & { _retry?: boolean }
    const status = error.response?.status
    const isAuthEndpoint =
      originalRequest?.url?.includes('/Auth/login') ||
      originalRequest?.url?.includes('/Auth/register') ||
      originalRequest?.url?.includes('/Auth/forgot-password') ||
      originalRequest?.url?.includes('/Auth/reset-password')

    if (status === 401 && originalRequest && !originalRequest._retry && !isAuthEndpoint) {
      originalRequest._retry = true

      const newToken = await getRefreshPromise()
      if (newToken) {
        originalRequest.headers.Authorization = `Bearer ${newToken}`
        return apiClient(originalRequest)
      }

      redirectToLogin()
    } else if (status === 403 && !isAuthEndpoint) {
      redirectToLogin()
    }

    return Promise.reject(error)
  }
)

export async function logout(): Promise<void> {
  try {
    await apiClient.post('/api/identity/Auth/logout')
  } catch {
    // Still clear local session even if server is unreachable
  } finally {
    clearAuthSession()
  }
}
