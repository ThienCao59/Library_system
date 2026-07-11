/** Parse API datetime as UTC (backend stores DateTime.UtcNow). */
export function parseUtcDate(dateStr: string | Date | null | undefined): Date | null {
  if (!dateStr) return null
  if (dateStr instanceof Date) return dateStr

  const raw = String(dateStr).trim()
  if (!raw) return null

  const hasTz = raw.endsWith('Z') || /[+-]\d{2}:\d{2}$/.test(raw)
  const normalized = hasTz ? raw : `${raw.replace(/\.\d{3,7}$/, '')}Z`
  const d = new Date(normalized)
  return Number.isNaN(d.getTime()) ? null : d
}

export function formatVnDateTime(dateStr: string | Date | null | undefined): string {
  const d = parseUtcDate(dateStr)
  if (!d) return '-'
  return d.toLocaleString('vi-VN', {
    timeZone: 'Asia/Ho_Chi_Minh',
    hour: '2-digit',
    minute: '2-digit',
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  })
}

export function formatVnDate(dateStr: string | Date | null | undefined): string {
  const d = parseUtcDate(dateStr)
  if (!d) return '-'
  return d.toLocaleDateString('vi-VN', { timeZone: 'Asia/Ho_Chi_Minh' })
}
