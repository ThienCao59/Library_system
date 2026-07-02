<template>
  <div class="page-shell">
    <a-card class="hero-card">
      <div class="hero-copy">
        <div class="eyebrow">Quản lý mượn trả</div>
        <h2>Danh sách phiếu mượn</h2>
        <p>Duyệt yêu cầu mượn, gia hạn và theo dõi các trạng thái xử lý.</p>
      </div>
      <div class="hero-summary">
        <div class="hero-badges">
          <div class="hero-badge">
            <span class="badge-label">Chờ duyệt</span>
            <strong>{{ store.pendingTx.length }}</strong>
          </div>
          <div class="hero-badge">
            <span class="badge-label">Đang mượn</span>
            <strong>{{ store.borrowedTx.length }}</strong>
          </div>
          <div class="hero-badge">
            <span class="badge-label">Chờ trả</span>
            <strong>{{ store.returnPendingTx.length }}</strong>
          </div>
          <div class="hero-badge alert">
            <span class="badge-label">Quá hạn</span>
            <strong>{{ store.overdueTx.length }}</strong>
          </div>
        </div>
      </div>
    </a-card>

    <a-card class="panel-card">
      <div class="toolbar">
        <a-radio-group v-model:value="filter" button-style="solid" size="small">
          <a-radio-button value="all">Tất cả</a-radio-button>
          <a-radio-button value="Pending">Chờ duyệt</a-radio-button>
          <a-radio-button value="Borrowed">Đang mượn</a-radio-button>
          <a-radio-button value="RenewPending">Gia hạn</a-radio-button>
          <a-radio-button value="ReturnPending">Chờ trả</a-radio-button>
          <a-radio-button value="Overdue">Quá hạn</a-radio-button>
          <a-radio-button value="Returned">Đã trả</a-radio-button>
        </a-radio-group>

        <a-space v-if="!readOnlyEmbed" wrap>
          <a-button
            type="primary"
            :disabled="!bulkApproveIds.length"
            :loading="approvingAll"
            @click="approveSelected"
          >
            <CheckOutlined /> Duyệt {{ bulkApproveIds.length || 'tất cả' }}
          </a-button>
          <a-button
            :disabled="!pendingFilteredTx.length"
            :loading="approvingAll"
            @click="approveAllPending"
          >
            Duyệt tất cả chờ duyệt
          </a-button>
          <a-button
            :disabled="!bulkRenewIds.length"
            :loading="approvingAll"
            @click="renewSelected"
          >
            <ReloadOutlined /> Duyệt gia hạn {{ bulkRenewIds.length || '' }}
          </a-button>
          <a-button
            :disabled="!renewFilteredTx.length"
            :loading="approvingAll"
            @click="renewAllPending"
          >
            Duyệt tất cả gia hạn
          </a-button>
          <a-button
            :disabled="!bulkReturnIds.length"
            :loading="approvingAll"
            @click="returnSelectedAsGood"
          >
            <CheckOutlined /> Trả sách tốt {{ bulkReturnIds.length || '' }}
          </a-button>
          <a-button
            danger
            ghost
            :disabled="!returnFilteredTx.length"
            :loading="approvingAll"
            @click="returnAllAsGood"
          >
            Trả tất cả là sách tốt
          </a-button>
        </a-space>

        <a-tag color="blue">{{ filteredTx.length }} phiếu</a-tag>
      </div>

      <a-table
        :columns="columns"
        :data-source="filteredTx"
        :loading="store.loading"
        :pagination="{ pageSize: 10 }"
        :row-selection="rowSelection"
        bordered
        size="middle"
        row-key="Id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'reader'">
            <div>
              <div class="font-medium">{{ readerNameOf(record) }}</div>
              <div class="muted">{{ store.cardNumberOf(record) }}</div>
            </div>
          </template>
          <template v-else-if="column.key === 'book'">
            <div>
              <div class="font-medium">{{ bookTitleOf(record) }}</div>
              <div class="muted">Book ID: {{ store.bookIdOf(record) }}</div>
            </div>
          </template>
          <template v-else-if="column.key === 'Status'">
            <div class="status-wrap">
              <a-tag :color="statusColor(record)">{{ statusLabel(record) }}</a-tag>
              <div class="status-detail">{{ statusDetail(record) }}</div>
            </div>
          </template>
          <template v-else-if="column.key === 'BorrowedAt'">{{ fmtDateTime(record.BorrowedAt) }}</template>
          <template v-else-if="column.key === 'DueAt'">{{ fmtDateTime(record.DueAt) }}</template>
          <template v-else-if="column.key === 'actions'">
            <a-space v-if="store.isPending(record)">
              <a-button type="primary" size="small" :loading="actionId === record.Id + 'a'" @click="doApprove(record)">
                <CheckOutlined /> Duyệt mượn
              </a-button>
              <a-button danger size="small" :loading="actionId === record.Id + 'r'" @click="doReject(record)">
                <CloseOutlined /> Từ chối
              </a-button>
            </a-space>
            <a-space v-else-if="isRenewPending(record)">
              <a-button type="primary" size="small" :loading="actionId === record.Id + 're'" @click="doRenew(record)">
                <ReloadOutlined /> Duyệt gia hạn
              </a-button>
              <a-button danger size="small" :loading="actionId === record.Id + 'rrn'" @click="doRejectRenew(record)">
                <CloseOutlined /> Từ chối gia hạn
              </a-button>
            </a-space>
            <a-space v-else-if="store.isReturnPending(record)">
              <a-button type="primary" size="small" @click="openConditionDialog(record)">
                <CheckOutlined /> Kiểm tra & trả
              </a-button>
              <a-button size="small" :loading="actionId === record.Id + 'rr'" @click="doRejectReturn(record)">
                <CloseOutlined /> Chưa nhận
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <a-modal
      v-model:open="conditionDialog"
      title="Kiểm tra tình trạng sách"
      ok-text="Xác nhận trả"
      cancel-text="Hủy"
      :confirm-loading="confirming"
      @ok="doApproveReturn"
    >
      <div v-if="selectedReturn" class="condition-summary">
        <div class="font-medium">{{ selectedReturn.TenSach || selectedReturn.BookId }}</div>
        <div class="muted">{{ selectedReturn.CardNumber }} · Book ID: {{ selectedReturn.BookId }}</div>
      </div>

      <a-form layout="vertical">
        <a-form-item label="Tình trạng sách khi nhận lại" required>
          <a-select v-model:value="conditionForm.condition">
            <a-select-option value="Good">Tốt, dùng bình thường</a-select-option>
            <a-select-option value="LightDamage">Hư nhẹ / bẩn nhẹ</a-select-option>
            <a-select-option value="HeavyDamage">Hư nặng / rách nhiều</a-select-option>
            <a-select-option value="Lost">Mất sách</a-select-option>
          </a-select>
        </a-form-item>

        <a-form-item label="Ghi chú kiểm tra">
          <a-textarea
            v-model:value="conditionForm.conditionNote"
            :rows="4"
            placeholder="Ví dụ: Bìa hơi cong, trang 24 có vết bẩn..."
            allow-clear
          />
        </a-form-item>
        </a-form>
      </a-modal>

    <a-modal
      v-model:open="rejectDialog"
      :title="rejectDialogTitle"
      ok-text="Từ chối"
      cancel-text="Hủy"
      :confirm-loading="rejecting"
      @ok="confirmReject"
    >
      <div v-if="selectedReject" class="condition-summary">
        <div class="font-medium">{{ rejectSummaryTitle }}</div>
        <div class="muted">{{ rejectSummaryDetail }}</div>
      </div>

      <a-form layout="vertical">
        <a-form-item label="Lý do từ chối" required>
          <a-textarea
            v-model:value="rejectForm.reason"
            :rows="4"
            :placeholder="rejectPlaceholder"
            allow-clear
          />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script setup>
import { ref, computed, reactive, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { message } from 'ant-design-vue'
import dayjs from 'dayjs'
import { useLibrarianStore } from '@/stores/librarian'
import { CheckOutlined, CloseOutlined, ReloadOutlined } from '@ant-design/icons-vue'

const store = useLibrarianStore()
const route = useRoute()
const embedMode = store.embedMode
const readOnlyEmbed = computed(() => embedMode && !store.hasAuthToken())
const filter = ref('all')
const actionId = ref(null)
const selectedRowKeys = ref([])
const approvingAll = ref(false)
const conditionDialog = ref(false)
const confirming = ref(false)
const selectedReturn = ref(null)
const rejectDialog = ref(false)
const rejecting = ref(false)
const selectedReject = ref(null)
const conditionForm = reactive({
  condition: 'Good',
  conditionNote: ''
})
const rejectForm = reactive({
  reason: ''
})

const columns = computed(() => {
  const baseColumns = [
    { title: 'Độc giả', key: 'reader', width: 220 },
    { title: 'Sách', key: 'book' },
    { title: 'Ngày mượn', key: 'BorrowedAt', width: 120 },
    { title: 'Hạn trả', key: 'DueAt', width: 120 },
    { title: 'Trạng thái', key: 'Status', width: 160 }
  ]

  if (readOnlyEmbed.value) return baseColumns
  return [...baseColumns, { title: 'Hành động', key: 'actions', width: 280 }]
})

const filteredTx = computed(() => {
  const source = (() => {
    if (filter.value === 'all') return store.transactions
    if (filter.value === 'Pending') return store.pendingTx
    if (filter.value === 'Borrowed') return store.borrowedTx
    if (filter.value === 'ReturnPending') return store.returnPendingTx
    if (filter.value === 'Overdue') return store.overdueTx
    if (filter.value === 'Returned') return store.returnedTx
    return store.transactions.filter(t => store.statusOf(t) === filter.value)
  })()

  const q = String(route.query.q || '').trim()
  if (!q) return source
  return source.filter(record => store.matchesReaderQuery(record, q, [bookTitleOf(record), store.bookIdOf(record)]))
})

const pendingFilteredTx = computed(() => filteredTx.value.filter(record => store.isPending(record)))
const renewFilteredTx = computed(() => filteredTx.value.filter(record => isRenewPending(record)))
const returnFilteredTx = computed(() => filteredTx.value.filter(record => store.isReturnPending(record)))

const bulkApproveIds = computed(() => selectedRowKeys.value.filter(id => {
  const record = filteredTx.value.find(item => String(item.Id || item.id) === String(id))
  return record && store.isPending(record)
}))

const bulkRenewIds = computed(() => selectedRowKeys.value.filter(id => {
  const record = filteredTx.value.find(item => String(item.Id || item.id) === String(id))
  return record && isRenewPending(record)
}))

const bulkReturnIds = computed(() => selectedRowKeys.value.filter(id => {
  const record = filteredTx.value.find(item => String(item.Id || item.id) === String(id))
  return record && store.isReturnPending(record)
}))

function isBulkSelectable(record) {
  return store.isPending(record) || isRenewPending(record) || store.isReturnPending(record)
}

const rowSelection = computed(() => {
  if (readOnlyEmbed.value) return null

  return {
    selectedRowKeys: selectedRowKeys.value,
    preserveSelectedRowKeys: false,
    onChange: keys => {
      selectedRowKeys.value = keys
    },
    getCheckboxProps: record => ({
      disabled: !isBulkSelectable(record),
      title: store.isPending(record) ? 'Chọn để duyệt hàng loạt' : 'Chỉ chọn được phiếu chờ duyệt'
    })
  }
})

async function approveIds(ids = []) {
  const targetIds = [...new Set(ids.map(id => String(id || '').trim()).filter(Boolean))]
  if (!targetIds.length) {
    message.warning('Chưa có phiếu chờ duyệt nào được chọn.')
    return
  }

  approvingAll.value = true
  try {
    const results = await store.approveMany(targetIds)
    const successCount = results.filter(item => item.ok).length
    const failedCount = results.length - successCount
    selectedRowKeys.value = []

    if (successCount && !failedCount) {
      message.success(`Đã duyệt ${successCount} phiếu mượn.`)
    } else if (successCount) {
      message.warning(`Đã duyệt ${successCount} phiếu, ${failedCount} phiếu bị lỗi.`)
    } else {
      message.error('Không duyệt được phiếu nào.')
    }
  } finally {
    approvingAll.value = false
  }
}

async function approveSelected() {
  await approveIds(bulkApproveIds.value)
}

async function approveAllPending() {
  await approveIds(pendingFilteredTx.value.map(record => record.Id || record.id))
}

async function renewIds(ids = []) {
  const targetIds = [...new Set(ids.map(id => String(id || '').trim()).filter(Boolean))]
  if (!targetIds.length) {
    message.warning('Chưa có phiếu chờ gia hạn nào được chọn.')
    return
  }

  approvingAll.value = true
  try {
    const results = await store.renewMany(targetIds, {
      extraDays: 7,
      reason: 'Gia hạn hàng loạt theo đề nghị độc giả'
    })
    const successCount = results.filter(item => item.ok).length
    const failedCount = results.length - successCount
    selectedRowKeys.value = []

    if (successCount && !failedCount) {
      message.success(`Đã duyệt gia hạn ${successCount} phiếu.`)
    } else if (successCount) {
      message.warning(`Đã duyệt gia hạn ${successCount} phiếu, ${failedCount} phiếu bị lỗi.`)
    } else {
      message.error('Không duyệt được phiếu gia hạn nào.')
    }
  } finally {
    approvingAll.value = false
  }
}

async function renewSelected() {
  await renewIds(bulkRenewIds.value)
}

async function renewAllPending() {
  await renewIds(renewFilteredTx.value.map(record => record.Id || record.id))
}

async function returnIdsAsGood(ids = []) {
  const targetIds = [...new Set(ids.map(id => String(id || '').trim()).filter(Boolean))]
  if (!targetIds.length) {
    message.warning('Chưa có phiếu chờ trả nào được chọn.')
    return
  }

  approvingAll.value = true
  try {
    const results = await store.approveReturnMany(targetIds, {
      condition: 'Good',
      conditionNote: 'Xác nhận hàng loạt: sách tốt, dùng bình thường'
    })
    const successCount = results.filter(item => item.ok).length
    const failedCount = results.length - successCount
    selectedRowKeys.value = []

    if (successCount && !failedCount) {
      message.success(`Đã xác nhận trả ${successCount} phiếu là sách tốt.`)
    } else if (successCount) {
      message.warning(`Đã xác nhận trả ${successCount} phiếu, ${failedCount} phiếu bị lỗi.`)
    } else {
      message.error('Không xác nhận trả được phiếu nào.')
    }
  } finally {
    approvingAll.value = false
  }
}

async function returnSelectedAsGood() {
  await returnIdsAsGood(bulkReturnIds.value)
}

async function returnAllAsGood() {
  await returnIdsAsGood(returnFilteredTx.value.map(record => record.Id || record.id))
}

async function doApprove(r) {
  actionId.value = r.Id + 'a'
  const res = await store.approve(r.Id)
  if (res.ok) {
    selectedRowKeys.value = selectedRowKeys.value.filter(id => String(id) !== String(r.Id || r.id))
  }
  if (res.ok) message.success('Đã duyệt mượn sách.')
  else message.error(await readError(res))
  actionId.value = null
}

async function doReject(r) {
  openRejectDialog(r, 'borrow')
}

async function doRenew(r) {
  actionId.value = r.Id + 're'
  const res = await store.renew(r.Id, {
    extraDays: 7,
    reason: 'Gia hạn theo đề nghị độc giả'
  })
  if (res.ok) message.success('Đã duyệt gia hạn.')
  else message.error(await readError(res))
  actionId.value = null
}

async function doRejectRenew(r) {
  openRejectDialog(r, 'renew')
}

function openConditionDialog(record) {
  selectedReturn.value = record
  conditionForm.condition = 'Good'
  conditionForm.conditionNote = ''
  conditionDialog.value = true
}

async function doApproveReturn() {
  if (!selectedReturn.value) return
  confirming.value = true
  actionId.value = selectedReturn.value.Id + 'ra'
  const res = await store.approveReturn(selectedReturn.value.Id, {
    condition: conditionForm.condition,
    conditionNote: conditionForm.conditionNote
  })
  if (res.ok) {
    message.success('Đã kiểm tra tình trạng và xác nhận trả sách.')
    conditionDialog.value = false
    selectedReturn.value = null
  } else {
    message.error(await readError(res))
  }
  actionId.value = null
  confirming.value = false
}

async function doRejectReturn(r) {
  openRejectDialog(r, 'return')
}

function openRejectDialog(record, kind) {
  selectedReject.value = { record, kind }
  rejectForm.reason = defaultRejectReason(kind)
  rejectDialog.value = true
}

const rejectDialogTitle = computed(() => {
  if (!selectedReject.value) return 'Từ chối'
  if (selectedReject.value.kind === 'renew') return 'Từ chối gia hạn'
  if (selectedReject.value.kind === 'return') return 'Từ chối trả sách'
  return 'Từ chối yêu cầu mượn'
})

const rejectPlaceholder = computed(() => {
  if (!selectedReject.value) return 'Nhập lý do từ chối'
  if (selectedReject.value.kind === 'renew') return 'Nhập lý do từ chối gia hạn'
  if (selectedReject.value.kind === 'return') return 'Nhập lý do từ chối trả sách'
  return 'Nhập lý do từ chối yêu cầu mượn'
})

const rejectSummaryTitle = computed(() => {
  if (!selectedReject.value) return '—'
  const record = selectedReject.value.record || {}
  return `${readerNameOf(record)} · ${bookTitleOf(record)}`
})

const rejectSummaryDetail = computed(() => {
  if (!selectedReject.value) return '—'
  const record = selectedReject.value.record || {}
  return `${store.cardNumberOf(record)} · Book ID: ${store.bookIdOf(record)}`
})

function defaultRejectReason(kind) {
  if (kind === 'renew') return 'Không đủ điều kiện gia hạn'
  if (kind === 'return') return 'Không đủ điều kiện trả sách'
  return 'Không đủ điều kiện mượn sách'
}

async function confirmReject() {
  if (!selectedReject.value) return
  const record = selectedReject.value.record || {}
  const id = record.Id || record.id
  if (!id) return

  rejecting.value = true
  try {
    const kind = selectedReject.value.kind
    const reason = rejectForm.reason.trim() || defaultRejectReason(kind)
    let res
    if (kind === 'renew') {
      res = await store.rejectRenew(id, reason)
    } else if (kind === 'return') {
      res = await store.rejectReturn(id, reason)
    } else {
      res = await store.reject(id, reason)
    }

    if (res.ok) {
      message.success(
        kind === 'renew'
          ? 'Đã từ chối gia hạn.'
          : kind === 'return'
            ? 'Đã chuyển lại trạng thái đang mượn.'
            : 'Đã từ chối yêu cầu mượn.'
      )
      rejectDialog.value = false
      selectedReject.value = null
    } else {
      message.error(await readError(res))
    }
  } finally {
    rejecting.value = false
  }
}

async function readError(res) {
  const data = await res.json().catch(() => null)
  return data?.Message || data?.message || 'Lỗi xử lý.'
}

function readerNameOf(record = {}) {
  return (
    record.ReaderName ||
    record.readerName ||
    record.FullName ||
    record.fullName ||
    record.Username ||
    record.username ||
    store.cardNumberOf(record)
  )
}

function bookTitleOf(record = {}) {
  return store.bookTitleOf(record)
}

function formatDurationText(startValue, endValue) {
  const start = new Date(startValue)
  const end = new Date(endValue)
  if (Number.isNaN(start.getTime()) || Number.isNaN(end.getTime())) return '—'

  const diffMs = Math.max(0, end.getTime() - start.getTime())
  const totalSeconds = Math.floor(diffMs / 1000)
  const days = Math.floor(totalSeconds / 86400)
  const hours = Math.floor((totalSeconds % 86400) / 3600)
  const minutes = Math.floor((totalSeconds % 3600) / 60)
  const seconds = totalSeconds % 60

  return `${days} ngày ${String(hours).padStart(2, '0')}:${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`
}

function fmtDateTime(d) {
  return d ? dayjs(d).format('DD/MM/YYYY HH:mm:ss') : '—'
}

function statusColor(record) {
  if (store.isPending(record)) return 'orange'
  if (store.isReturnPending(record)) return 'purple'
  if (store.isOverdue(record)) return 'red'
  if (store.isReturned(record)) return 'green'
  return 'blue'
}

function statusLabel(record) {
  if (store.isPending(record)) return 'Chờ duyệt'
  if (store.isReturnPending(record)) return 'Chờ kiểm tra trả'
  if (isRenewPending(record)) return 'Chờ duyệt gia hạn'
  if (store.isOverdue(record)) return 'Quá hạn'
  if (store.isReturned(record)) return 'Đã trả'
  return 'Đang mượn'
}

function statusDetail(record) {
  if (store.isPending(record)) {
    return 'Chưa bắt đầu tính thời gian mượn'
  }

  const borrowedAt = record.BorrowedAt || record.borrowedAt
  const dueAt = record.DueAt || record.dueAt
  if (!borrowedAt || !dueAt) return '—'

  if (store.isReturnPending(record)) {
    return `Đã mượn: ${formatDurationText(borrowedAt, new Date())}`
  }

  if (isRenewPending(record)) {
    return `Đang chờ duyệt gia hạn: ${formatDurationText(borrowedAt, new Date())}`
  }

  if (store.isReturned(record)) {
    const returnedAt = record.ReturnedAt || record.returnedAt
    return returnedAt
      ? `Đã trả sau: ${formatDurationText(borrowedAt, returnedAt)}`
      : `Đã mượn: ${formatDurationText(borrowedAt, dueAt)}`
  }

  if (store.isOverdue(record)) {
    return `Quá hạn: ${formatDurationText(dueAt, new Date())}`
  }

  return `Còn lại: ${formatDurationText(new Date(), dueAt)}`
}

function isRenewPending(record) {
  return store.statusOf(record) === 'RenewPending'
}

onMounted(() => {
  if (!store.transactions.length || !store.fines.length) {
    store.loadAll()
  }
})
</script>

<style scoped>
.page-shell {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.hero-card,
.panel-card {
  border-radius: 18px !important;
  border: 1px solid #edf1ee !important;
  box-shadow: 0 10px 24px rgba(15, 23, 42, 0.04) !important;
}

.hero-card :deep(.ant-card-body) {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  gap: 24px;
  padding: 22px 24px;
}

.hero-copy {
  flex: 1 1 auto;
  min-width: 0;
}

.hero-copy h2 {
  margin: 6px 0 6px;
  font-size: 26px;
  font-weight: 800;
  color: #103b35;
}

.hero-copy p {
  margin: 0;
  color: #7d8a83;
}

.eyebrow {
  color: #1f5f55;
  font-size: 12px;
  font-weight: 800;
  text-transform: uppercase;
  letter-spacing: .08em;
}

.hero-summary {
  flex: 0 0 620px;
  max-width: 620px;
  display: flex;
  justify-content: flex-end;
}

.hero-badges {
  display: grid;
  grid-template-columns: repeat(4, minmax(120px, 1fr));
  gap: 12px;
  width: 100%;
}

.hero-badge {
  background: #f7fbf8;
  border: 1px solid #e5eee8;
  border-radius: 14px;
  padding: 14px 16px;
  display: flex;
  flex-direction: column;
  gap: 4px;
  align-items: flex-start;
}

.hero-badge.alert {
  background: #fff8ed;
  border-color: #f0d4a7;
}

.badge-label {
  color: #7d8a83;
  font-size: 12px;
  font-weight: 700;
}

.hero-badge strong {
  color: #103b35;
  font-size: 22px;
  line-height: 1;
}

.toolbar {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  margin-bottom: 16px;
}

.font-medium { font-weight: 600; }
.muted {
  color: #94a3b8;
  font-size: 12px;
  margin-top: 2px;
}

.condition-summary {
  padding: 12px 14px;
  margin-bottom: 16px;
  border: 1px solid #e5e7eb;
  border-radius: 8px;
  background: #f8fafc;
}

.status-wrap {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.status-detail {
  color: #6b7280;
  font-size: 12px;
  line-height: 1.35;
}

@media (max-width: 992px) {
  .hero-card :deep(.ant-card-body) {
    flex-direction: column;
    align-items: stretch;
  }

  .hero-summary {
    flex-basis: auto;
    max-width: none;
    width: 100%;
    justify-content: stretch;
  }

  .hero-badges {
    width: 100%;
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }
}

@media (max-width: 640px) {
  .hero-badges {
    grid-template-columns: 1fr;
  }

  .toolbar {
    flex-direction: column;
    align-items: stretch;
  }
}
</style>
