// src/utils/excelHelper.js
import * as XLSX from 'xlsx';

/**
 * Export an array of reader objects to an Excel file.
 * @param {Array<Object>} readers - List of reader objects.
 * @returns {Blob} Excel file as a Blob.
 */
export function exportReadersToXlsx(readers) {
  const ws = XLSX.utils.json_to_sheet(readers);
  const wb = XLSX.utils.book_new();
  XLSX.utils.book_append_sheet(wb, ws, 'Readers');
  const wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });
  return new Blob([wbout], { type: 'application/octet-stream' });
}

/**
 * Normalize Excel row keys to lowercase for consistent access.
 * @param {Array<Object>} rows
 * @returns {Array<Object>}
 */
export function normalizeExcelRows(rows) {
  if (!Array.isArray(rows)) return [];

  return rows
    .map((row) => {
      const normalized = {};
      Object.entries(row || {}).forEach(([key, value]) => {
        const k = String(key).trim().toLowerCase().replace(/\s+/g, '');
        normalized[k] = typeof value === 'string' ? value.trim() : value;
      });
      return normalized;
    })
    .filter((row) => Object.values(row).some((v) => v !== '' && v != null));
}

/**
 * Import an Excel file and convert it to JSON array.
 * @param {File} file - .xlsx file selected by the user.
 * @returns {Promise<Array<Object>>} Parsed data with normalized keys.
 */
export function importXlsxFile(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = (e) => {
      const data = e.target?.result;
      const binary = '' + new Uint8Array(data).reduce((s, byte) => s + String.fromCharCode(byte), '');
      try {
        const wb = XLSX.read(binary, { type: 'binary' });
        const wsName = wb.SheetNames[0];
        const ws = wb.Sheets[wsName];
        const json = XLSX.utils.sheet_to_json(ws, { defval: '' });
        resolve(normalizeExcelRows(json));
      } catch (err) {
        reject(err);
      }
    };
    reader.onerror = (e) => reject(e);
    reader.readAsArrayBuffer(file);
  });
}
