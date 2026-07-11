/// <reference types="vite/client" />

interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string
  readonly VITE_N2_LIBRARIAN_URL?: string
  readonly VITE_N2_READER_URL?: string
  readonly VITE_N1_BOOKS_URL?: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}

declare module '@/utils/excelHelper.js';
