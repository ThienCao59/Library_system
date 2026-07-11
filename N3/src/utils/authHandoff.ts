import { apiClient } from './apiClient'

export const N2_LIBRARIAN_URL =
  import.meta.env.VITE_N2_LIBRARIAN_URL || 'http://163.223.210.87:5082/ui/librarian/'

/** Trang xử lý mượn/trả của N2 (hash route) */
export const N2_LIBRARIAN_LOANS_URL =
  import.meta.env.VITE_N2_LIBRARIAN_LOANS_URL ||
  `${N2_LIBRARIAN_URL.replace(/\/?$/, '/')}?embed=1#/loans`

/** Trang doanh thu của N2 (hash route #/payments) */
export const N2_LIBRARIAN_PAYMENTS_URL =
  import.meta.env.VITE_N2_LIBRARIAN_PAYMENTS_URL ||
  `${N2_LIBRARIAN_URL.replace(/\/?$/, '/')}?embed=1#/finance/revenue`

/** Trang phí phạt của N2 (hash route #/fines) */
export const N2_LIBRARIAN_FINES_URL =
  import.meta.env.VITE_N2_LIBRARIAN_FINES_URL ||
  `${N2_LIBRARIAN_URL.replace(/\/?$/, '/')}?embed=1#/fines`

/** Trang đánh giá của N2 (embed/reviews) */
export const N2_LIBRARIAN_REVIEWS_URL =
  import.meta.env.VITE_N2_LIBRARIAN_REVIEWS_URL ||
  `${N2_LIBRARIAN_URL.replace(/\/?$/, '/')}embed/reviews`

export const N2_READER_URL =
  import.meta.env.VITE_N2_READER_URL || 'http://163.223.210.87:5082/ui/reader/'

export const N1_BOOKS_URL =
  import.meta.env.VITE_N1_BOOKS_URL || 'http://163.223.210.87:8081/books'

export async function createAuthHandoffCode(): Promise<string> {
  const { data } = await apiClient.post('/api/identity/Auth/handoff')
  return data.code || data.Code
}

export function buildHandoffUrl(baseUrl: string, code: string): string {
  const url = new URL(baseUrl)
  url.searchParams.set('code', code)
  url.searchParams.delete('token')
  return url.toString()
}

export async function redirectWithHandoffCode(baseUrl: string): Promise<void> {
  const code = await createAuthHandoffCode()
  window.location.href = buildHandoffUrl(baseUrl, code)
}

/** Dùng ở N1/N2: đổi ?code= thành JWT qua POST, không để token trên URL */
export async function redeemAuthHandoffCode(code: string): Promise<Record<string, unknown>> {
  const { data } = await apiClient.post('/api/identity/Auth/handoff/redeem', { code })
  return data
}
