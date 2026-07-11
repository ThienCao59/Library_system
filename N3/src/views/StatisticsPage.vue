<template>
  <div class="guide-page">

    <!-- Navbar -->
    <header class="navbar">
      <div class="logo" @click="$router.push('/')">
        <img v-if="lib.logoUrl" :src="lib.logoUrl" class="logo-img" alt="logo" />
        <v-icon v-else icon="mdi-library" class="mr-2 logo-icon"></v-icon>
        <span class="logo-text">{{ lib.name }}</span>
      </div>
      <nav class="nav-links">
        <a @click.prevent="$router.push('/')">{{ t('nav.home') }}</a>
        <a @click.prevent="$router.push('/features')">{{ t('nav.about') }}</a>
        <a class="active" @click.prevent="$router.push('/statistics')">{{ t('nav.guide') }}</a>
        <a @click.prevent="$router.push('/reviews')">{{ t('nav.contact') }}</a>
      </nav>
      <div class="nav-auth">
        <button class="btn-login" @click="$router.push('/login')">{{ t('nav.login') }}</button>
        <button class="btn-register" @click="$router.push('/register')">{{ t('nav.register') }}</button>
      </div>
    </header>

    <!-- Hero -->
    <div class="guide-hero">
      <div class="hero-blob b1"></div>
      <div class="hero-blob b2"></div>
      <div class="particle" v-for="n in 10" :key="n" :class="`p${n}`"></div>
      <div class="guide-hero-inner">
        <div class="ph-tag scroll-reveal">{{ t('auto.txt_3f5ddd17') }}</div>
        <h1 class="ph-h scroll-reveal reveal-d1">
          {{ t('auto.txt_85500d8f') }} <span class="hl">{{ lib.name }}</span> {{ t('auto.txt_6c5b64f8') }}
        </h1>
        <p class="ph-p scroll-reveal reveal-d2">
          {{ t('auto.txt_ca058d3a') }}
        </p>
        <!-- Quick nav pills -->
        <div class="quick-nav scroll-reveal reveal-d3">
          <a class="qpill" href="#steps">{{ t('auto.txt_193923c4') }}</a>
          <a class="qpill" href="#faq">{{ t('auto.txt_2a59ccea') }}</a>
          <a class="qpill" href="#rules">{{ t('auto.txt_7f660d23') }}</a>
          <a class="qpill" href="#tips">{{ t('auto.txt_9ff81168') }}</a>
        </div>
      </div>
    </div>

    <!-- Quick steps -->
    <div id="steps" class="section-wrap bg-gradient-green">
      <div class="sec-center">
        <div class="sec-label light scroll-reveal">{{ t('auto.txt_e366bb51') }}</div>
        <h2 class="sec-h white scroll-reveal reveal-d1">{{ t('auto.txt_a03a2c80') }} <span class="hl-light">{{ t('auto.txt_e553ad5c') }}</span> {{ t('auto.txt_e42eee81') }}</h2>
      </div>
      <div class="steps-visual">
        <div v-for="(s, i) in quickSteps" :key="s.t"
          class="step-card scroll-reveal"
          :class="`reveal-d${i+1}`">
          <div class="step-num-big">{{ i + 1 }}</div>
          <div class="step-ico">{{ s.ico }}</div>
          <div class="step-t">{{ s.t }}</div>
          <div class="step-d">{{ s.d }}</div>
          <div class="step-connector" v-if="i < quickSteps.length - 1"></div>
        </div>
      </div>
    </div>

    <!-- Detailed guide accordion -->
    <div class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_e67cad73') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_db0eacb8') }} <span class="hl">{{ t('auto.txt_bd8a4f03') }}</span></h2>
      <div class="acc-list scroll-reveal reveal-d2">
        <div v-for="(item, i) in guideItems" :key="i"
          class="acc-item"
          :class="{ open: openAcc === i }">
          <div class="acc-hd" @click="openAcc = openAcc === i ? -1 : i">
            <div class="acc-left">
              <div class="acc-num">{{ String(i+1).padStart(2,'0') }}</div>
              <div class="acc-ico-wrap" :style="{ background: item.color + '22' }">
                <span>{{ item.ico }}</span>
              </div>
              <div class="acc-t">{{ item.t }}</div>
            </div>
            <div class="acc-chevron" :class="{ rotated: openAcc === i }">›</div>
          </div>
          <transition name="slide-down">
            <div v-if="openAcc === i" class="acc-body" @click.stop>
              <p class="acc-text">{{ item.content }}</p>
              <div v-if="item.steps" class="acc-steps">
                <div v-for="(st, si) in item.steps" :key="si" class="acc-step-item">
                  <div class="acc-step-dot" :style="{ background: item.color }"></div>
                  <span>{{ st }}</span>
                </div>
              </div>
              <div v-if="item.tip" class="acc-tip">
                <span class="tip-ico">💡</span> {{ item.tip }}
              </div>
            </div>
          </transition>
        </div>
      </div>
    </div>

    <!-- Rules section -->
    <div id="rules" class="section-wrap bg-light">
      <div class="sec-label scroll-reveal">{{ t('auto.txt_cbb263ca') }}</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_cbb263ca') }} <span class="hl">{{ t('auto.txt_c1f002ab') }}</span></h2>
      <div class="rules-grid">
        <div v-for="(r, i) in rules" :key="r.t"
          class="rule-card scroll-reveal"
          :class="`reveal-d${(i%3)+1}`"
          :style="{ '--accent': r.color }">
          <div class="rule-ico">{{ r.ico }}</div>
          <div class="rule-t">{{ r.t }}</div>
          <div class="rule-d">{{ r.d }}</div>
          <div class="rule-bar"></div>
        </div>
      </div>
    </div>

    <!-- Tips section -->
    <div id="tips" class="section-wrap bg-gradient-teal">
      <div class="sec-center">
        <div class="sec-label light scroll-reveal">{{ t('auto.txt_b718a505') }}</div>
        <h2 class="sec-h white scroll-reveal reveal-d1">Dùng {{ lib.name }} <span class="hl-light">{{ t('auto.txt_4b998510') }}</span></h2>
      </div>
      <div class="tips-grid">
        <div v-for="(tip, i) in tips" :key="tip.t"
          class="tip-card scroll-reveal"
          :class="`reveal-d${(i%4)+1}`">
          <div class="tip-num">{{ String(i+1).padStart(2,'0') }}</div>
          <div class="tip-ico">{{ tip.ico }}</div>
          <div class="tip-t">{{ tip.t }}</div>
          <div class="tip-d">{{ tip.d }}</div>
        </div>
      </div>
    </div>

    <!-- FAQ -->
    <div id="faq" class="section-wrap bg-white">
      <div class="sec-label scroll-reveal">FAQ</div>
      <h2 class="sec-h scroll-reveal reveal-d1">{{ t('auto.txt_c1a8b2f2') }} <span class="hl">{{ t('auto.txt_0ed5eb04') }}</span></h2>
      <div class="faq-grid">
        <div v-for="(q, i) in faqs" :key="i"
          class="faq-card scroll-reveal"
          :class="`reveal-d${(i%2)+1}`">
          <div class="faq-q">{{ q.q }}</div>
          <div class="faq-a">{{ q.a }}</div>
        </div>
      </div>
    </div>

    <!-- CTA -->
    <div class="cta-section">
      <div class="cta-blob"></div>
      <div class="cta-inner scroll-reveal">
        <h2 class="cta-h">{{ t('auto.txt_85225ad9') }}</h2>
        <p class="cta-p">{{ t('auto.txt_5bc906c4') }}</p>
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

const openAcc = ref(-1)

const quickSteps = computed(() => [
  { ico: '📝', t: t('statistics.steps.register_t'), d: t('statistics.steps.register_d') },
  { ico: '🪪', t: t('statistics.steps.card_t'), d: t('statistics.steps.card_d') },
  { ico: '🔍', t: t('statistics.steps.borrow_t'), d: t('statistics.steps.borrow_d') },
  { ico: '📖', t: t('statistics.steps.return_t'), d: t('statistics.steps.return_d') },
])

const guideItems = computed(() => [
  {
    ico: '🔐', t: t('statistics.guide.login_t'), color: '#3A8A52',
    content: t('statistics.guide.login_c'),
    steps: [`${lib.name}`, t('statistics.guide.login_t'), t('statistics.steps.register_t'), t('auto.txt_8929ef31'), t('auto.txt_555f1f22')],
    tip: t('statistics.guide.login_tip')
  },
  {
    ico: '📚', t: t('statistics.guide.search_t'), color: '#E07B2A',
    content: t('statistics.guide.search_c'),
    steps: [t('auto.txt_8929ef31'), t('auto.txt_92849db7'), t('auto.txt_8929ef31'), t('auto.txt_93faf48c'), t('auto.txt_555f1f22')],
    tip: t('statistics.guide.search_tip')
  },
  {
    ico: '🔄', t: t('statistics.guide.renew_t'), color: '#7B4FA0',
    content: t('statistics.guide.renew_c'),
    steps: [t('statistics.guide.renew_t'), t('statistics.guide.renew_t'), t('auto.txt_682697e7'), t('auto.txt_555f1f22')],
    tip: t('statistics.guide.renew_tip')
  },
  {
    ico: '🔔', t: t('statistics.guide.notify_t'), color: '#E8C840',
    content: t('statistics.guide.notify_c'),
    steps: [t('settings.title'), t('statistics.guide.notify_t'), t('profile.phone')],
    tip: t('statistics.guide.notify_tip')
  },
  {
    ico: '📊', t: t('statistics.guide.history_t'), color: '#2AABB0',
    content: t('statistics.guide.history_c'),
    steps: [t('statistics.guide.history_t'), t('statistics.guide.history_t'), t('statistics.tips.wishlist_t'), t('statistics.tips.ai_t')],
    tip: t('statistics.guide.history_tip')
  },
  {
    ico: '🛡️', t: t('statistics.guide.security_t'), color: '#C0392B',
    content: t('statistics.guide.security_c'),
    steps: [t('statistics.guide.security_t'), t('statistics.guide.security_t'), t('sidebar.logout'), t('statistics.guide.security_t')],
    tip: t('statistics.guide.security_tip')
  },
])

const rules = computed(() => [
  { ico: '📅', t: t('statistics.rules.duration_t'), d: t('statistics.rules.duration_d'), color: '#3A8A52' },
  { ico: '📦', t: t('statistics.rules.quantity_t'), d: t('statistics.rules.quantity_d'), color: '#E07B2A' },
  { ico: '💰', t: t('statistics.rules.fine_t'), d: t('statistics.rules.fine_d'), color: '#C0392B' },
  { ico: '🪪', t: t('statistics.rules.card_t'), d: t('statistics.rules.card_d'), color: '#7B4FA0' },
  { ico: '📖', t: t('statistics.rules.condition_t'), d: t('statistics.rules.condition_d'), color: '#2AABB0' },
  { ico: '🕐', t: t('statistics.rules.hours_t'), d: t('statistics.rules.hours_d'), color: '#E8C840' },
])

const tips = computed(() => [
  { ico: '⭐', t: t('statistics.tips.preborrow_t'), d: t('statistics.tips.preborrow_d') },
  { ico: '🔖', t: t('statistics.tips.wishlist_t'), d: t('statistics.tips.wishlist_d') },
  { ico: '📱', t: t('statistics.tips.mobile_t'), d: t('statistics.tips.mobile_d') },
  { ico: '🔔', t: t('statistics.tips.notify_t'), d: t('statistics.tips.notify_d') },
  { ico: '🤖', t: t('statistics.tips.ai_t'), d: t('statistics.tips.ai_d') },
  { ico: '🏃', t: t('statistics.tips.early_t'), d: t('statistics.tips.early_d') },
  { ico: '📊', t: t('statistics.tips.stats_t'), d: t('statistics.tips.stats_d') },
  { ico: '🎯', t: t('statistics.tips.filter_t'), d: t('statistics.tips.filter_d') },
])

const faqs = computed(() => [
  { q: t('statistics.faq.q1'), a: t('statistics.faq.a1') },
  { q: t('statistics.faq.q2'), a: t('statistics.faq.a2') },
  { q: t('statistics.faq.q3'), a: t('statistics.faq.a3') },
  { q: t('statistics.faq.q4'), a: t('statistics.faq.a4') },
  { q: t('statistics.faq.q5'), a: t('statistics.faq.a5') },
  { q: t('statistics.faq.q6'), a: `${t('statistics.faq.a6')}` },
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

.guide-page { font-family: 'Be Vietnam Pro', sans-serif; background: #F9F6F1; color: #2D3A2E; overflow-x: hidden; padding-top: 66px; }

/* Navbar */
.navbar { position: fixed; top: 0; left: 0; right: 0; z-index: 999; background: rgba(249,246,241,.92); backdrop-filter: blur(16px); border-bottom: 1px solid rgba(139,189,151,.2); display: flex; align-items: center; justify-content: space-between; padding: 0 4%; height: 66px; }
.logo { display: flex; align-items: center; cursor: pointer; }
.logo-icon { font-size: 28px; color: #1E5652 !important; }
.logo-img { width: 32px; height: 32px; border-radius: 6px; object-fit: contain; margin-right: 8px; }
.logo-text { font-size: 24px; font-weight: 800; color: #1E5652; letter-spacing: 1px; font-family: 'Be Vietnam Pro', sans-serif; }
.nav-links { display: flex; gap: .25rem; }
.nav-links a { padding: .45rem .9rem; border-radius: 8px; cursor: pointer; font-size: 1rem; font-weight: 600; color: #5A6B5B; transition: color .2s, background .2s; text-decoration: none; position: relative; padding-bottom: 6px; }
.nav-links a::after { content: ''; position: absolute; bottom: 0; left: .9rem; right: .9rem; height: 2px; background: #3A8A52; border-radius: 2px; transform: scaleX(0); transition: transform .3s ease; }
.nav-links a:hover::after, .nav-links a.active::after { transform: scaleX(1); }
.nav-links a:hover, .nav-links a.active { color: #3A8A52; }
.btn-lang {
  padding: .45rem 1rem; border-radius: 50px; border: 1.5px solid rgba(58,138,82,.4); cursor: pointer;
  background: transparent; color: #3A8A52; font-weight: 700; font-size: .82rem;
  transition: background .2s, border-color .2s; margin-right: 4px;
}
.btn-lang:hover { background: #ECF6EE; border-color: #3A8A52; }
.btn-login { padding: .5rem 1.2rem; border-radius: 50px; border: none; cursor: pointer; background: transparent; color: #3A8A52; font-weight: 700; font-size: .9rem; transition: background .2s; }
.btn-login:hover { background: #ECF6EE; }
.btn-register { padding: .5rem 1.35rem; border-radius: 50px; border: none; cursor: pointer; background: linear-gradient(135deg,#3A8A52,#5BAD70); color: #fff; font-weight: 700; font-size: .9rem; box-shadow: 0 3px 12px rgba(58,138,82,.3); transition: transform .2s; }
.btn-register:hover { transform: translateY(-2px); }

/* Hero */
.guide-hero { background: linear-gradient(135deg,#065F46,#047857,#065F46); padding: 6rem 2rem 4rem; text-align: center; position: relative; overflow: hidden; min-height: 460px; display: flex; align-items: center; justify-content: center; }
.hero-blob { position: absolute; border-radius: 50%; pointer-events: none; filter: blur(70px); }
.b1 { width: 500px; height: 500px; top: -150px; right: -100px; background: rgba(207,124,7,.2); animation: blob1 12s ease-in-out infinite; }
.b2 { width: 350px; height: 350px; bottom: -100px; left: -80px; background: rgba(245,240,232,.1); animation: blob2 15s ease-in-out infinite; }
@keyframes blob1 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(30px,-20px) scale(1.05)} 66%{transform:translate(-20px,15px) scale(.97)} }
@keyframes blob2 { 0%,100%{transform:translate(0,0) scale(1)} 33%{transform:translate(-25px,20px) scale(1.03)} 66%{transform:translate(20px,-15px) scale(.98)} }

.particle { position: absolute; border-radius: 50%; background: rgba(207,124,7,.3); pointer-events: none; animation: floatP linear infinite; }
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
@keyframes floatP { 0%{transform:translateY(0) scale(1);opacity:.5} 50%{transform:translateY(-35px) scale(1.1);opacity:1} 100%{transform:translateY(0) scale(1);opacity:.5} }

.guide-hero-inner { position: relative; z-index: 1; max-width: 700px; }
.ph-tag { display: inline-block; background: rgba(255,255,255,.15); color: rgba(255,255,255,.9); padding: .3rem .9rem; border-radius: 50px; font-size: .78rem; font-weight: 700; letter-spacing: .06em; text-transform: uppercase; margin-bottom: 1rem; border: 1px solid rgba(255,255,255,.2); }
.ph-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(2.2rem,4.5vw,3.8rem); font-weight: 700; color: #fff; margin-bottom: 1rem; line-height: 1.2; }
.ph-h .hl { color: #8BD4A0; }
.ph-p { color: rgba(255,255,255,.75); font-size: 1.05rem; line-height: 1.8; margin-bottom: 2rem; }

.quick-nav { display: flex; flex-wrap: wrap; gap: .6rem; justify-content: center; }
.qpill { padding: .5rem 1.2rem; border-radius: 50px; background: rgba(255,255,255,.1); border: 1px solid rgba(255,255,255,.2); color: rgba(255,255,255,.85); font-size: .85rem; font-weight: 600; text-decoration: none; transition: all .25s; cursor: pointer; }
.qpill:hover { background: rgba(255,255,255,.2); transform: translateY(-2px); }

/* Section commons */
.section-wrap { padding: 4rem 5%; }
.bg-white { background: #fff; }
.bg-light { background: #F9F6F1; }
.bg-gradient-green { background: linear-gradient(135deg,#F5F0E8,#EDE8DF); }
.bg-gradient-teal { background: linear-gradient(135deg,#065F46,#047857,#065F46); }
.sec-label { display: inline-block; background: #FDF6EC; color: #CF7C07; padding: .28rem .85rem; border-radius: 50px; font-size: .75rem; font-weight: 700; letter-spacing: .06em; text-transform: uppercase; margin-bottom: .9rem; }
.sec-label.light { background: rgba(255,255,255,.15); color: rgba(255,255,255,.9); }
.sec-h { font-family: 'Cormorant Garamond',serif; font-size: clamp(1.9rem,3vw,2.8rem); font-weight: 700; color: #1E2E1F; line-height: 1.25; margin-bottom: 2rem; }
.sec-h.white { color: #fff; }
.sec-h .hl { color: #CF7C07; }
.hl-light { color: #F5D08A; }
.sec-center { text-align: center; }

/* Quick steps */
.steps-visual { display: grid; grid-template-columns: repeat(4,1fr); gap: 1.5rem; margin-top: 2.5rem; position: relative; }
.step-card { background: rgba(255,255,255,.85); border: 1px solid rgba(207,124,7,.25); border-radius: 20px; padding: 2rem 1.4rem; text-align: center; transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .3s; position: relative; }
.step-card:hover { transform: translateY(-10px); box-shadow: 0 20px 40px rgba(30,86,82,.15); background: #fff; }
.step-num-big { font-family: 'Cormorant Garamond',serif; font-size: 4rem; font-weight: 700; color: rgba(207,124,7,.15); line-height: 1; position: absolute; top: .5rem; right: 1rem; }
.step-ico { font-size: 2.5rem; margin-bottom: .8rem; animation: floatIco 3s ease-in-out infinite; }
.step-card:nth-child(2) .step-ico { animation-delay: .5s; }
.step-card:nth-child(3) .step-ico { animation-delay: 1s; }
.step-card:nth-child(4) .step-ico { animation-delay: 1.5s; }
@keyframes floatIco { 0%,100%{transform:translateY(0)} 50%{transform:translateY(-8px)} }
.step-t { font-size: 1rem; font-weight: 700; color: #065F46; margin-bottom: .5rem; }
.step-d { font-size: .85rem; color: #5C6765; line-height: 1.6; }

/* Accordion */
.acc-list { display: flex; flex-direction: column; gap: .75rem; }
.acc-item { background: #F9F6F1; border-radius: 16px; border: 1.5px solid #EAE6E0; overflow: hidden; transition: border-color .3s, box-shadow .3s; cursor: pointer; }
.acc-item.open { border-color: #F5D08A; box-shadow: 0 8px 28px rgba(207,124,7,.1); background: #fff; }
.acc-hd { padding: 1.2rem 1.5rem; display: flex; align-items: center; justify-content: space-between; user-select: none; }
.acc-left { display: flex; align-items: center; gap: 1rem; }
.acc-num { font-size: .75rem; font-weight: 800; color: #A8B9AA; min-width: 28px; }
.acc-ico-wrap { width: 40px; height: 40px; border-radius: 10px; display: flex; align-items: center; justify-content: center; font-size: 1.2rem; flex-shrink: 0; }
.acc-t { font-weight: 700; color: #1E2E1F; font-size: 1rem; }
.acc-chevron { font-size: 1.4rem; color: #A8B9AA; transition: transform .35s cubic-bezier(.22,1,.36,1), color .2s; }
.acc-chevron.rotated { transform: rotate(90deg); color: #065F46; }
.acc-body { padding: 0 1.5rem 1.5rem 4.5rem; }
.acc-text { font-size: .9rem; color: #6A7B6B; line-height: 1.75; margin-bottom: 1rem; }
.acc-steps { display: flex; flex-direction: column; gap: .5rem; margin-bottom: 1rem; }
.acc-step-item { display: flex; align-items: center; gap: .75rem; font-size: .88rem; color: #2D3A2E; }
.acc-step-dot { width: 8px; height: 8px; border-radius: 50%; flex-shrink: 0; }
.acc-tip { background: #FDF6EC; border: 1px solid #F5D08A; border-radius: 10px; padding: .75rem 1rem; font-size: .85rem; color: #7A5200; display: flex; align-items: flex-start; gap: .5rem; }
.tip-ico { flex-shrink: 0; }
.slide-down-enter-active { transition: all .5s cubic-bezier(.22,1,.36,1); }
.slide-down-leave-active { transition: all .35s ease; }
.slide-down-enter-from, .slide-down-leave-to { opacity: 0; transform: translateY(-8px); }

/* Rules */
.rules-grid { display: grid; grid-template-columns: repeat(3,1fr); gap: 1.25rem; }
.rule-card { background: #fff; border-radius: 18px; padding: 1.8rem 1.5rem; border: 1px solid #EAE6E0; position: relative; overflow: hidden; transition: transform .4s cubic-bezier(.22,1,.36,1), box-shadow .4s; }
.rule-card:hover { transform: translateY(-8px); box-shadow: 0 20px 40px rgba(45,95,58,.1); }
.rule-bar { position: absolute; bottom: 0; left: 0; right: 0; height: 4px; background: var(--accent); transform: scaleX(0); transform-origin: left; transition: transform .4s ease; }
.rule-card:hover .rule-bar { transform: scaleX(1); }
.rule-ico { font-size: 2rem; margin-bottom: .8rem; }
.rule-t { font-weight: 700; color: #1E2E1F; font-size: 1rem; margin-bottom: .5rem; }
.rule-d { font-size: .88rem; color: #6A7B6B; line-height: 1.65; }

/* Tips */
.tips-grid { display: grid; grid-template-columns: repeat(4,1fr); gap: 1.2rem; margin-top: 2rem; }
.tip-card { background: rgba(255,255,255,.08); border: 1px solid rgba(255,255,255,.15); border-radius: 18px; padding: 1.6rem 1.3rem; backdrop-filter: blur(10px); transition: transform .4s cubic-bezier(.22,1,.36,1), background .3s; position: relative; overflow: hidden; }
.tip-card:hover { transform: translateY(-8px); background: rgba(255,255,255,.14); }
.tip-num { font-family: 'Cormorant Garamond',serif; font-size: 3.5rem; font-weight: 700; color: rgba(255,255,255,.08); line-height: 1; position: absolute; top: .5rem; right: .8rem; }
.tip-ico { font-size: 2rem; margin-bottom: .7rem; }
.tip-t { font-size: .95rem; font-weight: 700; color: #fff; margin-bottom: .4rem; }
.tip-d { font-size: .82rem; color: rgba(255,255,255,.65); line-height: 1.6; }

/* FAQ */
.faq-grid { display: grid; grid-template-columns: repeat(2,1fr); gap: 1.25rem; }
.faq-card { background: #F9F6F1; border-radius: 16px; padding: 1.6rem 1.5rem; border: 1px solid #EAE6E0; transition: transform .35s cubic-bezier(.22,1,.36,1), box-shadow .35s, border-color .3s; }
.faq-card:hover { transform: translateY(-5px); box-shadow: 0 12px 32px rgba(207,124,7,.1); border-color: #F5D08A; }
.faq-q { font-weight: 700; color: #1E2E1F; font-size: .98rem; margin-bottom: .6rem; display: flex; align-items: flex-start; gap: .5rem; }
.faq-q::before { content: '?'; width: 22px; height: 22px; border-radius: 50%; background: #065F46; color: #fff; font-size: .7rem; font-weight: 800; display: flex; align-items: center; justify-content: center; flex-shrink: 0; margin-top: 1px; }
.faq-a { font-size: .88rem; color: #6A7B6B; line-height: 1.7; padding-left: 1.7rem; }

/* CTA */
.cta-section { background: linear-gradient(135deg,#065F46,#047857,#065F46); padding: 5rem 2rem; text-align: center; position: relative; overflow: hidden; }
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
  .steps-visual,.tips-grid{grid-template-columns:repeat(2,1fr)}
  .rules-grid,.faq-grid{grid-template-columns:1fr}
  .footer-top{grid-template-columns:1fr 1fr}
}
@media(max-width:600px){
  .steps-visual,.tips-grid{grid-template-columns:1fr}
  .nav-links{display:none}
}
</style>


