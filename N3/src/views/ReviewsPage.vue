<template>
  <div class="contact-page">

    <!-- Navbar -->
    <header class="navbar">
      <div class="logo" @click="$router.push('/')">
        <img v-if="lib.logoUrl" :src="lib.logoUrl" class="logo-img" alt="logo" />
        <v-icon v-else icon="mdi-library" class="mr-2 logo-icon"></v-icon>
        <span class="logo-text">{{ lib.name }}</span>
      </div>
      <nav class="nav-links">
        <a @click.prevent="$router.push('/')">{{ t('auto.txt_03f0c664') }}</a>
        <a @click.prevent="$router.push('/features')">{{ t('auto.txt_33f0741f') }}</a>
        <a @click.prevent="$router.push('/statistics')">{{ t('auto.txt_3b5d38f4') }}</a>
        <a class="active" @click.prevent="$router.push('/reviews')">{{ t('auto.txt_9276b119') }}</a>
      </nav>
      <div class="nav-auth">
        <button class="btn-login" @click="$router.push('/login')">{{ t('auto.txt_9a192725') }}</button>
        <button class="btn-register" @click="$router.push('/register')">{{ t('auto.txt_0bb0951d') }}</button>
      </div>
    </header>

    <!-- Hero -->
    <div class="page-hero">
      <div class="hero-blob b1"></div>
      <div class="hero-blob b2"></div>
      <div class="particle" v-for="n in 10" :key="n" :class="`p${n}`"></div>
      <div class="hero-inner">
        <div class="ph-tag scroll-reveal">{{ t('auto.txt_e14ff521') }}</div>
        <h1 class="ph-h scroll-reveal reveal-d1">{{ t('auto.txt_46ff2d0e') }} <span class="hl">{{ t('auto.txt_fdd8dba9') }}</span> {{ t('auto.txt_ff84406f') }}</h1>
        <p class="ph-p scroll-reveal reveal-d2">{{ t('reviews.hero_sub') }} {{ lib.name }}. {{ t('auto.txt_9045a161') }}.</p>
        <div class="hero-rating scroll-reveal reveal-d3">
          <div class="stars">★★★★★</div>
          <div class="rating-text">{{ t('auto.txt_817c0946') }}</div>
        </div>
      </div>
    </div>

    <!-- Rating summary -->
    <div class="section-wrap bg-light">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_09c0fd66') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_b4292de3') }} <span class="hl">{{ t('auto.txt_74ca205a') }}</span></h2>
      <div class="rating-summary">
        <div class="rating-big scroll-reveal reveal-d1">
          <div class="rating-score">4.9</div>
          <div class="rating-stars">★★★★★</div>
          <div class="rating-count">{{ t('auto.txt_50f04be1') }}</div>
        </div>
        <div class="rating-bars scroll-reveal reveal-d2">
          <div v-for="bar in ratingBars" :key="bar.stars" class="rbar-row">
            <span class="rbar-label">{{ bar.stars }}★</span>
            <div class="rbar-track">
              <div class="rbar-fill" :style="{ width: bar.pct + '%', background: bar.color }"></div>
            </div>
            <span class="rbar-pct">{{ bar.pct }}%</span>
          </div>
        </div>
        <div class="rating-cats scroll-reveal reveal-d3">
          <div v-for="cat in ratingCats" :key="cat.l" class="rcat">
            <div class="rcat-score" :style="{ color: cat.color }">{{ cat.score }}</div>
            <div class="rcat-label">{{ cat.l }}</div>
            <div class="rcat-bar"><div class="rcat-fill" :style="{ width: (cat.score/5*100)+'%', background: cat.color }"></div></div>
          </div>
        </div>
      </div>
    </div>

    <!-- Reviews grid -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_b65e4540') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_27118604') }} <span class="hl">{{ t('auto.txt_fdd8dba9') }}</span></h2>
      <div class="reviews-grid">
        <div v-for="(r, i) in reviews" :key="i"
          class="review-card scroll-reveal"
          :class="`reveal-d${(i%3)+1}`">
          <div class="review-top">
            <div class="review-avatar" :style="{ background: r.color }">{{ r.avatar }}</div>
            <div>
              <div class="review-name">{{ r.name }}</div>
              <div class="review-role">{{ r.role }}</div>
            </div>
            <div class="review-stars">{{ '★'.repeat(r.stars) }}</div>
          </div>
          <p class="review-text">"{{ r.text }}"</p>
          <div class="review-date">{{ r.date }}</div>
        </div>
      </div>
    </div>

    <!-- Divider -->
    <div class="section-divider">
      <div class="divider-line"></div>
      <div class="divider-text">{{ t('auto.txt_7400e913') }}</div>
      <div class="divider-line"></div>
    </div>

    <!-- Contact info + form -->
    <div class="section-wrap bg-light">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_9276b119') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_5c458e9e') }} <span class="hl">{{ t('auto.txt_b8ead6ed') }}</span> {{ t('auto.txt_5c0d1a90') }}</h2>
      <div class="contact-grid">
        <!-- Left: info -->
        <div class="contact-info">
          <div v-for="(c, i) in contactInfo" :key="c.t"
            class="ci-item scroll-reveal"
            :class="`reveal-d${i+1}`">
            <div class="ci-ico" :style="{ background: c.bg }">{{ c.ico }}</div>
            <div>
              <div class="ci-t">{{ c.t }}</div>
              <div class="ci-d">{{ c.d }}</div>
            </div>
          </div>

          <!-- Map placeholder -->
          <div class="map-box scroll-reveal reveal-d4">
            <div class="map-inner">
              <div class="map-pin">📍</div>
              <div class="map-label">{{ lib.address }}</div>
            </div>
          </div>
        </div>

        <!-- Right: form -->
        <div class="contact-form scroll-reveal reveal-d2">
          <div v-if="formSent" class="form-success">
            <div class="success-ico">✅</div>
            <h3>{{ t('auto.txt_d71ff104') }}</h3>
            <p>{{ t('auto.txt_6b2019cc') }}</p>
            <button class="btn-submit" @click="formSent = false">{{ t('auto.txt_cf1c9c7e') }}</button>
          </div>
          <template v-else>
            <h3 class="form-title">{{ t('auto.txt_f57718cd') }}</h3>
            <div class="form-row">
              <div class="fg">
                <label class="fl">{{ t('auto.txt_623825de') }}</label>
                <input class="fi" v-model="form.name" placeholder="Nguyễn Văn A" />
              </div>
              <div class="fg">
                <label class="fl">Email</label>
                <input class="fi" v-model="form.email" placeholder="email@example.com" type="email" />
              </div>
            </div>
            <div class="fg">
              <label class="fl">{{ t('auto.txt_f5d6a50b') }}</label>
              <select class="fi" v-model="form.subject">
                <option value="">{{ t('auto.txt_83127882') }}</option>
                <option>{{ t('auto.txt_da242a1a') }}</option>
                <option>{{ t('auto.txt_f2330a3d') }}</option>
                <option>{{ t('auto.txt_d5b31a6c') }}</option>
                <option>{{ t('auto.txt_1c731333') }}</option>
                <option>{{ t('auto.txt_06c1f85a') }}</option>
              </select>
            </div>
            <div class="fg">
              <label class="fl">{{ t('auto.txt_ee7ca513') }}</label>
              <textarea class="fi" v-model="form.message" rows="5" :placeholder="t('auto.txt_1922c670')"></textarea>
            </div>
            <button class="btn-submit" @click="submitForm">{{ t('auto.txt_a31400ba') }}</button>
          </template>
        </div>
      </div>
    </div>

    <!-- FAQ mini -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_8f21db8e') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_c1a8b2f2') }} <span class="hl">{{ t('auto.txt_0ed5eb04') }}</span></h2>
      <div class="mini-faq">
        <div v-for="(q, i) in miniFaqs" :key="i"
          class="mfaq-card scroll-reveal"
          :class="`reveal-d${(i%2)+1}`">
          <div class="mfaq-q">{{ q.q }}</div>
          <div class="mfaq-a">{{ q.a }}</div>
        </div>
      </div>
    </div>

    <!-- CTA -->
    <div class="cta-section">
      <div class="cta-blob"></div>
      <div class="cta-inner scroll-reveal">
        <h2 class="cta-h">{{ t('auto.txt_66768b62') }}</h2>
        <p class="cta-p">{{ t('auto.txt_90d29591') }}</p>
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

const formSent = ref(false)
const form = ref({ name: '', email: '', subject: '', message: '' })
const submitForm = () => { formSent.value = true }

// Contact info using libraryStore
const contactInfo = computed(() => [
  { ico: '📍', t: t('auto.txt_7db5b909'), d: lib.address, bg: '#ECF6EE' },
  { ico: '📞', t: t('profile.phone'), d: `Hotline: ${lib.phone}`, bg: '#FFF6E8' },
  { ico: '✉️', t: 'Email', d: lib.email, bg: '#EFF4FF' },
  { ico: '🕐', t: t('statistics.rules.hours_t'), d: t('statistics.rules.hours_d'), bg: '#FDF0FF' },
])

const ratingBars = [
  { stars: 5, pct: 82, color: '#3A8A52' },
  { stars: 4, pct: 12, color: '#6BB87C' },
  { stars: 3, pct: 4,  color: '#CF7C07' },
  { stars: 2, pct: 1,  color: '#E07B2A' },
  { stars: 1, pct: 1,  color: '#C0392B' },
]

const ratingCats = computed(() => [
  { l: t('reviews.ui_label'), score: 4.9, color: '#3A8A52' },
  { l: t('reviews.speed_label'), score: 4.8, color: '#CF7C07' },
  { l: t('reviews.feature_label'), score: 4.9, color: '#2AABB0' },
  { l: t('reviews.support_label'), score: 4.7, color: '#7B4FA0' },
])

const reviews = computed(() => [
  { avatar: '👨‍🎓', name: t('reviews.list.0.name'), role: t('reviews.list.0.role'), stars: 5, color: '#ECF6EE', text: t('reviews.list.0.text'), date: '15/04/2025' },
  { avatar: '👩‍💼', name: t('reviews.list.1.name'), role: t('reviews.list.1.role'), stars: 5, color: '#FFF6E8', text: t('reviews.list.1.text'), date: '10/04/2025' },
  { avatar: '👨‍🔬', name: t('reviews.list.2.name'), role: t('reviews.list.2.role'), stars: 5, color: '#EFF4FF', text: t('reviews.list.2.text'), date: '08/04/2025' },
  { avatar: '👩‍🏫', name: t('reviews.list.3.name'), role: t('reviews.list.3.role'), stars: 5, color: '#FDF0FF', text: t('reviews.list.3.text'), date: '05/04/2025' },
  { avatar: '👨‍💻', name: t('reviews.list.4.name'), role: t('reviews.list.4.role'), stars: 4, color: '#F0FBF4', text: t('reviews.list.4.text'), date: '01/04/2025' },
  { avatar: '👩‍🎨', name: t('reviews.list.5.name'), role: t('reviews.list.5.role'), stars: 5, color: '#FFF0F0', text: t('reviews.list.5.text'), date: '28/03/2025' },
])


const miniFaqs = computed(() => [
  { q: t('reviews.faq1_q'), a: `${t('reviews.faq1_a')} ${lib.phone} ${t('reviews.faq1_a2')} ${lib.email}.` },
  { q: t('reviews.faq2_q'), a: t('reviews.faq2_a') },
  { q: t('reviews.faq3_q'), a: t('reviews.faq3_a') },
  { q: t('reviews.faq4_q'), a: `${t('reviews.faq4_a')} ${lib.address}.` },
])

let observer: IntersectionObserver | null = null
onMounted(() => {
  observer = new IntersectionObserver(
    (entries) => entries.forEach(e => { if (e.isIntersecting) e.target.classList.add('revealed') }),
    { threshold: 0.1 }
  )
  document.querySelectorAll('.scroll-reveal').forEach(el => observer?.observe(el))
})
onUnmounted(() => observer?.disconnect())
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Cormorant+Garamond:wght@400;600;700&family=Be+Vietnam+Pro:wght@300;400;500;600;700;800&display=swap');
*, *::before, *::after { box-sizing: border-box; }
.contact-page { font-family: 'Be Vietnam Pro', sans-serif; background: #F9F6F1; color: #2D3A2E; overflow-x: hidden; padding-top: 66px; }

/* Navbar */
.navbar { position: fixed; top: 0; left: 0; right: 0; z-index: 999; background: rgba(249,246,241,.92); backdrop-filter: blur(16px); border-bottom: 1px solid rgba(139,189,151,.2); display: flex; align-items: center; justify-content: space-between; padding: 0 4%; height: 66px; }
.logo { display: flex; align-items: center; cursor: pointer; }
.logo-icon { font-size: 28px; color: #1E5652 !important; }
.logo-img { width: 32px; height: 32px; border-radius: 6px; object-fit: contain; margin-right: 8px; }
.logo-text { font-size: 24px; font-weight: 800; color: #1E5652; letter-spacing: 1px; font-family: 'Be Vietnam Pro', sans-serif; }
.nav-links { display: flex; gap: .25rem; }
.nav-links a { padding: .45rem .9rem; border-radius: 8px; cursor: pointer; font-size: 1rem; font-weight: 600; color: #5A6B5B; transition: color .2s; text-decoration: none; position: relative; padding-bottom: 6px; }
.nav-links a::after { content: ''; position: absolute; bottom: 0; left: .9rem; right: .9rem; height: 2px; background: #3A8A52; border-radius: 2px; transform: scaleX(0); transition: transform .3s; }
.nav-links a:hover::after, .nav-links a.active::after { transform: scaleX(1); }
.nav-links a:hover, .nav-links a.active { color: #3A8A52; }
.btn-login { padding: .5rem 1.2rem; border-radius: 50px; border: none; cursor: pointer; background: transparent; color: #3A8A52; font-weight: 700; font-size: .9rem; }
.btn-register { padding: .5rem 1.35rem; border-radius: 50px; border: none; cursor: pointer; background: linear-gradient(135deg,#3A8A52,#5BAD70); color: #fff; font-weight: 700; font-size: .9rem; box-shadow: 0 3px 12px rgba(58,138,82,.3); transition: transform .2s; }
.btn-register:hover { transform: translateY(-2px); }

/* Hero — rose/pink gradient để khác 3 trang kia */
.page-hero { background: linear-gradient(135deg,#0F2317,#1E3A24,#2D5C3A); padding: 6rem 2rem 4rem; text-align: center; position: relative; overflow: hidden; min-height: 420px; display: flex; align-items: center; justify-content: center; }
.hero-blob { position: absolute; border-radius: 50%; pointer-events: none; filter: blur(70px); }
.b1 { width: 500px; height: 500px; top: -150px; right: -100px; background: rgba(107,184,124,.25); animation: blob1 12s ease-in-out infinite; }
.b2 { width: 350px; height: 350px; bottom: -100px; left: -80px; background: rgba(207,124,7,.15); animation: blob2 15s ease-in-out infinite; }
@keyframes blob1 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(30px,-20px) scale(1.05)} 66%{transform:translate(-20px,15px) scale(.97)} }
@keyframes blob2 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(-25px,20px) scale(1.03)} 66%{transform:translate(20px,-15px) scale(.98)} }
.particle { position: absolute; border-radius: 50%; background: rgba(139,212,160,.3); pointer-events: none; animation: floatP linear infinite; }
.p1{width:8px;height:8px;left:10%;top:20%;animation-duration:8s}
.p2{width:5px;height:5px;left:25%;top:70%;animation-duration:11s;animation-delay:1s}
.p3{width:10px;height:10px;left:40%;top:15%;animation-duration:9s;animation-delay:2s}
.p4{width:6px;height:6px;left:60%;top:80%;animation-duration:13s;animation-delay:.5s}
.p5{width:8px;height:8px;left:70%;top:25%;animation-duration:10s;animation-delay:3s}
.p6{width:4px;height:4px;left:80%;top:60%;animation-duration:7s;animation-delay:1.5s}
.p7{width:12px;height:12px;left:88%;top:35%;animation-duration:12s;animation-delay:2.5s}
.p8{width:5px;height:5px;left:5%;top:55%;animation-duration:9s;animation-delay:4s}
.p9{width:7px;height:7px;left:50%;top:45%;animation-duration:11s;animation-delay:3.5s}
.p10{width:9px;height:9px;left:30%;top:85%;animation-duration:8s;animation-delay:2s}
@keyframes floatP { 0%{transform:translateY(0);opacity:.5} 50%{transform:translateY(-35px);opacity:1} 100%{transform:translateY(0);opacity:.5} }
.hero-inner { position: relative; z-index: 1; max-width: 650px; }
.ph-tag { display: inline-block; background: rgba(255,255,255,.15); color: rgba(255,255,255,.9); padding: .3rem .9rem; border-radius: 50px; font-size: .78rem; font-weight: 700; letter-spacing: .06em; text-transform: uppercase; margin-bottom: 1rem; border: 1px solid rgba(255,255,255,.2); }
.ph-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(2.2rem,4.5vw,3.8rem); font-weight: 700; color: #fff; margin-bottom: 1rem; line-height: 1.2; }
.ph-h .hl { color: #8BD4A0; }
.ph-p { color: rgba(255,255,255,.75); font-size: 1.05rem; line-height: 1.8; margin-bottom: 1.5rem; }
.hero-rating { display: inline-flex; align-items: center; gap: .8rem; background: rgba(255,255,255,.12); border: 1px solid rgba(255,255,255,.2); border-radius: 50px; padding: .6rem 1.4rem; }
.stars { color: #FCD34D; font-size: 1.2rem; letter-spacing: 2px; }
.rating-text { color: rgba(255,255,255,.85); font-size: .9rem; font-weight: 600; }

/* Section commons */
.section-wrap { padding: 4rem 5%; }
.bg-white { background: #fff; }
.bg-light { background: #F9F6F1; }
.sec-label { display: inline-block; background: #ECF6EE; color: #3A8A52; padding: .28rem .85rem; border-radius: 50px; font-size: .75rem; font-weight: 700; letter-spacing: .06em; text-transform: uppercase; margin-bottom: .9rem; }
.sec-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(1.9rem,3vw,2.8rem); font-weight: 700; color: #1E2E1F; line-height: 1.25; margin-bottom: 2rem; }
.sec-h .hl { color: #3A8A52; }

/* Rating summary */
.rating-summary { display: grid; grid-template-columns: auto 1fr auto; gap: 3rem; align-items: center; background: #fff; border-radius: 20px; padding: 2.5rem; border: 1px solid #EAE6E0; box-shadow: 0 4px 20px rgba(0,0,0,.04); }
.rating-big { text-align: center; }
.rating-score { font-family: 'Cormorant Garamond',serif; font-size: 5rem; font-weight: 700; color: #1E2E1F; line-height: 1; }
.rating-stars { color: #FCD34D; font-size: 1.4rem; letter-spacing: 3px; margin: .3rem 0; }
.rating-count { font-size: .85rem; color: #8A9E8B; }
.rbar-row { display: flex; align-items: center; gap: .8rem; margin-bottom: .6rem; }
.rbar-label { font-size: .85rem; font-weight: 600; color: #5A6B5B; min-width: 24px; }
.rbar-track { flex: 1; height: 8px; background: #EAE6E0; border-radius: 4px; overflow: hidden; }
.rbar-fill { height: 100%; border-radius: 4px; transition: width 1s ease; }
.rbar-pct { font-size: .8rem; color: #8A9E8B; min-width: 36px; text-align: right; }
.rating-cats { display: flex; flex-direction: column; gap: .8rem; }
.rcat { display: flex; align-items: center; gap: .8rem; }
.rcat-score { font-weight: 800; font-size: 1.1rem; min-width: 32px; }
.rcat-label { font-size: .85rem; color: #5A6B5B; min-width: 70px; }
.rcat-bar { flex: 1; height: 6px; background: #EAE6E0; border-radius: 3px; overflow: hidden; }
.rcat-fill { height: 100%; border-radius: 3px; }

/* Reviews grid */
.reviews-grid { display: grid; grid-template-columns: repeat(3,1fr); gap: 1.4rem; }
.review-card { background: #F9F6F1; border-radius: 18px; padding: 1.6rem; border: 1px solid #EAE6E0; transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .4s; }
.review-card:hover { transform: translateY(-6px); box-shadow: 0 16px 40px rgba(58,138,82,.1); border-color: #B3DAC0; }
.review-top { display: flex; align-items: center; gap: .8rem; margin-bottom: 1rem; }
.review-avatar { width: 44px; height: 44px; border-radius: 50%; display: flex; align-items: center; justify-content: center; font-size: 1.4rem; flex-shrink: 0; }
.review-name { font-weight: 700; color: #1E2E1F; font-size: .95rem; }
.review-role { font-size: .78rem; color: #8A9E8B; }
.review-stars { margin-left: auto; color: #FCD34D; font-size: .95rem; letter-spacing: 1px; }
.review-text { font-size: .88rem; color: #5A6B5B; line-height: 1.7; font-style: italic; margin-bottom: .8rem; }
.review-date { font-size: .75rem; color: #A8B9AA; }

/* Divider */
.section-divider { display: flex; align-items: center; gap: 1.5rem; padding: 2rem 5%; }
.divider-line { flex: 1; height: 1px; background: linear-gradient(90deg,transparent,#B3DAC0,transparent); }
.divider-text { font-size: 1rem; font-weight: 700; color: #3A8A52; white-space: nowrap; }

/* Contact grid */
.contact-grid { display: grid; grid-template-columns: 1fr 1.4fr; gap: 3rem; }
.contact-info { display: flex; flex-direction: column; gap: 1.2rem; }
.ci-item { display: flex; gap: 1rem; align-items: flex-start; }
.ci-ico { width: 46px; height: 46px; border-radius: 12px; display: flex; align-items: center; justify-content: center; font-size: 1.2rem; flex-shrink: 0; }
.ci-t { font-weight: 700; color: #1E2E1F; font-size: .95rem; margin-bottom: .2rem; }
.ci-d { font-size: .88rem; color: #6A7B6B; line-height: 1.6; white-space: pre-line; }
.map-box { background: linear-gradient(135deg,#0F2317,#1E3A24); border-radius: 16px; padding: 2rem; display: flex; align-items: center; justify-content: center; min-height: 140px; margin-top: .5rem; }
.map-inner { text-align: center; }
.map-pin { font-size: 2.5rem; margin-bottom: .5rem; animation: bounce 2s ease-in-out infinite; }
@keyframes bounce { 0%,100%{transform:translateY(0)} 50%{transform:translateY(-8px)} }
.map-label { color: rgba(255,255,255,.8); font-size: .9rem; line-height: 1.6; }

/* Form */
.contact-form { background: #fff; border-radius: 20px; padding: 2.2rem; border: 1px solid #EAE6E0; box-shadow: 0 8px 32px rgba(58,138,82,.06); }
.form-title { font-weight: 700; color: #1E2E1F; font-size: 1.05rem; margin-bottom: 1.4rem; }
.form-row { display: grid; grid-template-columns: 1fr 1fr; gap: 1rem; }
.fg { margin-bottom: 1.1rem; }
.fl { display: block; font-size: .82rem; font-weight: 700; color: #2D3A2E; margin-bottom: .45rem; }
.fi { width: 100%; padding: .78rem 1rem; border-radius: 10px; border: 1.5px solid #E0DDD8; outline: none; background: #F9F6F1; font-family: inherit; font-size: .88rem; color: #2D3A2E; transition: border-color .25s, box-shadow .25s, background .25s; }
.fi:focus { border-color: #3A8A52; box-shadow: 0 0 0 3px rgba(58,138,82,.1); background: #fff; }
textarea.fi { resize: vertical; min-height: 120px; }
.btn-submit { width: 100%; padding: .9rem; background: linear-gradient(135deg,#3A8A52,#5BAD70); border: none; border-radius: 50px; cursor: pointer; color: #fff; font-family: inherit; font-size: .95rem; font-weight: 700; box-shadow: 0 4px 16px rgba(58,138,82,.3); transition: transform .25s, box-shadow .25s; }
.btn-submit:hover { transform: translateY(-2px); box-shadow: 0 8px 24px rgba(58,138,82,.4); }
.form-success { text-align: center; padding: 2.5rem 1rem; }
.success-ico { font-size: 3rem; margin-bottom: 1rem; animation: scaleIn .5s cubic-bezier(.22,1,.36,1); }
@keyframes scaleIn { from{transform:scale(.5);opacity:0} to{transform:scale(1);opacity:1} }
.form-success h3 { font-weight: 700; color: #1E2E1F; margin-bottom: .5rem; }
.form-success p { color: #6A7B6B; font-size: .9rem; margin-bottom: 1.5rem; }

/* Mini FAQ */
.mini-faq { display: grid; grid-template-columns: repeat(2,1fr); gap: 1.25rem; }
.mfaq-card { background: #F9F6F1; border-radius: 16px; padding: 1.6rem; border: 1px solid #EAE6E0; transition: transform .35s cubic-bezier(.22,1,.36,1), box-shadow .35s, border-color .3s; }
.mfaq-card:hover { transform: translateY(-5px); box-shadow: 0 12px 32px rgba(58,138,82,.08); border-color: #B3DAC0; }
.mfaq-q { font-weight: 700; color: #1E2E1F; font-size: .98rem; margin-bottom: .6rem; display: flex; gap: .5rem; }
.mfaq-q::before { content: '?'; width: 22px; height: 22px; border-radius: 50%; background: #3A8A52; color: #fff; font-size: .7rem; font-weight: 800; display: flex; align-items: center; justify-content: center; flex-shrink: 0; margin-top: 1px; }
.mfaq-a { font-size: .88rem; color: #6A7B6B; line-height: 1.7; padding-left: 1.7rem; }

/* CTA */
.cta-section { background: linear-gradient(135deg,#0F2317,#1E3A24,#2D5C3A); padding: 5rem 2rem; text-align: center; position: relative; overflow: hidden; }
.cta-blob { position: absolute; top: -100px; left: 50%; transform: translateX(-50%); width: 600px; height: 600px; border-radius: 50%; background: rgba(107,184,124,.15); pointer-events: none; }
.cta-inner { position: relative; z-index: 1; }
.cta-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(2rem,4vw,3rem); font-weight: 700; color: #fff; margin-bottom: .9rem; }
.cta-p { color: rgba(255,255,255,.75); font-size: 1.05rem; max-width: 500px; margin: 0 auto 2rem; line-height: 1.75; }
.cta-btns { display: flex; gap: 1rem; justify-content: center; }
.btn-cta-p { padding: .85rem 2rem; border-radius: 50px; border: none; cursor: pointer; background: linear-gradient(135deg,#fff,#ECF6EE); color: #2D5C3A; font-weight: 700; font-size: 1rem; box-shadow: 0 4px 16px rgba(0,0,0,.15); transition: transform .25s; }
.btn-cta-p:hover { transform: translateY(-3px); }
.btn-cta-g { padding: .85rem 2rem; border-radius: 50px; cursor: pointer; background: transparent; color: rgba(255,255,255,.9); border: 1.5px solid rgba(255,255,255,.4); font-weight: 600; font-size: 1rem; transition: background .25s; }
.btn-cta-g:hover { background: rgba(255,255,255,.1); }

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

/* Scroll reveal */
.scroll-reveal { opacity: 0; transform: translateY(28px); transition: opacity .7s cubic-bezier(.22,1,.36,1), transform .7s cubic-bezier(.22,1,.36,1); }
.scroll-reveal.revealed { opacity: 1; transform: none; }
.reveal-d1{transition-delay:.08s} .reveal-d2{transition-delay:.16s} .reveal-d3{transition-delay:.24s} .reveal-d4{transition-delay:.32s}

@media(max-width:900px){
  .rating-summary{grid-template-columns:1fr;gap:1.5rem}
  .reviews-grid,.mini-faq{grid-template-columns:1fr 1fr}
  .contact-grid{grid-template-columns:1fr}
  .footer-top{grid-template-columns:1fr 1fr}
}
@media(max-width:600px){
  .reviews-grid,.mini-faq,.form-row{grid-template-columns:1fr}
  .nav-links{display:none}
}
</style>

