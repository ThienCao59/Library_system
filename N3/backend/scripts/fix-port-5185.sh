#!/bin/bash
# Giải phóng port 5185 khi deploy lại Catalog Service (N1) bị:
# "Bind for :::5185 failed: port is already allocated"
set -euo pipefail

echo "=== Kiểm tra container đang chiếm port 5185 ==="
docker ps -a --format 'table {{.Names}}\t{{.Ports}}\t{{.Status}}' | grep -E '5185|NAMES' || true

echo ""
echo "=== Dừng container catalog cũ (nếu có) ==="
docker rm -f library_catalog_service 2>/dev/null || true

echo ""
echo "=== Dừng mọi container khác đang bind 5185 ==="
for cid in $(docker ps -q); do
  if docker port "$cid" 2>/dev/null | grep -q '5185'; then
    name=$(docker inspect -f '{{.Name}}' "$cid" | sed 's/^\///')
    echo "Removing $name ($cid)"
    docker rm -f "$cid"
  fi
done

echo ""
echo "=== Kiểm tra lại port 5185 ==="
if ss -tlnp 2>/dev/null | grep -q ':5185 '; then
  echo "WARN: Port 5185 vẫn bị chiếm bởi process ngoài Docker:"
  ss -tlnp | grep ':5185 ' || true
else
  echo "OK: Port 5185 đã trống."
  echo "Catalog (N1) KHÔNG nằm trong docker-compose.yml N3."
  echo "Chạy: bash scripts/restore-catalog-container.sh"
  echo "Hoặc nhờ N1 deploy lại từ repo/image của họ."
fi
