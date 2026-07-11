const ACCESS_TOKEN_KEY = 'accessToken'
const REFRESH_TOKEN_KEY = 'refreshToken'

export interface AuthSession {
  accessToken: string
  refreshToken: string
  username: string
  email: string
  role: string
}

export function getAccessToken(): string | null {
  return localStorage.getItem(ACCESS_TOKEN_KEY)
}

export function getRefreshToken(): string | null {
  return localStorage.getItem(REFRESH_TOKEN_KEY)
}

export function setTokens(accessToken: string, refreshToken: string): void {
  localStorage.setItem(ACCESS_TOKEN_KEY, accessToken)
  localStorage.setItem(REFRESH_TOKEN_KEY, refreshToken)
}

export function saveAuthSession(data: Record<string, unknown>): void {
  const accessToken = String(data.accessToken ?? data.AccessToken ?? '')
  const refreshToken = String(data.refreshToken ?? data.RefreshToken ?? '')

  setTokens(accessToken, refreshToken)
  localStorage.setItem('username', String(data.username ?? data.Username ?? ''))
  localStorage.setItem('email', String(data.email ?? data.Email ?? ''))
  let role = String(data.role ?? data.Role ?? '')
  if (!role && accessToken) {
    const payload = parseJwtPayload(accessToken)
    const roleClaim = payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload?.role || payload?.Role || ''
    role = String(Array.isArray(roleClaim) ? roleClaim[0] : roleClaim)
  }
  localStorage.setItem('role', role)
}

export function clearAuthSession(): void {
  localStorage.removeItem(ACCESS_TOKEN_KEY)
  localStorage.removeItem(REFRESH_TOKEN_KEY)
  localStorage.removeItem('role')
  localStorage.removeItem('username')
  localStorage.removeItem('email')
}

export function parseJwtPayload(token: string): Record<string, unknown> | null {
  try {
    const payload = token.split('.')[1]
    if (!payload) return null

    const base64 = payload.replace(/-/g, '+').replace(/_/g, '/')
    const json = decodeURIComponent(
      atob(base64)
        .split('')
        .map((char) => `%${char.charCodeAt(0).toString(16).padStart(2, '0')}`)
        .join('')
    )

    return JSON.parse(json) as Record<string, unknown>
  } catch {
    return null
  }
}

export function isTokenExpired(token: string | null, bufferSeconds = 60): boolean {
  if (!token) return true

  const payload = parseJwtPayload(token)
  const exp = payload?.exp
  if (typeof exp !== 'number') return true

  return Date.now() >= (exp - bufferSeconds) * 1000
}

export function isAuthenticated(): boolean {
  const token = getAccessToken()
  return !!token && !isTokenExpired(token)
}

export function getUserRole(): string | null {
  return localStorage.getItem('role')
}
