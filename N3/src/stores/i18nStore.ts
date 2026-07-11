import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import vi from '../locales/vi.json'
import en from '../locales/en.json'

export type Locale = 'vi' | 'en'

const messages: Record<Locale, Record<string, any>> = {
  vi,
  en
}

export const useI18nStore = defineStore('i18n', () => {
  const locale = ref<Locale>(
    (localStorage.getItem('smartlib_locale') as Locale) || 'vi'
  )

  const t = computed(() => (keyPath: string): string => {
    const keys = keyPath.split('.')
    let val: any = messages[locale.value]
    
    for (const k of keys) {
      if (val === undefined || val === null) break
      val = val[k]
    }
    
    if (val !== undefined && typeof val === 'string') return val

    // Fallback to Vietnamese
    let fallback: any = messages.vi
    for (const k of keys) {
      if (fallback === undefined || fallback === null) break
      fallback = fallback[k]
    }
    
    if (fallback !== undefined && typeof fallback === 'string') return fallback

    return keyPath // Return the key itself if not found
  })

  function setLocale(l: Locale) {
    locale.value = l
    localStorage.setItem('smartlib_locale', l)
  }

  return { locale, t, setLocale }
})
