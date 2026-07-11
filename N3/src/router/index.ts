import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '../views/LandingPage.vue'
import { getUserRole, isAuthenticated } from '@/utils/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'landing',
      component: LandingPage
    },
    {
      path: '/features',
      name: 'features',
      component: () => import('../views/FeaturesPage.vue')
    },
    {
      path: '/statistics',
      name: 'statistics',
      component: () => import('../views/StatisticsPage.vue')
    },
    {
      path: '/reviews',
      name: 'reviews',
      component: () => import('../views/ReviewsPage.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginPage.vue')
    },
    {
      path: '/forgot-password',
      name: 'forgot-password',
      component: () => import('../views/ForgotPasswordPage.vue')
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('../views/RegisterPage.vue')
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('../views/DashboardPage.vue')
    },
    {
      path: '/reader/:id',
      name: 'reader-detail',
      component: () => import('../views/ReaderProfilePage.vue')
    },
    {
      path: '/profile',
      name: 'profile',
      component: () => import('../views/ReaderProfilePage.vue')
    }
  ]
})

router.beforeEach((to, from, next) => {
  const requiresAdminLibrarian = ['dashboard', 'reader-detail'].includes(String(to.name))
  const requiresAuthAny = ['profile'].includes(String(to.name))
  const guestOnlyRoutes = ['login', 'register']

  if (requiresAdminLibrarian || requiresAuthAny) {
    if (!isAuthenticated()) {
      next('/login')
      return
    }

    if (requiresAdminLibrarian) {
      const role = (getUserRole() || '').toLowerCase()
      if (role !== 'admin' && role !== 'librarian') {
        next('/login')
        return
      }
    }
  }

  if (guestOnlyRoutes.includes(String(to.name)) && isAuthenticated()) {
    const role = (getUserRole() || '').toLowerCase()
    if (role === 'admin') {
      next('/dashboard')
      return
    }
  }

  next()
})

export default router
