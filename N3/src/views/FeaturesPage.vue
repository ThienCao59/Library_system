<template>
  <div class="about-page">

    <!-- Navbar -->
    <header class="navbar">
      <div class="logo" @click="$router.push('/')">
        <img v-if="lib.logoUrl" :src="lib.logoUrl" class="logo-img" alt="logo" />
        <v-icon v-else icon="mdi-library" class="mr-2 logo-icon"></v-icon>
        <span class="logo-text">{{ lib.name }}</span>
      </div>
      <nav class="nav-links">
        <a @click.prevent="$router.push('/')">{{ t('nav.home') }}</a>
        <a class="active" @click.prevent="$router.push('/features')">{{ t('nav.about') }}</a>
        <a @click.prevent="$router.push('/statistics')">{{ t('nav.guide') }}</a>
        <a @click.prevent="$router.push('/reviews')">{{ t('nav.contact') }}</a>
      </nav>
      <div class="nav-auth">
        <button class="btn-login" @click="$router.push('/login')">{{ t('nav.login') }}</button>
        <button class="btn-register" @click="$router.push('/register')">{{ t('nav.register') }}</button>
      </div>
    </header>

    <!-- Hero -->
    <div class="page-hero">
      <div class="hero-blob b1"></div>
      <div class="hero-blob b2"></div>
      <div class="hero-blob b3"></div>
      <!-- Floating particles -->
      <div class="particle" v-for="n in 12" :key="n" :class="`p${n}`"></div>
      <div class="page-hero-inner">
        <div class="ph-tag scroll-reveal">{{ t('auto.txt_1437f79c') }}</div>
        <h1 class="ph-h scroll-reveal reveal-d1">
          {{ t('auto.txt_3b318cbe') }} <span class="hl">{{ lib.name }}</span>
        </h1>
        <p class="ph-p scroll-reveal reveal-d2">
          {{ t('auto.txt_0e5b1034') }}<br/>
          {{ t('auto.txt_25751bcd') }}
        </p>
        <!-- Hero mini stats -->
        <div class="hero-stats scroll-reveal reveal-d3">
          <div class="hstat" v-for="s in heroStats" :key="s.l">
            <div class="hstat-n">{{ s.n }}</div>
            <div class="hstat-l">{{ s.l }}</div>
          </div>
        </div>
        <!-- Scroll hint -->
        <div class="scroll-hint scroll-reveal reveal-d4">
          <span>{{ t('auto.txt_9045a161') }}</span>
          <div class="scroll-arrow">↓</div>
        </div>
      </div>
    </div>

    <!-- Mission cards -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_cc73abff') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_5c458e9e') }} <span class="hl">{{ t('auto.txt_48411906') }}</span></h2>
      <div class="ig">
        <div v-for="(c, i) in coreValues" :key="c.t"
          class="ic scroll-reveal"
          :class="`reveal-d${i+1}`"
          @mouseenter="hoveredCard = i"
          @mouseleave="hoveredCard = -1">
          <div class="ic-ico">{{ c.ico }}</div>
          <div class="ic-t">{{ c.t }}</div>
          <p class="ic-p">{{ c.p }}</p>
          <div class="ic-glow" :class="{ active: hoveredCard === i }"></div>
        </div>
      </div>
    </div>

    <!-- Why SmartLib -->
    <div class="why-section">
      <div class="why-inner">
        <div class="sec-label scroll-reveal">{{ t('auto.txt_e2df5844') }}</div>
        <h2 class="sec-h scroll-reveal reveal-d1">{{ lib.name }} <span>{{ t('features.why.diff') }}</span> <span class="hl">{{ t('auto.txt_2e148974') }}</span></h2>
        <div class="why-grid">
          <div v-for="(w, i) in whyUs" :key="w.t"
            class="why-card scroll-reveal"
            :class="`reveal-d${(i % 3) + 1}`">
            <div class="why-ico-wrap">
              <div class="why-ico">{{ w.ico }}</div>
              <div class="why-ring"></div>
            </div>
            <div class="why-t">{{ w.t }}</div>
            <div class="why-d">{{ w.d }}</div>
          </div>
        </div>
      </div>
    </div>

    <!-- Features tabbed -->
    <div class="section-wrap bg-light">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_c4be013b') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_2e542d2c') }} <span class="hl">{{ t('auto.txt_e1438e83') }}</span></h2>
      <div class="features-grid">
        <div v-for="(f, i) in featureCards" :key="f.t"
          class="fc scroll-reveal"
          :class="`reveal-d${(i % 3) + 1}`">
          <div class="fi" :class="`fi${i+1}`">{{ f.ico }}</div>
          <div class="fn">{{ f.t }}</div>
          <div class="fd">{{ f.d }}</div>
        </div>
      </div>
    </div>

    <!-- Timeline -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_ab967c92') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_3061f544') }} <span class="hl">{{ t('auto.txt_5c886e25') }}</span></h2>
      <div class="tl">
        <div v-for="(t, i) in timeline" :key="t.yr"
          class="tli scroll-reveal"
          :class="`reveal-d${(i % 3) + 1}`">
          <div class="tl-dot"></div>
          <div class="tl-yr">{{ t.yr }}</div>
          <div class="tl-t">{{ t.t }}</div>
          <p class="tl-d">{{ t.d }}</p>
        </div>
      </div>
    </div>

    <!-- Team -->
    <div class="section-wrap bg-light">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_e189c453') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_8ddd22da') }} <span class="hl">{{ t('features.team.building') }} {{ lib.name }}</span></h2>
      <div class="team-grid">
        <div v-for="(m, i) in team" :key="m.name"
          class="team-card scroll-reveal"
          :class="`reveal-d${(i % 4) + 1}`">
          <div class="team-avatar">{{ m.avatar }}</div>
          <div class="team-name">{{ m.name }}</div>
          <div class="team-role">{{ m.role }}</div>
          <div class="team-tags">
            <span v-for="tag in m.tags" :key="tag" class="team-tag">{{ tag }}</span>
          </div>
        </div>
      </div>
    </div>

    <!-- Tech stack -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_1bc7300d') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_f9e30f99') }} <span class="hl">{{ t('auto.txt_be5aa24e') }}</span></h2>
      <div class="tech-grid">
        <div v-for="(t, i) in techStack" :key="t.name"
          class="tech-item scroll-reveal"
          :class="`reveal-d${(i % 6) + 1}`">
          <div class="tech-ico">{{ t.ico }}</div>
          <div class="tech-name">{{ t.name }}</div>
          <div class="tech-cat">{{ t.cat }}</div>
        </div>
      </div>
    </div>

    <!-- CTA -->
    <div class="cta-section">
      <div class="cta-blob"></div>
      <div class="cta-inner scroll-reveal">
        <h2 class="cta-h">{{ t('auto.txt_5607de2d') }}</h2>
        <p class="cta-p">{{ t('auto.txt_c84f1db8') }}</p>
        <div class="cta-btns">
          <button class="btn-cta-p" @click="$router.push('/register')">{{ t('auto.txt_8b6b7866') }}</button>
          <button class="btn-cta-g" @click="$router.push('/login')">{{ t('auto.txt_ad3ff017') }}</button>
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
            <li><a>{{ t('auto.txt_7557a4be') }}</a></li>
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
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { useI18nStore } from '@/stores/i18nStore'
import { useLibraryStore } from '@/stores/libraryStore'

const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)
const lib = useLibraryStore()

const toggleLocale = () => i18n.setLocale(i18n.locale === 'vi' ? 'en' : 'vi')

const hoveredCard = ref(-1)

const heroStats = computed(() => [
  { n: '12,450+', l: t('landing.stats.books_cap') },
  { n: '3,200+', l: t('landing.stats.members_cap') },
  { n: '28,900+', l: t('landing.stats.borrows_cap') },
  { n: '4.9★', l: t('features.stat.rating') },
])

const whyUs = computed(() => [
  { ico: '⚡', t: t('features.why.card_3s'), d: t('features.why.card_3s_d') },
  { ico: '🔍', t: t('features.why.search'), d: t('features.why.search_d') },
  { ico: '📱', t: t('features.why.anywhere'), d: t('features.why.anywhere_d') },
  { ico: '🔐', t: t('features.why.security'), d: t('features.why.security_d') },
  { ico: '🤖', t: t('features.why.auto'), d: t('features.why.auto_d') },
  { ico: '📊', t: t('features.why.report'), d: t('features.why.report_d') },
])

const coreValues = computed(() => [
  { ico: '🎯', t: t('features.core.goal'), p: t('features.core.goal_d') },
  { ico: '🌟', t: t('features.core.mission'), p: t('features.core.mission_d') },
  { ico: '💡', t: t('features.core.vision'), p: t('features.core.vision_d') },
  { ico: '🤝', t: t('features.core.value'), p: t('features.core.value_d') },
])

const featureCards = computed(() => [
  { ico: '🔍', t: t('features.cards.search'), d: t('features.cards.search_d') },
  { ico: '📖', t: t('features.cards.borrow'), d: t('features.cards.borrow_d') },
  { ico: '🪪', t: t('features.cards.digital_card'), d: t('features.cards.digital_card_d') },
  { ico: '🔔', t: t('features.cards.notify'), d: t('features.cards.notify_d') },
  { ico: '📊', t: t('features.cards.report'), d: t('features.cards.report_d') },
  { ico: '⚙️', t: t('features.cards.admin'), d: t('features.cards.admin_d') },
  { ico: '🔐', t: t('features.cards.jwt'), d: t('features.cards.jwt_d') },
  { ico: '🚀', t: t('features.cards.micro'), d: t('features.cards.micro_d') },
  { ico: '📱', t: t('features.cards.multiplatform'), d: t('features.cards.multiplatform_d') },
])

const timeline = computed(() => [
  { yr: '2020', t: t('features.timeline.2020_t'), d: t('features.timeline.2020_d') },
  { yr: '2021', t: t('features.timeline.2021_t'), d: t('features.timeline.2021_d') },
  { yr: '2022', t: t('features.timeline.2022_t'), d: t('features.timeline.2022_d') },
  { yr: '2023', t: t('features.timeline.2023_t'), d: t('features.timeline.2023_d') },
  { yr: '2024', t: t('features.timeline.2024_t'), d: t('features.timeline.2024_d') },
  { yr: '2025', t: t('features.timeline.2025_t'), d: t('features.timeline.2025_d') },
])

const team = [
  { avatar: '👩‍💻', name: 'Lê Thùy Dung', role: 'Frontend Developer', tags: ['Vue.js', 'TypeScript', 'CSS'] },
  { avatar: '👨‍💻', name: 'Cao Văn Thiện', role: 'Backend Developer', tags: ['ASP.NET', 'C#', 'SQL'] },
  { avatar: '👩‍🎨', name: 'Nguyễn Hương Giang', role: 'UI/UX Designer', tags: ['Figma', 'Design', 'UX'] },
  { avatar: '👨‍🔧', name: 'Vũ Đức Nguyễn Chuẩn', role: 'DevOps Engineer', tags: ['Docker', 'Nginx', 'CI/CD'] },
  { avatar: '👨‍💼', name: 'Tăng Tất Cương', role: 'Backend Developer', tags: ['API', 'RabbitMQ', 'JWT'] },
  { avatar: '👨‍🔬', name: 'Nguyễn Xuân Anh', role: 'Database Engineer', tags: ['SQL Server', 'EF Core', 'Query'] },
  { avatar: '👨‍🚀', name: 'Trịnh Việt Đức', role: 'Fullstack Developer', tags: ['Vue.js', 'ASP.NET', 'Docker'] },
]

const techStack = [
  { ico: '⚡', name: 'ASP.NET Core', cat: 'Backend' },
  { ico: '💚', name: 'Vue.js 3', cat: 'Frontend' },
  { ico: '🗄️', name: 'SQL Server', cat: 'Database' },
  { ico: '🐇', name: 'RabbitMQ', cat: 'Messaging' },
  { ico: '🐳', name: 'Docker', cat: 'DevOps' },
  { ico: '🔀', name: 'Ocelot Gateway', cat: 'API Gateway' },
  { ico: '🔐', name: 'JWT Auth', cat: 'Security' },
  { ico: '📦', name: 'Entity Framework', cat: 'ORM' },
  { ico: '🌐', name: 'Nginx', cat: 'Web Server' },
]

let observer: IntersectionObserver | null = null

onMounted(() => {
  observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) entry.target.classList.add('revealed')
      })
    },
    { threshold: 0.12 }
  )
  document.querySelectorAll('.scroll-reveal').forEach((el) => observer?.observe(el))
})

onUnmounted(() => observer?.disconnect())
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Cormorant+Garamond:wght@400;600;700&family=Be+Vietnam+Pro:wght@300;400;500;600;700;800&display=swap');

*, *::before, *::after { box-sizing: border-box; }

.about-page {
  font-family: 'Be Vietnam Pro', sans-serif;
  background: #F9F6F1;
  color: #2D3A2E;
  overflow-x: hidden;
  padding-top: 66px;
}

/* ── Navbar ── */
.navbar {
  position: fixed; top: 0; left: 0; right: 0; z-index: 999;
  background: rgba(249,246,241,.92);
  backdrop-filter: blur(16px);
  border-bottom: 1px solid rgba(139,189,151,.2);
  display: flex; align-items: center; justify-content: space-between;
  padding: 0 4%; height: 66px;
}
.logo { display: flex; align-items: center; cursor: pointer; }
.logo-icon { font-size: 28px; color: #1E5652 !important; }
.logo-img { width: 32px; height: 32px; border-radius: 6px; object-fit: contain; margin-right: 8px; }
.logo-text { font-size: 24px; font-weight: 800; color: #1E5652; letter-spacing: 1px; font-family: 'Be Vietnam Pro', sans-serif; }
.logo-name { font-family: 'Cormorant Garamond',serif; font-size: 1.4rem; font-weight: 700; color: #2D3A2E; }
.logo-name em { color: #3A8A52; font-style: normal; }
.nav-links { display: flex; gap: .25rem; }
.nav-links a {
  padding: .45rem .9rem; border-radius: 8px; cursor: pointer;
  font-size: 1rem; font-weight: 600; color: #5A6B5B;
  transition: color .2s, background .2s; text-decoration: none;
  position: relative; padding-bottom: 6px;
}
.nav-links a::after {
  content: ''; position: absolute; bottom: 0; left: .9rem; right: .9rem;
  height: 2px; background: #3A8A52; border-radius: 2px;
  transform: scaleX(0); transition: transform .3s ease;
}
.nav-links a:hover::after, .nav-links a.active::after { transform: scaleX(1); }
.nav-links a:hover, .nav-links a.active { color: #3A8A52; }
.btn-lang {
  padding: .45rem 1rem; border-radius: 50px; border: 1.5px solid rgba(58,138,82,.4); cursor: pointer;
  background: transparent; color: #3A8A52; font-weight: 700; font-size: .82rem;
  transition: background .2s, border-color .2s; margin-right: 4px;
}
.btn-lang:hover { background: #ECF6EE; border-color: #3A8A52; }
.btn-login {
  padding: .5rem 1.2rem; border-radius: 50px; border: none; cursor: pointer;
  background: transparent; color: #3A8A52; font-weight: 700; font-size: .9rem;
  transition: background .2s;
}
.btn-login:hover { background: #ECF6EE; }
.btn-register {
  padding: .5rem 1.35rem; border-radius: 50px; border: none; cursor: pointer;
  background: linear-gradient(135deg,#3A8A52,#5BAD70); color: #fff;
  font-weight: 700; font-size: .9rem;
  box-shadow: 0 3px 12px rgba(58,138,82,.3);
  transition: transform .2s, box-shadow .2s;
}
.btn-register:hover { transform: translateY(-2px); box-shadow: 0 6px 18px rgba(58,138,82,.4); }

/* ── Hero ── */
.page-hero {
  background: linear-gradient(135deg,#1E3A24,#2D5C3A,#3A8A52);
  padding: 5rem 2rem 4rem;
  text-align: center;
  position: relative; overflow: hidden;
  min-height: 480px;
  display: flex; align-items: center; justify-content: center;
}

/* Floating particles */
.particle {
  position: absolute;
  border-radius: 50%;
  background: rgba(139,212,160,.25);
  pointer-events: none;
  animation: floatParticle linear infinite;
}
.p1  { width:8px;  height:8px;  left:10%; top:20%; animation-duration:8s;  animation-delay:0s; }
.p2  { width:5px;  height:5px;  left:20%; top:70%; animation-duration:11s; animation-delay:1s; }
.p3  { width:10px; height:10px; left:35%; top:15%; animation-duration:9s;  animation-delay:2s; }
.p4  { width:6px;  height:6px;  left:50%; top:80%; animation-duration:13s; animation-delay:.5s; }
.p5  { width:8px;  height:8px;  left:65%; top:25%; animation-duration:10s; animation-delay:3s; }
.p6  { width:4px;  height:4px;  left:75%; top:60%; animation-duration:7s;  animation-delay:1.5s; }
.p7  { width:12px; height:12px; left:85%; top:35%; animation-duration:12s; animation-delay:2.5s; }
.p8  { width:5px;  height:5px;  left:90%; top:75%; animation-duration:9s;  animation-delay:4s; }
.p9  { width:7px;  height:7px;  left:5%;  top:50%; animation-duration:14s; animation-delay:.8s; }
.p10 { width:9px;  height:9px;  left:45%; top:45%; animation-duration:11s; animation-delay:3.5s; }
.p11 { width:4px;  height:4px;  left:28%; top:88%; animation-duration:8s;  animation-delay:2s; }
.p12 { width:6px;  height:6px;  left:70%; top:10%; animation-duration:10s; animation-delay:1s; }

@keyframes floatParticle {
  0%   { transform: translateY(0) translateX(0) scale(1); opacity: .6; }
  25%  { transform: translateY(-30px) translateX(15px) scale(1.1); opacity: 1; }
  50%  { transform: translateY(-15px) translateX(-10px) scale(.9); opacity: .7; }
  75%  { transform: translateY(-40px) translateX(8px) scale(1.05); opacity: .9; }
  100% { transform: translateY(0) translateX(0) scale(1); opacity: .6; }
}

.page-hero-inner { position: relative; z-index: 1; max-width: 700px; }
.ph-tag {
  display: inline-block; background: rgba(255,255,255,.15); color: rgba(255,255,255,.9);
  padding: .3rem .9rem; border-radius: 50px; font-size: .78rem; font-weight: 700;
  letter-spacing: .06em; text-transform: uppercase; margin-bottom: 1rem;
  border: 1px solid rgba(255,255,255,.2);
  animation: shimmerBadge 3s ease-in-out infinite;
}
@keyframes shimmerBadge {
  0%,100% { box-shadow: 0 0 0 rgba(139,212,160,0); }
  50% { box-shadow: 0 0 20px rgba(139,212,160,.4); }
}
.ph-h {
  font-family: 'Cormorant Garamond',serif;
  font-size: clamp(2.4rem,5vw,4rem); font-weight: 700; color: #fff; margin-bottom: .9rem;
  line-height: 1.2;
}
.ph-h .hl {
  color: #8BD4A0;
  position: relative;
  display: inline-block;
}
.ph-h .hl::after {
  content: '';
  position: absolute; left: 0; bottom: -4px; width: 100%; height: 2px;
  background: linear-gradient(90deg,#8BD4A0,transparent);
  animation: underlineGrow 2s ease-in-out infinite alternate;
}
@keyframes underlineGrow {
  from { width: 0; } to { width: 100%; }
}
.ph-p { color: rgba(255,255,255,.78); font-size: 1.05rem; line-height: 1.8; margin-bottom: 2.5rem; }

/* Hero stats */
.hero-stats {
  display: flex; justify-content: center; gap: 0;
  background: rgba(255,255,255,.08);
  border: 1px solid rgba(255,255,255,.15);
  border-radius: 16px; overflow: hidden;
  backdrop-filter: blur(10px);
  margin-bottom: 2rem;
}
.hstat {
  flex: 1; padding: 1.2rem 1rem; text-align: center;
  border-right: 1px solid rgba(255,255,255,.1);
  transition: background .3s;
}
.hstat:last-child { border-right: none; }
.hstat:hover { background: rgba(255,255,255,.1); }
.hstat-n {
  font-family: 'Cormorant Garamond',serif;
  font-size: 1.8rem; font-weight: 700; color: #fff; line-height: 1;
}
.hstat-l { font-size: .75rem; color: rgba(255,255,255,.6); margin-top: .25rem; }

/* Scroll hint */
.scroll-hint { display: flex; flex-direction: column; align-items: center; gap: .4rem; color: rgba(255,255,255,.6); font-size: 1rem; font-weight: 600; letter-spacing: .05em; }
.scroll-arrow {
  font-size: 1.2rem; color: rgba(255,255,255,.5);
  animation: bounceDown 1.5s ease-in-out infinite;
}
@keyframes bounceDown {
  0%,100% { transform: translateY(0); }
  50% { transform: translateY(8px); }
}

.hero-blob {
  position: absolute; border-radius: 50%; pointer-events: none; filter: blur(70px);
}
.b1 { width:500px; height:500px; top:-150px; right:-100px; background:rgba(107,184,124,.2); animation:blob1 12s ease-in-out infinite; }
.b2 { width:350px; height:350px; bottom:-100px; left:-80px; background:rgba(201,217,145,.18); animation:blob2 15s ease-in-out infinite; }
.b3 { width:250px; height:250px; top:30%; left:40%; background:rgba(255,220,130,.1); animation:blob1 18s ease-in-out infinite reverse; }
@keyframes blob1 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(30px,-20px) scale(1.05)} 66%{transform:translate(-20px,15px) scale(.97)} }
@keyframes blob2 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(-25px,20px) scale(1.03)} 66%{transform:translate(20px,-15px) scale(.98)} }

/* ── Why SmartLib ── */
.why-section {
  background: linear-gradient(180deg, #1E3A24 0%, #2D5C3A 50%, #1a4020 100%);
  padding: 4rem 4%;
}
.why-inner { max-width: 1200px; margin: 0 auto; }
.why-inner .sec-label { background: rgba(255,255,255,.15); color: rgba(255,255,255,.9); }
.why-inner .sec-h { color: #fff; }
.why-inner .sec-h .hl { color: #8BD4A0; }
.why-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 1.5rem;
  margin-top: 2rem;
}
.why-card {
  background: rgba(255,255,255,.07);
  border: 1px solid rgba(255,255,255,.12);
  border-radius: 20px;
  padding: 2rem 1.6rem;
  text-align: center;
  backdrop-filter: blur(10px);
  transition: transform .4s cubic-bezier(.22,1,.36,1), background .3s, border-color .3s, box-shadow .4s;
  cursor: default;
  position: relative;
  overflow: hidden;
}
.why-card::before {
  content: '';
  position: absolute; inset: 0; border-radius: 20px;
  background: linear-gradient(135deg, rgba(139,212,160,.1), transparent);
  opacity: 0; transition: opacity .4s;
}
.why-card:hover {
  transform: translateY(-10px);
  background: rgba(255,255,255,.12);
  border-color: rgba(139,212,160,.4);
  box-shadow: 0 20px 48px rgba(0,0,0,.3);
}
.why-card:hover::before { opacity: 1; }
.why-ico-wrap {
  position: relative;
  width: 64px; height: 64px;
  margin: 0 auto 1.2rem;
  display: flex; align-items: center; justify-content: center;
}
.why-ico {
  font-size: 2rem;
  position: relative; z-index: 1;
  transition: transform .3s;
}
.why-card:hover .why-ico { transform: scale(1.2) rotate(-8deg); }
.why-ring {
  position: absolute; inset: 0;
  border-radius: 50%;
  border: 2px solid rgba(139,212,160,.3);
  animation: ringPulse 2.5s ease-in-out infinite;
}
.why-card:nth-child(2) .why-ring { animation-delay: .4s; }
.why-card:nth-child(3) .why-ring { animation-delay: .8s; }
.why-card:nth-child(4) .why-ring { animation-delay: 1.2s; }
.why-card:nth-child(5) .why-ring { animation-delay: 1.6s; }
.why-card:nth-child(6) .why-ring { animation-delay: 2s; }
@keyframes ringPulse {
  0%,100% { transform: scale(1); opacity: .4; }
  50% { transform: scale(1.3); opacity: 0; }
}
.why-t { font-size: 1.05rem; font-weight: 700; color: #fff; margin-bottom: .6rem; }
.why-d { font-size: .88rem; color: rgba(255,255,255,.65); line-height: 1.7; }

/* ── Section commons ── */
.section-wrap { padding: 3.5rem 4%; }
.bg-white { background: #fff; }
.bg-light { background: #F9F6F1; }
.sec-label {
  display: inline-block; background: #ECF6EE; color: #3A8A52;
  padding: .28rem .85rem; border-radius: 50px; font-size: .75rem;
  font-weight: 700; letter-spacing: .06em; text-transform: uppercase; margin-bottom: .9rem;
}
.sec-h {
  font-family: 'Cormorant Garamond',serif;
  font-size: clamp(1.9rem,3vw,2.8rem); font-weight: 700; color: #1E2E1F;
  line-height: 1.25; margin-bottom: 2.5rem;
}
.sec-h .hl { color: #3A8A52; }

/* ── Core values grid ── */
.ig { display: grid; grid-template-columns: repeat(4,1fr); gap: 1.5rem; }
.ic {
  background: #F9F6F1; border-radius: 18px; padding: 2rem 1.6rem;
  border: 1px solid #EAE6E0; position: relative; overflow: hidden;
  transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .4s, border-color .3s;
  cursor: default;
}
.ic:hover { transform: translateY(-8px); box-shadow: 0 20px 48px rgba(45,95,58,.12); border-color: #C2DFC9; }
.ic-ico { font-size: 2.2rem; margin-bottom: 1rem; }
.ic-t { font-family: 'Cormorant Garamond',serif; font-size: 1.3rem; font-weight: 700; color: #1E2E1F; margin-bottom: .6rem; }
.ic-p { font-size: .9rem; color: #6A7B6B; line-height: 1.7; }
.ic-glow {
  position: absolute; inset: 0; border-radius: 18px;
  background: linear-gradient(135deg,rgba(58,138,82,.06),transparent);
  opacity: 0; transition: opacity .4s;
}
.ic-glow.active { opacity: 1; }

/* ── Stats strip ── */
.stats-strip { background: linear-gradient(135deg,#1E3A24,#2D5C3A,#3A8A52); padding: 4rem 6%; }
.stats-in { display: grid; grid-template-columns: repeat(4,1fr); }
.scard {
  text-align: center; padding: 1.5rem 1rem;
  border-right: 1px solid rgba(255,255,255,.1);
  transition: transform .3s; cursor: default; position: relative;
}
.scard:last-child { border-right: none; }
.scard:hover { transform: translateY(-6px); }
.sicon { font-size: 1.7rem; margin-bottom: .5rem; }
.snum { font-family: 'Cormorant Garamond',serif; font-size: 3rem; font-weight: 700; color: #fff; line-height: 1; }
.snum sup { font-size: 1.5rem; color: #8BD4A0; }
.slbl { font-size: .85rem; color: rgba(255,255,255,.6); margin-top: .3rem; }

/* ── Features grid ── */
.features-grid { display: grid; grid-template-columns: repeat(3,1fr); gap: 1.25rem; }
.fc {
  background: #fff; border-radius: 18px; padding: 1.75rem 1.6rem;
  border: 1px solid #EAE6E0; cursor: default; position: relative; overflow: hidden;
  transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .4s, border-color .3s;
}
.fc::after {
  content: ''; position: absolute; inset: 0; border-radius: 18px;
  background: linear-gradient(135deg,rgba(58,138,82,.06),transparent);
  opacity: 0; transition: opacity .4s;
}
.fc:hover { transform: translateY(-8px) scale(1.01); box-shadow: 0 20px 48px rgba(45,95,58,.12); border-color: #C2DFC9; }
.fc:hover::after { opacity: 1; }
.fi {
  width: 50px; height: 50px; border-radius: 13px;
  display: flex; align-items: center; justify-content: center;
  font-size: 1.4rem; margin-bottom: 1.2rem;
  transition: transform .3s;
}
.fc:hover .fi { transform: scale(1.1) rotate(-4deg); }
.fi1{background:#ECF6EE} .fi2{background:#FFF6E8} .fi3{background:#E8F0FF}
.fi4{background:#FFF0F0} .fi5{background:#F0FBF4} .fi6{background:#FDF0FF}
.fi7{background:#E8F8FF} .fi8{background:#FFF8E8} .fi9{background:#F0F0FF}
.fn { font-size: 1rem; font-weight: 700; color: #1E2E1F; margin-bottom: .5rem; }
.fd { font-size: .88rem; color: #6A7B6B; line-height: 1.65; }

/* ── Timeline ── */
.tl { position: relative; padding-left: 2.5rem; max-width: 700px; }
.tl::before {
  content: ''; position: absolute; left: .75rem; top: 0; bottom: 0;
  width: 2px; background: linear-gradient(180deg,#3A8A52,rgba(58,138,82,.1));
}
.tli { position: relative; margin-bottom: 2.5rem; }
.tl-dot {
  position: absolute; left: -1.85rem; top: 4px;
  width: 14px; height: 14px; border-radius: 50%;
  background: #3A8A52; border: 3px solid #F9F6F1;
  box-shadow: 0 0 0 3px rgba(58,138,82,.2);
  transition: transform .3s, box-shadow .3s;
}
.tli:hover .tl-dot { transform: scale(1.3); box-shadow: 0 0 0 5px rgba(58,138,82,.15); }
.tl-yr { font-size: .72rem; font-weight: 800; color: #3A8A52; text-transform: uppercase; letter-spacing: .06em; margin-bottom: .25rem; }
.tl-t { font-weight: 700; color: #1E2E1F; margin-bottom: .3rem; font-size: 1rem; }
.tl-d { font-size: .9rem; color: #6A7B6B; line-height: 1.65; }

/* ── Team ── */
.team-grid { display: grid; grid-template-columns: repeat(4,1fr); gap: 1.2rem; }
.team-card {
  background: #fff; border-radius: 18px; padding: 1.6rem 1.2rem;
  border: 1px solid #EAE6E0; text-align: center;
  transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .4s;
  position: relative; overflow: hidden;
}
.team-card::before {
  content: ''; position: absolute; top: 0; left: 0; right: 0; height: 3px;
  background: linear-gradient(90deg,#3A8A52,#6BB87C,#3A8A52);
  background-size: 200% 100%;
  transform: scaleX(0); transform-origin: left;
  transition: transform .4s ease;
}
.team-card:hover::before { transform: scaleX(1); }
.team-card:hover { transform: translateY(-8px); box-shadow: 0 20px 40px rgba(45,95,58,.12); }
.team-avatar { font-size: 2.8rem; margin-bottom: .7rem; transition: transform .3s; }
.team-card:hover .team-avatar { transform: scale(1.15) rotate(-5deg); }
.team-name { font-weight: 700; color: #1E2E1F; font-size: .95rem; margin-bottom: .2rem; }
.team-role { font-size: .82rem; color: #3A8A52; font-weight: 600; margin-bottom: .7rem; }
.team-tags { display: flex; flex-wrap: wrap; gap: .35rem; justify-content: center; }
.team-tag {
  font-size: .7rem; font-weight: 600; padding: .18rem .55rem;
  border-radius: 50px; background: #ECF6EE; color: #3A8A52;
  transition: background .2s, color .2s;
}
.team-card:hover .team-tag { background: #3A8A52; color: #fff; }

/* ── Tech stack ── */
.tech-grid { display: grid; grid-template-columns: repeat(3,1fr); gap: 1rem; }
.tech-item {
  background: #F9F6F1; border-radius: 14px; padding: 1.4rem 1.2rem;
  border: 1px solid #EAE6E0; display: flex; align-items: center; gap: 1rem;
  transition: transform .3s, box-shadow .3s, border-color .3s;
}
.tech-item:hover { transform: translateX(6px); box-shadow: 0 8px 24px rgba(45,95,58,.08); border-color: #B3DAC0; }
.tech-ico { font-size: 1.8rem; flex-shrink: 0; }
.tech-name { font-weight: 700; color: #1E2E1F; font-size: .95rem; }
.tech-cat { font-size: .75rem; color: #3A8A52; font-weight: 600; margin-top: .15rem; }

/* ── CTA ── */
.cta-section {
  background: linear-gradient(135deg,#1E3A24,#3A8A52);
  padding: 6rem 2rem; text-align: center; position: relative; overflow: hidden;
}
.cta-blob {
  position: absolute; top: -100px; left: 50%; transform: translateX(-50%);
  width: 600px; height: 600px; border-radius: 50%;
  background: rgba(107,184,124,.15); pointer-events: none;
}
.cta-inner { position: relative; z-index: 1; }
.cta-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(2rem,4vw,3rem); font-weight: 700; color: #fff; margin-bottom: .9rem; }
.cta-p { color: rgba(255,255,255,.75); font-size: 1.05rem; max-width: 500px; margin: 0 auto 2rem; line-height: 1.75; }
.cta-btns { display: flex; gap: 1rem; justify-content: center; }
.btn-cta-p {
  padding: .85rem 2rem; border-radius: 50px; border: none; cursor: pointer;
  background: linear-gradient(135deg,#fff,#ECF6EE); color: #2D5C3A;
  font-weight: 700; font-size: 1rem;
  box-shadow: 0 4px 16px rgba(0,0,0,.15);
  transition: transform .25s, box-shadow .25s;
}
.btn-cta-p:hover { transform: translateY(-3px); box-shadow: 0 8px 24px rgba(0,0,0,.2); }
.btn-cta-g {
  padding: .85rem 2rem; border-radius: 50px; cursor: pointer;
  background: transparent; color: rgba(255,255,255,.9);
  border: 1.5px solid rgba(255,255,255,.4);
  font-weight: 600; font-size: 1rem;
  transition: background .25s, border-color .25s;
}
.btn-cta-g:hover { background: rgba(255,255,255,.1); border-color: rgba(255,255,255,.7); }

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

/* ── Scroll reveal ── */
.scroll-reveal { opacity: 0; transform: translateY(28px); transition: opacity .7s cubic-bezier(.22,1,.36,1), transform .7s cubic-bezier(.22,1,.36,1); }
.scroll-reveal.revealed { opacity: 1; transform: none; }
.reveal-d1 { transition-delay: .08s; }
.reveal-d2 { transition-delay: .16s; }
.reveal-d3 { transition-delay: .24s; }
.reveal-d4 { transition-delay: .32s; }

@media (max-width: 900px) {
  .ig, .team-grid { grid-template-columns: repeat(2,1fr); }
  .features-grid, .tech-grid { grid-template-columns: 1fr 1fr; }
  .stats-in { grid-template-columns: repeat(2,1fr); }
  .footer-top { grid-template-columns: 1fr 1fr; }
}
@media (max-width: 600px) {
  .ig, .team-grid, .features-grid, .tech-grid { grid-template-columns: 1fr; }
  .nav-links { display: none; }
}
</style>
