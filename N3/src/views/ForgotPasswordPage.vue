<template>
  <div class="auth-page-container">
    <div class="split-container forgot-style">
      <!-- LEFT PANEL: Cozy Tree Reading scenery exactly like Sample 2 / LoginPage -->
      <div class="scenery-panel-left">
        <svg class="reading-illustration" viewBox="0 0 400 500" preserveAspectRatio="none" xmlns="http://www.w3.org/2000/svg">
          <rect width="400" height="500" fill="#FAF9F6" />
          
          <circle cx="80" cy="120" r="40" fill="#E6EADF" opacity="0.6" />
          <circle cx="160" cy="80" r="20" fill="#DCE2D5" opacity="0.5" />
          <circle cx="340" cy="180" r="30" fill="#FAF9F6" opacity="0.8" />
          
          <path d="M 50 180 C 20 220 30 280 80 320 C 130 360 280 380 300 300 C 330 200 240 120 180 140 C 120 160 80 140 50 180 Z" fill="#E2ECE9" opacity="0.9" />

          <!-- Cozy Tree -->
          <g transform="translate(140, 160)">
            <path d="M 55 120 Q 60 180 60 210 L 50 210 Q 50 180 45 120 Z" fill="#533527" />
            <path d="M 10 150 Q 55 90 100 150 L 85 155 Q 55 110 25 155 Z" fill="#1E5652" />
            <path d="M 0 160 Q 55 80 110 160 C 95 140 15 140 0 160 Z" fill="#153E3B" />
            <path d="M 20 110 Q 55 60 90 110 L 80 115 Q 55 80 30 115 Z" fill="#277D78" />
            <path d="M 15 120 Q 55 50 95 120 C 85 105 25 105 15 120 Z" fill="#1E5652" />
            <polygon points="55,20 25,80 85,80" fill="#2A7973" />
            <polygon points="55,30 35,70 75,70" fill="#359A92" />
            <polygon points="55,5 58,14 67,14 60,20 62,29 55,23 48,29 50,20 43,14 52,14" fill="#F5B041" />

            <circle cx="40" cy="70" r="5" fill="#D35230" />
            <circle cx="70" cy="75" r="5" fill="#CF7C07" />
            <circle cx="30" cy="115" r="6" fill="#FAF9F6" />
            <circle cx="80" cy="110" r="6" fill="#D35230" />
            <circle cx="55" cy="135" r="5" fill="#D35230" />
          </g>

          <g transform="translate(130, 310) scale(0.7)">
            <circle cx="30" cy="20" r="6" fill="#1E332E" />
            <path d="M 30 26 C 25 26 15 32 15 42 L 32 42 L 35 48 L 45 48 L 38 35 Z" fill="#1E332E" />
            <path d="M 36 34 L 44 32 L 48 35 L 40 37 Z" fill="#CF7C07" />
          </g>

          <text x="75" y="45" font-family="'Inter', sans-serif" font-size="14" font-weight="800" fill="#1E5652">SmartLib.NET</text>
          <text x="30" y="460" font-family="sans-serif" font-size="9" fill="#7A8583">© 2026 SmartLib.NET System</text>
          <text x="30" y="475" font-family="sans-serif" font-size="9" fill="#9AA6A4">Powered by Group 3 Services</text>
        </svg>

        <svg class="scenery-wave-left" viewBox="0 0 100 100" preserveAspectRatio="none">
          <path d="M 0,0 Q 30,30 10,50 T 40,75 T 0,100 Z" fill="#FAF9F6" />
        </svg>
      </div>

      <!-- RIGHT PANEL: Elegant Password Recovery Form in Deep Emerald -->
      <div class="form-panel-right">
        <div class="form-content-wrapper">
          <h2 class="auth-title-forgot">Reset Password</h2>
          
          <div class="step-indicator mb-6">
            <span :class="{ active: step === 1, done: step > 1 }">1. Request</span>
            <span class="step-line"></span>
            <span :class="{ active: step === 2 }">2. Reset</span>
          </div>

          <!-- STEP 1: Enter email to receive token -->
          <a-form v-if="step === 1" :model="emailState" @finish="onRequestToken" layout="vertical">
            <p class="subtitle-text mb-6">{{ t('auto.txt_3833739b') }}</p>
            
            <a-form-item
              label="Email Address"
              name="email"
              :rules="[
                { required: true, message: 'Vui lòng nhập email!' },
                { type: 'email', message: 'Địa chỉ email không hợp lệ!' }
              ]"
            >
              <a-input v-model:value="emailState.email" placeholder="example@domain.com" class="pill-input" />
            </a-form-item>

            <a-form-item class="mt-8">
              <a-button type="primary" html-type="submit" class="btn-forgot-pill" :loading="loading">
                {{ t('auto.txt_769e52d9') }}
              </a-button>
            </a-form-item>
          </a-form>

          <!-- STEP 2: Input token and new password -->
          <a-form v-else :model="resetState" @finish="onResetPassword" layout="vertical">
            <p class="subtitle-text mb-4">{{ t('auto.txt_98809dfd') }} <strong>{{ emailState.email }}</strong>{{ t('auto.txt_d47944cc') }}</p>
            
            <a-form-item
              :label="t('auto.txt_1b001c91')"
              name="token"
              :rules="[{ required: true, message: 'Vui lòng nhập mã xác thực từ email!' }]"
            >
              <a-input v-model:value="resetState.token" :placeholder="t('auto.txt_f3786db5')" class="pill-input" />
            </a-form-item>

            <a-form-item
              :label="t('auto.txt_4129871f')"
              name="newPassword"
              :rules="[
                { required: true, message: 'Vui lòng nhập mật khẩu mới!' },
                { validator: validatePasswordStrength }
              ]"
            >
              <a-input-password v-model:value="resetState.newPassword" :placeholder="t('auto.txt_159f5728')" class="pill-input" />
            </a-form-item>

            <a-form-item
              :label="t('auto.txt_46d5b92d')"
              name="confirmPassword"
              :rules="[
                { required: true, message: 'Vui lòng nhập lại mật khẩu mới!' },
                { validator: validateConfirmPassword }
              ]"
            >
              <a-input-password v-model:value="resetState.confirmPassword" placeholder="xxxxxxxx" class="pill-input" />
            </a-form-item>

            <a-form-item class="mt-8">
              <a-button type="primary" html-type="submit" class="btn-forgot-pill" :loading="loading">
                {{ t('auto.txt_691e5b59') }}
              </a-button>
            </a-form-item>
          </a-form>

          <div class="footer-links-forgot mt-6">
            <span>{{ t('auto.txt_c23a8def') }} </span>
            <a href="#" class="btn-login-link" @click.prevent="$router.push('/login')">{{ t('auto.txt_32b703d0') }}</a>
          </div>

          <div class="text-center mt-6">
            <a href="#" class="back-link-forgot" @click.prevent="$router.push('/')">
              <v-icon icon="mdi-arrow-left" class="mr-1"></v-icon> {{ t('auto.txt_3cc53a09') }}
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useI18nStore } from '@/stores/i18nStore'
const i18n = useI18nStore()
const t = (key: string) => i18n.t(key)

import { reactive, ref } from 'vue'
import { message } from 'ant-design-vue'
import { useRouter } from 'vue-router'
import { apiClient } from '@/utils/apiClient'

const router = useRouter()
const loading = ref(false)
const step = ref(1)

const emailState = reactive({
  email: ''
})

const resetState = reactive({
  token: '',
  newPassword: '',
  confirmPassword: ''
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
    return Promise.reject(
      new Error(
        'Mật khẩu phải gồm ít nhất: 6 ký tự, 1 chữ hoa, 1 chữ thường, 1 chữ số, và 1 ký tự đặc biệt (e.g. @, #, !, $).'
      )
    )
  }
  
  return Promise.resolve()
}

const validateConfirmPassword = (_rule: any, value: string) => {
  if (!value) {
    return Promise.resolve()
  }
  if (value !== resetState.newPassword) {
    return Promise.reject(new Error('Mật khẩu nhập lại không khớp!'))
  }
  return Promise.resolve()
}

const onRequestToken = async () => {
  loading.value = true
  try {
    const response = await apiClient.post('/api/identity/Auth/forgot-password', {
      Email: emailState.email
    })
    
    message.success(response.data?.Message || 'Mã xác thực đã được gửi thành công! Vui lòng kiểm tra hòm thư của bạn.')
    
    step.value = 2
  } catch (error: any) {
    console.error('Request Reset Token Error:', error)
    const errMessage = error.response?.data?.Message || error.response?.data?.message || 'Không thể gửi mã xác thực. Vui lòng kiểm tra lại email!'
    message.error(errMessage)
  } finally {
    loading.value = false
  }
}

const onResetPassword = async () => {
  loading.value = true
  try {
    const response = await apiClient.post('/api/identity/Auth/reset-password', {
      Email: emailState.email,
      Token: resetState.token,
      NewPassword: resetState.newPassword,
      ConfirmPassword: resetState.confirmPassword
    })
    
    message.success(response.data?.Message || 'Mật khẩu đã được cập nhật thành công!')
    router.push('/login')
  } catch (error: any) {
    console.error('Reset Password Error:', error)
    const errMessage = error.response?.data?.Message || error.response?.data?.message || 'Cập nhật thất bại. Mã xác thực không chính xác hoặc đã hết hạn!'
    message.error(errMessage)
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
  background-color: #FAF9F6;
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

.scenery-panel-left {
  flex: 1.1;
  position: relative;
  height: 100%;
  background-color: #FAF9F6;
  overflow: hidden;
  border-right: 1px solid rgba(0,0,0,0.02);
}

.reading-illustration {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.scenery-wave-left {
  position: absolute;
  top: 0;
  right: -2px;
  bottom: 0;
  width: 100px;
  height: 100%;
  z-index: 10;
  pointer-events: none;
}

.form-panel-right {
  flex: 1;
  background-color: #1E5652;
  color: white;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 40px 60px;
  position: relative;
  z-index: 5;
}

.form-content-wrapper {
  width: 100%;
}

.auth-title-forgot {
  font-size: 38px;
  font-weight: 800;
  color: #FAF9F6;
  margin-bottom: 8px;
  font-family: 'Outfit', sans-serif;
  letter-spacing: 0.5px;
}

.subtitle-text {
  font-size: 13px;
  color: #BBE1DC;
  line-height: 1.6;
}

.step-indicator {
  display: flex;
  align-items: center;
  gap: 12px;
  font-size: 12px;
  font-weight: 700;
  color: rgba(255, 255, 255, 0.4);
}

.step-indicator span.active {
  color: #FAF9F6;
}

.step-indicator span.done {
  color: #BBE1DC;
  text-decoration: line-through;
}

.step-line {
  flex: 1;
  height: 1px;
  background-color: rgba(255, 255, 255, 0.15);
}

/* Pill Input overrides */
:deep(.pill-input) {
  background-color: rgba(255, 255, 255, 0.06) !important;
  border: 1.5px solid rgba(255, 255, 255, 0.15) !important;
  color: white !important;
  border-radius: 24px !important;
  padding: 8px 18px !important;
}

:deep(.pill-input input), :deep(.pill-input .ant-input) {
  background: transparent !important;
  background-color: transparent !important;
  color: white !important;
  border: none !important;
  box-shadow: none !important;
}

:deep(.pill-input input::placeholder) {
  color: rgba(255, 255, 255, 0.35) !important;
}

:deep(.pill-input:hover), :deep(.pill-input:focus-within) {
  border-color: #BBE1DC !important;
  box-shadow: 0 0 0 2px rgba(187, 225, 220, 0.1) !important;
}

:deep(.ant-input-password-icon) {
  color: rgba(255,255,255,0.6) !important;
}

:deep(.ant-form-item-label > label) {
  color: #BBE1DC !important;
  font-weight: 700 !important;
  font-size: 13px !important;
  padding-bottom: 2px !important;
}

/* Pill Forgot Button */
.btn-forgot-pill {
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

.btn-forgot-pill:hover {
  background-color: #EBF3E8 !important;
  transform: translateY(-1px);
}

.footer-links-forgot {
  text-align: center;
  font-size: 13px;
  color: #BBE1DC;
}

.btn-login-link {
  color: white;
  font-weight: 800;
  text-decoration: underline;
}

.back-link-forgot {
  color: #BBE1DC;
  text-decoration: none;
  font-size: 13px;
  font-weight: 600;
  transition: color 0.3s;
  display: inline-block;
}

.back-link-forgot:hover {
  color: white;
}

/* Force autofill background to match deep emerald theme instead of default browser white */
:deep(input:-webkit-autofill),
:deep(input:-webkit-autofill:hover), 
:deep(input:-webkit-autofill:focus), 
:deep(input:-webkit-autofill:active) {
  -webkit-box-shadow: 0 0 0 1000px #1E5652 inset !important;
  -webkit-text-fill-color: white !important;
  transition: background-color 5000s ease-in-out 0s;
}

@media (max-width: 850px) {
  .split-container {
    flex-direction: column;
    height: auto;
    max-width: 460px;
  }
  .scenery-panel-left {
    display: none;
  }
  .form-panel-right {
    padding: 40px 30px;
  }
}
</style>
