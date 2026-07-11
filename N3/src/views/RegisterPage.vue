<template>
  <div class="auth-page-container">
    <div class="split-container signup-style">
      <!-- LEFT PANEL: Open Book with Knowledge Particles -->
      <div class="scenery-panel">
        <svg class="reading-illustration" viewBox="0 0 400 520" preserveAspectRatio="xMidYMid meet" xmlns="http://www.w3.org/2000/svg">
          <defs>
            <radialGradient id="rp-bgGlow" cx="50%" cy="60%" r="55%">
              <stop offset="0%" stop-color="#EDD9A3" stop-opacity="0.7"/>
              <stop offset="100%" stop-color="#FAF3E0" stop-opacity="0"/>
            </radialGradient>
            <linearGradient id="rp-pageL" x1="0%" y1="0%" x2="100%" y2="100%">
              <stop offset="0%" stop-color="#FFFFFF"/>
              <stop offset="100%" stop-color="#F5ECD7"/>
            </linearGradient>
            <linearGradient id="rp-pageR" x1="100%" y1="0%" x2="0%" y2="100%">
              <stop offset="0%" stop-color="#FFFDF7"/>
              <stop offset="100%" stop-color="#EDE0C8"/>
            </linearGradient>
            <filter id="rp-glow" x="-50%" y="-50%" width="200%" height="200%">
              <feGaussianBlur stdDeviation="2.5" result="blur"/>
              <feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge>
            </filter>
            <filter id="rp-softGlow" x="-80%" y="-80%" width="260%" height="260%">
              <feGaussianBlur stdDeviation="5" result="blur"/>
              <feMerge><feMergeNode in="blur"/><feMergeNode in="SourceGraphic"/></feMerge>
            </filter>
            <polygon id="rp-star4" points="0,-5 1.2,-1.2 5,0 1.2,1.2 0,5 -1.2,1.2 -5,0 -1.2,-1.2"/>
          </defs>
          <rect width="400" height="520" fill="#FAF3E0"/>
          <circle cx="200" cy="300" r="180" fill="url(#rp-bgGlow)"/>
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
            <path d="M -5,-62 Q -108,-57 -112,0 Q -108,57 -5,62 Z" fill="url(#rp-pageL)" stroke="#D9C9A8" stroke-width="1"/>
            <line x1="-95" y1="-38" x2="-18" y2="-38" stroke="#C9B88A" stroke-width="1.8" opacity="0.55"/>
            <line x1="-90" y1="-24" x2="-18" y2="-24" stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="-96" y1="-10" x2="-18" y2="-10" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="-88" y1="4"  x2="-18" y2="4"  stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="-93" y1="18" x2="-18" y2="18" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="-82" y1="32" x2="-18" y2="32" stroke="#C9B88A" stroke-width="1.8" opacity="0.3"/>
            <line x1="-90" y1="46" x2="-18" y2="46" stroke="#C9B88A" stroke-width="1.8" opacity="0.25"/>
            <circle cx="-60" cy="-48" r="6" fill="none" stroke="#1E5652" stroke-width="1.5" opacity="0.3"/>
            <line x1="-60" y1="-54" x2="-60" y2="-42" stroke="#1E5652" stroke-width="1" opacity="0.3"/>
            <line x1="-66" y1="-48" x2="-54" y2="-48" stroke="#1E5652" stroke-width="1" opacity="0.3"/>
            <path d="M 5,-62 Q 108,-57 112,0 Q 108,57 5,62 Z" fill="url(#rp-pageR)" stroke="#D9C9A8" stroke-width="1"/>
            <line x1="18" y1="-38" x2="95" y2="-38" stroke="#C9B88A" stroke-width="1.8" opacity="0.55"/>
            <line x1="18" y1="-24" x2="90" y2="-24" stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="18" y1="-10" x2="96" y2="-10" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="18" y1="4"  x2="88" y2="4"  stroke="#C9B88A" stroke-width="1.8" opacity="0.45"/>
            <line x1="18" y1="18" x2="93" y2="18" stroke="#C9B88A" stroke-width="1.8" opacity="0.4"/>
            <line x1="18" y1="32" x2="82" y2="32" stroke="#C9B88A" stroke-width="1.8" opacity="0.3"/>
            <line x1="18" y1="46" x2="90" y2="46" stroke="#C9B88A" stroke-width="1.8" opacity="0.25"/>
            <rect x="-5" y="-62" width="10" height="124" rx="3" fill="#1E5652"/>
            <rect x="-3" y="-62" width="6" height="124" rx="2" fill="#2A7A74" opacity="0.5"/>
            <g class="rp-page-turn">
              <path d="M 5,-62 Q 65,-58 72,-15 Q 65,30 5,62 Z" fill="#FFF8EE" stroke="#D9C9A8" stroke-width="0.8" opacity="0.75"/>
            </g>
          </g>
          <!-- Orbs -->
          <circle class="rp-orb ro1" cx="160" cy="230" r="7"   fill="#CF7C07" filter="url(#rp-softGlow)" opacity="0.9"/>
          <circle class="rp-orb ro2" cx="200" cy="218" r="5"   fill="#1E5652" filter="url(#rp-softGlow)" opacity="0.85"/>
          <circle class="rp-orb ro3" cx="240" cy="225" r="8"   fill="#E8A020" filter="url(#rp-softGlow)" opacity="0.8"/>
          <circle class="rp-orb ro4" cx="178" cy="210" r="4"   fill="#2A9D8F" filter="url(#rp-glow)"     opacity="0.9"/>
          <circle class="rp-orb ro5" cx="222" cy="205" r="6"   fill="#CF7C07" filter="url(#rp-softGlow)" opacity="0.75"/>
          <circle class="rp-orb ro6" cx="195" cy="200" r="3.5" fill="#F4A261" filter="url(#rp-glow)"     opacity="0.85"/>
          <circle class="rp-orb ro7" cx="215" cy="195" r="5"   fill="#1E5652" filter="url(#rp-softGlow)" opacity="0.7"/>
          <circle class="rp-orb ro8" cx="170" cy="215" r="3"   fill="#E8A020" filter="url(#rp-glow)"     opacity="0.8"/>
          <!-- Sparkle stars -->
          <use href="#rp-star4" class="rp-spark rs1"  transform="translate(155,185)" fill="#CF7C07" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs2"  transform="translate(205,170)" fill="#1E5652" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs3"  transform="translate(248,178)" fill="#E8A020" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs4"  transform="translate(182,160)" fill="#2A9D8F" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs5"  transform="translate(228,155)" fill="#CF7C07" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs6"  transform="translate(168,145)" fill="#F4A261" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs7"  transform="translate(215,138)" fill="#1E5652" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs8"  transform="translate(192,130)" fill="#E8A020" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs9"  transform="translate(240,125)" fill="#CF7C07" filter="url(#rp-glow)"/>
          <use href="#rp-star4" class="rp-spark rs10" transform="translate(160,120)" fill="#2A9D8F" filter="url(#rp-glow)"/>
          <!-- Twinkle dots -->
          <circle class="rp-twinkle rt1"  cx="130" cy="160" r="2.5" fill="#CF7C07" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt2"  cx="270" cy="150" r="2"   fill="#1E5652" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt3"  cx="145" cy="110" r="3"   fill="#E8A020" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt4"  cx="260" cy="105" r="2.5" fill="#CF7C07" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt5"  cx="200" cy="95"  r="2"   fill="#2A9D8F" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt6"  cx="175" cy="80"  r="3"   fill="#F4A261" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt7"  cx="225" cy="75"  r="2"   fill="#CF7C07" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt8"  cx="155" cy="65"  r="2.5" fill="#1E5652" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt9"  cx="245" cy="60"  r="2"   fill="#E8A020" filter="url(#rp-glow)"/>
          <circle class="rp-twinkle rt10" cx="200" cy="50"  r="3"   fill="#CF7C07" filter="url(#rp-glow)"/>
          <!-- Floating mini books -->
          <g class="rp-float-book rfb1" transform="translate(58,140)">
            <rect x="0" y="0" width="30" height="38" rx="3" fill="#1E5652"/>
            <rect x="3" y="3" width="24" height="32" rx="1" fill="#FFF8EE"/>
            <line x1="7" y1="11" x2="23" y2="11" stroke="#C9B88A" stroke-width="1.5"/>
            <line x1="7" y1="18" x2="20" y2="18" stroke="#C9B88A" stroke-width="1.5"/>
            <line x1="7" y1="25" x2="22" y2="25" stroke="#C9B88A" stroke-width="1.5"/>
          </g>
          <g class="rp-float-book rfb2" transform="translate(308,110)">
            <rect x="0" y="0" width="26" height="34" rx="3" fill="#CF7C07"/>
            <rect x="3" y="3" width="20" height="28" rx="1" fill="#FFF8EE"/>
            <circle cx="13" cy="17" r="6" fill="none" stroke="#CF7C07" stroke-width="1.5"/>
          </g>
          <g class="rp-float-book rfb3" transform="translate(48,360)">
            <rect x="0" y="0" width="24" height="30" rx="2" fill="#E07A5F"/>
            <rect x="3" y="3" width="18" height="24" rx="1" fill="#FFF8EE"/>
            <line x1="6" y1="10" x2="18" y2="10" stroke="#E07A5F" stroke-width="1.5"/>
            <line x1="6" y1="16" x2="14" y2="16" stroke="#E07A5F" stroke-width="1.5"/>
          </g>
          <g class="rp-float-book rfb4" transform="translate(318,360)">
            <rect x="0" y="0" width="28" height="22" rx="2" fill="#2A9D8F"/>
            <rect x="3" y="3" width="22" height="16" rx="1" fill="#FFF8EE"/>
            <line x1="6" y1="9" x2="22" y2="9" stroke="#2A9D8F" stroke-width="1.5"/>
            <line x1="6" y1="14" x2="18" y2="14" stroke="#2A9D8F" stroke-width="1.5"/>
          </g>
          <!-- SMARTLIB text -->
          <g transform="translate(200,448)">
            <text class="rp-bl rbl1" x="-74" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">S</text>
            <text class="rp-bl rbl2" x="-52" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">M</text>
            <text class="rp-bl rbl3" x="-26" y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">A</text>
            <text class="rp-bl rbl4" x="-4"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">R</text>
            <text class="rp-bl rbl5" x="20"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#1E5652" letter-spacing="3">T</text>
            <text class="rp-bl rbl6" x="42"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">L</text>
            <text class="rp-bl rbl7" x="60"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">I</text>
            <text class="rp-bl rbl8" x="72"  y="0" font-family="'Inter',sans-serif" font-size="22" font-weight="900" fill="#CF7C07" letter-spacing="3">B</text>
          </g>
          <text x="200" y="472" font-family="'Inter',sans-serif" font-size="11" font-weight="600" fill="#8B7D6B" letter-spacing="3" text-anchor="middle">DIGITAL LIBRARY</text>
        </svg>
      </div>

      <!-- RIGHT PANEL: Elegant Sign Up Form in deep emerald matching Sample 1 -->
      <div class="form-panel">
        <div class="form-scroll-container">
          <h2 class="welcome-hello">{{ t('register.title') }}</h2>
          <p class="welcome-glad mb-6">{{ t('register.subtitle') }}</p>

          <!-- OAuth Social Pill Buttons -->
          <div class="social-login-container mb-6">
            <a-button class="btn-social-google" shape="round">
              <v-icon icon="mdi-google" class="mr-2 google-icon"></v-icon>
              {{ t('register.google.btn') }}
            </a-button>
            <a-button class="btn-social-circle" shape="circle">
              <v-icon icon="mdi-facebook"></v-icon>
            </a-button>
            <a-button class="btn-social-circle" shape="circle">
              <v-icon icon="mdi-twitter"></v-icon>
            </a-button>
          </div>

          <div class="divider-or mb-6">
            <span class="divider-line"></span>
            <span class="divider-text">{{ t('register.or') }}</span>
            <span class="divider-line"></span>
          </div>

          <!-- Form fields using pill-shape inputs exactly like Sample 1 -->
          <a-form :model="formState" @finish="onFinish" layout="vertical">
            <div class="form-row">
              <div class="form-col">
                <a-form-item
                  :label="t('register.name')"
                  name="fullName"
                  :rules="[{ required: true, message: t('register.name') + '!' }]"
                >
                  <a-input v-model:value="formState.fullName" :placeholder="t('register.name')" class="pill-input" />
                </a-form-item>
              </div>
              <div class="form-col">
                <a-form-item
                  :label="t('register.username')"
                  name="username"
                  :rules="[{ required: true, message: t('register.username') + '!' }]"
                >
                  <a-input v-model:value="formState.username" :placeholder="t('register.username')" class="pill-input" />
                </a-form-item>
              </div>
            </div>

            <div class="form-row">
              <div class="form-col">
                <a-form-item
                  :label="t('register.email')"
                  name="email"
                  :rules="[
                    { required: true, message: t('register.email') + '!' },
                    { type: 'email', message: t('register.email.invalid') }
                  ]"
                >
                  <a-input v-model:value="formState.email" placeholder="example@domain.com" class="pill-input" />
                </a-form-item>
              </div>
              <div class="form-col">
                <a-form-item
                  :label="t('register.password')"
                  name="password"
                  :rules="[
                    { required: true, message: t('register.password') + '!' },
                    { validator: validatePasswordStrength }
                  ]"
                >
                  <a-input-password v-model:value="formState.password" placeholder="xxxxxxxx" class="pill-input" />
                </a-form-item>
              </div>
            </div>

            <div class="form-row">
              <div class="form-col">
                <a-form-item
                  :label="t('register.confirm.password')"
                  name="confirmPassword"
                  :rules="[
                    { required: true, message: t('register.confirm.password') + '!' },
                    { validator: validateConfirmPassword }
                  ]"
                >
                  <a-input-password v-model:value="formState.confirmPassword" placeholder="xxxxxxxx" class="pill-input" />
                </a-form-item>
              </div>
              <div class="form-col">
                <a-form-item
                  :label="t('register.phone')"
                  name="phoneNumber"
                >
                  <a-input v-model:value="formState.phoneNumber" placeholder="0912 345 678" class="pill-input" />
                </a-form-item>
              </div>

            </div>

            <div class="mb-6">
              <a-checkbox v-model:checked="formState.agree" class="terms-checkbox">
                {{ t('register.terms') }}<a href="#" class="terms-link">{{ t('register.terms.link') }}</a>{{ t('register.and') }}<a href="#" class="terms-link">{{ t('register.privacy') }}</a>
              </a-checkbox>
            </div>

            <a-form-item class="mb-2">
              <a-button type="primary" html-type="submit" class="btn-signup-pill" :loading="loading" :disabled="!formState.agree">
                {{ t('register.btn') }}
              </a-button>
            </a-form-item>
          </a-form>

          <div class="footer-links-signup mt-6">
            <span>{{ t('register.have.account') }}</span>
            <a href="#" class="btn-signin-link" @click.prevent="$router.push('/login')">{{ t('register.signin.link') }}</a>
          </div>

          <div class="text-center mt-4">
            <a href="#" class="back-link" @click.prevent="$router.push('/')">
              <v-icon icon="mdi-arrow-left" class="mr-1"></v-icon> {{ t('register.back.home') }}
            </a>
          </div>
        </div>
      </div>
    </div>

    <!-- Celebration Modal (Same 3D digital card as N3) -->
    <a-modal v-model:open="showSuccessModal" :footer="null" :closable="false" width="500px" centered>
      <div class="success-modal-content text-center py-6">
        <div class="success-icon-wrapper mb-4">
          <v-icon icon="mdi-check-circle" color="success" size="64"></v-icon>
        </div>
        <h3 class="success-title mb-2">{{ t('register.success.title') }}</h3>
        <p class="success-subtitle mb-6 text-grey-darken-1">{{ t('register.success.sub') }}</p>
        
        <!-- Virtual card -->
        <div class="virtual-card-wrapper mx-auto mb-8">
          <div class="virtual-card">
            <div class="card-bg-glow"></div>
            <div class="card-header d-flex justify-space-between align-center">
              <div class="card-brand d-flex align-center">
                <v-icon icon="mdi-library" color="white" class="mr-1"></v-icon>
                <span>SmartLib.NET</span>
              </div>
              <div class="card-type">DIGITAL CARD</div>
            </div>
            
            <div class="card-body my-6 text-left">
              <div class="card-label">{{ t('register.card.code') }}</div>
              <div class="card-number">{{ generatedCardNumber }}</div>
            </div>

            <div class="card-footer d-flex justify-space-between text-left">
              <div>
                <div class="card-label">{{ t('register.card.name') }}</div>
                <div class="card-val">{{ formState.fullName }}</div>
              </div>
              <div class="text-right">
                <div class="card-label">{{ t('register.card.expiry') }}</div>
                <div class="card-val">{{ expiryDateString }}</div>
              </div>
            </div>
          </div>
        </div>

        <a-button type="primary" shape="round" class="btn-success-close" @click="closeModalAndRedirect">
          {{ t('register.success.btn') }}
        </a-button>
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { apiClient } from '@/utils/apiClient'
import { message } from 'ant-design-vue'
import { useI18nStore } from '@/stores/i18nStore'

const router = useRouter()
const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)
const loading = ref(false)
const showSuccessModal = ref(false)
const generatedCardNumber = ref('')

const formState = reactive({
  fullName: '',
  email: '',
  password: '',
  confirmPassword: '',
  username: '',
  role: 'Reader',
  phoneNumber: '',
  agree: false
})

const expiryDateString = computed(() => {
  const nextYear = new Date()
  nextYear.setFullYear(nextYear.getFullYear() + 1)
  return nextYear.toLocaleDateString('vi-VN')
})

const validatePasswordStrength = (_rule: any, value: string) => {
  if (!value) {
    return Promise.resolve()
  }
  
  const hasMinLength = value.length >= 6
  const hasUpperCase = /[A-Z]/.test(value)
  const hasLowerCase = /[a-z]/.test(value)
  const hasDigit = /[0-9]/.test(value)
  const hasSpecial = /[^A-Za-z0-9]/.test(value)
  
  if (!hasMinLength || !hasUpperCase || !hasLowerCase || !hasDigit || !hasSpecial) {
    return Promise.reject(new Error(t('register.password.strength')))
  }
  
  return Promise.resolve()
}

const validateConfirmPassword = (_rule: any, value: string) => {
  if (!value) {
    return Promise.resolve()
  }
  if (value !== formState.password) {
    return Promise.reject(new Error(t('register.password.mismatch')))
  }
  return Promise.resolve()
}

const onFinish = async (values: any) => {
  loading.value = true
  try {
    const response = await apiClient.post('/api/identity/Auth/register', {
      Username: formState.username,
      Email: formState.email,
      Password: formState.password,
      FullName: formState.fullName,
      PhoneNumber: formState.phoneNumber || null
    })
    
    const data = response.data
    // Accept both camelCase and PascalCase from API Gateway
    generatedCardNumber.value = data.cardNumber || data.CardNumber || `LIB-${Math.floor(100000 + Math.random() * 900000)}`
    showSuccessModal.value = true
    message.success(t('register.success.msg'))
  } catch (error: any) {
    console.error('Register API Error:', error)
    const responseData = error.response?.data
    
    if (responseData?.Errors && Array.isArray(responseData.Errors)) {
      // Loop through all Microsoft Identity validation errors and display them
      responseData.Errors.forEach((err: any) => {
        message.error(err.Description || err.description || t('register.error.default'))
      })
    } else if (responseData?.errors && Array.isArray(responseData.errors)) {
      responseData.errors.forEach((err: any) => {
        message.error(err.description || err.Description || t('register.error.default'))
      })
    } else {
      const errMessage = responseData?.Message || responseData?.message || t('register.error.connection')
      message.error(errMessage)
    }
  } finally {
    loading.value = false
  }
}

const closeModalAndRedirect = () => {
  showSuccessModal.value = false
  router.push('/login')
}
</script>

<style scoped>
/* Scoped styles replicating Sample 1 exactly */
.auth-page-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background-color: #ffffff;
  padding: 40px;
}

.split-container {
  display: flex;
  width: 100%;
  max-width: 960px;
  height: 600px;
  background: white;
  border-radius: 30px;
  box-shadow: 0 15px 40px rgba(0,0,0,0.06);
  overflow: hidden;
  position: relative;
}

/* LEFT PANEL (Book Illustration) */
.scenery-panel {
  flex: 1.1;
  position: relative;
  height: 100%;
  background-color: #FAF3E0;
  overflow: hidden;
}

.reading-illustration {
  width: 100%;
  height: 100%;
  display: block;
}

/* RIGHT PANEL (Sign Up Form) */
.form-panel {
  flex: 1.2;
  background-color: #1E5652; /* Lighter emerald color scheme */
  color: white;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 40px 60px;
  position: relative;
  z-index: 5;
}

.form-scroll-container {
  width: 100%;
  max-height: 100%;
  overflow-y: auto;
  padding-right: 5px;
}

/* Hide scrollbar for form scroll container */
.form-scroll-container::-webkit-scrollbar {
  width: 0px;
}

.welcome-hello {
  font-size: 38px;
  font-weight: 800;
  color: #FAF9F6;
  margin-bottom: 4px;
}

.welcome-glad {
  font-size: 16px;
  color: #BBE1DC;
}

/* Social Buttons */
.social-login-container {
  display: flex;
  align-items: center;
  gap: 12px;
}

.btn-social-google {
  background-color: rgba(255, 255, 255, 0.08) !important;
  border: 1px solid rgba(255, 255, 255, 0.2) !important;
  color: white !important;
  font-weight: 600 !important;
  font-size: 13px !important;
  padding: 0 24px !important;
  height: 40px !important;
  display: inline-flex;
  align-items: center;
}

.btn-social-google:hover {
  background-color: rgba(255, 255, 255, 0.15) !important;
}

.google-icon {
  color: #FAF9F6 !important;
  font-size: 18px !important;
}

.btn-social-circle {
  background-color: rgba(255, 255, 255, 0.08) !important;
  border: 1px solid rgba(255, 255, 255, 0.2) !important;
  color: white !important;
  width: 40px !important;
  height: 40px !important;
  min-width: 40px !important;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

.btn-social-circle:hover {
  background-color: rgba(255, 255, 255, 0.15) !important;
}

/* Text Divider */
.divider-or {
  display: flex;
  align-items: center;
  gap: 16px;
  color: #BBE1DC;
  font-size: 14px;
}

.divider-line {
  flex: 1;
  height: 1px;
  background-color: rgba(255, 255, 255, 0.15);
}

.divider-text {
  font-weight: 600;
}

/* Form inputs styling */
.form-row {
  display: flex;
  gap: 16px;
}

.form-col {
  flex: 1;
}

/* Pill Input overrides */
:deep(.pill-input) {
  background-color: rgba(255, 255, 255, 0.06) !important;
  border: 1.5px solid rgba(255, 255, 255, 0.15) !important;
  color: white !important;
  border-radius: 24px !important;
  padding: 8px 18px !important;
}

:deep(.pill-input-select) {
  width: 100% !important;
}

:deep(.pill-input-select .ant-select-selector) {
  background-color: rgba(255, 255, 255, 0.06) !important;
  border: 1.5px solid rgba(255, 255, 255, 0.15) !important;
  color: white !important;
  border-radius: 24px !important;
  height: 43px !important;
  padding: 6px 18px !important;
  box-shadow: none !important;
}

:deep(.pill-input-select .ant-select-selection-item) {
  color: white !important;
  font-weight: 600 !important;
  font-size: 14px !important;
}

:deep(.pill-input-select .ant-select-arrow) {
  color: rgba(255, 255, 255, 0.6) !important;
  right: 18px !important;
}

:deep(.pill-input-select:hover .ant-select-selector), 
:deep(.pill-input-select.ant-select-focused .ant-select-selector) {
  border-color: #BBE1DC !important;
}

:deep(.pill-input input), :deep(.pill-input .ant-input) {
  background: transparent !important;
  background-color: transparent !important;
  color: white !important;
  border: none !important;
  box-shadow: none !important;
}

:deep(.pill-input::placeholder),
:deep(.pill-input input::placeholder),
:deep(.pill-input .ant-input::placeholder) {
  color: rgba(255, 255, 255, 0.35) !important;
  opacity: 1 !important;
}

:deep(.pill-input:hover), :deep(.pill-input:focus-within) {
  border-color: #BBE1DC !important;
  box-shadow: 0 0 0 2px rgba(187, 225, 220, 0.1) !important;
}

:deep(.ant-input-password-icon) {
  color: rgba(255,255,255,0.6) !important;
}

:deep(.ant-form-item-label > label) {
  color: white !important;
  font-weight: 700 !important;
  font-size: 13px !important;
  padding-bottom: 2px !important;
}

.terms-checkbox {
  color: #BBE1DC !important;
  font-size: 13px;
}

:deep(.ant-checkbox-wrapper:hover .ant-checkbox-inner),
:deep(.ant-checkbox-checked .ant-checkbox-inner) {
  border-color: #CF7C07 !important;
  background-color: #CF7C07 !important;
}

.terms-link {
  color: #FAF9F6;
  font-weight: 700;
  text-decoration: underline;
}

/* Pill Signup Button */
.btn-signup-pill {
  width: 100%;
  height: 46px !important;
  border-radius: 24px !important;
  background-color: #FAF9F6 !important;
  border-color: #FAF9F6 !important;
  color: #1E5652 !important; /* Dark text matching background */
  font-weight: 800 !important;
  font-size: 15px !important;
  box-shadow: 0 6px 15px rgba(0,0,0,0.15) !important;
  transition: transform 0.2s, background-color 0.3s !important;
}

.btn-signup-pill:hover {
  background-color: #FAF9F6 !important;
  transform: translateY(-1px);
}

.btn-signup-pill:disabled {
  background-color: rgba(255, 255, 255, 0.1) !important;
  border-color: rgba(255, 255, 255, 0.05) !important;
  color: rgba(255, 255, 255, 0.3) !important;
}

.footer-links-signup {
  text-align: center;
  font-size: 13px;
  color: #BBE1DC;
}

.btn-signin-link {
  color: white;
  font-weight: 800;
  text-decoration: underline;
}

.back-link {
  color: #BBE1DC;
  text-decoration: none;
  font-size: 13px;
  font-weight: 600;
  transition: color 0.3s;
  display: block;
}

.back-link:hover {
  color: white;
}

/* Celebration Modal & Thẻ Số Đồ Họa */
.success-modal-content {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.success-icon-wrapper {
  animation: scaleUp 0.5s cubic-bezier(0.175, 0.885, 0.32, 1.275);
}

.success-title {
  font-size: 24px;
  font-weight: 800;
  color: #114B44;
}

.virtual-card-wrapper {
  perspective: 1000px;
  width: 100%;
  max-width: 360px;
}

.virtual-card {
  position: relative;
  background: linear-gradient(135deg, #114B44 0%, #1E5652 100%);
  color: white;
  border-radius: 16px;
  padding: 24px;
  box-shadow: 0 15px 30px rgba(30, 86, 82, 0.3);
  overflow: hidden;
  border: 1px solid rgba(255,255,255,0.1);
  transform: rotateX(5deg);
  transform-style: preserve-3d;
}

.card-bg-glow {
  position: absolute;
  top: -50%;
  right: -50%;
  width: 150%;
  height: 150%;
  background: radial-gradient(circle, rgba(207,124,7,0.15) 0%, rgba(255,255,255,0) 70%);
  z-index: 1;
}

.card-brand span {
  font-weight: 800;
  letter-spacing: 0.5px;
  font-size: 14px;
}

.card-type {
  font-size: 10px;
  font-weight: 700;
  letter-spacing: 1.5px;
  background-color: rgba(255,255,255,0.15);
  padding: 2px 8px;
  border-radius: 4px;
}

.card-label {
  font-size: 9px;
  color: #CBE0DE;
  font-weight: 700;
  letter-spacing: 1px;
  margin-bottom: 4px;
}

.card-number {
  font-size: 28px;
  font-weight: 800;
  font-family: 'Courier New', Courier, monospace;
  letter-spacing: 2px;
  position: relative;
  z-index: 2;
  text-shadow: 0 2px 4px rgba(0,0,0,0.2);
}

.card-val {
  font-size: 14px;
  font-weight: 700;
  letter-spacing: 0.5px;
}

.btn-success-close {
  background-color: #CF7C07 !important;
  border-color: #CF7C07 !important;
  color: white !important;
  font-weight: 700 !important;
  padding: 10px 32px !important;
  height: 44px !important;
  box-shadow: 0 4px 15px rgba(207, 124, 7, 0.3);
}

@keyframes scaleUp {
  0% { transform: scale(0.5); opacity: 0; }
  100% { transform: scale(1); opacity: 1; }
}

@media (max-width: 850px) {
  .split-container {
    flex-direction: column;
    height: auto;
    max-width: 460px;
  }
  .scenery-panel {
    display: none;
  }
  .form-panel {
    padding: 40px 30px;
  }
}

@keyframes rp-rise {
  0%   { transform: translateY(0px) scale(1);   opacity: 0.9; }
  60%  { opacity: 1; }
  100% { transform: translateY(-160px) scale(0.4); opacity: 0; }
}
@keyframes rp-sparkle {
  0%   { transform: translateY(0px) rotate(0deg) scale(1);   opacity: 1; }
  50%  { transform: translateY(-80px) rotate(180deg) scale(1.4); opacity: 0.9; }
  100% { transform: translateY(-160px) rotate(360deg) scale(0.3); opacity: 0; }
}
@keyframes rp-twinkle {
  0%,100% { opacity: 0.2; transform: scale(0.7); }
  50%     { opacity: 1;   transform: scale(1.6); }
}
@keyframes rp-float {
  0%,100% { transform: translateY(0px); }
  50%     { transform: translateY(-14px); }
}
@keyframes rp-pageturn {
  0%,100% { transform: scaleX(1); }
  50%     { transform: scaleX(-0.2); }
}
@keyframes rp-bounce {
  0%,20%  { transform: translateY(0) scale(1); }
  40%     { transform: translateY(-14px) scale(1.15); }
  60%     { transform: translateY(-6px) scale(1.05); }
  80%     { transform: translateY(-10px) scale(1.1); }
  100%    { transform: translateY(0) scale(1); }
}
@keyframes rp-glow-pulse {
  0%,100% { opacity: 1; filter: drop-shadow(0 0 0px #CF7C07); }
  50%     { opacity: 0.8; filter: drop-shadow(0 0 10px #CF7C07); }
}

:deep(.rp-orb) { animation: rp-rise 3.5s ease-out infinite; }
:deep(.ro1) { animation-delay:0s;    animation-duration:3.2s; }
:deep(.ro2) { animation-delay:0.45s; animation-duration:3.8s; }
:deep(.ro3) { animation-delay:0.9s;  animation-duration:3.4s; }
:deep(.ro4) { animation-delay:1.35s; animation-duration:4.0s; }
:deep(.ro5) { animation-delay:1.8s;  animation-duration:3.6s; }
:deep(.ro6) { animation-delay:0.25s; animation-duration:3.1s; }
:deep(.ro7) { animation-delay:0.7s;  animation-duration:3.9s; }
:deep(.ro8) { animation-delay:2.1s;  animation-duration:3.3s; }

:deep(.rp-spark) { animation: rp-sparkle 4s ease-out infinite; }
:deep(.rs1)  { animation-delay:0s;    animation-duration:3.8s; }
:deep(.rs2)  { animation-delay:0.4s;  animation-duration:4.2s; }
:deep(.rs3)  { animation-delay:0.8s;  animation-duration:3.6s; }
:deep(.rs4)  { animation-delay:1.2s;  animation-duration:4.5s; }
:deep(.rs5)  { animation-delay:1.6s;  animation-duration:3.9s; }
:deep(.rs6)  { animation-delay:2.0s;  animation-duration:4.1s; }
:deep(.rs7)  { animation-delay:0.3s;  animation-duration:3.7s; }
:deep(.rs8)  { animation-delay:0.9s;  animation-duration:4.3s; }
:deep(.rs9)  { animation-delay:1.5s;  animation-duration:3.5s; }
:deep(.rs10) { animation-delay:2.2s;  animation-duration:4.0s; }

:deep(.rp-twinkle) { animation: rp-twinkle 2.5s ease-in-out infinite; }
:deep(.rt1)  { animation-delay:0s;   } :deep(.rt2)  { animation-delay:0.3s; }
:deep(.rt3)  { animation-delay:0.6s; } :deep(.rt4)  { animation-delay:0.9s; }
:deep(.rt5)  { animation-delay:1.2s; } :deep(.rt6)  { animation-delay:1.5s; }
:deep(.rt7)  { animation-delay:1.8s; } :deep(.rt8)  { animation-delay:0.4s; }
:deep(.rt9)  { animation-delay:0.7s; } :deep(.rt10) { animation-delay:1.0s; }

:deep(.rp-float-book) { animation: rp-float 4s ease-in-out infinite; }
:deep(.rfb1) { animation-delay:0s;   } :deep(.rfb2) { animation-delay:1.2s; }
:deep(.rfb3) { animation-delay:2.4s; } :deep(.rfb4) { animation-delay:0.8s; }

:deep(.rp-page-turn) { transform-origin: 5px center; animation: rp-pageturn 5s ease-in-out infinite; }
:deep(.rp-bl) { animation: rp-bounce 1.6s cubic-bezier(0.36,0.07,0.19,0.97) infinite; }
:deep(.rbl6), :deep(.rbl7), :deep(.rbl8) { animation: rp-bounce 1.6s cubic-bezier(0.36,0.07,0.19,0.97) infinite, rp-glow-pulse 2s ease-in-out infinite; }
:deep(.rbl1) { animation-delay:0s;    } :deep(.rbl2) { animation-delay:0.12s; }
:deep(.rbl3) { animation-delay:0.24s; } :deep(.rbl4) { animation-delay:0.36s; }
:deep(.rbl5) { animation-delay:0.48s; } :deep(.rbl6) { animation-delay:0.60s; }
:deep(.rbl7) { animation-delay:0.72s; } :deep(.rbl8) { animation-delay:0.84s; }
</style>
