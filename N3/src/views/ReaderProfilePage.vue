<template>
  <div class="profile-page">

    <!-- LANG TOGGLE (floating top-right) -->
    <button
      class="lang-fab"
      @click="i18n.setLocale(i18n.locale === 'vi' ? 'en' : 'vi')"
      :title="i18n.locale === 'vi' ? 'Switch to English' : 'Chuyển sang Tiếng Việt'"
    >
      {{ i18n.locale === 'vi' ? '🇬🇧 EN' : '🇻🇳 VI' }}
    </button>

    <!-- TOP HERO BAR -->
    <div class="hero-bar">
      <div class="hero-bar-inner">
        <button class="back-btn" @click="goBack" v-if="!isSelfProfile">
          <ArrowLeftOutlined /> {{ t('btn.back') }}
        </button>
        <div>
          <p class="hero-label">{{ isSelfProfile ? t('profile.my.account') : t('profile.reader.detail') }}</p>
          <h1 class="hero-title">{{ isSelfProfile ? t('profile.title.self') : (profileData?.fullName || profileData?.FullName || '—') }}</h1>
        </div>
        <a-button v-if="isSelfProfile && profileData" class="btn-edit" @click="openEdit">
          <EditOutlined /> {{ t('profile.edit.btn') }}
        </a-button>
      </div>
    </div>

    <!-- LOADING -->
    <div v-if="loading" class="bento-grid">
      <div class="sk-block tall"></div>
      <div class="sk-block"></div>
      <div class="sk-block"></div>
    </div>

    <!-- ERROR -->
    <div v-else-if="!profileData" class="error-box">
      <FrownOutlined class="error-icon" />
      <p>{{ t('profile.load.error') }}</p>
      <a-button type="primary" @click="fetchProfileData">{{ t('btn.retry') }}</a-button>
    </div>

    <!-- BENTO GRID -->
    <div v-else class="bento-grid animate-in">

      <!-- CARD 1: Avatar + tên -->
      <div class="bento-card bento-identity">
        <div class="identity-avatar">
          <span>{{ getInitials(profileData.fullName || profileData.FullName) }}</span>
        </div>
        <h2 class="identity-name">{{ profileData.fullName || profileData.FullName }}</h2>
        <span class="role-pill" :class="(profileData.role || profileData.Role || 'reader').toLowerCase()">
          {{ profileData.role || profileData.Role || 'Reader' }}
        </span>
        <div class="identity-divider"></div>
        <div class="identity-info-list">
          <div class="identity-row">
            <span class="i-icon"><MailOutlined /></span>
            <div>
              <span class="i-label">Email</span>
              <span class="i-val">{{ profileData.email || profileData.Email || '—' }}</span>
            </div>
          </div>
          <div class="identity-row">
            <span class="i-icon"><PhoneOutlined /></span>
            <div>
              <span class="i-label">{{ t('profile.phone') }}</span>
              <span class="i-val">{{ profileData.phoneNumber || profileData.PhoneNumber || '—' }}</span>
            </div>
          </div>
          <div class="identity-row">
            <span class="i-icon"><UserOutlined /></span>
            <div>
              <span class="i-label">{{ t('profile.username') }}</span>
              <span class="i-val username">@{{ profileData.username || profileData.Username || '—' }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- CARD 2: Thẻ thư viện -->
      <div class="bento-card bento-card-lib">
        <p class="bento-section-label"><CreditCardOutlined /> {{ t('profile.library.card') }}</p>
        <div v-if="libraryCard" class="lib-card" :class="libraryCard.status?.toLowerCase()">
          <div class="lc-top">
            <span class="lc-brand"><ReadOutlined /> H-Library</span>
            <span class="lc-chip"></span>
          </div>
          <div class="lc-number">{{ formatCardNumber(libraryCard.cardNumber) }}</div>
          <div class="lc-footer">
            <div class="lc-meta">
              <span class="lc-meta-label">{{ t('profile.card.issued') }}</span>
              <span class="lc-meta-val">{{ formatDate(libraryCard.issueDate) }}</span>
            </div>
            <div class="lc-meta">
              <span class="lc-meta-label">{{ t('profile.card.expiry') }}</span>
              <span class="lc-meta-val">{{ formatDate(libraryCard.expiryDate) }}</span>
            </div>
            <span class="lc-status-badge" :class="libraryCard.status?.toLowerCase()">
              {{ libraryCard.status || 'Active' }}
            </span>
            <button class="btn-print-card" @click="showPrintModal = true" title="In thẻ thư viện">
              <PrinterOutlined />
            </button>
          </div>
        </div>
        <div v-else class="no-card">
          <DisconnectOutlined />
          <p>{{ t('profile.no.card') }}</p>
        </div>
      </div>

      <!-- CARD 3: Trạng thái -->
      <div class="bento-card bento-status">
        <p class="bento-section-label"><SafetyCertificateOutlined /> {{ t('profile.account.status') }}</p>
        <div class="status-chip" :class="isActive ? 'active' : 'locked'">
          <span class="status-dot"></span>
          {{ isActive ? t('profile.active') : t('profile.locked') }}
        </div>
        <p class="status-desc">
          {{ isActive ? t('profile.active.desc') : t('profile.locked.desc') }}
        </p>
        <div class="created-row">
          <HistoryOutlined class="cr-icon" />
          <div>
            <span class="i-label">{{ t('profile.created.at') }}</span>
            <span class="i-val">{{ formatDate(profileData.createdAt || profileData.CreatedAt) }}</span>
          </div>
        </div>
      </div>

    </div>

    <!-- MODAL CHỈNH SỬA -->
    <a-modal
      v-model:open="showEditModal"
      :title="t('profile.edit.title')"
      :footer="null"
      :width="440"
    >
      <a-form :model="editForm" layout="vertical" @finish="handleSave" style="margin-top:8px">
        <a-form-item :label="t('profile.full.name')" name="fullName">
          <a-input v-model:value="editForm.fullName" :placeholder="t('profile.name.placeholder')" size="large" />
        </a-form-item>
        <a-form-item :label="t('profile.phone')" name="phoneNumber">
          <a-input v-model:value="editForm.phoneNumber" :placeholder="t('profile.phone.placeholder')" size="large" />
        </a-form-item>
        <div class="modal-btns">
          <a-button size="large" @click="showEditModal = false">{{ t('btn.cancel') }}</a-button>
          <a-button type="primary" size="large" html-type="submit" :loading="saving" class="btn-save-modal">
            {{ t('profile.save.changes') }}
          </a-button>
        </div>
      </a-form>
    </a-modal>

    <!-- MODAL IN THẺ -->
    <a-modal
      v-model:open="showPrintModal"
      title="Thẻ Thư Viện (Dùng để Đăng nhập QR)"
      :footer="null"
      :width="420"
    >
      <div id="print-area" class="print-wrapper">
        <div class="print-card-box">
          <div class="pc-header">SMARTLIB LIBRARY CARD</div>
          <div class="pc-body">
            <div class="pc-info">
              <p class="pc-name">{{ profileData?.fullName || profileData?.FullName }}</p>
              <p class="pc-role">{{ profileData?.role || profileData?.Role || 'Reader' }}</p>
              <div class="pc-number">{{ formatCardNumber(libraryCard?.cardNumber || '') }}</div>
            </div>
            <div class="pc-qr">
              <qrcode-vue :value="libraryCard?.cardNumber || ''" :size="90" level="H" />
            </div>
          </div>
          <div class="pc-footer">Vui lòng xuất trình hoặc quét mã QR này tại trạm kiểm soát.</div>
        </div>
      </div>
      <div class="modal-btns no-print">
        <a-button size="large" @click="showPrintModal = false">Đóng</a-button>
        <a-button type="primary" size="large" class="btn-save-modal" @click="doPrint">🖨️ In Thẻ (Print)</a-button>
      </div>
    </a-modal>

  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { message } from 'ant-design-vue'
import { apiClient } from '@/utils/apiClient'
import { formatVnDate } from '@/utils/dateTime'
import { useI18nStore } from '@/stores/i18nStore'
import QrcodeVue from 'qrcode.vue'
import {
  ArrowLeftOutlined, UserOutlined, MailOutlined, PhoneOutlined,
  IdcardOutlined, CreditCardOutlined, ReadOutlined, EditOutlined,
  DisconnectOutlined, SafetyCertificateOutlined,
  LockOutlined, HistoryOutlined, FrownOutlined, PrinterOutlined
} from '@ant-design/icons-vue'

const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)

const route = useRoute()
const router = useRouter()

const loading = ref(true)
const profileData = ref<any>(null)
const showEditModal = ref(false)
const saving = ref(false)
const editForm = ref({ fullName: '', phoneNumber: '' })
const showPrintModal = ref(false)

const doPrint = () => {
  window.print()
}

const isSelfProfile = computed(() => route.path === '/profile')
const readerId = computed(() => route.params.id as string)

const libraryCard = computed(() => {
  const card = profileData.value?.libraryCard || profileData.value?.LibraryCard
  if (!card) return null
  return {
    cardNumber: card.cardNumber || card.CardNumber,
    issueDate: card.issueDate || card.IssueDate,
    expiryDate: card.expiryDate || card.ExpiryDate,
    status: card.status || card.Status
  }
})

const isActive = computed(() =>
  profileData.value?.isActive || profileData.value?.IsActive
)

const fetchProfileData = async () => {
  loading.value = true
  try {
    if (isSelfProfile.value) {
      const res = await apiClient.get('/api/identity/User/profile')
      profileData.value = res.data
      editForm.value.fullName = res.data.fullName || res.data.FullName || ''
      editForm.value.phoneNumber = res.data.phoneNumber || res.data.PhoneNumber || ''
    } else {
      const res = await apiClient.get('/api/identity/User/readers')
      const found = res.data.find((r: any) => r.id === readerId.value || r.Id === readerId.value)
      if (found) profileData.value = found
      else message.error(t('profile.not.found'))
    }
  } catch {
    message.error(t('profile.load.err2'))
  } finally {
    loading.value = false
  }
}

const openEdit = () => {
  editForm.value.fullName = profileData.value?.fullName || profileData.value?.FullName || ''
  editForm.value.phoneNumber = profileData.value?.phoneNumber || profileData.value?.PhoneNumber || ''
  showEditModal.value = true
}

const handleSave = async () => {
  saving.value = true
  try {
    await apiClient.put('/api/identity/User/profile', {
      fullName: editForm.value.fullName,
      phoneNumber: editForm.value.phoneNumber
    })
    message.success(t('profile.save.success'))
    showEditModal.value = false
    await fetchProfileData()
  } catch {
    message.error(t('profile.save.error'))
  } finally {
    saving.value = false
  }
}

const goBack = () => router.push('/dashboard')

const getInitials = (name: string) => {
  const parts = (name?.trim() || '').split(' ').filter(Boolean)
  if (parts.length >= 2) {
    const first = parts[0] ?? ''
    const last = parts[parts.length - 1] ?? ''
    return (first.charAt(0) + last.charAt(0)).toUpperCase()
  }
  return (name || 'U').substring(0, 2).toUpperCase()
}

const formatCardNumber = (num: string) =>
  num ? num.replace(/(.{4})/g, '$1 ').trim() : '**** **** ****'

const formatDate = (d: string) => d ? formatVnDate(d) : '—'

onMounted(fetchProfileData)
</script>

<style scoped>
*, *::before, *::after { box-sizing: border-box; }

/* ── LANG FAB ─────────────────────────────────── */
.lang-fab {
  position: fixed;
  top: 16px;
  right: 16px;
  z-index: 999;
  background: rgba(255,255,255,0.95);
  border: 1px solid #E8EDE9;
  border-radius: 20px;
  padding: 6px 14px;
  font-size: 13px;
  font-weight: 700;
  cursor: pointer;
  box-shadow: 0 2px 12px rgba(0,0,0,0.1);
  transition: all 0.2s;
  color: #1E5652;
  letter-spacing: 0.3px;
}
.lang-fab:hover {
  background: #1E5652;
  color: white;
  border-color: #1E5652;
  transform: translateY(-1px);
  box-shadow: 0 4px 16px rgba(30,86,82,0.25);
}

/* ── PAGE ─────────────────────────────────────── */
.profile-page {
  min-height: 100vh;
  background: #FAF9F6;
  font-family: 'Inter', 'Outfit', sans-serif;
  color: #2d3748;
}

/* ── HERO BAR ─────────────────────────────────── */
.hero-bar {
  background: linear-gradient(135deg, #114B44 0%, #1E5652 100%);
  padding: 36px 0 40px;
}
.hero-bar-inner {
  max-width: 1100px;
  margin: 0 auto;
  padding: 0 32px;
  display: flex;
  align-items: flex-end;
  gap: 20px;
}
.back-btn {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  background: rgba(255,255,255,0.12);
  border: 1px solid rgba(255,255,255,0.2);
  color: rgba(255,255,255,0.85);
  border-radius: 8px;
  padding: 8px 14px;
  font-size: 13px;
  font-weight: 600;
  cursor: pointer;
  margin-bottom: 4px;
  transition: background 0.2s;
  align-self: flex-start;
}
.back-btn:hover { background: rgba(255,255,255,0.2); }

.hero-label {
  margin: 0 0 4px;
  font-size: 13px;
  font-weight: 600;
  color: rgba(255,255,255,0.6);
  text-transform: uppercase;
  letter-spacing: 1px;
}
.hero-title {
  margin: 0;
  font-size: 30px;
  font-weight: 800;
  color: #ffffff;
  letter-spacing: -0.3px;
}

.btn-edit {
  margin-left: auto;
  display: inline-flex;
  align-items: center;
  gap: 8px;
  padding: 10px 22px;
  background: #CF7C07;
  color: white !important;
  border: none !important;
  border-radius: 10px;
  font-size: 14px;
  font-weight: 700;
  cursor: pointer;
  box-shadow: 0 4px 14px rgba(207,124,7,0.35);
  transition: all 0.2s;
  height: auto !important;
}
.btn-edit:hover {
  background: #b86d06 !important;
  color: white !important;
  transform: translateY(-1px);
  box-shadow: 0 6px 20px rgba(207,124,7,0.45);
}

/* ── BENTO GRID ───────────────────────────────── */
.bento-grid {
  max-width: 1100px;
  margin: 0 auto;
  padding: 32px 32px 60px;
  display: grid;
  grid-template-columns: 320px 1fr 1fr;
  grid-template-rows: auto;
  gap: 20px;
}

@media (max-width: 900px) {
  .bento-grid { grid-template-columns: 1fr; padding: 24px 16px 48px; }
  .bento-identity { grid-row: auto; }
}

.animate-in {
  animation: fadeUp 0.45s ease-out both;
}
@keyframes fadeUp {
  from { opacity: 0; transform: translateY(18px); }
  to   { opacity: 1; transform: translateY(0); }
}

/* ── BASE CARD ────────────────────────────────── */
.bento-card {
  background: #ffffff;
  border-radius: 18px;
  border: 1px solid #E8EDE9;
  box-shadow: 0 2px 12px rgba(17,75,68,0.05);
  padding: 28px;
  transition: box-shadow 0.25s, transform 0.25s;
}
.bento-card:hover {
  box-shadow: 0 8px 28px rgba(17,75,68,0.09);
  transform: translateY(-3px);
}

.bento-section-label {
  font-size: 12px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #9CA3AF;
  margin: 0 0 20px;
  display: flex;
  align-items: center;
  gap: 6px;
}

/* ── IDENTITY CARD ────────────────────────────── */
.bento-identity {
  grid-row: span 2;
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  padding: 36px 28px 28px;
}
.identity-avatar {
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background: linear-gradient(135deg, #1E5652, #114B44);
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 34px;
  font-weight: 800;
  color: white;
  letter-spacing: 1px;
  box-shadow: 0 8px 24px rgba(17,75,68,0.2);
  margin-bottom: 16px;
  flex-shrink: 0;
}
.identity-name {
  margin: 0 0 10px;
  font-size: 20px;
  font-weight: 700;
  color: #1a202c;
  line-height: 1.3;
}
.role-pill {
  display: inline-block;
  padding: 4px 14px;
  border-radius: 20px;
  font-size: 11px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.8px;
  margin-bottom: 24px;
}
.role-pill.reader    { background: #E6F0EE; color: #1E5652; }
.role-pill.admin     { background: #FFF4E5; color: #CF7C07; }
.role-pill.librarian { background: #EBF4FF; color: #3182CE; }

.identity-divider {
  width: 100%;
  height: 1px;
  background: #F0F2F0;
  margin-bottom: 24px;
}

.identity-info-list {
  width: 100%;
  display: flex;
  flex-direction: column;
  gap: 16px;
  text-align: left;
}
.identity-row {
  display: flex;
  align-items: flex-start;
  gap: 12px;
}
.i-icon {
  width: 36px;
  height: 36px;
  border-radius: 9px;
  background: #F3F6F5;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 15px;
  color: #1E5652;
  flex-shrink: 0;
  margin-top: 1px;
}
.i-label {
  display: block;
  font-size: 11px;
  font-weight: 600;
  color: #9CA3AF;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  margin-bottom: 2px;
}
.i-val {
  display: block;
  font-size: 14px;
  font-weight: 600;
  color: #1a202c;
}
.i-val.username {
  color: #1E5652;
  font-family: 'Courier New', monospace;
}

/* ── LIBRARY CARD ─────────────────────────────── */
.bento-card-lib { }

.lib-card {
  border-radius: 16px;
  padding: 22px 24px 18px;
  color: white;
  position: relative;
  overflow: hidden;
  min-height: 160px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
.lib-card.active  { background: linear-gradient(135deg, #114B44 0%, #1E5652 60%, #2a9080 100%); }
.lib-card.expired,
.lib-card.locked  { background: linear-gradient(135deg, #5a6272, #7b8494); }

.lib-card::after {
  content: '';
  position: absolute;
  top: -40px; right: -40px;
  width: 150px; height: 150px;
  border-radius: 50%;
  background: rgba(255,255,255,0.06);
  pointer-events: none;
}

.lc-top {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.lc-brand {
  font-size: 15px;
  font-weight: 700;
  letter-spacing: 1px;
  display: flex;
  align-items: center;
  gap: 6px;
}
.lc-chip {
  width: 38px; height: 28px;
  background: linear-gradient(135deg, #FFD700, #DAA520);
  border-radius: 6px;
  box-shadow: 0 2px 6px rgba(0,0,0,0.2);
}
.lc-number {
  font-family: 'Courier New', monospace;
  font-size: 20px;
  font-weight: 700;
  letter-spacing: 3px;
  margin: 12px 0 4px;
}
.lc-footer {
  display: flex;
  align-items: flex-end;
  gap: 20px;
}
.lc-meta { display: flex; flex-direction: column; }
.lc-meta-label { font-size: 9px; opacity: 0.65; letter-spacing: 1px; }
.lc-meta-val   { font-size: 13px; font-weight: 700; font-family: 'Courier New', monospace; }
.lc-status-badge {
  margin-left: auto;
  padding: 4px 10px;
  border-radius: 8px;
  font-size: 10px;
  font-weight: 800;
  text-transform: uppercase;
  background: rgba(255,255,255,0.15);
  letter-spacing: 0.8px;
}
.lc-status-badge.active  { color: #A7F3D0; }
.lc-status-badge.expired { color: #FED7D7; }
.lc-status-badge.locked  { color: #FEEBC8; }

.no-card {
  text-align: center;
  color: #a0aec0;
  padding: 28px 0;
  font-size: 36px;
}
.no-card p { font-size: 14px; margin-top: 8px; }

/* ── STATUS CARD ──────────────────────────────── */
.bento-status { }

.status-chip {
  display: inline-flex;
  align-items: center;
  gap: 8px;
  padding: 8px 18px;
  border-radius: 50px;
  font-size: 14px;
  font-weight: 700;
  margin-bottom: 12px;
}
.status-chip.active {
  background: #ECFDF5;
  color: #065F46;
  border: 1px solid #A7F3D0;
}
.status-chip.locked {
  background: #FFFBEB;
  color: #92400E;
  border: 1px solid #FCD34D;
}
.status-dot {
  width: 8px; height: 8px;
  border-radius: 50%;
}
.status-chip.active  .status-dot { background: #10B981; box-shadow: 0 0 0 3px rgba(16,185,129,0.2); }
.status-chip.locked  .status-dot { background: #F59E0B; box-shadow: 0 0 0 3px rgba(245,158,11,0.2); }

.status-desc {
  font-size: 13px;
  color: #6B7280;
  line-height: 1.6;
  margin: 0 0 20px;
}

.created-row {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 14px;
  background: #F9FAFB;
  border-radius: 12px;
  border: 1px solid #E5E7EB;
}
.cr-icon {
  font-size: 18px;
  color: #1E5652;
  flex-shrink: 0;
}

/* ── SKELETON ─────────────────────────────────── */
.sk-block {
  background: #E5E9E7;
  border-radius: 18px;
  animation: pulse 1.4s ease-in-out infinite;
  height: 220px;
}
.sk-block.tall { height: 460px; }
@keyframes pulse { 0%,100% { opacity: 1; } 50% { opacity: 0.5; } }

/* ── ERROR ────────────────────────────────────── */
.error-box {
  text-align: center;
  padding: 100px 20px;
  color: #718096;
}
.error-icon { font-size: 56px; color: #E53E3E; margin-bottom: 16px; }

/* ── MODAL ────────────────────────────────────── */
.modal-btns {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
  padding-top: 16px;
  border-top: 1px solid #f0f0f0;
}
.btn-save-modal {
  background: linear-gradient(135deg, #1E5652, #114B44) !important;
  border: none !important;
}

/* ── PRINT MODAL ──────────────────────────────── */
.btn-print-card {
  margin-left: 10px;
  background: rgba(255,255,255,0.2);
  border: none;
  color: white;
  width: 32px;
  height: 32px;
  border-radius: 6px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background 0.2s;
}
.btn-print-card:hover { background: rgba(255,255,255,0.4); }

.print-wrapper {
  padding: 20px 0;
  display: flex;
  justify-content: center;
}
.print-card-box {
  width: 340px;
  border: 2px solid #1E5652;
  border-radius: 12px;
  background: #fff;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}
.pc-header {
  background: #1E5652;
  color: white;
  text-align: center;
  font-weight: 800;
  padding: 10px;
  font-size: 14px;
  letter-spacing: 1px;
}
.pc-body {
  display: flex;
  padding: 20px;
  align-items: center;
  justify-content: space-between;
}
.pc-info {
  flex: 1;
}
.pc-name {
  margin: 0;
  font-size: 16px;
  font-weight: 800;
  color: #1a202c;
  line-height: 1.2;
}
.pc-role {
  margin: 4px 0 12px;
  font-size: 11px;
  color: #6A7B6B;
  text-transform: uppercase;
  font-weight: 600;
}
.pc-number {
  font-family: 'Courier New', monospace;
  font-size: 14px;
  font-weight: 700;
  color: #1E5652;
}
.pc-qr {
  margin-left: 15px;
  border: 4px solid #fff;
  box-shadow: 0 0 0 1px #eee;
}
.pc-footer {
  background: #f9f9f9;
  text-align: center;
  font-size: 10px;
  color: #888;
  padding: 8px;
  border-top: 1px solid #eee;
}

@media print {
  body * {
    visibility: hidden;
  }
  .no-print {
    display: none !important;
  }
  #print-area, #print-area * {
    visibility: visible;
  }
  #print-area {
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    width: auto;
    margin: 0;
  }
  .ant-modal-mask, .ant-modal-wrap {
    background: white !important;
  }
  .ant-modal-content {
    box-shadow: none !important;
  }
  .ant-modal-header, .ant-modal-close {
    display: none !important;
  }
}

</style>
