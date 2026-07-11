import { createApp } from 'vue'
import App from './App.vue'
import { createPinia } from 'pinia'
import router from './router'

// Vuetify 3
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'

// Ant Design Vue
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css'

// ApexCharts
import VueApexCharts from 'vue3-apexcharts'

const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'light',
    themes: {
      light: {
        colors: {
          primary: '#1E5652', // Dark Emerald/Teal
          secondary: '#CF7C07', // Warm Ochre
          background: '#FAF9F6', // Soft Ivory
        }
      }
    }
  }
})

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(vuetify)
app.use(Antd)
app.use(VueApexCharts)

app.mount('#app')
