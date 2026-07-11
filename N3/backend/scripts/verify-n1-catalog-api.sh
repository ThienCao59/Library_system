#!/bin/bash
# Kiểm tra N1 Catalog API qua API Gateway (N3)
# Usage: GATEWAY=http://163.223.210.87:5000 TOKEN=<jwt> ./verify-n1-catalog-api.sh
set -euo pipefail

GATEWAY="${GATEWAY:-http://localhost:5000}"
AUTH_HEADER=()
if [[ -n "${TOKEN:-}" ]]; then
  AUTH_HEADER=(-H "Authorization: Bearer $TOKEN")
fi

call() {
  local method="$1"
  local path="$2"
  local body="${3:-}"
  echo ""
  echo ">>> $method $GATEWAY$path"
  if [[ -n "$body" ]]; then
    curl -sS -w "\nHTTP %{http_code}\n" -X "$method" "${AUTH_HEADER[@]}" \
      -H "Content-Type: application/json" \
      -d "$body" "$GATEWAY$path" | head -c 2000
  else
    curl -sS -w "\nHTTP %{http_code}\n" -X "$method" "${AUTH_HEADER[@]}" \
      "$GATEWAY$path" | head -c 2000
  fi
}

echo "Gateway: $GATEWAY"
echo "(Ocelot map: /api/catalog/* -> catalog-service:5185/api/*)"

call GET "/api/catalog/categories"
call GET "/api/catalog/books"
call GET "/api/catalog/stats/popular"

# Thay {id} bằng book/category thật khi test
if [[ -n "${BOOK_ID:-}" ]]; then
  call GET "/api/catalog/books/${BOOK_ID}/reviews"
fi
if [[ -n "${CATEGORY_ID:-}" ]]; then
  call GET "/api/catalog/categories/${CATEGORY_ID}/usage"
fi

echo ""
echo "Done. POST reviews/write-off cần TOKEN + BOOK_ID — test thủ công khi có dữ liệu."
