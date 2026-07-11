import { defineStore } from 'pinia'
import { ref, watch } from 'vue'

const LS_KEY = 'smartlib_library_info'

function loadFromStorage() {
  try {
    const raw = localStorage.getItem(LS_KEY)
    return raw ? JSON.parse(raw) : {}
  } catch {
    return {}
  }
}

export const useLibraryStore = defineStore('library', () => {
  const saved = loadFromStorage()

  const name        = ref<string>(saved.name        ?? 'SmartLib.NET')
  const address     = ref<string>(saved.address     ?? 'Đường 3/2, Xuân Khánh, Ninh Kiều, Cần Thơ')
  const phone       = ref<string>(saved.phone       ?? '1900 6789')
  const email       = ref<string>(saved.email       ?? 'info@smartlib.net')
  const description = ref<string>(saved.description ?? 'Thư viện số thông minh thế hệ mới — nơi tri thức gặp công nghệ, mở cánh cửa học tập không giới hạn cho mọi người Việt.')
  const logoUrl     = ref<string>(saved.logoUrl     ?? '')

  // Auto-save to localStorage whenever any value changes
  watch([name, address, phone, email, description, logoUrl], () => {
    localStorage.setItem(LS_KEY, JSON.stringify({
      name: name.value,
      address: address.value,
      phone: phone.value,
      email: email.value,
      description: description.value,
      logoUrl: logoUrl.value
    }))
  })

  function update(data: Partial<{
    name: string
    address: string
    phone: string
    email: string
    description: string
    logoUrl: string
  }>) {
    if (data.name        !== undefined) name.value        = data.name
    if (data.address     !== undefined) address.value     = data.address
    if (data.phone       !== undefined) phone.value       = data.phone
    if (data.email       !== undefined) email.value       = data.email
    if (data.description !== undefined) description.value = data.description
    if (data.logoUrl     !== undefined) logoUrl.value     = data.logoUrl
  }

  return { name, address, phone, email, description, logoUrl, update }
})
