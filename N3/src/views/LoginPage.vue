<template>
  <div class="auth-page-container">
    <div class="split-container login-style">

      <!-- LEFT PANEL: Open Book with Knowledge Particles -->
      <div class="scenery-panel-left">
        <svg class="reading-illustration" viewBox="0 0 400 520" preserveAspectRatio="xMidYMid meet" xmlns="http://www.w3.org/2000/svg">
          <defs>
            <radialGradient id="bgGlow" cx="50%" cy="60%" r="55%">
              <stop offset="0%" stop-color="#EDD9A3" stop-opacity="0.7"/>
              <stop offset="100%" stop-color="#FAF3E0" stop-opacity="0"/>
            </radialGradient>
            <linearGradient id="pageL" x1="0%" y1="0%" x2="100%" y2="100%">
              <stop offset="0%" stop-color="#FFFFFF"/>
              <stop offset="100%" stop-color="#F5ECD7"/>
            </linearGradient>
            <linearGradient id="pageR" x1="100%" y1="0%" x2="0%" y2="100%">
              <stop offset="0%" stop-color="#FFFDF7"/>
              <stop offset="100%" stop-color="#EDE0C8"/>
            </linearGradient>
            <filter id="glow" x="-50%" y="-50%" width="200%" height="200%">
              <feGaussianBlur stdDeviation="2.5" result="blur"/>
              <feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge>
            </filter>
            <filter id="softGlow" x="-80%" y="-80%" width="260%" height="260%">
              <feGaussianBlur stdDeviation="5" result="blur"/>
              <feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge>
            </filter>
            <polygon id="star4" points="0,-5 1.2,-1.2 5,0 1.2,1.2 0,5 -1.2,1.2 -5,0 -1.2,-1.2"/>
          </defs>
          <rect width="400" height="520" fill="#FAF3E0"/>
          <circle cx="200" cy="300" r="180" fill="url(#bgGlow)"/>
          <g fill="#1E5652" opacity="0.08">
            <circle cx="40" cy="50" r="2.5"/><circle cx="80" cy="30" r="1.5"/>
            <circle cx="360" cy="60" r="2"/><circle cx="330" cy="40" r="1.5"/>
            <circle cx="30" cy="460" r="2"/><circle cx="370" cy="450" r="2.5"/>
            <circle cx="60" cy="200" r="1.5"/><circle cx="350" cy="180" r="2"/>
            <circle cx="100" cy="420" r="1.5"/><circle cx="310" cy="410" r="1.5"/>
          </g>
          <!-- Open Book -->
          <g transform="translate(200, 295)">
            <ellipse cx="0" cy="80" rx="115" ry="14" fill="#1E5652" opacity="0.07"/>
            <path d="M -5,60 Q -125,55 -130,0 Q -125,-55 -5,-60 Z" fill="#C8A96E" opacity="0.35"/>
            <path d="M -5,-62 Q -108,-57 -112,0 Q -108,57 -5,62 Z" fill="url(#pageL)" stroke="#D9C9A8" stroke-width="1"/>
            <line x1="-95" y1="-38" x2="-18" y2="-38" stroke="#C9B88A" stroke-width="1.8" opacity="0.55"/>
            <line x1="-90" y1="-24" x2="-18" y2="-24" stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="-96" y1="-10" x2="-18" y2="-10" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="-88" y1="4"   x2="-18" y2="4"   stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="-93" y1="18"  x2="-18" y2="18"  stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="-82" y1="32"  x2="-18" y2="32"  stroke="#C9B88A" stroke-width="1.8" opacity="0.3"/>
            <line x1="-90" y1="46"  x2="-18" y2="46"  stroke="#C9B88A" stroke-width="1.8" opacity="0.25"/>
            <circle cx="-60" cy="-48" r="6" fill="none" stroke="#1E5652" stroke-width="1.5" opacity="0.3"/>
            <line x1="-60" y1="-54" x2="-60" y2="-42" stroke="#1E5652" stroke-width="1" opacity="0.3"/>
            <line x1="-66" y1="-48" x2="-54" y2="-48" stroke="#1E5652" stroke-width="1" opacity="0.3"/>
            <path d="M 5,-62 Q 108,-57 112,0 Q 108,57 5,62 Z" fill="url(#pageR)" stroke="#D9C9A8" stroke-width="1"/>
            <line x1="18" y1="-38" x2="95" y2="-38" stroke="#C9B88A" stroke-width="1.8" opacity="0.55"/>
            <line x1="18" y1="-24" x2="90" y2="-24" stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="18" y1="-10" x2="96" y2="-10" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="18" y1="4"   x2="88" y2="4"   stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="18" y1="18"  x2="93" y2="18"  stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="18" y1="32"  x2="82" y2="32"  stroke="#C9B88A" stroke-width="1.8" opacity="0.3"/>
            <line x1="18" y1="46"  x2="90" y2="46"  stroke="#C9B88A" stroke-width="1.8" opacity="0.25"/>
            <rect x="-5" y="-62" width="10" height="124" rx="3" fill="#1E5652"/>
            <rect x="-3" y="-62" width="6"  height="124" rx="2" fill="#2A7A74" opacity="0.5"/>
            <g class="lp-page-turn">
              <path d="M 5,-62 Q 65,-58 72,-15 Q 65,30 5,62 Z" fill="#FFF8EE" stroke="#D9C9A8" stroke-width="0.8" opacity="0.75"/>
            </g>
          </g>
          <!-- Orbs -->
          <circle class="lp-orb o1" cx="160" cy="230" r="7"   fill="#CF7C07" filter="url(#softGlow)" opacity="0.9"/>
          <circle class="lp-orb o2" cx="200" cy="218" r="5"   fill="#1E5652" filter="url(#softGlow)" opacity="0.85"/>
          <circle class="lp-orb o3" cx="240" cy="225" r="8"   fill="#E8A020" filter="url(#softGlow)" opacity="0.8"/>
          <circle class="lp-orb o4" cx="178" cy="210" r="4"   fill="#2A9D8F" filter="url(#glow)"     opacity="0.9"/>
          <circle class="lp-orb o5" cx="222" cy="205" r="6"   fill="#CF7C07" filter="url(#softGlow)" opacity="0.75"/>
          <circle class="lp-orb o6" cx="195" cy="200" r="3.5" fill="#F4A261" filter="url(#glow)"     opacity="0.85"/>
          <circle class="lp-orb o7" cx="215" cy="195" r="5"   fill="#1E5652" filter="url(#softGlow)" opacity="0.7"/>
          <circle class="lp-orb o8" cx="170" cy="215" r="3"   fill="#E8A020" filter="url(#glow)"     opacity="0.8"/>
          <!-- Sparkle stars -->
          <use href="#star4" class="lp-spark s1"  transform="translate(155,185)" fill="#CF7C07" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s2"  transform="translate(205,170)" fill="#1E5652" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s3"  transform="translate(248,178)" fill="#E8A020" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s4"  transform="translate(182,160)" fill="#2A9D8F" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s5"  transform="translate(228,155)" fill="#CF7C07" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s6"  transform="translate(168,145)" fill="#F4A261" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s7"  transform="translate(215,138)" fill="#1E5652" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s8"  transform="translate(192,130)" fill="#E8A020" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s9"  transform="translate(240,125)" fill="#CF7C07" filter="url(#glow)"/>
          <use href="#star4" class="lp-spark s10" transform="translate(160,120)" fill="#2A9D8F" filter="url(#glow)"/>
          <!-- Twinkle dots -->
          <circle class="lp-twinkle t1"  cx="130" cy="160" r="2.5" fill="#CF7C07" filter="url(#glow)"/>
          <circle class="lp-twinkle t2"  cx="270" cy="150" r="2"   fill="#1E5652" filter="url(#glow)"/>
          <circle class="lp-twinkle t3"  cx="145" cy="110" r="3"   fill="#E8A020" filter="url(#glow)"/>
          <circle class="lp-twinkle t4"  cx="260" cy="105" r="2.5" fill="#CF7C07" filter="url(#glow)"/>
          <circle class="lp-twinkle t5"  cx="200" cy="95"  r="2"   fill="#2A9D8F" filter="url(#glow)"/>
          <circle class="lp-twinkle t6"  cx="175" cy="80"  r="3"   fill="#F4A261" filter="url(#glow)"/>
          <circle class="lp-twinkle t7"  cx="225" cy="75"  r="2"   fill="#CF7C07" filter="url(#glow)"/>
          <circle class="lp-twinkle t8"  cx="155" cy="65"  r="2.5" fill="#1E5652" filter="url(#glow)"/>
          <circle class="lp-twinkle t9"  cx="245" cy="60"  r="2"   fill="#E8A020" filter="url(#glow)"/>
          <circle class="lp-twinkle t10" cx="200" cy="50"  r="3"   fill="#CF7C07" filter="url(#glow)"/>
          <!-- Floating mini books -->
          <g class="lp-float-book fb1" transform="translate(58,140)">
            <rect x="0" y="0" width="30" height="38" rx="3" fill="#1E5652"/>
            <rect x="3" y="3" width="24" height="32" rx="1" fill="#FFF8EE"/>
            <line x1="7" y1="11" x2="23" y2="11" stroke="#C9B88A" stroke-width="1.5"/>
            <line x1="7" y1="18" x2="20" y2="18" stroke="#C9B88A" stroke-width="1.5"/>
            <line x1="7" y1="25" x2="22" y2="25" stroke="#C9B88A" stroke-width="1.5"/>
          </g>
          <g class="lp-float-book fb2" transform="translate(308,110)">
            <rect x="0" y="0" width="26" height="34" rx="3" fill="#CF7C07"/>
            <rect x="3" y="3" width="20" height="28" rx="1" fill="#FFF8EE"/>
            <circle cx="13" cy="17" r="6" fill="none" stroke="#CF7C07" stroke-width="1.5"/>
          </g>
          <g class="lp-float-book fb3" transform="translate(48,360)">
            <rect x="0" y="0" width="24" height="30" rx="2" fill="#E07A5F"/>
            <rect x="3" y="3" width="18" height="24" rx="1" fill="#FFF8EE"/>
            <line x1="6" y1="10" x2="18" y2="10" stroke="#E07A5F" stroke-width="1.5"/>
            <line x1="6" y1="16" x2="14" y2="16" stroke="#E07A5F" stroke-width="1.5"/>
          </g>
          <g class="lp-float-book fb4" transform="translate(318,360)">
            <rect x="0" y="0" width="28" height="22" rx="2" fill="#2A9D8F"/>
            <rect x="3" y="3" width="22" height="16" rx="1" fill="#FFF8EE"/>
            <line x1="6" y1="9"  x2="22" y2="9"  stroke="#2A9D8F" stroke-width="1.5"/>
            <line x1="6" y1="14" x2="18" y2="14" stroke="#2A9D8F" stroke-width="1.5"/>
          </g>
          <!-- SMARTLIB text -->
          <g transform="translate(200,448)">
            <text class="lp-bl bl1" x="-74" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">S</text>
            <text class="lp-bl bl2" x="-52" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">M</text>
            <text class="lp-bl bl3" x="-26" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">A</text>
            <text class="lp-bl bl4" x="-4"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">R</text>
            <text class="lp-bl bl5" x="20"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">T</text>
            <text class="lp-bl bl6" x="42"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">L</text>
            <text class="lp-bl bl7" x="60"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">I</text>
            <text class="lp-bl bl8" x="72"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">B</text>
          </g>
          <text x="200" y="472" font-family="'Inter',sans-serif" font-size="11" font-weight="600" fill="#8B7D6B" letter-spacing="3" text-anchor="middle">DIGITAL LIBRARY</text>
        </svg>
      </div>

      <!-- RIGHT PANEL: Login Form -->
      <div class="form-panel-right">
        <div class="form-content-wrapper">
          <h2 class="auth-title-login">{{ t('login.title') }}</h2>
          <a-form :model="formState" @finish="onFinish" layout="vertical" autocomplete="off">
            <a-form-item :label="t('login.username.label')" name="username" :rules="[{ required: true, message: t('login.username.required') }]">
              <a-input v-model:value="formState.username" :placeholder="t('login.username.placeholder')" class="pill-input" autocomplete="off"/>
            </a-form-item>
            <a-form-item :label="t('login.password.label')" name="password" :rules="[{ required: true, message: t('login.password.required') }]" class="mb-2">
              <a-input-password v-model:value="formState.password" :placeholder="t('login.password.placeholder')" class="pill-input" autocomplete="new-password"/>
            </a-form-item>
            <div class="text-right mb-6">
              <a href="#" class="forgot-password-link" @click.prevent="$router.push('/forgot-password')">{{ t('login.forgot') }}</a>
            </div>
            <a-form-item>
              <a-button type="primary" html-type="submit" class="btn-login-pill" :loading="loading">{{ t('login.btn') }}</a-button>
            </a-form-item>
            <div class="text-center mb-4">
              <button class="btn-faceid-pill" type="button" @click="showQrModal = true">
                <span style="margin-right:6px">📷</span>
                Đăng nhập bằng Thẻ QR
              </button>
            </div>
          </a-form>
          <div class="footer-links-signin mt-6">
            <span>{{ t('login.no.account') }}</span>
            <a href="#" class="btn-register-link" @click.prevent="$router.push('/register')">{{ t('login.register.link') }}</a>
          </div>
          <div class="text-center mt-6">
            <a href="#" class="back-link-login" @click.prevent="$router.push('/')">
              <v-icon icon="mdi-arrow-left" class="mr-1"></v-icon> {{ t('login.back.home') }}
            </a>
          </div>
          <div class="contact-us-footer mt-8 text-center">
            <span>{{ t('login.contact') }}</span>
            <a :href="`mailto:${lib.email}`" class="support-email">{{ lib.email }}</a>
          </div>
        </div>
      </div>

    </div>
  </div>

  <!-- QR Login Modal -->
  <QrLoginModal :show="showQrModal" @close="showQrModal = false" @success="onQrLoginSuccess" />
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import { message } from 'ant-design-vue'
import { useRouter } from 'vue-router'
import { apiClient } from '@/utils/apiClient'
import { saveAuthSession, clearAuthSession, parseJwtPayload } from '@/utils/auth'
import { N2_LIBRARIAN_URL, N2_READER_URL, redirectWithHandoffCode } from '@/utils/authHandoff'
import { useI18nStore } from '@/stores/i18nStore'
import { useLibraryStore } from '@/stores/libraryStore'
import QrLoginModal from '@/components/QrLoginModal.vue'

const router = useRouter()
const loading = ref(false)
const formState = reactive({ username: '', password: '' })
const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)
const lib = useLibraryStore()
const showQrModal = ref(false)

async function onQrLoginSuccess(data: any) {
  showQrModal.value = false
  saveAuthSession(data)

  const token = data.accessToken || data.AccessToken || ''
  const payload = parseJwtPayload(token)
  const roleClaim =
    payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
    payload?.role ||
    payload?.Role ||
    ''
  const userRole = Array.isArray(roleClaim) ? roleClaim[0] : roleClaim

  message.success('Đăng nhập bằng thẻ QR thành công!')
  const roleLower = String(userRole).toLowerCase()
  if (roleLower === 'admin') {
    window.location.assign('/dashboard')
  } else if (roleLower === 'librarian') {
    await redirectWithHandoffCode(N2_LIBRARIAN_URL)
  } else {
    await redirectWithHandoffCode(N2_READER_URL)
  }
}

const onFinish = async (values: any) => {
  loading.value = true
  clearAuthSession()
  try {
    const response = await apiClient.post('/api/identity/Auth/login', {
      Username: formState.username,
      Password: formState.password
    })
    const data = response.data
    saveAuthSession(data)
    
    const token = data.accessToken || data.AccessToken || ''
    const payload = parseJwtPayload(token)
    const roleClaim = payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload?.role || payload?.Role || ''
    const userRole = Array.isArray(roleClaim) ? roleClaim[0] : roleClaim
    
    loading.value = false
    message.success(t('login.success'))
    const roleLower = String(userRole).toLowerCase()
    if (roleLower === 'admin') {
      // Use window.location (full page reload) so DashboardPage re-reads localStorage on mount
      window.location.assign('/dashboard')
    } else if (roleLower === 'librarian') {
      await redirectWithHandoffCode(N2_LIBRARIAN_URL)
    } else {
      await redirectWithHandoffCode(N2_READER_URL)
    }
    return
  } catch (error: any) {
    const errMessage = error.response?.data?.Message || error.response?.data?.message || t('login.error')
    message.error(errMessage)
    loading.value = false
  }
}


// Helper: convert base64url string to Uint8Array
const base64urlToUint8Array = (base64url: string): Uint8Array<ArrayBuffer> => {
  const base64 = base64url.replace(/-/g, '+').replace(/_/g, '/')
  const padded = base64.padEnd(base64.length + (4 - (base64.length % 4)) % 4, '=')
  const binary = atob(padded)
  const buf = new ArrayBuffer(binary.length)
  const view = new Uint8Array(buf)
  for (let i = 0; i < binary.length; i++) view[i] = binary.charCodeAt(i)
  return view
}

// Helper: convert ArrayBuffer/Uint8Array to base64url string
const bufferToBase64url = (buffer: ArrayBuffer | Uint8Array): string => {
  const bytes = buffer instanceof Uint8Array ? buffer : new Uint8Array(buffer)
  let binary = ''
  bytes.forEach(b => binary += String.fromCharCode(b))
  return btoa(binary).replace(/\+/g, '-').replace(/\//g, '_').replace(/=/g, '')
}

const loginWithFaceId = async () => {
  if (!formState.username) {
    message.warning('Vui lòng nhập Username trước khi đăng nhập bằng Face ID')
    return
  }
  if (!window.PublicKeyCredential) {
    message.error('Trình duyệt không hỗ trợ Face ID / Touch ID (WebAuthn)')
    return
  }

  loading.value = true
  clearAuthSession()
  try {
    // 1. Lấy challenge từ backend
    const optionsRes = await apiClient.post('/api/identity/Auth/Fido2/loginOptions', {
      Username: formState.username
    })
    const options = optionsRes.data

    // 2. Chuyển đổi challenge và allowCredentials sang đúng format cho browser
    const publicKeyOptions: PublicKeyCredentialRequestOptions = {
      challenge: base64urlToUint8Array(options.challenge),
      timeout: options.timeout ?? 60000,
      rpId: options.rpId,
      userVerification: options.userVerification ?? 'preferred',
      allowCredentials: (options.allowCredentials ?? []).map((c: any) => ({
        id: base64urlToUint8Array(c.id),
        type: c.type,
        transports: c.transports
      }))
    }

    // 3. Gọi WebAuthn thật — browser sẽ hiện popup Face ID / Touch ID / Windows Hello
    const assertion = await navigator.credentials.get({ publicKey: publicKeyOptions }) as PublicKeyCredential
    if (!assertion) throw new Error('Không nhận được phản hồi từ thiết bị xác thực')

    const assertionResponse = assertion.response as AuthenticatorAssertionResponse

    // 4. Gửi kết quả lên backend
    const loginRes = await apiClient.post('/api/identity/Auth/Fido2/login', {
      Username: formState.username,
      AssertionResponse: {
        id: assertion.id,
        rawId: bufferToBase64url(assertion.rawId),
        type: assertion.type,
        response: {
          authenticatorData: bufferToBase64url(assertionResponse.authenticatorData),
          clientDataJSON: bufferToBase64url(assertionResponse.clientDataJSON),
          signature: bufferToBase64url(assertionResponse.signature),
          userHandle: assertionResponse.userHandle ? bufferToBase64url(assertionResponse.userHandle) : null
        }
      }
    })

    const data = loginRes.data
    saveAuthSession(data)

    const token = data.accessToken || data.AccessToken || ''
    const payload = parseJwtPayload(token)
    const roleClaim = payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload?.role || payload?.Role || ''
    const userRole = Array.isArray(roleClaim) ? roleClaim[0] : roleClaim

    message.success('Đăng nhập bằng Face ID thành công!')
    const roleLower = String(userRole).toLowerCase()
    if (roleLower === 'admin') {
      window.location.assign('/dashboard')
    } else if (roleLower === 'librarian') {
      await redirectWithHandoffCode(N2_LIBRARIAN_URL)
    } else {
      await redirectWithHandoffCode(N2_READER_URL)
    }
  } catch (error: any) {
    if (error.name === 'NotAllowedError') {
      message.error('Face ID: Người dùng từ chối hoặc không xác thực được')
    } else {
      const errMessage = error.response?.data?.Message || error.message || t('login.error')
      message.error('Face ID: ' + errMessage)
    }
  } finally {
    loading.value = false
  }
}
</script>

<style scoped>
.auth-page-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #ffffff;
  padding: 20px;
}
.split-container {
  display: flex;
  width: 100%;
  max-width: 960px;
  min-height: 500px;
  max-height: 90vh;
  background: white;
  border-radius: 30px;
  box-shadow: 0 15px 40px rgba(0,0,0,0.06);
  overflow: hidden;
  position: relative;
}
.scenery-panel-left {
  flex: 1.1;
  position: relative;
  height: auto;
  min-height: 100%;
  background-color: #FAF3E0;
  overflow: hidden;
}
.reading-illustration {
  width: 100%;
  height: 100%;
  display: block;
}
.form-panel-right {
  flex: 1;
  background-color: #1E5652;
  color: white;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 40px 50px;
  position: relative;
  z-index: 5;
}
.form-content-wrapper { width: 100%; }
.auth-title-login {
  font-size: 36px;
  font-weight: 800;
  color: #FAF9F6;
  margin-bottom: 24px;
  font-family: 'Outfit', sans-serif;
  letter-spacing: 0.5px;
}
:deep(.pill-input) {
  background-color: rgba(255,255,255,0.06) !important;
  border: 1.5px solid rgba(255,255,255,0.15) !important;
  color: white !important;
  border-radius: 24px !important;
  padding: 8px 18px !important;
}
:deep(.pill-input input), :deep(.pill-input .ant-input) {
  background: transparent !important;
  color: white !important;
  border: none !important;
  box-shadow: none !important;
}
:deep(.pill-input::placeholder), :deep(.pill-input input::placeholder), :deep(.pill-input .ant-input::placeholder) {
  color: rgba(255, 255, 255, 0.35) !important;
  opacity: 1 !important;
}
:deep(.pill-input:hover), :deep(.pill-input:focus-within) {
  border-color: #BBE1DC !important;
  box-shadow: 0 0 0 2px rgba(187,225,220,0.1) !important;
}
:deep(input:-webkit-autofill), :deep(input:-webkit-autofill:hover),
:deep(input:-webkit-autofill:focus), :deep(input:-webkit-autofill:active) {
  -webkit-box-shadow: 0 0 0 1000px #1E5652 inset !important;
  -webkit-text-fill-color: white !important;
  transition: background-color 5000s ease-in-out 0s;
}
:deep(.ant-input-password-icon) { color: rgba(255,255,255,0.6) !important; }
:deep(.ant-form-item-label > label) {
  color: white !important;
  font-weight: 700 !important;
  font-size: 13px !important;
  padding-bottom: 2px !important;
}
.forgot-password-link { color: #BBE1DC; font-size: 12px; text-decoration: underline; transition: color 0.3s; }
.forgot-password-link:hover { color: white; }
.btn-login-pill {
  width: 100%;
  height: 46px !important;
  border-radius: 24px !important;
  background-color: #FAF9F6 !important;
  border-color: #FAF9F6 !important;
  color: #1E5652 !important;
  font-weight: 800 !important;
  font-size: 15px !important;
  box-shadow: 0 6px 15px rgba(0,0,0,0.15) !important;
  transition: transform 0.2s, background-color 0.3s !important;
}
.btn-login-pill:hover { background-color: #EBF3E8 !important; transform: translateY(-1px); }

.btn-faceid-pill {
  width: 100%;
  height: 46px !important;
  border-radius: 24px !important;
  background-color: transparent !important;
  border-color: rgba(255, 255, 255, 0.4) !important;
  color: white !important;
  font-weight: 600 !important;
  font-size: 15px !important;
  transition: all 0.3s !important;
}
.btn-faceid-pill:hover {
  background-color: rgba(255, 255, 255, 0.1) !important;
  border-color: white !important;
}

.footer-links-signin { text-align: center; font-size: 13px; color: #BBE1DC; }
.btn-register-link { color: white; font-weight: 800; text-decoration: underline; }
.back-link-login { color: #BBE1DC; text-decoration: none; font-size: 13px; font-weight: 600; transition: color 0.3s; display: inline-block; }
.back-link-login:hover { color: white; }
.contact-us-footer { font-size: 11px; color: rgba(255,255,255,0.4); }
.support-email { color: rgba(255,255,255,0.6); text-decoration: underline; }
.support-email:hover { color: white; }

@media (max-width: 1024px) {
  .split-container { max-width: 800px; }
  .form-panel-right { padding: 30px 40px; }
  .auth-title-login { font-size: 30px; }
}
@media (max-width: 850px) {
  .split-container { flex-direction: column; height: auto; max-height: none; max-width: 460px; border-radius: 24px; }
  .scenery-panel-left { display: none; }
  .form-panel-right { padding: 36px 28px; border-radius: 24px; }
  .auth-title-login { font-size: 28px; }
}
@media (max-width: 480px) {
  .auth-page-container { padding: 10px; }
  .split-container { max-width: 100%; border-radius: 20px; }
  .form-panel-right { padding: 28px 20px; }
  .auth-title-login { font-size: 24px; margin-bottom: 16px; }
}
@media (min-height: 800px) {
  .split-container { min-height: 600px; }
}

@keyframes lp-rise {
  0%   { transform: translateY(0px) scale(1);   opacity: 0.9; }
  60%  { opacity: 1; }
  100% { transform: translateY(-160px) scale(0.4); opacity: 0; }
}
@keyframes lp-sparkle {
  0%   { transform: translateY(0px) rotate(0deg) scale(1);   opacity: 1; }
  50%  { transform: translateY(-80px) rotate(180deg) scale(1.4); opacity: 0.9; }
  100% { transform: translateY(-160px) rotate(360deg) scale(0.3); opacity: 0; }
}
@keyframes lp-twinkle {
  0%,100% { opacity: 0.2; transform: scale(0.7); }
  50%     { opacity: 1;   transform: scale(1.6); }
}
@keyframes lp-float {
  0%,100% { transform: translateY(0px); }
  50%     { transform: translateY(-14px); }
}
@keyframes lp-pageturn {
  0%,100% { transform: scaleX(1); }
  50%     { transform: scaleX(-0.2); }
}
@keyframes lp-bounce {
  0%,20%  { transform: translateY(0) scale(1); }
  40%     { transform: translateY(-14px) scale(1.15); }
  60%     { transform: translateY(-6px) scale(1.05); }
  80%     { transform: translateY(-10px) scale(1.1); }
  100%    { transform: translateY(0) scale(1); }
}
@keyframes lp-glow-pulse {
  0%,100% { opacity: 1; filter: drop-shadow(0 0 0px #CF7C07); }
  50%     { opacity: 0.8; filter: drop-shadow(0 0 10px #CF7C07); }
}

:deep(.lp-orb) { animation: lp-rise 3.5s ease-out infinite; }
:deep(.o1)  { animation-delay:0s;    animation-duration:3.2s; }
:deep(.o2)  { animation-delay:0.45s; animation-duration:3.8s; }
:deep(.o3)  { animation-delay:0.9s;  animation-duration:3.4s; }
:deep(.o4)  { animation-delay:1.35s; animation-duration:4.0s; }
:deep(.o5)  { animation-delay:1.8s;  animation-duration:3.6s; }
:deep(.o6)  { animation-delay:0.25s; animation-duration:3.1s; }
:deep(.o7)  { animation-delay:0.7s;  animation-duration:3.9s; }
:deep(.o8)  { animation-delay:2.1s;  animation-duration:3.3s; }

:deep(.lp-spark) { animation: lp-sparkle 4s ease-out infinite; }
:deep(.s1)  { animation-delay:0s;    animation-duration:3.8s; }
:deep(.s2)  { animation-delay:0.4s;  animation-duration:4.2s; }
:deep(.s3)  { animation-delay:0.8s;  animation-duration:3.6s; }
:deep(.s4)  { animation-delay:1.2s;  animation-duration:4.5s; }
:deep(.s5)  { animation-delay:1.6s;  animation-duration:3.9s; }
:deep(.s6)  { animation-delay:2.0s;  animation-duration:4.1s; }
:deep(.s7)  { animation-delay:0.3s;  animation-duration:3.7s; }
:deep(.s8)  { animation-delay:0.9s;  animation-duration:4.3s; }
:deep(.s9)  { animation-delay:1.5s;  animation-duration:3.5s; }
:deep(.s10) { animation-delay:2.2s;  animation-duration:4.0s; }

:deep(.lp-twinkle) { animation: lp-twinkle 2.5s ease-in-out infinite; }
:deep(.t1)  { animation-delay:0s;   } :deep(.t2)  { animation-delay:0.3s;  }
:deep(.t3)  { animation-delay:0.6s; } :deep(.t4)  { animation-delay:0.9s;  }
:deep(.t5)  { animation-delay:1.2s; } :deep(.t6)  { animation-delay:1.5s;  }
:deep(.t7)  { animation-delay:1.8s; } :deep(.t8)  { animation-delay:0.4s;  }
:deep(.t9)  { animation-delay:0.7s; } :deep(.t10) { animation-delay:1.0s;  }

:deep(.lp-float-book) { animation: lp-float 4s ease-in-out infinite; }
:deep(.fb1) { animation-delay:0s;   } :deep(.fb2) { animation-delay:1.2s; }
:deep(.fb3) { animation-delay:2.4s; } :deep(.fb4) { animation-delay:0.8s; }

:deep(.lp-page-turn) { transform-origin: 5px center; animation: lp-pageturn 5s ease-in-out infinite; }
:deep(.lp-bl) { animation: lp-bounce 1.6s cubic-bezier(0.36,0.07,0.19,0.97) infinite; }
:deep(.bl1) { animation-delay:0s;   } :deep(.bl2) { animation-delay:0.1s;  }
:deep(.bl3) { animation-delay:0.2s; } :deep(.bl4) { animation-delay:0.3s;  }
:deep(.bl5) { animation-delay:0.4s; } :deep(.bl6) { animation-delay:0.5s;  }
:deep(.bl7) { animation-delay:0.6s; } :deep(.bl8) { animation-delay:0.7s;  }
:deep(.lp-bl.bl6), :deep(.lp-bl.bl7), :deep(.lp-bl.bl8) { animation: lp-bounce 1.6s cubic-bezier(0.36,0.07,0.19,0.97) infinite, lp-glow-pulse 2s ease-in-out infinite; }
</style>
