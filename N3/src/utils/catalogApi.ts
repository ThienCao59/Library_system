/**
 * N1 Catalog Service — gọi qua API Gateway: /api/catalog/* → catalog-service:5185/api/*
 */
import { apiClient } from './apiClient'

const CATALOG_PREFIX = '/api/catalog'

export interface BookReview {
  id: string
  bookId: string
  userId: string
  cardNumber: string
  username: string
  fullName: string
  rating: number
  comment: string
  createdAt: string
}

export interface CategoryUsage {
  isUsed: boolean
  bookNames: string[]
}

export interface WriteOffPayload {
  quantity: number
  reason: 'HeavyDamage' | 'Lost'
  note?: string
}

export interface PostReviewPayload {
  rating: number
  comment: string
  userId?: string
  cardNumber?: string
  username?: string
  fullName?: string
}

/** GET /api/categories */
export async function fetchCategories() {
  const { data } = await apiClient.get(`${CATALOG_PREFIX}/categories`)
  return data
}

/** GET /api/categories/{id}/usage */
export async function fetchCategoryUsage(categoryId: string | number): Promise<CategoryUsage> {
  const { data } = await apiClient.get(`${CATALOG_PREFIX}/categories/${categoryId}/usage`)
  return data
}

/** GET /api/books — có averageRating, reviewCount, latestReviews (N1 bản mới) */
export async function fetchBooks() {
  const { data } = await apiClient.get(`${CATALOG_PREFIX}/books`)
  return data
}

/** GET /api/books/{id}/reviews */
export async function fetchBookReviews(bookId: string | number): Promise<BookReview[]> {
  const { data } = await apiClient.get(`${CATALOG_PREFIX}/books/${bookId}/reviews`)
  return data
}

/** POST /api/books/{id}/reviews */
export async function postBookReview(bookId: string | number, payload: PostReviewPayload) {
  const { data } = await apiClient.post(`${CATALOG_PREFIX}/books/${bookId}/reviews`, payload)
  return data
}

/** POST /api/books/{id}/write-off */
export async function writeOffBook(bookId: string | number, payload: WriteOffPayload) {
  const { data } = await apiClient.post(`${CATALOG_PREFIX}/books/${bookId}/write-off`, payload)
  return data
}

/** GET /api/stats/popular */
export async function fetchPopularBooks() {
  const { data } = await apiClient.get(`${CATALOG_PREFIX}/stats/popular`)
  return data
}
