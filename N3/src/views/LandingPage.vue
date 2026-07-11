<template>
  <div class="landing-container">
    <!-- 1. Thanh điều hướng (Header Navigation) -->
    <header class="navbar">
      <div class="logo">
        <img v-if="lib.logoUrl" :src="lib.logoUrl" class="logo-img" alt="logo" />
        <v-icon v-else icon="mdi-library" class="mr-2 logo-icon"></v-icon>
        <span class="logo-text">{{ lib.name }}</span>
      </div>
      <nav class="nav-links">
        <a href="#" class="active" @click.prevent="$router.push('/')">{{ t('nav.home') }}</a>
        <a href="#" @click.prevent="$router.push('/features')">{{ t('nav.about') }}</a>
        <a href="#" @click.prevent="$router.push('/statistics')">{{ t('nav.guide') }}</a>
        <a href="#" @click.prevent="$router.push('/reviews')">{{ t('nav.contact') }}</a>
      </nav>
      <div class="nav-auth">
        <a-button type="text" class="btn-login mr-2" @click="$router.push('/login')">{{ t('nav.login') }}</a-button>
        <a-button type="primary" shape="round" class="btn-register" @click="$router.push('/register')">{{ t('nav.register') }}</a-button>
      </div>
    </header>

    <!-- 2. Khu vực Chào mừng (Hero Section - Trọng tâm) -->
    <main class="hero-section">
      <div class="hero-left">
        <h1 class="hero-title">{{ lib.name || 'Online library' }}</h1>
        <p class="hero-description">
          {{ lib.description }}
        </p>
        <button class="btn-learn-more" @click="$router.push('/register')">
          {{ t('hero.cta') }}
        </button>
      </div>

      <!-- Wavy separator and bookshelf graphic -->
      <div class="hero-right">
        <!-- Wave border trái -->
        <svg class="wave-border" viewBox="0 0 70 100" preserveAspectRatio="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M70,0 H0 V100 H70 C70,100 35,88 45,75 C55,62 70,55 60,42 C50,29 30,25 40,13 C50,1 70,0 70,0 Z" fill="#eef7ee"/>
        </svg>
        <div class="bookshelf-wrap">
          <!-- Shelf 1 -->
          <div class="shelf-row">
            <div class="shelf-books">
              <div class="bk teal tall"     @click="$router.push('/features')"><span>Clean Code</span></div>
              <div class="bk orange medium" @click="$router.push('/features')"><span>Refactoring</span></div>
              <div class="bk pink tall"     @click="$router.push('/features')"><span>Design Patterns</span></div>
              <div class="bk yellow short"  @click="$router.push('/features')"><span>Vue 3</span></div>
              <div class="bk green tall"    @click="$router.push('/features')"><span>Microservices</span></div>
              <div class="bk purple medium" @click="$router.push('/features')"><span>Domain Driven</span></div>
              <div class="bk ochre medium lean-left" @click="$router.push('/features')"><span>TypeScript</span></div>
              <div class="bk blue tall"     @click="$router.push('/features')"><span>ASP.NET Core</span></div>
            </div>
            <div class="shelf-plank"></div>
          </div>

          <!-- Shelf 2 -->
          <div class="shelf-row">
            <div class="shelf-books">
              <div class="bk orange tall"   @click="$router.push('/features')"><span>C# 12</span></div>
              <div class="bk purple medium" @click="$router.push('/features')"><span>EF Core</span></div>
              <div class="bk teal2 tall"    @click="$router.push('/features')"><span>RabbitMQ</span></div>
              <div class="bk red short"     @click="$router.push('/features')"><span>Redis</span></div>
              <div class="bk yellow medium" @click="$router.push('/features')"><span>React</span></div>
              <div class="bk pink medium lean-right" @click="$router.push('/features')"><span>Angular</span></div>
              <div class="bk green tall"    @click="$router.push('/features')"><span>Node.js</span></div>
              <div class="bk beige medium"  @click="$router.push('/features')"><span>MongoDB</span></div>
            </div>
            <div class="shelf-plank"></div>
          </div>

          <!-- Shelf 3 -->
          <div class="shelf-row">
            <div class="shelf-books">
              <div class="bk green tall"    @click="$router.push('/features')"><span>Linux</span></div>
              <div class="bk red medium"    @click="$router.push('/features')"><span>Git Flow</span></div>
              <div class="bk yellow tall"   @click="$router.push('/features')"><span>CI/CD</span></div>
              <div class="bk beige medium lean-left" @click="$router.push('/features')"><span>Ansible</span></div>
              <div class="bk blue tall"     @click="$router.push('/features')"><span>Prometheus</span></div>
              <div class="bk pink2 medium"  @click="$router.push('/features')"><span>Grafana</span></div>
              <div class="bk orange tall"   @click="$router.push('/features')"><span>Kafka</span></div>
              <div class="bk teal short"    @click="$router.push('/features')"><span>Vault</span></div>
            </div>
            <div class="shelf-plank"></div>
          </div>
        </div>
      </div>
    </main>

    <!-- 3. Giới thiệu Tính năng nổi bật (Features Section) -->
    <section id="features" class="features-section py-16 px-8">
      <h2 class="section-title text-center mb-12">{{ t('landing.features.title') }}</h2>
      
      <div class="showcase-section">
        <!-- Feature 1 -->
        <div class="feature-row scroll-reveal">
          <div class="feature-visual scroll-reveal slide-from-left">
            <div class="visual-frame">
              <div class="sim-card">
                <div class="card-chip"></div>
                <div class="card-logo">{{ lib.name }}</div>
                <div class="card-body">
                  <div class="sim-qr">
                    <svg viewBox="0 0 100 100" class="qr-svg">
                      <rect x="0" y="0" width="100" height="100" fill="white" />
                      <rect x="10" y="10" width="25" height="25" fill="#1E5652" />
                      <rect x="15" y="15" width="15" height="15" fill="white" />
                      <rect x="65" y="10" width="25" height="25" fill="#1E5652" />
                      <rect x="70" y="15" width="15" height="15" fill="white" />
                      <rect x="10" y="65" width="25" height="25" fill="#1E5652" />
                      <rect x="15" y="70" width="15" height="15" fill="white" />
                      <rect x="45" y="20" width="10" height="10" fill="#1E5652" />
                      <rect x="40" y="45" width="15" height="10" fill="#1E5652" />
                      <rect x="20" y="45" width="10" height="15" fill="#CF7C07" />
                      <rect x="65" y="45" width="20" height="20" fill="#1E5652" />
                      <rect x="45" y="70" width="20" height="15" fill="#CF7C07" />
                    </svg>
                  </div>
                  <div class="sim-info">
                    <div class="info-lbl">{{ t('landing.card.title') }}</div>
                    <div class="info-val">LIB-884920</div>
                    <div class="info-lbl mt-2">{{ t('auto.txt_8f69d55b') }}</div>
                    <div class="info-val">{{ t('landing.card.name') }}</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="feature-text scroll-reveal delay-1">
            <span class="feature-num">01</span>
            <h2 class="feature-title scroll-reveal delay-1">{{ t('landing.f1.title') }}</h2>
            <p class="feature-desc scroll-reveal delay-2">{{ t('landing.f1.desc') }}</p>
            <a href="#" class="feature-link scroll-reveal delay-3" @click.prevent="$router.push('/features')">{{ t('landing.f1.link') }}</a>
          </div>
        </div>

        <!-- Feature 2 -->
        <div class="feature-row scroll-reveal">
          <div class="feature-text scroll-reveal delay-1">
            <span class="feature-num">02</span>
            <h2 class="feature-title scroll-reveal delay-1">{{ t('landing.f2.title') }}</h2>
            <p class="feature-desc scroll-reveal delay-2">{{ t('landing.f2.desc') }}</p>
            <a href="#" class="feature-link scroll-reveal delay-3" @click.prevent="$router.push('/features')">{{ t('landing.f1.link') }}</a>
          </div>
          <div class="feature-visual scroll-reveal slide-from-right delay-1">
            <div class="visual-frame light">
              <div class="search-bar-sim">
                <v-icon icon="mdi-magnify" class="mr-2"></v-icon>
                <input type="text" value="ASP.NET Core" disabled />
                <div class="search-pill">{{ t('landing.search_placeholder') }}</div>
              </div>
              <div class="search-results-sim">
                <div class="result-card-sim">
                  <div class="book-cover-mini">C#</div>
                  <div class="result-info">
                    <div class="result-title">{{ t('landing.book1.title') }}</div>
                    <div class="result-author">{{ t('landing.book1.author') }}</div>
                    <div class="result-tag available">{{ t('landing.book1.status') }}</div>
                  </div>
                </div>
                <div class="result-card-sim">
                  <div class="book-cover-mini gold">EF</div>
                  <div class="result-info">
                    <div class="result-title">{{ t('landing.book2.title') }}</div>
                    <div class="result-author">{{ t('landing.book2.author') }}</div>
                    <div class="result-tag borrowing">{{ t('landing.book2.status') }}</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Feature 3 -->
        <div class="feature-row scroll-reveal">
          <div class="feature-visual scroll-reveal slide-from-left">
            <div class="visual-frame dark">
              <div class="arch-node client">Client UI (Vue.js)</div>
              <div class="arch-arrow">▼</div>
              <div class="arch-node gateway">API Gateway — Ocelot</div>
              <div class="arch-services">
                <div class="arch-node service">
                  <span class="dot green"></span> Identity
                </div>
                <div class="arch-node service">
                  <span class="dot green"></span> Catalog
                </div>
                <div class="arch-node service orange">
                  <span class="dot orange"></span> RabbitMQ
                </div>
              </div>
            </div>
          </div>
          <div class="feature-text scroll-reveal delay-1">
            <span class="feature-num">03</span>
            <h2 class="feature-title scroll-reveal delay-1">{{ t('landing.f3.title') }}</h2>
            <p class="feature-desc scroll-reveal delay-2">{{ t('landing.f3.desc') }}</p>
            <a href="#" class="feature-link scroll-reveal delay-3" @click.prevent="$router.push('/features')">{{ t('landing.f1.link') }}</a>
          </div>
        </div>
      </div>
    </section>

    <!-- 4. Các con số ấn tượng (Statistics Counter Section) -->
    <section id="stats" class="stats-section">

      <!-- Marquee ticker -->
      <div class="marquee-wrap">
        <div class="marquee-track">
          <div class="marquee-item">📚 12,450 {{ t('landing.stats.books') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">👥 3,200 {{ t('landing.stats.members') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">🏆 {{ t('landing.stats.award') }} 2024 <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">💾 5,800 {{ t('landing.stats.digital_docs') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">⭐ {{ t('landing.stats.rating') }} 4.9/5 <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">🔄 28,900 {{ t('landing.stats.borrows') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <!-- duplicate for seamless loop -->
          <div class="marquee-item">📚 12,450 {{ t('landing.stats.books') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">👥 3,200 {{ t('landing.stats.members') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">🏆 {{ t('landing.stats.award') }} 2024 <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">💾 5,800 {{ t('landing.stats.digital_docs') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">⭐ {{ t('landing.stats.rating') }} 4.9/5 <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
          <div class="marquee-item">🔄 28,900 {{ t('landing.stats.borrows') }} <svg width="4" height="4" viewBox="0 0 4 4"><circle cx="2" cy="2" r="2" fill="#8BBD97"/></svg></div>
        </div>
      </div>

      <!-- Số liệu -->
      <div class="stats-in">
        <div class="scard">
          <div class="sicon">📚</div>
          <div class="snum">12,450<sup>+</sup></div>
          <div class="slbl">{{ t('landing.stats.books_cap') }}</div>
        </div>
        <div class="scard">
          <div class="sicon">👥</div>
          <div class="snum">3,200<sup>+</sup></div>
          <div class="slbl">{{ t('landing.stats.members_cap') }}</div>
        </div>
        <div class="scard">
          <div class="sicon">📤</div>
          <div class="snum">28,900<sup>+</sup></div>
          <div class="slbl">{{ t('landing.stats.borrows_cap') }}</div>
        </div>
        <div class="scard">
          <div class="sicon">💾</div>
          <div class="snum">5,800<sup>+</sup></div>
          <div class="slbl">{{ t('landing.stats.digital_docs_cap') }}</div>
        </div>
      </div>
    </section>

    <!-- 5. How it works -->
    <div class="how-bg">
      <div class="how-sec">
        <div class="sec-center">
          <div class="sec-label scroll-reveal">{{ t('landing.how.label') }}</div>
          <h2 class="sec-h scroll-reveal reveal-d1">{{ t('landing.how.title') }}</h2>
          <p class="sec-p scroll-reveal reveal-d2">{{ t('landing.how.sub') }}</p>
        </div>
        <div class="steps-row">
          <div class="step scroll-reveal reveal-d1">
            <div class="step-num">1</div>
            <div class="st-t">{{ t('landing.step1.t') }}</div>
            <div class="st-d">{{ t('landing.step1.d') }}</div>
          </div>
          <div class="step-line scroll-reveal reveal-d2"><span class="step-arrow">›</span></div>
          <div class="step scroll-reveal reveal-d2">
            <div class="step-num">2</div>
            <div class="st-t">{{ t('landing.step2.t') }}</div>
            <div class="st-d">{{ t('landing.step2.d') }}</div>
          </div>
          <div class="step-line scroll-reveal reveal-d3"><span class="step-arrow">›</span></div>
          <div class="step scroll-reveal reveal-d3">
            <div class="step-num">3</div>
            <div class="st-t">{{ t('landing.step3.t') }}</div>
            <div class="st-d">{{ t('landing.step3.d') }}</div>
          </div>
          <div class="step-line scroll-reveal reveal-d4"><span class="step-arrow">›</span></div>
          <div class="step scroll-reveal reveal-d4">
            <div class="step-num">4</div>
            <div class="st-t">{{ t('landing.step4.t') }}</div>
            <div class="st-d">{{ t('landing.step4.d') }}</div>
          </div>
        </div>
      </div>
    </div>

    <!-- 6. News -->
    <div class="news-bg">
      <div class="how-sec">
        <div class="sec-center">
          <div class="sec-label scroll-reveal">{{ t('landing.news.label') }}</div>
          <h2 class="sec-h scroll-reveal reveal-d1">{{ t('landing.news.title') }}</h2>
        </div>
        <div class="ng">
          <div class="nc scroll-reveal reveal-d1" @click="$router.push('/features')">
            <div class="nc-img" style="background:#FFFBEF">🎉</div>
            <div class="nc-body">
              <span class="ntag tev">{{ t('auto.txt_45a0ffe8') }}</span>
              <div class="nt">{{ t('auto.txt_80cb24f5') }}</div>
              <div class="nd">📅 20/04/2025</div>
            </div>
          </div>
          <div class="nc scroll-reveal reveal-d2" @click="$router.push('/features')">
            <div class="nc-img" style="background:#F4FAF6">📚</div>
            <div class="nc-body">
              <span class="ntag tbk">{{ t('auto.txt_2ddd25ec') }}</span>
              <div class="nt">500+ {{ t('landing.stats.books') }} công nghệ mới tháng 4</div>
              <div class="nd">📅 15/04/2025</div>
            </div>
          </div>
          <div class="nc scroll-reveal reveal-d3" @click="$router.push('/features')">
            <div class="nc-img" style="background:#EFF4FF">📢</div>
            <div class="nc-body">
              <span class="ntag tno">{{ t('auto.txt_fa0565c8') }}</span>
              <div class="nt">{{ t('auto.txt_8ab3e5f5') }}</div>
              <div class="nd">📅 10/04/2025</div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- 7. Footer -->
    <footer class="footer">
      <div class="footer-top">
        <div class="ft-logo-col">
          <div class="ft-logo">
            <div class="logo-box-ft">📚</div>
            <span class="ft-logo-name">{{ lib.name }}</span>
          </div>
          <p class="ft-desc">{{ lib.description }}</p>
          <div class="ft-social">
            <div class="soc">📘</div>
            <div class="soc">💼</div>
            <div class="soc">🐦</div>
            <div class="soc">▶️</div>
          </div>
        </div>
        <div>
          <div class="ft-col-h">{{ t('landing.ft.explore') }}</div>
          <ul class="ft-links">
            <li><a @click.prevent="$router.push('/')">{{ t('nav.home') }}</a></li>
            <li><a @click.prevent="$router.push('/features')">{{ t('nav.about') }}</a></li>
            <li><a @click.prevent="$router.push('/statistics')">{{ t('nav.guide') }}</a></li>
            <li><a @click.prevent="$router.push('/reviews')">{{ t('nav.contact') }}</a></li>
          </ul>
        </div>
        <div>
          <div class="ft-col-h">{{ t('landing.ft.services') }}</div>
          <ul class="ft-links">
            <li><a>{{ t('auto.txt_9013c13c') }}</a></li>
            <li><a>{{ t('auto.txt_6ec698b2') }}</a></li>
            <li><a>{{ t('auto.txt_ab124f2b') }}</a></li>
            <li><a>{{ t('auto.txt_bdf85ddb') }}</a></li>
            <li><a>{{ t('landing.stats.digital_docs_cap') }}</a></li>
          </ul>
        </div>
        <div>
          <div class="ft-col-h">{{ t('landing.ft.newsletter') }}</div>
          <p class="nl-label">{{ t('landing.ft.nl.label') }}</p>
          <div class="newsletter-box">
            <div class="nl-form">
              <input class="nl-input" :placeholder="t('landing.ft.nl.placeholder')" />
              <button class="nl-btn">{{ t('landing.ft.nl.btn') }}</button>
            </div>
          </div>
          <div style="margin-top:1.5rem">
            <div class="ft-col-h" style="margin-bottom:.65rem">{{ t('landing.ft.contact') }}</div>
            <ul class="ft-links">
              <li><a>📍 {{ lib.address }}</a></li>
              <li><a>📞 Hotline: {{ lib.phone }}</a></li>
              <li><a>✉️ {{ lib.email }}</a></li>
            </ul>
          </div>
        </div>
      </div>
      <div class="footer-divider"><hr class="fdiv" /></div>
      <div class="footer-bot">
        <span class="fc-text">{{ t('footer.copyright').replace('©2026', `© 2026 ${lib.name}`) }}</span>
        <div class="fc-links">
          <a>{{ t('landing.ft.privacy') }}</a>
          <a>{{ t('landing.ft.terms') }}</a>
          <a>{{ t('landing.ft.cookie') }}</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { onMounted, onUnmounted } from 'vue'
import { useLibraryStore } from '@/stores/libraryStore'
import { useI18nStore } from '@/stores/i18nStore'

const router = useRouter()
const lib = useLibraryStore()
const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)

let observer: IntersectionObserver | null = null

onMounted(() => {
  observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('revealed')
        } else {
          entry.target.classList.remove('revealed')
        }
        
        // Counter animation logic
        if (entry.isIntersecting && entry.target.id === 'stats' && !entry.target.classList.contains('counted')) {
          entry.target.classList.add('counted'); // Only count up once per full reload for better UX
          const counters = entry.target.querySelectorAll('.counter');
          counters.forEach(counter => {
            const el = counter as HTMLElement;
            const target = +(el.getAttribute('data-target') ?? '0');
            const duration = 4500; // Increased duration for slower animation
            let startTimestamp: number | null = null;
            const step = (timestamp: number) => {
              if (!startTimestamp) startTimestamp = timestamp;
              const progress = Math.min((timestamp - startTimestamp) / duration, 1);
              const easeOutQuart = 1 - Math.pow(1 - progress, 4);
              el.innerText = Math.floor(easeOutQuart * target).toLocaleString();
              if (progress < 1) {
                window.requestAnimationFrame(step);
              } else {
                el.innerText = target.toLocaleString();
              }
            };
            window.requestAnimationFrame(step);
          });
        }
      })
    },
    { threshold: 0.15, rootMargin: '0px 0px -50px 0px' }
  )

  document.querySelectorAll('.scroll-reveal, #stats').forEach((el) => {
    observer?.observe(el)
  })
})

onUnmounted(() => {
  observer?.disconnect()
})

interface Book {
  title: string
  color: 'teal' | 'red' | 'ochre' | 'terracotta' | 'beige'
  height: 'tall' | 'medium' | 'short'
  leaning?: boolean
  angle?: number
}

const getBooksForShelf = (shelf: number): Book[] => {
  if (shelf === 1) {
    return [
      { title: 'Clean Code', color: 'teal', height: 'tall' },
      { title: 'Refactoring', color: 'red', height: 'medium' },
      { title: 'Patterns', color: 'ochre', height: 'short', leaning: true, angle: -12 },
      { title: 'Domain Driven', color: 'terracotta', height: 'tall' },
      { title: 'Microservices', color: 'beige', height: 'medium' },
      { title: 'Kubernetes', color: 'teal', height: 'short' }
    ]
  } else if (shelf === 2) {
    return [
      { title: 'Algorithms', color: 'red', height: 'tall' },
      { title: 'Rust Prog', color: 'ochre', height: 'medium' },
      { title: 'TypeScript', color: 'teal', height: 'short' },
      { title: 'VueJS 3', color: 'terracotta', height: 'medium', leaning: true, angle: 10 },
      { title: 'Pinia Store', color: 'beige', height: 'short' }
    ]
  } else {
    return [
      { title: 'C# 12 Deep', color: 'ochre', height: 'tall' },
      { title: 'ASP.NET Core', color: 'teal', height: 'medium' },
      { title: 'EF Core 9', color: 'red', height: 'short' },
      { title: 'RabbitMQ', color: 'terracotta', height: 'tall' },
      { title: 'Docker Dev', color: 'beige', height: 'medium' }
    ]
  }
}
</script>

<style scoped>
/* Scoped styles to implement the beautiful landing page */
.landing-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: #FAF9F6;
  overflow-x: hidden;
  padding-top: 0;
}

/* Navbar */
.navbar {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 16px 60px;
  background-color: rgba(250, 249, 246, 0.92); /* Glassmorphism ivory */
  backdrop-filter: blur(10px);
  -webkit-backdrop-filter: blur(10px);
  z-index: 1000;
  border-bottom: 1px solid rgba(0, 0, 0, 0.05);
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.02);
  transition: all 0.3s ease;
}

.logo {
  display: flex;
  align-items: center;
}

.logo-icon {
  font-size: 28px;
  color: #1E5652 !important;
}

.logo-img {
  width: 32px;
  height: 32px;
  object-fit: contain;
  margin-right: 8px;
  border-radius: 6px;
}

.logo-text {
  font-size: 24px;
  font-weight: 800;
  color: #1E5652; /* Emerald */
  letter-spacing: 1px;
}

.nav-links {
  display: flex;
  gap: 32px;
}

.nav-links a {
  text-decoration: none;
  font-size: 16px;
  font-weight: 600;
  color: #5C6765;
  letter-spacing: 0.3px;
  transition: color 0.3s;
  position: relative;
  padding-bottom: 3px;
}

.nav-links a::after {
  content: '';
  position: absolute;
  bottom: 0; left: 0;
  width: 0; height: 2px;
  background: #1E5652;
  border-radius: 2px;
  transition: width 0.3s ease;
}

.nav-links a.active::after,
.nav-links a:hover::after {
  width: 100%;
}

.nav-links a.active, .nav-links a:hover {
  color: #1E5652;
}

.nav-auth {
  display: flex;
  align-items: center;
}

.btn-login {
  font-size: 15px !important; /* Larger font size */
  font-weight: 700 !important;
  color: #1E5652 !important;
  text-transform: uppercase !important;
  letter-spacing: 1.5px !important;
}

.btn-register {
  background-color: #1E5652 !important;
  border-color: #1E5652 !important;
  color: white !important;
  font-weight: 700 !important;
  font-size: 14px !important;
  text-transform: uppercase !important;
  letter-spacing: 1.5px !important;
  box-shadow: 0 4px 10px rgba(30, 86, 82, 0.2);
  transition: transform 0.3s, background-color 0.3s;
}

.btn-register:hover {
  background-color: #153E3B !important;
  transform: translateY(-1px);
}

/* Hero Section */
.hero-section {
  display: flex;
  height: 82vh;
  min-height: 560px;
  padding-top: 72px;
  position: relative;
  background: linear-gradient(135deg, #e8f5e9 0%, #f1f8f1 40%, #eef7ee 70%, #e0f2e0 100%);
}

.hero-left {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 0 40px 40px 80px;
  z-index: 5;
}

.hero-title {
  font-size: 52px;
  font-weight: 800;
  color: #114B44;
  margin-bottom: 16px;
  line-height: 1.1;
  font-family: 'Outfit', sans-serif;
}

.hero-description {
  font-size: 15px;
  color: #5C6765;
  line-height: 1.7;
  margin-bottom: 28px;
  max-width: 440px;
}

.btn-learn-more {
  position: relative;
  overflow: hidden; /* Crucial for mirror/shine effect */
  background-color: #CF7C07; /* Warm ochre from image button */
  color: white;
  border: none;
  border-radius: 40px;
  padding: 16px 40px;
  font-size: 16px;
  font-weight: 700;
  cursor: pointer;
  width: fit-content;
  box-shadow: 0 6px 20px rgba(207, 124, 7, 0.35);
  transition: transform 0.3s, background-color 0.3s, box-shadow 0.3s;
}

/* Infinite Mirror/Shine Effect */
.btn-learn-more::after {
  content: '';
  position: absolute;
  top: -50%;
  left: -60%;
  width: 30%;
  height: 200%;
  background: rgba(255, 255, 255, 0.4);
  transform: rotate(30deg);
  transition: none;
  pointer-events: none;
  animation: btnShine 4.5s infinite ease-in-out;
}

@keyframes btnShine {
  0% {
    left: -60%;
  }
  15% {
    left: 140%;
  }
  100% {
    left: 140%;
  }
}

.btn-learn-more:hover {
  background-color: #B56C05;
  transform: translateY(-2px);
  box-shadow: 0 8px 24px rgba(207, 124, 7, 0.5);
}

/* Hero Right: Dashboard Card */
.hero-right {
  flex: 1.1;
  position: relative;
  background: #2C1F14;
  overflow: hidden;
  display: flex;
  align-items: stretch;
}

.wave-border {
  position: absolute;
  top: 0;
  left: 0;
  width: 70px;
  height: 100%;
  z-index: 10;
  pointer-events: none;
  display: block;
}

.bookshelf-wrap {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  padding: 20px 20px 20px 74px;
  gap: 0;
  overflow: hidden;
}

.shelf-row {
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
}

.shelf-books {
  display: flex;
  align-items: flex-end;
  gap: 5px;
  padding-bottom: 0;
}

.shelf-plank {
  height: 12px;
  background: linear-gradient(180deg, #9B6A42 0%, #7B4F2E 60%, #5C3820 100%);
  border-radius: 3px;
  box-shadow: 0 4px 10px rgba(0,0,0,0.5);
  margin-top: 2px;
}

/* Book base styles */
.bk {
  position: relative;
  border-radius: 2px 3px 3px 2px;
  cursor: pointer;
  transition: transform 0.28s cubic-bezier(0.34, 1.56, 0.64, 1), box-shadow 0.28s ease;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
  box-shadow: -2px 0 8px rgba(0,0,0,0.45);
  overflow: hidden;
}

/* Shimmer / mirror effect */
.bk::after {
  content: '';
  position: absolute;
  top: 0; left: -60%;
  width: 35%;
  height: 100%;
  background: linear-gradient(120deg, transparent 0%, rgba(255,255,255,0.28) 50%, transparent 100%);
  transform: skewX(-15deg);
  animation: bookShimmer 4s infinite ease-in-out;
  pointer-events: none;
}

/* Stagger shimmer per nth-child */
.bk:nth-child(2)::after  { animation-delay: 0.4s; }
.bk:nth-child(3)::after  { animation-delay: 0.8s; }
.bk:nth-child(4)::after  { animation-delay: 1.2s; }
.bk:nth-child(5)::after  { animation-delay: 1.6s; }
.bk:nth-child(6)::after  { animation-delay: 2.0s; }
.bk:nth-child(7)::after  { animation-delay: 2.4s; }
.bk:nth-child(8)::after  { animation-delay: 2.8s; }

@keyframes bookShimmer {
  0%   { left: -60%; opacity: 0; }
  10%  { opacity: 1; }
  40%  { left: 130%; opacity: 0; }
  100% { left: 130%; opacity: 0; }
}

.bk:hover {
  transform: translateY(-22px) scale(1.08) !important;
  box-shadow: 0 18px 32px rgba(0,0,0,0.65);
  z-index: 50;
}

/* Book title text - vertical */
.bk span {
  writing-mode: vertical-rl;
  text-orientation: mixed;
  transform: rotate(180deg);
  font-size: 10px;
  font-weight: 700;
  color: rgba(255,255,255,0.9);
  letter-spacing: 1px;
  text-transform: uppercase;
  overflow: hidden;
  max-height: 88%;
  text-overflow: ellipsis;
  white-space: nowrap;
  pointer-events: none;
  user-select: none;
  position: relative;
  z-index: 2;
}

/* Sizes */
.bk.tall   { width: 38px; height: 130px; }
.bk.medium { width: 34px; height: 100px; }
.bk.short  { width: 30px; height: 72px; }

/* Leaning */
.bk.lean-left  { transform-origin: bottom left;  transform: rotate(-14deg); }
.bk.lean-right { transform-origin: bottom right; transform: rotate(13deg); }
.bk.lean-left:hover  { transform: translateY(-18px) rotate(-14deg) scale(1.06); }
.bk.lean-right:hover { transform: translateY(-18px) rotate(13deg) scale(1.06); }

/* Colors */
.bk.teal    { background: linear-gradient(160deg, #1A7A6E, #0F9B8A); border-left: 4px solid #0A4F47; }
.bk.teal2   { background: linear-gradient(160deg, #2AABB0, #1A8A90); border-left: 4px solid #0F6870; }
.bk.orange  { background: linear-gradient(160deg, #E07B2A, #F09040); border-left: 4px solid #A0521A; }
.bk.ochre   { background: linear-gradient(160deg, #D48806, #F5B041); border-left: 4px solid #945F00; }
.bk.pink    { background: linear-gradient(160deg, #D4607A, #E8809A); border-left: 4px solid #9E3A52; }
.bk.pink2   { background: linear-gradient(160deg, #F0A0B8, #F8C0D0); border-left: 4px solid #C07090; }
.bk.yellow  { background: linear-gradient(160deg, #D4B800, #F0D020); border-left: 4px solid #A08800; }
.bk.green   { background: linear-gradient(160deg, #2D8C5C, #3AAA70); border-left: 4px solid #1A5C38; }
.bk.purple  { background: linear-gradient(160deg, #7B4FA0, #9B6FC0); border-left: 4px solid #4E2870; }
.bk.red     { background: linear-gradient(160deg, #C0392B, #E04030); border-left: 4px solid #7D1F15; }
.bk.blue    { background: linear-gradient(160deg, #2E6DB4, #4080CC); border-left: 4px solid #1A4070; }
.bk.beige   { background: linear-gradient(160deg, #D8CEB8, #EDE5D0); border-left: 4px solid #B0A080; }
.bk.beige span { color: rgba(60,40,20,0.8); }

/* Fluid Wavy SVG Divider */
.wave-separator {
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  width: 140px;
  height: 100%;
  z-index: 2;
  filter: drop-shadow(4px 0px 8px rgba(0,0,0,0.15));
}

/* Beautiful dynamic bookshelf */
.bookshelf-container {
  position: absolute;
  top: 60px;
  right: 60px;
  bottom: 60px;
  left: 160px;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  z-index: 3;
}

.shelf {
  position: relative;
  height: 200px;
  display: flex;
  align-items: flex-end;
  padding-bottom: 15px;
}

.shelf-wood {
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  height: 15px;
  background: linear-gradient(90deg, #533527, #784C38);
  border-radius: 4px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.5);
}

.books-container {
  display: flex;
  align-items: flex-end;
  gap: 8px;
  padding-left: 20px;
  width: 100%;
}

/* Custom CSS books */
.book {
  position: relative;
  width: 36px;
  border-radius: 4px 4px 1px 1px;
  cursor: pointer;
  transition: transform 0.3s cubic-bezier(0.175, 0.885, 0.32, 1.275), box-shadow 0.3s;
  box-shadow: -3px 0 8px rgba(0, 0, 0, 0.3);
  display: flex;
  justify-content: center;
}

.book:hover {
  transform: translateY(-15px) scale(1.05) !important;
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.5);
  z-index: 100 !important;
}

/* Book leaning style */
.book.leaning {
  transform-origin: bottom left;
  z-index: 5;
}

/* Book Sizes */
.book.tall { height: 150px; width: 38px; }
.book.medium { height: 125px; width: 34px; }
.book.short { height: 100px; width: 30px; }

/* Book Colors - curated to match the cozy bookshelf palette in the image */
.book.teal { background: linear-gradient(135deg, #19524F, #277D78); border-left: 5px solid #0F3331; }
.book.red { background: linear-gradient(135deg, #9C3328, #BF4E41); border-left: 5px solid #662019; }
.book.ochre { background: linear-gradient(135deg, #D48806, #F5B041); border-left: 5px solid #945F00; }
.book.terracotta { background: linear-gradient(135deg, #B25A38, #CF7C5C); border-left: 5px solid #7E3E25; }
.book.beige { background: linear-gradient(135deg, #E6E2D3, #F2EFE6); border-left: 5px solid #B8B3A0; }

/* Book spines details */
.book-spine {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  height: 100%;
  padding: 12px 2px;
  box-sizing: border-box;
}

.book-title {
  writing-mode: vertical-rl;
  text-orientation: mixed;
  transform: rotate(180deg);
  font-size: 10px;
  font-weight: 800;
  color: rgba(255, 255, 255, 0.85);
  letter-spacing: 1.5px;
  text-transform: uppercase;
  max-height: 90px;
  overflow: hidden;
}

.book.beige .book-title {
  color: #434D4B;
}

.spine-lines {
  width: 80%;
  height: 10px;
  border-top: 2px solid rgba(255, 255, 255, 0.35);
  border-bottom: 2px solid rgba(255, 255, 255, 0.35);
}

.book.beige .spine-lines {
  border-color: rgba(0, 0, 0, 0.15);
}

/* Features Section */
.features-section {
  background-color: #F8F9FA;
  position: relative;
  overflow: hidden;
  z-index: 1;
}

/* Mesh gradient orbs */
.features-section::before,
.features-section::after {
  content: '';
  position: absolute;
  border-radius: 50%;
  filter: blur(100px);
  z-index: -1;
  animation: floatOrb 15s infinite alternate ease-in-out;
}

.features-section::before {
  top: -10%;
  left: -10%;
  width: 600px;
  height: 600px;
  background: rgba(207, 124, 7, 0.15); /* Gold */
}

.features-section::after {
  bottom: -10%;
  right: -10%;
  width: 700px;
  height: 700px;
  background: rgba(30, 86, 82, 0.15); /* Emerald */
  animation-duration: 20s;
}

@keyframes floatOrb {
  0% { transform: translate(0, 0) scale(1); }
  50% { transform: translate(50px, 100px) scale(1.1); }
  100% { transform: translate(-50px, -50px) scale(0.9); }
}

.section-title {
  font-size: 36px;
  font-weight: 800;
  color: #114B44;
  letter-spacing: 0.5px;
}

/* VinAI Style Feature Sections */
.showcase-section {
  max-width: 1200px;
  margin: 0 auto;
  padding: 40px 0;
}

.feature-row {
  display: flex;
  align-items: center;
  gap: 60px;
  margin-bottom: 100px;
}

.feature-row:last-child {
  margin-bottom: 0;
}

.feature-row.reverse {
  flex-direction: row-reverse;
}

.feature-visual {
  flex: 1.1;
  display: flex;
  justify-content: center;
  align-items: center;
}

.visual-frame {
  width: 100%;
  border-radius: 20px;
  overflow: hidden;
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.08), inset 0 1px 0 rgba(255,255,255,0.6);
  background: rgba(255, 255, 255, 0.6);
  backdrop-filter: blur(20px);
  -webkit-backdrop-filter: blur(20px);
  border: 1px solid rgba(255, 255, 255, 0.8);
  transition: transform 0.5s cubic-bezier(0.165, 0.84, 0.44, 1), box-shadow 0.5s ease;
}

.visual-frame:hover {
  transform: translateY(-10px);
  box-shadow: 0 30px 60px rgba(0, 0, 0, 0.12), inset 0 1px 0 rgba(255,255,255,0.8);
}

.visual-frame.dark {
  background: rgba(17, 75, 68, 0.85);
  border: 1px solid rgba(255, 255, 255, 0.1);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15), inset 0 1px 0 rgba(255,255,255,0.1);
}

.feature-text {
  flex: 1;
}

.feature-num {
  font-size: 72px;
  font-weight: 900;
  color: #CF7C07; /* Mix-match with gold */
  font-family: 'Outfit', sans-serif;
  line-height: 1;
  margin-bottom: 16px;
  display: block;
}

.feature-title {
  font-size: 32px;
  font-weight: 700;
  color: #114B44;
  margin-bottom: 24px;
  line-height: 1.3;
}

.feature-desc {
  font-size: 16px;
  color: #5C6765;
  line-height: 1.8;
  margin-bottom: 30px;
}

.feature-link {
  color: #CF7C07; 
  font-size: 15px;
  font-weight: 600;
  text-decoration: none;
  transition: opacity 0.3s;
}

.feature-link:hover {
  opacity: 0.8;
}

/* Specific Visuals reused from FeaturesPage */
.sim-card { background: linear-gradient(135deg, #114B44 0%, #1E5652 100%); color: white; padding: 30px; position: relative; min-height: 300px; }
.card-chip { width: 40px; height: 30px; background: linear-gradient(135deg, #F5B041, #CF7C07); border-radius: 6px; margin-bottom: 40px; }
.card-logo { position: absolute; top: 30px; right: 30px; font-weight: 800; font-size: 14px; }
.sim-qr { width: 100px; height: 100px; background: white; padding: 10px; border-radius: 8px; margin-right: 20px; }
.qr-svg { width: 100%; height: 100%; }
.sim-info { display: flex; flex-direction: column; justify-content: center; }
.info-lbl { font-size: 10px; color: #BBE1DC; font-weight: 700; margin-bottom: 4px; }
.info-val { font-size: 16px; font-weight: 800; margin-bottom: 16px; }
.card-body { display: flex; align-items: center; }

.search-bar-sim { display: flex; align-items: center; background: #FAF9F6; margin: 30px; padding: 12px 20px; border-radius: 30px; box-shadow: 0 4px 15px rgba(0,0,0,0.05); }
.search-bar-sim input { border: none; outline: none; flex: 1; font-weight: 600; font-size: 15px; background: transparent; }
.search-pill { background: #1E5652; color: white; padding: 6px 16px; border-radius: 20px; font-size: 12px; font-weight: 600; }
.search-results-sim { padding: 0 30px 30px; }
.result-card-sim { display: flex; gap: 20px; align-items: center; margin-bottom: 20px; background: #FAF9F6; padding: 16px; border-radius: 12px; }
.book-cover-mini { width: 50px; height: 70px; background: #1E5652; color: white; display: flex; align-items: center; justify-content: center; font-weight: bold; border-radius: 6px; }
.book-cover-mini.gold { background: #CF7C07; }
.result-title { font-weight: 700; color: #114B44; margin-bottom: 4px; }
.result-author { font-size: 12px; color: #7A8583; margin-bottom: 8px; }
.result-tag { font-size: 10px; font-weight: 700; padding: 4px 8px; border-radius: 4px; display: inline-block; }
.result-tag.available { background: #EBF3E8; color: #1E5652; }
.result-tag.borrowing { background: #FDF2E2; color: #CF7C07; }

.arch-node { padding: 12px 24px; border-radius: 8px; font-weight: 600; text-align: center; }
.arch-node.client { background: rgba(255,255,255,0.1); color: white; border: 1px dashed rgba(255,255,255,0.4); margin: 30px; }
.arch-arrow { text-align: center; color: #CF7C07; font-size: 20px; margin: 10px 0; }
.arch-node.gateway { background: #CF7C07; color: white; margin: 0 30px; }
.arch-services { display: flex; gap: 15px; margin: 30px; }
.arch-node.service { flex: 1; background: #1E5652; color: white; font-size: 13px; display: flex; align-items: center; justify-content: center; gap: 8px; padding: 16px; }
.arch-node.service.orange { background: #8B4513; }
.dot { width: 8px; height: 8px; border-radius: 50%; }
.dot.green { background: #2ECC71; box-shadow: 0 0 8px #2ECC71; }
.dot.orange { background: #E67E22; box-shadow: 0 0 8px #E67E22; }

/* Statistics Section */
.stats-section {
  background: linear-gradient(135deg, #1E3A24, #2D5C3A, #3A8A52);
  overflow: hidden;
}

/* Marquee ticker */
.marquee-wrap {
  padding: 1.4rem 0;
  background: #ECF6EE;
  border-top: 1px solid #D4EAD9;
  border-bottom: 1px solid #D4EAD9;
  overflow: hidden;
}
.marquee-track {
  display: flex;
  width: max-content;
  animation: marquee 22s linear infinite;
}
.marquee-track:hover { animation-play-state: paused; }
@keyframes marquee {
  from { transform: translateX(0); }
  to   { transform: translateX(-50%); }
}
.marquee-item {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 0 2.5rem;
  white-space: nowrap;
  font-size: .875rem;
  font-weight: 500;
  color: #4A7A55;
}

/* Stats grid */
.stats-in {
  max-width: 1180px;
  margin: 0 auto;
  padding: 3.5rem 2rem;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
}
.scard {
  text-align: center;
  padding: 1.5rem 1rem;
  border-right: 1px solid rgba(255,255,255,0.1);
  transition: transform .3s;
  cursor: default;
}
.scard:last-child { border-right: none; }
.scard:hover { transform: translateY(-6px); }
.sicon {
  font-size: 1.7rem;
  margin-bottom: .5rem;
}
.snum {
  font-family: 'Cormorant Garamond', 'Georgia', serif;
  font-size: 3rem;
  font-weight: 700;
  color: #fff;
  line-height: 1;
}
.snum sup {
  font-size: 1.5rem;
  color: #8BD4A0;
  vertical-align: super;
}
.slbl {
  font-size: .82rem;
  color: rgba(255,255,255,0.6);
  margin-top: .3rem;
}

/* Testimonials Section */
.testimonials-section {
  background-color: #F4F3ED; /* Slightly darker cream for depth */
}

.carousel-item-wrapper {
  padding: 30px 20px; /* Space for the shadow to render without clipping */
}

.testimonial-slide {
  background-color: white;
  border-radius: 24px;
  box-shadow: 0 15px 35px rgba(0,0,0,0.06);
  transition: transform 0.4s, box-shadow 0.4s;
}

.testimonial-slide:hover {
  transform: translateY(-5px);
  box-shadow: 0 20px 45px rgba(0,0,0,0.08);
}

.testimonial-text {
  font-size: 18px;
  font-style: italic;
  color: #5C6765;
  line-height: 1.9;
}

.testimonial-author {
  font-size: 16px;
  font-weight: 800;
  color: #1E5652;
}

/* Ant design Carousel customization */
:deep(.ant-carousel .slick-dots li button) {
  background: #1E5652 !important;
}
:deep(.ant-carousel .slick-dots li.slick-active button) {
  background: #CF7C07 !important;
}

/* Footer Section */
.footer {
  background-color: #171B1A;
  padding: 32px 60px 16px; /* reduced height */
  color: white;
}

.footer-top {
  display: flex;
  justify-content: space-between;
  gap: 60px;
}

.footer-brand-col { flex: 1.5; }
.footer-links-col { flex: 1; }
.footer-contact-col { flex: 1.5; }

.footer-bottom {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.footer-title {
  font-size: 18px;
  font-weight: 700;
  color: #FAF9F6;
  letter-spacing: 1px;
  text-transform: uppercase;
}

.footer-desc {
  font-size: 16px;
  line-height: 1.6;
}

.footer-links {
  list-style: none;
  padding: 0;
}

.footer-links li {
  margin-bottom: 20px;
}

.footer-links a {
  color: #9AA6A4;
  text-decoration: none;
  font-size: 16px;
  transition: color 0.3s;
  padding-bottom: 4px;
}

.footer-links a:hover {
  color: #CF7C07;
}

.footer-contact-item {
  font-size: 16px;
  display: flex;
  align-items: center;
  margin-bottom: 20px !important;
}

@media (max-width: 959px) {
  .footer-top {
    flex-direction: column;
    gap: 30px;
  }

  .hero-section {
    flex-direction: column;
  }
  .hero-left {
    padding: 60px 40px;
    align-items: center;
    text-align: center;
  }
  .hero-title {
    font-size: 54px;
  }
  .hero-description {
    max-width: 100%;
  }
  .hero-right {
    height: 500px;
  }
  .wave-separator {
    transform: rotate(90deg);
    top: -70px;
    left: 0;
    width: 100%;
    height: 140px;
  }
  .bookshelf-container {
    left: 40px;
    top: 80px;
    right: 40px;
    bottom: 40px;
  }
}

/* Scroll Reveal Animations */
.scroll-reveal {
  opacity: 0;
  transform: translateY(50px);
  transition: opacity 0.7s cubic-bezier(0.16, 1, 0.3, 1),
              transform 0.7s cubic-bezier(0.16, 1, 0.3, 1);
}
.scroll-reveal.slide-from-right { transform: translateX(60px); }
.scroll-reveal.slide-from-left { transform: translateX(-60px); }
.scroll-reveal.revealed { opacity: 1; transform: translateY(0) translateX(0); }
.scroll-reveal.delay-1 { transition-delay: 0.1s; }
.scroll-reveal.delay-2 { transition-delay: 0.22s; }
.scroll-reveal.delay-3 { transition-delay: 0.34s; }

/* Reveal delay aliases for how/news sections */
.reveal-d1 { transition-delay: 0.08s; }
.reveal-d2 { transition-delay: 0.18s; }
.reveal-d3 { transition-delay: 0.28s; }
.reveal-d4 { transition-delay: 0.38s; }

/* Step number pulse on hover */
.step-num {
  position: relative;
}
.step-num::before {
  content: '';
  position: absolute;
  inset: -6px;
  border-radius: 50%;
  background: rgba(58,138,82,.2);
  opacity: 0;
  transform: scale(0.8);
  transition: opacity .3s, transform .3s;
}
.step:hover .step-num::before {
  opacity: 1;
  transform: scale(1);
}

/* News card image zoom on hover */
.nc-img {
  transition: transform .4s cubic-bezier(.22,1,.36,1);
  overflow: hidden;
}
.nc:hover .nc-img {
  transform: scale(1.04);
}

/* Scard glow on hover */
.scard::after {
  content: '';
  position: absolute;
  inset: 0;
  background: radial-gradient(circle at center, rgba(107,184,124,.12) 0%, transparent 70%);
  opacity: 0;
  transition: opacity .3s;
  pointer-events: none;
}
.scard { position: relative; }
.scard:hover::after { opacity: 1; }

/* ── Shared section helpers ── */
.sec-center { text-align: center; }
.sec-label {
  display: inline-block;
  background: #ECF6EE;
  color: #3A8A52;
  padding: .28rem .85rem;
  border-radius: 50px;
  font-size: .75rem;
  font-weight: 700;
  letter-spacing: .06em;
  text-transform: uppercase;
  margin-bottom: .9rem;
}
.sec-h {
  font-family: 'Cormorant Garamond', 'Georgia', serif;
  font-size: clamp(1.9rem, 3vw, 2.8rem);
  font-weight: 700;
  color: #1E2E1F;
  line-height: 1.25;
  margin-bottom: .9rem;
}
.sec-h .hl { color: #3A8A52; }
.sec-p { color: #6A7B6B; font-size: 1rem; line-height: 1.75; max-width: 500px; margin: 0 auto; }

/* ── How it works ── */
.how-bg { background: linear-gradient(180deg, #F4FAF6 0%, #F9F6F1 100%); }
.how-sec {
  max-width: 1180px;
  margin: 0 auto;
  padding: 3rem 2rem;
}
.steps-row {
  display: flex;
  align-items: flex-start;
  justify-content: center;
  gap: 0;
  margin-top: 3.5rem;
}
.step {
  flex: 1;
  max-width: 210px;
  text-align: center;
}
.step-num {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  margin: 0 auto 1rem;
  background: linear-gradient(135deg, #3A8A52, #6BB87C);
  color: #fff;
  display: flex;
  align-items: center;
  justify-content: center;
  font-family: 'Cormorant Garamond', 'Georgia', serif;
  font-size: 1.5rem;
  font-weight: 700;
  box-shadow: 0 8px 24px rgba(58,138,82,.28);
  transition: transform .3s, box-shadow .3s;
}
.step:hover .step-num { transform: scale(1.12); box-shadow: 0 12px 32px rgba(58,138,82,.38); }
.step-line {
  flex: 1;
  height: 2px;
  margin-top: 30px;
  background: linear-gradient(90deg, #B3DAC0, #8BD4A0);
  position: relative;
  max-width: 100px;
}
.step-arrow {
  position: absolute;
  right: -6px;
  top: -11px;
  color: #6BB87C;
  font-size: 1.4rem;
}
.st-t { font-size: .9rem; font-weight: 700; color: #1E2E1F; margin-bottom: .35rem; }
.st-d { font-size: .8rem; color: #6A7B6B; line-height: 1.55; padding: 0 .5rem; }

/* ── News ── */
.news-bg { background: #F9F6F1; }
.ng {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 1.4rem;
  margin-top: 3rem;
}
.nc {
  background: #fff;
  border-radius: 18px;
  overflow: hidden;
  border: 1px solid #EAE6E0;
  cursor: pointer;
  transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .35s;
}
.nc:hover { transform: translateY(-6px); box-shadow: 0 16px 40px rgba(45,95,58,.11); }
.nc-img {
  height: 145px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 3.5rem;
}
.nc-body { padding: 1.15rem 1.3rem; }
.ntag {
  display: inline-block;
  font-size: .7rem;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: .05em;
  padding: .22rem .7rem;
  border-radius: 50px;
  margin-bottom: .65rem;
}
.tev { background: #FFF6E0; color: #9B7B00; }
.tbk { background: #ECF6EE; color: #2D7A42; }
.tno { background: #EFF4FF; color: #3355CC; }
.nt { font-size: .92rem; font-weight: 600; color: #1E2E1F; line-height: 1.45; margin-bottom: .5rem; }
.nd { font-size: .78rem; color: #8A9E8B; }

/* ── Footer mới ── */
.footer {
  background: linear-gradient(180deg, #172B1C 0%, #1E3A24 60%, #152517 100%);
  position: relative;
  overflow: hidden;
}
.footer::before {
  content: '';
  position: absolute;
  top: -200px; right: -200px;
  width: 500px; height: 500px;
  border-radius: 50%;
  background: radial-gradient(circle, rgba(58,138,82,.12) 0%, transparent 70%);
  pointer-events: none;
}
.footer-top {
  width: 100%;
  padding: 4rem 5% 2rem;
  display: grid;
  grid-template-columns: 1.6fr 1fr 1fr 1.4fr;
  gap: 4rem;
  position: relative;
  z-index: 1;
  box-sizing: border-box;
}
.ft-logo { display: flex; align-items: center; gap: 10px; margin-bottom: .9rem; }
.logo-box-ft {
  width: 38px; height: 38px;
  border-radius: 12px;
  background: linear-gradient(135deg, #3A8A52, #6BB87C);
  display: flex; align-items: center; justify-content: center;
  font-size: 1.1rem;
  box-shadow: 0 4px 12px rgba(58,138,82,.3);
}
.ft-logo-name {
  font-family: 'Cormorant Garamond', 'Georgia', serif;
  font-size: 1.45rem;
  font-weight: 700;
  color: #E8F4EB;
  letter-spacing: -.01em;
}
.ft-logo-name em { color: #6BB87C; font-style: normal; }
.ft-desc { font-size: 1rem; line-height: 1.75; color: rgba(255,255,255,.6); margin-top: .9rem; }
.ft-social { display: flex; gap: .6rem; margin-top: 1.3rem; }
.soc {
  width: 36px; height: 36px;
  border-radius: 9px;
  background: rgba(255,255,255,.06);
  border: 1px solid rgba(255,255,255,.1);
  display: flex; align-items: center; justify-content: center;
  font-size: .95rem;
  cursor: pointer;
  transition: all .25s;
  color: rgba(255,255,255,.5);
}
.soc:hover { background: rgba(58,138,82,.4); border-color: rgba(107,184,124,.4); transform: translateY(-2px); }
.ft-col-h {
  font-size: 1rem;
  font-weight: 700;
  letter-spacing: .08em;
  text-transform: uppercase;
  color: rgba(255,255,255,.95);
  margin-bottom: 1.2rem;
}
.ft-links { list-style: none; padding: 0; }
.ft-links li { margin-bottom: .85rem; }
.ft-links a {
  font-size: 1rem;
  color: rgba(255,255,255,.6);
  text-decoration: none;
  cursor: pointer;
  transition: color .2s;
  display: flex;
  align-items: center;
  gap: 6px;
}
.ft-links a:hover { color: #8BD4A0; }
.nl-label { font-size: .95rem; color: rgba(255,255,255,.7); margin-bottom: .75rem; line-height: 1.6; }
.newsletter-box {
  background: rgba(58,138,82,.1);
  border: 1px solid rgba(107,184,124,.2);
  border-radius: 14px;
  padding: 1.1rem;
}
.nl-form { display: flex; gap: .5rem; }
.nl-input {
  flex: 1;
  padding: .6rem .85rem;
  background: rgba(255,255,255,.06);
  border: 1px solid rgba(255,255,255,.12);
  border-radius: 8px;
  color: #fff;
  font-family: inherit;
  font-size: .82rem;
  outline: none;
}
.nl-input::placeholder { color: rgba(255,255,255,.3); }
.nl-btn {
  padding: .6rem .9rem;
  background: linear-gradient(135deg, #3A8A52, #5BAD70);
  border: none;
  border-radius: 8px;
  cursor: pointer;
  color: #fff;
  font-size: .8rem;
  font-weight: 600;
  white-space: nowrap;
  transition: transform .2s;
}
.nl-btn:hover { transform: scale(1.04); }
.footer-divider { width: 100%; padding: 0 6%; box-sizing: border-box; }
.fdiv { border: none; border-top: 1px solid rgba(255,255,255,.07); margin: 0; }
.footer-bot {
  width: 100%;
  padding: 1.4rem 6%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: relative;
  z-index: 1;
  box-sizing: border-box;
}
.fc-text { font-size: .95rem; color: rgba(255,255,255,.5); }
.fc-links { display: flex; gap: 1.5rem; }
.fc-links a {
  font-size: .92rem;
  color: rgba(255,255,255,.5);
  cursor: pointer;
  text-decoration: none;
  transition: color .2s;
}
.fc-links a:hover { color: #6BB87C; }
</style>
