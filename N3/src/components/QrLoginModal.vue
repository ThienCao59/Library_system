<template>
  <div v-if="show" class="qr-modal-overlay" @click.self="close">
    <div class="qr-modal-box">

      <!-- BƯỚC 1: Quét mã QR -->
      <template v-if="step === 1">
        <div class="qr-modal-header">
          <div class="qr-modal-icon">📷</div>
          <h3>Đăng nhập bằng Thẻ Thư Viện</h3>
          <p class="qr-modal-sub">Đưa mã QR trên thẻ vào trước camera để quét</p>
        </div>

        <div class="qr-video-wrapper">
          <video ref="videoRef" class="qr-video" autoplay playsinline muted></video>
          <div class="qr-scan-frame">
            <div class="scan-corner tl"></div>
            <div class="scan-corner tr"></div>
            <div class="scan-corner bl"></div>
            <div class="scan-corner br"></div>
            <div class="scan-laser"></div>
          </div>
        </div>

        <p v-if="scanError" class="qr-error">{{ scanError }}</p>
        <p class="qr-hint">💡 Giữ thẻ ổn định, đảm bảo mã QR nằm trong khung</p>
        <button class="qr-btn-cancel" @click="close">Hủy</button>
      </template>

      <!-- BƯỚC 2: Nhập OTP -->
      <template v-if="step === 2">
        <div class="qr-modal-header">
          <div class="qr-modal-icon">✉️</div>
          <h3>Xin chào, {{ detectedName }}!</h3>
          <p class="qr-modal-sub">
            Mã OTP đã được gửi đến email<br/>
            <strong>{{ maskedEmail }}</strong>
          </p>
        </div>

        <div class="otp-input-row">
          <input
            v-for="(_, i) in otpDigits"
            :key="i"
            :ref="el => otpRefs[i] = el"
            v-model="otpDigits[i]"
            type="text"
            inputmode="numeric"
            maxlength="1"
            class="otp-digit-box"
            @input="onOtpInput(i)"
            @keydown.backspace="onOtpBackspace(i)"
            @paste.prevent="onOtpPaste"
          />
        </div>

        <p v-if="otpError" class="qr-error">{{ otpError }}</p>

        <button class="qr-btn-submit" :disabled="loading" @click="submitOtp">
          <span v-if="loading">Đang xác thực…</span>
          <span v-else>✅ Xác nhận đăng nhập</span>
        </button>
        <button class="qr-btn-resend" :disabled="resendCooldown > 0" @click="resendOtp">
          {{ resendCooldown > 0 ? `Gửi lại sau ${resendCooldown}s` : '🔄 Gửi lại mã' }}
        </button>
        <button class="qr-btn-cancel" @click="close">Hủy</button>
      </template>

    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onUnmounted, nextTick } from 'vue'
import jsQR from 'jsqr'
import { apiClient } from '@/utils/apiClient'

const props = defineProps<{ show: boolean }>()
const emit = defineEmits<{
  (e: 'close'): void
  (e: 'success', data: any): void
}>()

const step = ref(1)
const scanError = ref('')
const detectedName = ref('')
const maskedEmail = ref('')
const detectedCardNumber = ref('')
const loading = ref(false)
const otpError = ref('')
const otpDigits = ref(['', '', '', '', '', ''])
const otpRefs = ref<any[]>([])
const resendCooldown = ref(0)
let cooldownTimer: ReturnType<typeof setInterval> | null = null

// ── Camera ──────────────────────────────────────────────────────────
const videoRef = ref<HTMLVideoElement | null>(null)
let stream: MediaStream | null = null
let animFrame: number | null = null

async function startCamera() {
  scanError.value = ''
  try {
    stream = await navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' } })
    await nextTick()
    if (videoRef.value) {
      videoRef.value.srcObject = stream
      videoRef.value.play()
      scanFrame()
    }
  } catch {
    scanError.value = 'Không thể truy cập camera. Hãy kiểm tra quyền trình duyệt.'
  }
}

function stopCamera() {
  if (animFrame) cancelAnimationFrame(animFrame)
  stream?.getTracks().forEach(t => t.stop())
  stream = null
}

function scanFrame() {
  const video = videoRef.value
  if (!video || video.readyState !== video.HAVE_ENOUGH_DATA) {
    animFrame = requestAnimationFrame(scanFrame)
    return
  }

  const canvas = document.createElement('canvas')
  canvas.width = video.videoWidth
  canvas.height = video.videoHeight
  const ctx = canvas.getContext('2d')!
  ctx.drawImage(video, 0, 0)
  const imageData = ctx.getImageData(0, 0, canvas.width, canvas.height)
  const code = jsQR(imageData.data, imageData.width, imageData.height)

  if (code?.data) {
    stopCamera()
    handleQrData(code.data)
    return
  }

  animFrame = requestAnimationFrame(scanFrame)
}

// ── QR Data Handler ──────────────────────────────────────────────────
async function handleQrData(raw: string) {
  // QR chỉ chứa CardNumber (VD: "LIB-123456")
  const cardNumber = raw.trim()
  if (!cardNumber) {
    scanError.value = 'Mã QR không hợp lệ. Vui lòng thử lại.'
    startCamera()
    return
  }

  detectedCardNumber.value = cardNumber
  loading.value = true
  try {
    const res = await apiClient.post('/api/identity/Auth/request-otp', { CardNumber: cardNumber })
    detectedName.value = res.data.fullName || 'Bạn'
    maskedEmail.value = res.data.email || ''
    step.value = 2
    await nextTick()
    otpRefs.value[0]?.focus()
    startResendCooldown()
  } catch (err: any) {
    const msg = err.response?.data?.Message || 'Không tìm thấy thẻ thư viện này.'
    scanError.value = msg
    startCamera()
  } finally {
    loading.value = false
  }
}

// ── OTP ──────────────────────────────────────────────────────────────
function onOtpInput(i: number) {
  otpError.value = ''
  const rawValue = String(otpDigits.value[i] || '')
  const val = rawValue.replace(/\D/g, '')
  otpDigits.value[i] = val.slice(-1)
  if (val && i < 5) nextTick(() => otpRefs.value[i + 1]?.focus())
}

function onOtpBackspace(i: number) {
  if (!otpDigits.value[i] && i > 0) {
    otpDigits.value[i - 1] = ''
    nextTick(() => otpRefs.value[i - 1]?.focus())
  }
}

function onOtpPaste(e: ClipboardEvent) {
  const text = (e.clipboardData?.getData('text') || '').replace(/\D/g, '').slice(0, 6)
  for (let i = 0; i < 6; i++) otpDigits.value[i] = text[i] || ''
  nextTick(() => otpRefs.value[Math.min(text.length, 5)]?.focus())
}

async function submitOtp() {
  const otp = otpDigits.value.join('')
  if (otp.length !== 6) {
    otpError.value = 'Vui lòng nhập đủ 6 chữ số.'
    return
  }

  loading.value = true
  otpError.value = ''
  try {
    const res = await apiClient.post('/api/identity/Auth/login-otp', {
      CardNumber: detectedCardNumber.value,
      Otp: otp
    })
    emit('success', res.data)
  } catch (err: any) {
    otpError.value = err.response?.data?.Message || 'Mã OTP không đúng hoặc đã hết hạn.'
    otpDigits.value = ['', '', '', '', '', '']
    nextTick(() => otpRefs.value[0]?.focus())
  } finally {
    loading.value = false
  }
}

async function resendOtp() {
  otpDigits.value = ['', '', '', '', '', '']
  otpError.value = ''
  loading.value = true
  try {
    const res = await apiClient.post('/api/identity/Auth/request-otp', {
      CardNumber: detectedCardNumber.value
    })
    maskedEmail.value = res.data.email || maskedEmail.value
    startResendCooldown()
    await nextTick()
    otpRefs.value[0]?.focus()
  } catch {
    otpError.value = 'Không thể gửi lại mã. Vui lòng thử lại.'
  } finally {
    loading.value = false
  }
}

function startResendCooldown() {
  resendCooldown.value = 60
  if (cooldownTimer) clearInterval(cooldownTimer)
  cooldownTimer = setInterval(() => {
    resendCooldown.value--
    if (resendCooldown.value <= 0 && cooldownTimer) clearInterval(cooldownTimer)
  }, 1000)
}

// ── Lifecycle ─────────────────────────────────────────────────────────
function close() {
  stopCamera()
  if (cooldownTimer) clearInterval(cooldownTimer)
  step.value = 1
  scanError.value = ''
  otpError.value = ''
  otpDigits.value = ['', '', '', '', '', '']
  detectedCardNumber.value = ''
  emit('close')
}

watch(() => props.show, async (val) => {
  if (val) {
    step.value = 1
    await nextTick()
    startCamera()
  } else {
    stopCamera()
  }
})

onUnmounted(() => {
  stopCamera()
  if (cooldownTimer) clearInterval(cooldownTimer)
})
</script>

<style scoped>
.qr-modal-overlay {
  position: fixed; inset: 0; z-index: 9999;
  background: rgba(0,0,0,0.65);
  display: flex; align-items: center; justify-content: center;
  backdrop-filter: blur(4px);
  animation: fadeIn .2s ease;
}
@keyframes fadeIn { from { opacity: 0 } to { opacity: 1 } }

.qr-modal-box {
  background: #fff;
  border-radius: 24px;
  padding: 32px 28px 24px;
  width: 100%;
  max-width: 400px;
  box-shadow: 0 24px 60px rgba(0,0,0,0.25);
  text-align: center;
  animation: slideUp .3s cubic-bezier(.22,1,.36,1);
}
@keyframes slideUp { from { transform: translateY(30px); opacity: 0 } to { transform: translateY(0); opacity: 1 } }

.qr-modal-header { margin-bottom: 20px; }
.qr-modal-icon { font-size: 2.6rem; margin-bottom: 8px; }
.qr-modal-header h3 { font-size: 1.3rem; font-weight: 800; color: #1E5652; margin: 0 0 6px; }
.qr-modal-sub { font-size: .9rem; color: #6A7B6B; line-height: 1.6; margin: 0; }

/* Camera */
.qr-video-wrapper {
  position: relative; width: 100%; aspect-ratio: 1;
  border-radius: 16px; overflow: hidden;
  background: #000; margin-bottom: 14px;
}
.qr-video { width: 100%; height: 100%; object-fit: cover; display: block; }
.qr-scan-frame {
  position: absolute; inset: 15%;
  border: none; pointer-events: none;
}
.scan-corner {
  position: absolute; width: 22px; height: 22px;
  border-color: #8BD4A0; border-style: solid;
}
.tl { top: 0; left: 0; border-width: 3px 0 0 3px; border-radius: 4px 0 0 0; }
.tr { top: 0; right: 0; border-width: 3px 3px 0 0; border-radius: 0 4px 0 0; }
.bl { bottom: 0; left: 0; border-width: 0 0 3px 3px; border-radius: 0 0 0 4px; }
.br { bottom: 0; right: 0; border-width: 0 3px 3px 0; border-radius: 0 0 4px 0; }

.scan-laser {
  position: absolute; left: 0; right: 0; height: 2px;
  background: linear-gradient(90deg, transparent, #3A8A52, transparent);
  top: 50%;
  animation: laserScan 1.8s ease-in-out infinite;
}
@keyframes laserScan {
  0%,100% { top: 5%; opacity: .6 }
  50% { top: 92%; opacity: 1 }
}

/* OTP */
.otp-input-row { display: flex; gap: 8px; justify-content: center; margin: 20px 0 8px; }
.otp-digit-box {
  width: 46px; height: 56px;
  border: 2px solid #DDE5E3;
  border-radius: 12px;
  font-size: 1.6rem; font-weight: 800; text-align: center;
  color: #1E5652;
  outline: none;
  transition: border-color .2s, box-shadow .2s;
}
.otp-digit-box:focus { border-color: #3A8A52; box-shadow: 0 0 0 3px rgba(58,138,82,.15); }

/* Buttons */
.qr-btn-submit {
  width: 100%; padding: 13px;
  border-radius: 50px; border: none; cursor: pointer;
  background: linear-gradient(135deg, #1E5652, #3A8A52);
  color: #fff; font-weight: 800; font-size: 1rem;
  margin-top: 12px;
  transition: transform .2s, opacity .2s;
}
.qr-btn-submit:disabled { opacity: .65; cursor: not-allowed; }
.qr-btn-submit:not(:disabled):hover { transform: translateY(-2px); }

.qr-btn-resend {
  width: 100%; padding: 10px;
  border-radius: 50px; border: 1.5px solid rgba(58,138,82,.35);
  background: transparent; color: #3A8A52;
  font-weight: 600; font-size: .9rem; cursor: pointer;
  margin-top: 8px;
  transition: background .2s;
}
.qr-btn-resend:disabled { opacity: .5; cursor: not-allowed; }
.qr-btn-resend:not(:disabled):hover { background: #ECF6EE; }

.qr-btn-cancel {
  width: 100%; padding: 10px;
  border-radius: 50px; border: none;
  background: transparent; color: #9CA3AF;
  font-size: .85rem; cursor: pointer; margin-top: 6px;
  transition: color .2s;
}
.qr-btn-cancel:hover { color: #6B7280; }

.qr-error { color: #dc2626; font-size: .85rem; margin: 6px 0 0; font-weight: 600; }
.qr-hint { font-size: .8rem; color: #9CA3AF; margin: 4px 0 12px; }
</style>
