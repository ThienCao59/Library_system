<template>
  <div class="dashboard-container">
    <!-- Route Protection Overlay -->
    <div v-if="!isAuthorized" class="access-denied">
      <div class="denied-card">
        <div class="denied-icon"><LockOutlined /></div>
        <h2>Access Denied</h2>
        <p>{{ t('auto.txt_543bb199') }}</p>
        <a-button type="primary" class="btn-home" @click="goHome">{{ t('auto.txt_d66e5b4e') }}</a-button>
      </div>
    </div>

    <template v-else>
      <!-- LEFT SIDEBAR (Perfect brand alignment: Deep Academic Emerald Teal #114B44) -->
      <aside class="dashboard-sidebar">
        <div class="sidebar-brand">
          <div class="brand-icon">
            <ReadOutlined style="font-size: 26px; color: #ffffff;" />
          </div>
          <div class="brand-text">
            <span class="brand-name">SmartLib</span>
            <span class="brand-sub">Library System</span>
          </div>
        </div>

        <!-- Dynamic Sidebar Menu based on Role -->
        <nav class="sidebar-menu">
          <!-- ADMIN MENU -->
          <template v-if="currentRole === 'Admin'">
            <div class="menu-group-label">{{ t("dashboard.menu.admin_system") }}</div>
            <a class="menu-item" :class="{ active: currentTab === 'overview' }" @click="switchTab('overview')">
              <span class="icon"><DashboardOutlined /></span> {{ t('sidebar.dashboard') }}
            </a>
            <a class="menu-item" :class="{ active: currentTab === 'books' }" @click="switchTab('books')">
              <span class="icon"><BookOutlined /></span> {{ t('sidebar.books') }}
            </a>
            <a class="menu-item" :class="{ active: currentTab === 'rules' }" @click="switchTab('rules')">
              <span class="icon"><SettingOutlined /></span> {{ t('sidebar.loans') }}
            </a>
            <div class="menu-submenu">
              <a
                class="menu-item submenu-toggle"
                :class="{ active: isUsersTab }"
                @click="usersMenuOpen = !usersMenuOpen"
              >
                <span class="icon"><TeamOutlined /></span>
                <span class="submenu-label">{{ t('sidebar.accounts') }}</span>
                <DownOutlined class="submenu-chevron" :class="{ open: usersMenuOpen }" />
              </a>
              <transition name="submenu-slide">
                <div v-show="usersMenuOpen" class="submenu-items">
                  <a class="menu-subitem" :class="{ active: currentTab === 'readers-list' }" @click="switchTab('readers-list')">
                    {{ t('sidebar.readers') }}
                  </a>
                  <a class="menu-subitem" :class="{ active: currentTab === 'staff-list' }" @click="switchTab('staff-list')">
                    {{ t('sidebar.staff') }}
                  </a>
                </div>
              </transition>
            </div>
            <a class="menu-item" :class="{ active: currentTab === 'cards-list' }" @click="switchTab('cards-list')">
              <span class="icon"><IdcardOutlined /></span> {{ t('sidebar.cards') }}
            </a>
            <a class="menu-item" :class="{ active: currentTab === 'reviews' }" @click="switchTab('reviews')">
              <span class="icon"><StarOutlined /></span> Quản lý đánh giá
            </a>
            <div class="menu-submenu">
              <a
                class="menu-item submenu-toggle"
                :class="{ active: isFinanceTab }"
                @click="financeMenuOpen = !financeMenuOpen"
              >
                <span class="icon"><DollarOutlined /></span>
                <span class="submenu-label">{{ t('sidebar.finance') }}</span>
                <DownOutlined class="submenu-chevron" :class="{ open: financeMenuOpen }" />
              </a>
              <transition name="submenu-slide">
                <div v-show="financeMenuOpen" class="submenu-items">
                  <a class="menu-subitem" :class="{ active: currentTab === 'finance-revenue' }" @click="switchTab('finance-revenue')">
                    {{ t('sidebar.revenue') }}
                  </a>
                  <a class="menu-subitem" :class="{ active: currentTab === 'finance-penalty' }" @click="switchTab('finance-penalty')">
                    {{ t('sidebar.penalty') }}
                  </a>
                </div>
              </transition>
            </div>
            <a class="menu-item" :class="{ active: currentTab === 'system-logs' }" @click="switchTab('system-logs')">
              <span class="icon"><HistoryOutlined /></span> {{ t('sidebar.logs') }}
            </a>
            <a class="menu-item" :class="{ active: currentTab === 'settings' }" @click="switchTab('settings')">
              <span class="icon"><SettingOutlined /></span> {{ t('sidebar.settings') }}
            </a>
          </template>

          <!-- LIBRARIAN MENU -->
          <template v-if="currentRole === 'Librarian'">
            <div class="menu-group-label">{{ t("dashboard.menu.librarian_system") }}</div>
            <a class="menu-item" :class="{ active: currentTab === 'rules' }" @click="switchTab('rules')">
              <span class="icon"><SyncOutlined /></span> {{ t('auto.txt_013590fd') }}
            </a>
            <div class="menu-submenu">
              <a
                class="menu-item submenu-toggle"
                :class="{ active: currentTab === 'finance-revenue' || currentTab === 'finance-penalty' }"
                @click="financeMenuOpen = !financeMenuOpen"
              >
                <span class="icon"><DollarOutlined /></span>
                <span class="submenu-label">{{ t("dashboard.menu.finance") }}</span>
                <DownOutlined class="submenu-chevron" :class="{ open: financeMenuOpen }" />
              </a>
              <transition name="submenu-slide">
                <div v-show="financeMenuOpen" class="submenu-items">
                  <a class="menu-subitem" :class="{ active: currentTab === 'finance-revenue' }" @click="switchTab('finance-revenue')">
                    Doanh thu
                  </a>
                  <a class="menu-subitem" :class="{ active: currentTab === 'finance-penalty' }" @click="switchTab('finance-penalty')">
                    {{ t('auto.txt_4f086c5d') }}
                  </a>
                </div>
              </transition>
            </div>
          </template>
        </nav>

        <div class="sidebar-footer">
          <div class="role-badge-container">
            <span class="role-tag" :class="currentRole.toLowerCase()">{{ currentRole }} Portal</span>
          </div>
          <button class="btn-logout" @click="handleLogout">
            <span class="logout-icon"><LogoutOutlined /></span> Logout
          </button>
        </div>
      </aside>

      <!-- MAIN CONTENT AREA (Harmonized background aligned with system Ivory #FAF9F6) -->
      <main class="dashboard-main">
        <!-- TOP BAR -->
        <header
          v-if="currentRole === 'Admin' && currentTab === 'overview'"
          class="dashboard-topbar"
        >
          <div v-if="currentTab === 'overview'" class="welcome-text">
            <h2>Welcome {{ currentUser }} !</h2>
          </div>
          <div v-else class="topbar-page-spacer"></div>
          <div class="topbar-actions">
            <template v-if="currentTab === 'overview'">
            <div class="spotlight-container" style="position: relative;">
              <div class="search-box-pill" :class="{ 'spotlight-active': showSpotlight && globalSearchQuery }">
                <span class="search-icon" @click="handleGlobalSearch" style="cursor: pointer;"><SearchOutlined /></span>
                <input 
                  v-model="globalSearchQuery" 
                  type="text" 
                  :placeholder="t('dash.search.placeholder')" 
                  @focus="showSpotlight = true"
                  @blur="showSpotlight = false"
                  @keyup.enter="handleGlobalSearch" 
                />
              </div>
              
              <!-- Spotlight Dropdown -->
              <div v-if="globalSearchQuery && showSpotlight" class="spotlight-dropdown">
                <div v-if="spotlightResults.length === 0" class="spotlight-empty">
                  <FrownOutlined style="margin-right: 4px;" /> {{ t('auto.txt_575a6091') }}
                </div>
                <div v-else class="spotlight-results">
                  <div class="spotlight-group" v-if="spotlightReaders.length">
                    <div class="spotlight-group-title">{{ t('auto.txt_8f69d55b') }}</div>
                    <div class="spotlight-item" v-for="r in spotlightReaders" :key="r.item.Id || r.item.id" @mousedown="handleSpotlightSelect(r)">
                      <div class="sl-icon teal"><UserOutlined /></div>
                      <div class="sl-info">
                        <div class="sl-title">{{ r.title }}</div>
                        <div class="sl-desc">{{ r.desc }}</div>
                      </div>
                    </div>
                  </div>
                  
                  <div class="spotlight-group" v-if="spotlightCards.length">
                    <div class="spotlight-group-title">{{ t('auto.txt_cd99aa1d') }}</div>
                    <div class="spotlight-item" v-for="c in spotlightCards" :key="c.item.CardNumber || c.item.cardNumber" @mousedown="handleSpotlightSelect(c)">
                      <div class="sl-icon gold"><IdcardOutlined /></div>
                      <div class="sl-info">
                        <div class="sl-title">{{ c.title }}</div>
                        <div class="sl-desc">{{ c.desc }}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="action-icon-btn theme-toggle" @click="switchTab('settings')" :title="t('auto.txt_c77a00e8')"><SettingOutlined /></div>
            </template>
            <a-popover
              v-model:open="showNotifications"
              trigger="click"
              placement="bottomRight"
              overlay-class-name="notification-popover"
              @openChange="onNotificationsOpen"
            >
              <template #content>
                <div class="notification-panel">
                  <div class="notification-panel-header">
                    <div>
                      <span>{{ t('auto.txt_2268a682') }}</span>
                      <p class="notification-panel-sub">{{ t('auto.txt_48ac043b') }}</p>
                    </div>
                    <a-button type="link" size="small" @click="fetchNotifications">{{ t('dashboard.overview.refresh') }}</a-button>
                  </div>
                  <a-spin :spinning="notificationsLoading">
                    <div v-if="!notifications.length" class="notification-empty">
                      <BellOutlined /> {{ t('auto.txt_1e892958') }}
                    </div>
                    <div v-else class="notification-list">
                      <button
                        v-for="item in notifications"
                        :key="item.id"
                        type="button"
                        class="notification-card"
                        :class="'notification-card--' + item.category"
                        @click="handleNotificationClick(item)"
                      >
                        <div class="notification-card-content">
                          <div class="notification-card-top">
                            <span class="notification-card-title">{{ item.title }}</span>
                            <time class="notification-card-time">{{ item.timeLabel }}</time>
                          </div>
                          <p class="notification-card-desc">{{ item.message }}</p>
                          <span v-if="item.actionTab" class="notification-card-action">{{ t('auto.txt_4f14c3fa') }}</span>
                        </div>
                      </button>
                    </div>
                  </a-spin>
                </div>
              </template>
              <div class="action-icon-btn notifications" @click.stop>
                <BellOutlined />
                <span v-if="notifications.length" class="notification-dot">
                  {{ notifications.length > 9 ? '9+' : notifications.length }}
                </span>
              </div>
            </a-popover>
            <div
              class="action-icon-btn lang-toggle"
              @click="i18n.setLocale(i18n.locale === 'vi' ? 'en' : 'vi')"
              :title="i18n.locale === 'vi' ? 'Switch to English' : 'Chuyển sang Tiếng Việt'"
              style="font-size:13px;font-weight:700;min-width:44px;letter-spacing:0.5px;"
            >
              {{ i18n.locale === 'vi' ? '🇬🇧 EN' : '🇻🇳 VI' }}
            </div>
            <div class="topbar-user-info">
              <div class="topbar-avatar">
                <img v-if="settings.adminAvatar" :src="settings.adminAvatar" style="width:100%;height:100%;object-fit:cover;border-radius:50%;" alt="avatar" />
                <UserOutlined v-else />
              </div>
              <div class="topbar-user-detail">
                <span class="topbar-username">{{ currentUser }}</span>
                <span class="topbar-role">{{ currentRole }}</span>
              </div>
            </div>
          </div>
        </header>

        <!-- ==================== 1. ADMIN - OVERVIEW TAB ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'overview'">
          <div class="tab-content animate-fade-in dashboard-overview">
            <div class="overview-cards-row">
              <div class="ov-card brand-border clickable" @click="openDetailModal('readers')">
                <div class="ov-icon-wrapper teal-bg"><TeamOutlined /></div>
                <div class="ov-info">
                  <div class="ov-value">{{ getUserStat('TotalUsers') }}</div>
                  <div class="ov-label">{{ t('dashboard.overview.total_readers') }}</div>
                </div>
              </div>

              <div class="ov-card brand-border clickable" @click="openDetailModal('cards')">
                <div class="ov-icon-wrapper gold-bg"><IdcardOutlined /></div>
                <div class="ov-info">
                  <div class="ov-value">{{ getCardStat('TotalCards') }}</div>
                  <div class="ov-label">{{ t('dashboard.overview.total_cards') }}</div>
                </div>
              </div>

              <div class="ov-card brand-border clickable" @click="switchTab('rules')">
                <div class="ov-icon-wrapper teal-bg"><SyncOutlined /></div>
                <div class="ov-info">
                  <div class="ov-value">{{ circulationStats.totalBorrows }}</div>
                  <div class="ov-label">{{ t('dashboard.overview.total_borrows') }}</div>
                </div>
              </div>

              <div class="ov-card gold-highlight clickable" @click="switchTab('books')">
                <div class="ov-icon-wrapper gold-bg"><ReadOutlined /></div>
                <div class="ov-info">
                  <div class="ov-value">{{ totalBooks }}</div>
                  <div class="ov-label">{{ t('dashboard.overview.total_books') }}</div>
                </div>
                <span class="info-tooltip" @click.stop="message.info('Tổng số lượng cuốn sách hiện có (từ Catalog Service)')">i</span>
              </div>
            </div>

            <div class="dashboard-main-grid">
              <div class="white-panel chart-card chart-card--wide">
                <div class="chart-card-header">
                  <h4><SyncOutlined />{{ t('dashboard.overview.book_borrows') }}</h4>
                  <span class="chart-card-hint">{{ t('dashboard.overview.by_month') }}</span>
                </div>
                <apexchart type="bar" height="280" :options="userGrowthOptions" :series="userGrowthSeries"></apexchart>
              </div>

              <div class="white-panel chart-card">
                <div class="chart-card-header">
                  <h4><IdcardOutlined />{{ t('dashboard.overview.card_statistics') }}</h4>
                  <a class="chart-link" @click="openDetailModal('cards')">{{ t('dashboard.overview.details') }}</a>
                </div>
                <div class="donut-wrap">
                  <apexchart type="donut" width="260" :options="activityOptions" :series="activitySeries"></apexchart>
                </div>
                <div class="card-stat-mini">
                  <span><i class="dot dot-active"></i> {{ t('dashboard.overview.active_cards_lbl') }} {{ getCardStat('ActiveCards') }}</span>
                  <span><i class="dot dot-locked"></i> {{ t('dashboard.overview.locked_cards_lbl') }} {{ getCardStat('LockedCards') }}</span>
                  <span><i class="dot dot-expired"></i> {{ t('dashboard.overview.expired_cards_lbl') }} {{ getCardStat('ExpiredCards') }}</span>
                </div>
              </div>

              <div class="white-panel chart-card book-category-card">
                <div class="chart-card-header">
                  <h4><ReadOutlined />{{ t('dashboard.overview.book_categories') }}</h4>
                  <span class="chart-card-hint">{{ t('dashboard.overview.by_category') }}</span>
                </div>
                <div class="book-category-layout">
                  <div class="book-category-chart-wrap">
                    <apexchart
                      type="donut"
                      width="280"
                      height="280"
                      :options="bookCategoryDonutOptions"
                      :series="bookCategoryDonutSeries"
                    />
                  </div>
                  <div v-if="booksCategoriesData.length" class="book-category-legend">
                    <div
                      v-for="(item, index) in booksCategoriesData"
                      :key="item[0]"
                      class="book-category-legend-item"
                    >
                      <span
                        class="book-category-legend-dot"
                        :style="{ backgroundColor: bookCategoryColors[index % bookCategoryColors.length] }"
                      />
                      <span class="book-category-legend-name" :title="item[0]">{{ item[0] }}</span>
                      <span class="book-category-legend-count">{{ item[1] }}</span>
                    </div>
                  </div>
                  <div v-else class="book-category-empty">{{ t('dashboard.overview.no_categories') }}</div>
                </div>
              </div>

              <div class="white-panel top-books-panel chart-card">
                <div class="top-books-header">
                  <div>
                    <h4><BookOutlined />{{ t('dashboard.overview.top_borrowed_books') }}</h4>
                    <p class="top-books-sub">{{ t('dashboard.overview.popularity_ranking') }}</p>
                  </div>
                </div>
                <div v-if="circulationStats.topBorrowedBooks && circulationStats.topBorrowedBooks.length" class="top-books-list">
                  <div
                    v-for="(book, index) in circulationStats.topBorrowedBooks.slice(0, 5)"
                    :key="index"
                    class="top-book-item"
                    :class="'rank-' + Math.min(Number(index) + 1, 4)"
                  >
                    <div class="top-book-rank">{{ Number(index) + 1 }}</div>
                    <div class="top-book-content">
                      <div class="top-book-row">
                        <span class="top-book-name">{{ book.bookName }}</span>
                        <span class="top-book-count">{{ book.borrowCount }} lượt</span>
                      </div>
                      <div class="top-book-track">
                        <div class="top-book-bar" :style="{ width: Math.max(book.percentage, 8) + '%' }"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div v-else class="top-books-empty">
                  <BookOutlined />
                  <p>{{ t('dashboard.overview.no_data') }}</p>
                </div>
              </div>
            </div>

            <!-- {{ t('dashboard.overview.system_logs') }} trên Dashboard -->
            <div class="white-panel dashboard-log-panel">
              <div class="dashboard-log-header">
                <div>
                  <h4><HistoryOutlined />{{ t('dashboard.overview.system_logs') }}</h4>
                  <p class="dashboard-log-sub">{{ t('dashboard.overview.logs_desc') }}</p>
                </div>
                <div class="dashboard-log-actions">
                  <a-button type="link" size="small" :loading="activityLogsLoading" @click="fetchActivityLogs">
                    <SyncOutlined />{{ t('dashboard.overview.refresh') }}</a-button>
                  <a-button type="link" size="small" @click="switchTab('system-logs')">{{ t('dashboard.overview.view_all') }}</a-button>
                </div>
              </div>
              <a-spin :spinning="activityLogsLoading">
                <div v-if="!dashboardActivityLogs.length" class="activity-log-empty">{{ t('dashboard.overview.no_logs') }}</div>
                <div v-else class="system-log-table dashboard-log-table">
                  <div
                    v-for="item in dashboardActivityLogs"
                    :key="'dash-' + item.id"
                    class="system-log-row"
                  >
                    <div class="system-log-icon" :class="item.category">
                      <component :is="item.icon" />
                    </div>
                    <div class="system-log-body">
                      <div class="system-log-title">{{ item.title }}</div>
                      <div class="system-log-desc">{{ item.message }}</div>
                    </div>
                    <div class="system-log-time">{{ item.timeLabel }}</div>
                  </div>
                </div>
              </a-spin>
            </div>
          </div>
        </template>

        <!-- ==================== SYSTEM LOGS TAB ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'system-logs'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row mgmt-hero-header">
              <div class="title-with-subtitle" style="margin: 0;">
                <h3><HistoryOutlined /> {{ t('dash.logs.title') }}</h3>
                <p class="subtitle-text">{{ t('dash.logs.sub') }}</p>
              </div>
              <a-button type="primary" class="btn-hero-action" @click="fetchActivityLogs">
                <SyncOutlined /> {{ t('btn.refresh') }}
              </a-button>
            </div>

            <div class="white-panel system-log-filters">
              <span class="filter-label">{{ t('dash.logs.filter') }}</span>
              <select v-model="logFilterType" class="custom-select-pill">
                <option value="all">{{ t('dash.logs.all') }}</option>
                <option value="auth">{{ t('dash.logs.auth') }}</option>
                <option value="user">{{ t('dash.logs.user') }}</option>
                <option value="book">{{ t('dash.logs.book') }}</option>
                <option value="circulation">{{ t('dash.logs.circulation') }}</option>
              </select>
            </div>

            <a-spin :spinning="activityLogsLoading">
              <div v-if="!filteredActivityLogs.length" class="activity-log-empty" style="padding: 48px;">
                {{ t('dash.logs.empty') }}
              </div>
              <div v-else class="system-log-table white-panel">
                <div
                  v-for="item in paginatedActivityLogs"
                  :key="item.id"
                  class="system-log-row"
                >
                  <div class="system-log-icon" :class="item.category">
                    <component :is="item.icon" />
                  </div>
                  <div class="system-log-body">
                    <div class="system-log-title">{{ item.title }}</div>
                    <div class="system-log-desc">{{ item.message }}</div>
                    <div class="system-log-meta">{{ item.eventType }}</div>
                  </div>
                  <div class="system-log-time">{{ item.timeLabel }}</div>
                </div>
              </div>
              <div v-if="filteredActivityLogs.length" class="system-log-pagination">
                <a-pagination
                  v-model:current="activityLogPage"
                  v-model:pageSize="activityLogPageSize"
                  :total="activityLogTotal"
                  :show-size-changer="false"
                  :show-total="(total: number) => `Tổng ${total} bản ghi · ${activityLogPageSize} / trang`"
                />
              </div>
            </a-spin>

            <!-- Footer for System Logs Tab -->
            <div style="text-align: center; padding: 40px 24px 20px; color: rgba(0, 0, 0, 0.45); font-size: 14px;">
              SmartLib Library Management System ©2026 Created by Admin
            </div>
          </div>
        </template>

        <!-- ==================== 7. ADMIN - READERS MANAGEMENT ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'readers-list'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row mgmt-hero-header">
              <div class="title-with-subtitle" style="margin: 0;">
                <h3><TeamOutlined /> {{ t('dash.readers.title') }}</h3>
                <p class="subtitle-text">{{ t('dash.readers.sub') }}</p>
              </div>
              <div style="display: flex; gap: 12px; align-items: center; flex-wrap: wrap;">
                <span class="stats-pill" style="background: rgba(255,255,255,0.1); color: white; border: 1px solid rgba(255,255,255,0.2); font-weight: 600;">{{ t('dash.readers.total') }}: {{ filteredReaders.length }}</span>
                <input type="file" ref="fileInputRef" accept=".xlsx" style="display: none;" @change="importReadersFromExcel" />
                <button class="btn-action-pastel" style="background: #217346; color: white; border-color: #217346; font-weight: 700; height: auto; padding: 8px 16px; border-radius: 20px;" @click="triggerFileUpload">
                  <FileExcelOutlined /> {{ t('btn.import') }}
                </button>
                <button class="btn-action-pastel" style="background: #ffffff; color: #1E5652; border-color: #ffffff; font-weight: 700; height: auto; padding: 8px 16px; border-radius: 20px;" @click="exportReadersToExcel">
                  <DownloadOutlined /> {{ t('btn.export') }}
                </button>
              </div>
            </div>

            <!-- Bulk action bar -->
            <div v-if="selectedReaderIds.length > 0" class="bulk-action-bar">
              <span class="bulk-count">Đã chọn {{ selectedReaderIds.length }} độc giả</span>
              <a-popconfirm
                :title="`Xóa ${selectedReaderIds.length} độc giả đã chọn?`"
                ok-text="Xóa"
                cancel-text="Hủy"
                ok-type="danger"
                @confirm="bulkDeleteReaders"
              >
                <a-button danger size="small" :loading="bulkDeleteReadersLoading">
                  <DeleteOutlined /> {{ t('auto.txt_2e6f0d86') }}
                </a-button>
              </a-popconfirm>
              <a-button size="small" @click="selectedReaderIds = []">{{ t('auto.txt_3651c4d7') }}</a-button>
            </div>


            <div v-if="detailLoading" class="page-loading-spinner-wrapper">
              <div class="spinner-spinner"></div>
              <p>{{ t('auto.txt_e293cd50') }}</p>
            </div>

            <div v-else-if="filteredReaders.length === 0" class="empty-state-card">
              <div class="empty-icon"><FrownOutlined /></div>
              <h4>{{ t('auto.txt_0e22f7e7') }}</h4>
              <p>{{ t('auto.txt_58ac4f04') }}</p>
            </div>

            <!-- Readers Table -->
            <div v-else class="white-panel table-panel" style="padding: 0; overflow: hidden;">
              <a-table
                :columns="readersColumns"
                :data-source="filteredReaders"
                :row-key="(record: any) => record.Id || record.id"
                :row-selection="readerRowSelection"
                :scroll="{ x: 1200 }"
                :pagination="{ current: readersCurrentPage, pageSize: readersPageSize, showSizeChanger: true, pageSizeOptions: ['10', '15', '20', '50'], onChange: onReadersPageChange }"
                class="ant-table-striped"
              >
                <template #customFilterDropdown="{ setSelectedKeys, selectedKeys, confirm, clearFilters, column }">
                  <div style="padding: 8px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); background: white;">
                    <a-input
                      ref="searchInput"
                      :placeholder="`Tìm kiếm ${column.title}`"
                      :value="selectedKeys[0]"
                      style="width: 200px; margin-bottom: 8px; display: block; border-radius: 6px;"
                      @change="(e: any) => setSelectedKeys(e.target.value ? [e.target.value] : [])"
                      @pressEnter="confirm()"
                    />
                    <div style="display: flex; gap: 8px;">
                      <a-button
                        type="primary"
                        size="small"
                        style="flex: 1; border-radius: 6px; display: flex; justify-content: center; align-items: center;"
                        @click="confirm()"
                      >
                        <template #icon><SearchOutlined /></template>
                        {{ t('auto.txt_92849db7') }}
                      </a-button>
                      <a-button size="small" style="flex: 1; border-radius: 6px;" @click="clearFilters(); confirm()">
                        {{ t('auto.txt_682697e7') }}
                      </a-button>
                    </div>
                  </div>
                </template>
                <template #customFilterIcon="{ filtered }">
                  <search-outlined :style="{ color: filtered ? '#1a6b5e' : '#bfbfbf', fontSize: '14px' }" />
                </template>
                <template #bodyCell="{ column, record, index }">
                  <template v-if="column.key === 'stt'">
                    <span style="font-weight: 600; color: #6C7A78;">{{ (readersCurrentPage - 1) * readersPageSize + index + 1 }}</span>
                  </template>
                  <template v-else-if="column.key === 'fullName'">
                    <div style="display:flex; align-items:center; gap:8px;">
                      <div style="width:32px;height:32px;border-radius:50%;background:linear-gradient(135deg,#1a6b5e,#2a9d8f);display:flex;align-items:center;justify-content:center;color:white;font-size:14px;flex-shrink:0;"><UserOutlined /></div>
                      <span class="font-bold text-dark">{{ record.FullName || record.fullName }}</span>
                    </div>
                  </template>
                  <template v-else-if="column.key === 'username'">
                    <span style="color:#6b7280;">@{{ record.Username || record.username }}</span>
                  </template>
                  <template v-else-if="column.key === 'email'">
                    <span style="color:#6b7280;">{{ record.Email || record.email }}</span>
                  </template>
                  <template v-else-if="column.key === 'cardNumber'">
                    <span class="card-number-badge">{{ record.LibraryCard?.CardNumber || record.libraryCard?.cardNumber || 'Chưa cấp' }}</span>
                  </template>
                  <template v-else-if="column.key === 'status'">
                    <span class="status-badge" :class="(record.IsActive || record.isActive) ? 'instock' : 'outstock'">
                      {{ (record.IsActive || record.isActive) ? 'Hoạt động' : 'Bị khóa' }}
                    </span>
                  </template>
                  <template v-else-if="column.key === 'action'">
                    <div class="table-action-row">
                      <button class="btn-view-detail" @click="openReaderDrawer(record)"><EyeOutlined /> {{ t('btn.view') }}</button>
                      <button class="btn-action-small btn-edit-inline" @click="openEditReaderDrawer(record)">
                        <EditOutlined /> {{ t('btn.edit') }}
                      </button>
                      <button class="btn-action-small" :class="(record.IsActive || record.isActive) ? 'btn-lock' : 'btn-unlock'" @click="toggleUserStatus(record)">
                        <template v-if="record.IsActive || record.isActive"><LockOutlined /> {{ t('btn.lock') }}</template>
                        <template v-else><UnlockOutlined /> {{ t('btn.unlock') }}</template>
                      </button>
                    </div>
                  </template>
                </template>
              </a-table>
            </div>

            <!-- Footer for Readers Tab -->
            <div style="text-align: center; padding: 40px 24px 20px; color: rgba(0, 0, 0, 0.45); font-size: 14px;">
              SmartLib Library Management System ©2026 Created by Admin
            </div>
          </div>
        </template>

        <!-- READER DETAIL DRAWER -->

        <!-- EDIT READER DRAWER -->
        <a-drawer
          v-model:open="showEditReaderDrawer"
          :title="t('auto.txt_1663500a')"
          placement="right"
          :width="460"
          :closable="true"
          class="reader-detail-drawer"
        >
          <div class="drawer-profile-wrapper" style="padding: 24px;">
            <div style="background: linear-gradient(135deg, #1E5652 0%, #114B44 100%); border-radius: 12px; padding: 16px; margin-bottom: 24px; display: flex; align-items: center; gap: 16px;">
              <div style="width: 50px; height: 50px; border-radius: 50%; background: rgba(255,255,255,0.2); display: flex; align-items: center; justify-content: center; font-size: 22px; color: white;"><UserOutlined /></div>
              <div>
                <div style="color: white; font-weight: 700; font-size: 16px;">{{ editReaderForm.fullName || 'Độc giả' }}</div>
                <div style="color: rgba(255,255,255,0.65); font-size: 13px;">ID: {{ editReaderForm.id }}</div>
              </div>
            </div>
            <div class="form-group-custom">
              <label style="font-weight: 600; color: #374151; display: block; margin-bottom: 6px;">{{ t('auto.txt_2fc8766c') }}</label>
              <a-input v-model:value="editReaderForm.fullName" :placeholder="t('auto.txt_72a24184')" />
            </div>
            <div class="form-group-custom" style="margin-top: 16px;">
              <label style="font-weight: 600; color: #374151; display: block; margin-bottom: 6px;">{{ t('auto.txt_c732ee4e') }}</label>
              <a-input v-model:value="editReaderForm.email" :placeholder="t('auto.txt_9b3c16d5')" />
            </div>
            <div class="form-group-custom" style="margin-top: 16px;">
              <label style="font-weight: 600; color: #374151; display: block; margin-bottom: 6px;">{{ t('auto.txt_6de63f11') }}</label>
              <a-input v-model:value="editReaderForm.phoneNumber" :placeholder="t('auto.txt_0f1d3d42')" />
            </div>
            <div style="margin-top: 30px; display: flex; gap: 12px;">
              <a-button @click="showEditReaderDrawer = false" style="flex: 1; border-radius: 8px;">{{ t('auto.txt_1e405035') }}</a-button>
              <a-button type="primary" :loading="editReaderLoading" @click="saveReaderEdit" style="flex: 1; background: #1E5652; border-color: #1E5652; border-radius: 8px;">{{ t('auto.txt_512898d2') }}</a-button>
            </div>
          </div>
        </a-drawer>

        <!-- ==================== 8. ADMIN - CARDS MANAGEMENT ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'cards-list'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row mgmt-hero-header">
              <div class="title-with-subtitle" style="margin: 0;">
                <h3><IdcardOutlined /> {{ t('auto.txt_6c1dfe9d') }}</h3>
                <p class="subtitle-text">{{ t('auto.txt_f5a875da') }}</p>
              </div>
              <span class="stats-pill">Tổng cộng: {{ filteredCards.length }} thẻ</span>
            </div>

            <!-- Search and Filter Panel -->
            <div class="white-panel search-filter-panel brand-border">
              <div class="search-input-pill">
                <span class="search-icon"><SearchOutlined /></span>
                <input v-model="cardsSearchQuery" type="text" :placeholder="t('auto.txt_fe030e24')" />
              </div>
              <div class="filter-group">
                <span class="filter-label">{{ t('auto.txt_a6993480') }}</span>
                <select v-model="cardsStatusFilter" class="custom-select-pill">
                  <option value="all">{{ t('auto.txt_fd1c3a01') }}</option>
                  <option value="active">{{ t('auto.txt_d2b65230') }}</option>
                  <option value="locked">{{ t('auto.txt_d9344990') }}</option>
                  <option value="expired">{{ t('auto.txt_35a6b808') }}</option>
                </select>
              </div>
            </div>

            <div v-if="detailLoading" class="page-loading-spinner-wrapper">
              <div class="spinner-spinner"></div>
              <p>{{ t('auto.txt_934ac7a3') }}</p>
            </div>

            <div v-else-if="filteredCards.length === 0" class="empty-state-card">
              <div class="empty-icon"><FrownOutlined /></div>
              <h4>{{ t('auto.txt_49785065') }}</h4>
              <p>{{ t('auto.txt_58ac4f04') }}</p>
            </div>

            <div v-else class="cards-grid-layout">
              <div v-for="card in paginatedCards" :key="card.CardNumber || card.cardNumber" class="library-credit-card" :class="(card.Status || card.status).toLowerCase()">
                <!-- Front design of realistic credit card -->
                <div class="card-inner-glass">
                  <!-- Top Row: Logo and Status -->
                  <div class="card-header-row">
                    <span class="card-brand-logo"><ReadOutlined /> SmartLib</span>
                    <span class="card-status-dot-badge">
                      <span class="pulse-dot"></span>
                      {{ card.Status || card.status }}
                    </span>
                  </div>

                  <!-- Chip and Contactless -->
                  <div class="card-chip-row">
                    <div class="gold-chip"></div>
                    <span class="wifi-icon"><WifiOutlined /></span>
                  </div>

                  <!-- Card Number with nice wide spacing -->
                  <div class="card-number-display">
                    {{ formatCardNumber(card.CardNumber || card.cardNumber) }}
                  </div>

                  <!-- Bottom Details Row -->
                  <div class="card-details-row">
                    <div class="detail-col">
                      <span class="detail-lbl">CARD HOLDER</span>
                      <span class="detail-val">{{ card.OwnerName || card.ownerName || 'READ ONLY' }}</span>
                    </div>
                    <div class="detail-col text-right">
                      <span class="detail-lbl">EXPIRED</span>
                      <span class="detail-val">{{ formatDate(card.ExpiryDate || card.expiryDate) }}</span>
                    </div>
                  </div>
                </div>

                <!-- Action overlay or buttons displayed under the realistic card in the card container -->
                <div class="card-action-underlay">
                  <div class="issue-date-label">Ngày cấp: {{ formatDate(card.IssueDate || card.issueDate) }}</div>
                  <button class="btn-action-card-toggle" @click="toggleCardStatus(card)">
                    <template v-if="(card.Status || card.status) === 'Active'"><LockOutlined /> {{ t('auto.txt_e67c30d3') }}</template>
                    <template v-else><UnlockOutlined /> {{ t('auto.txt_f15279aa') }}</template>
                  </button>
                </div>
              </div>
            </div>
            <div class="pagination-wrapper" style="display: flex; justify-content: flex-end; padding: 20px 0;">
              <a-pagination v-model:current="cardsCurrentPage" v-model:pageSize="cardsPageSize" :total="filteredCards.length" show-size-changer />
            </div>

            <!-- Footer for Cards Tab -->
            <div style="text-align: center; padding: 40px 24px 20px; color: rgba(0, 0, 0, 0.45); font-size: 14px;">
              SmartLib Library Management System ©2026 Created by Admin
            </div>
          </div>
        </template>

        <!-- ==================== 9. ADMIN - STAFF MANAGEMENT ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'staff-list'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row mgmt-hero-header">
              <div class="title-with-subtitle" style="margin: 0;">
                <h3><TeamOutlined /> {{ t('auto.txt_15e23a99') }}</h3>
                <p class="subtitle-text">{{ t('auto.txt_8b7f26f2') }}</p>
              </div>
              <div style="display: flex; gap: 12px; align-items: center;">
                <span class="stats-pill">Tổng cộng: {{ staffList.length }} nhân viên</span>
                <button class="btn-action-pastel btn-hero-action" @click="showCreateStaffModal = true">
                  <PlusOutlined /> {{ t('auto.txt_e7d653f5') }}
                </button>
              </div>
            </div>

            <!-- Bulk action bar staff -->
            <div v-if="selectedStaffIds.length > 0" class="bulk-action-bar">
              <span class="bulk-count">Đã chọn {{ selectedStaffIds.length }} nhân viên</span>
              <a-popconfirm
                :title="`Xóa ${selectedStaffIds.length} nhân viên đã chọn? (Admin sẽ bị bỏ qua)`"
                ok-text="Xóa"
                cancel-text="Hủy"
                ok-type="danger"
                @confirm="bulkDeleteStaff"
              >
                <a-button danger size="small" :loading="bulkDeleteStaffLoading">
                  <DeleteOutlined /> {{ t('auto.txt_2e6f0d86') }}
                </a-button>
              </a-popconfirm>
              <a-button size="small" @click="selectedStaffIds = []">{{ t('auto.txt_3651c4d7') }}</a-button>
            </div>

            <div v-if="staffLoading" class="page-loading-spinner-wrapper">
              <div class="spinner-spinner"></div>
              <p>{{ t('auto.txt_ec89be8d') }}</p>
            </div>

            <div v-else-if="staffList.length === 0" class="empty-state-card">
              <div class="empty-icon"><FrownOutlined /></div>
              <h4>{{ t('auto.txt_404c8085') }}</h4>
              <p>Bấm "Tạo Thủ thư" để thêm tài khoản Librarian mới.</p>
            </div>

            <template v-else>

              <div class="white-panel table-panel" style="padding: 0; overflow: hidden;">
                <a-table
                  :columns="staffColumns"
                  :data-source="filteredStaffList"
                  :row-key="(record: any) => record.Id || record.id"
                  :row-selection="staffRowSelection"
                  :scroll="{ x: 1200 }"
                  :pagination="{ pageSize: 15, showSizeChanger: true, pageSizeOptions: ['10', '15', '20', '50'] }"
                  class="ant-table-striped"
                >
                  <template #customFilterDropdown="{ setSelectedKeys, selectedKeys, confirm, clearFilters, column }">
                    <div style="padding: 8px; border-radius: 8px; box-shadow: 0 4px 12px rgba(0,0,0,0.1); background: white;">
                      <a-input
                        ref="searchInput"
                        :placeholder="`Tìm kiếm ${column.title}`"
                        :value="selectedKeys[0]"
                        style="width: 200px; margin-bottom: 8px; display: block; border-radius: 6px;"
                        @change="(e: any) => setSelectedKeys(e.target.value ? [e.target.value] : [])"
                        @pressEnter="confirm()"
                      />
                      <div style="display: flex; gap: 8px;">
                        <a-button
                          type="primary"
                          size="small"
                          style="flex: 1; border-radius: 6px; display: flex; justify-content: center; align-items: center;"
                          @click="confirm()"
                        >
                          <template #icon><SearchOutlined /></template>
                          {{ t('auto.txt_92849db7') }}
                        </a-button>
                        <a-button size="small" style="flex: 1; border-radius: 6px;" @click="clearFilters(); confirm()">
                          {{ t('auto.txt_682697e7') }}
                        </a-button>
                      </div>
                    </div>
                  </template>
                  <template #customFilterIcon="{ filtered }">
                    <search-outlined :style="{ color: filtered ? '#1a6b5e' : '#bfbfbf', fontSize: '14px' }" />
                  </template>
                  <template #bodyCell="{ column, record, index }">
                    <template v-if="column.key === 'stt'">
                      <span style="font-weight: 600; color: #6C7A78;">{{ index + 1 }}</span>
                    </template>
                    <template v-else-if="column.key === 'fullName'">
                      <div style="display:flex; align-items:center; gap:8px;">
                        <div style="width:32px;height:32px;border-radius:50%;background:linear-gradient(135deg,#114B44,#1E5652);display:flex;align-items:center;justify-content:center;color:white;font-size:14px;flex-shrink:0;"><UserOutlined /></div>
                        <span class="font-bold text-dark">{{ record.FullName || record.fullName }}</span>
                      </div>
                    </template>
                    <template v-else-if="column.key === 'username'">
                      <span style="color:#6b7280;">@{{ record.Username || record.username }}</span>
                    </template>
                    <template v-else-if="column.key === 'email'">
                      <span style="color:#6b7280;">{{ record.Email || record.email }}</span>
                    </template>
                    <template v-else-if="column.key === 'role'">
                      <a-tag :color="(record.Role || record.role) === 'Admin' ? 'volcano' : 'geekblue'">
                        {{ record.Role || record.role }}
                      </a-tag>
                    </template>
                    <template v-else-if="column.key === 'isActive'">
                      <span class="status-badge" :class="(record.IsActive ?? record.isActive) ? 'instock' : 'outstock'">
                        {{ (record.IsActive ?? record.isActive) ? 'Hoạt động' : 'Bị khóa' }}
                      </span>
                    </template>
                    <template v-else-if="column.key === 'createdAt'">
                      <span style="color:#6b7280;">{{ formatDate(record.CreatedAt || record.createdAt) }}</span>
                    </template>
                    <template v-else-if="column.key === 'actions'">
                      <div class="table-action-row">
                        <button
                          v-if="(record.Role || record.role) === 'Librarian'"
                          class="btn-action-small btn-lock"
                          @click="demoteLibrarian(record)"
                        >
                          <StopOutlined /> {{ t('staff.demote') }}
                        </button>
                        <span v-else style="color:#9ca3af;font-size:12px;">—</span>
                      </div>
                    </template>
                  </template>
                </a-table>
              </div>
            </template>

            <div style="text-align: center; padding: 40px 24px 20px; color: rgba(0, 0, 0, 0.45); font-size: 14px;">
              SmartLib Library Management System ©2026 Created by Admin
            </div>
          </div>

          <a-modal
            v-model:open="showCreateStaffModal"
            :title="t('staff.modal.title')"
            @ok="createStaff"
            :confirm-loading="createStaffLoading"
            :ok-text="t('btn.create')"
            :cancel-text="t('btn.cancel')"
          >
            <a-form layout="vertical">
              <a-form-item label="Username" required>
                <a-input v-model:value="staffForm.username" />
              </a-form-item>
              <a-form-item label="Email" required>
                <a-input v-model:value="staffForm.email" />
              </a-form-item>
              <a-form-item :label="t('staff.full.name')" required>
                <a-input v-model:value="staffForm.fullName" />
              </a-form-item>
              <a-form-item :label="t('staff.phone')">
                <a-input v-model:value="staffForm.phoneNumber" />
              </a-form-item>
              <a-form-item :label="t('staff.password')" required :extra="t('staff.password.hint')">
                <a-input-password v-model:value="staffForm.password" placeholder="Vd: Abc@1234" />
              </a-form-item>
            </a-form>
          </a-modal>
        </template>

        <!-- ==================== 2. ADMIN - BOOKS TAB (N1) ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'books'">
          <div class="tab-content animate-fade-in" style="padding: 0; height: calc(100vh - 80px);">
            <iframe
              v-if="booksIframeSrc"
              :src="booksIframeSrc"
              style="width: 100%; height: 100%; border: none; border-radius: 12px;"
              title="Catalog Service N1"
            />
            <div v-else class="books-iframe-loading">
              <LoadingOutlined spin style="font-size: 28px; margin-right: 8px;" />
              {{ t('auto.txt_b25937fb') }}
            </div>
          </div>
        </template>

        <!-- ==================== 3. ADMIN - LOANS TAB (N2) ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'rules'">
          <div class="tab-content animate-fade-in" style="padding: 0; height: calc(100vh - 80px);">
            <iframe
              v-if="loansIframeSrc"
              :src="loansIframeSrc"
              style="width: 100%; height: 100%; border: none; border-radius: 12px;"
              :title="t('auto.txt_b1a6cf3b')"
            />
            <div v-else class="books-iframe-loading">
              <LoadingOutlined spin style="font-size: 28px; margin-right: 8px;" />
              {{ t('auto.txt_b25937fb') }}
            </div>
          </div>
        </template>

        <!-- ==================== REVIEWS TAB ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'reviews'">
          <div class="tab-content animate-fade-in" style="padding: 0; height: calc(100vh - 80px);">
            <iframe
              v-if="reviewsIframeSrc"
              :src="reviewsIframeSrc"
              style="width: 100%; height: 100%; border: none; border-radius: 12px;"
              title="Quản lý đánh giá"
            />
            <div v-else class="books-iframe-loading">
              <LoadingOutlined spin style="font-size: 28px; margin-right: 8px;" />
              Đang tải dữ liệu...
            </div>
          </div>
        </template>

        <!-- ==================== FINANCE - REVENUE TAB ==================== -->
        <template v-if="(currentRole === 'Admin' || currentRole === 'Librarian') && currentTab === 'finance-revenue'">
          <div class="tab-content animate-fade-in" style="padding: 0; height: calc(100vh - 80px);">
            <iframe
              v-if="paymentsIframeSrc"
              :src="paymentsIframeSrc"
              style="width: 100%; height: 100%; border: none; border-radius: 12px;"
              :title="t('auto.txt_14a2633f')"
            />
            <div v-else class="books-iframe-loading">
              <LoadingOutlined spin style="font-size: 28px; margin-right: 8px;" />
              {{ t('auto.txt_b25937fb') }}
            </div>
          </div>
        </template>

        <!-- ==================== FINANCE - PENALTY TAB ==================== -->
        <template v-if="(currentRole === 'Admin' || currentRole === 'Librarian') && currentTab === 'finance-penalty'">
          <div class="tab-content animate-fade-in" style="padding: 0; height: calc(100vh - 80px);">
            <iframe
              v-if="finesIframeSrc"
              :src="finesIframeSrc"
              style="width: 100%; height: 100%; border: none; border-radius: 12px;"
              :title="t('auto.txt_05b21c78')"
            />
            <div v-else class="books-iframe-loading">
              <LoadingOutlined spin style="font-size: 28px; margin-right: 8px;" />
              {{ t('auto.txt_b25937fb') }}
            </div>
          </div>
        </template>

        <!-- ==================== ADMIN - SETTINGS TAB ==================== -->
        <template v-if="currentRole === 'Admin' && currentTab === 'settings'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row mgmt-hero-header">
              <div class="title-with-subtitle" style="margin: 0;">
                <h3><SettingOutlined /> {{ t('auto.txt_c77a00e8') }}</h3>
                <p class="subtitle-text">{{ t('auto.txt_f894048f') }}</p>
              </div>
            </div>

            <div class="settings-grid">

              <!-- 1. Thông tin thư viện -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#E6F0EE"><BankOutlined style="color:#1E5652" /></div>
                  <div>
                    <h4>{{ t('auto.txt_c600a340') }}</h4>
                    <p>{{ t('auto.txt_8858c435') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_82609cef') }}</label>
                    <a-input v-model:value="settings.libraryName" placeholder="VD: Thư viện SmartLib" />
                  </div>
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_7db5b909') }}</label>
                    <a-input v-model:value="settings.libraryAddress" placeholder="VD: 3/2, Xuân Khánh, Ninh Kiều, Cần Thơ" />
                  </div>
                  <div class="settings-field-row two-col">
                    <div>
                      <label>{{ t('auto.txt_6de63f11') }}</label>
                      <a-input v-model:value="settings.libraryPhone" placeholder="VD: 1900 6789" />
                    </div>
                    <div>
                      <label>{{ t('auto.txt_c732ee4e') }}</label>
                      <a-input v-model:value="settings.libraryEmail" placeholder="VD: info@smartlib.net" />
                    </div>
                  </div>
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_0327b13e') }}</label>
                    <a-textarea v-model:value="settings.libraryDesc" :rows="3" :placeholder="t('auto.txt_9a1b7969')" />
                  </div>
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_2473120f') }}</label>
                    <div class="logo-upload-area">
                      <div class="logo-preview">
                        <ReadOutlined v-if="!settings.logoUrl" style="font-size:28px;color:#9ca3af" />
                        <img v-else :src="settings.logoUrl" alt="logo" style="width:48px;height:48px;object-fit:contain;" />
                      </div>
                      <input ref="logoInputRef" type="file" accept="image/*" style="display:none" @change="onLogoChange" />
                      <a-button class="btn-upload-logo" @click="(logoInputRef as HTMLInputElement)?.click()"><UploadOutlined /> {{ t('auto.txt_acfae9d2') }}</a-button>
                    </div>
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save" @click="saveSettings('library')">
                    <SaveOutlined /> {{ t('auto.txt_7c2e305f') }}
                  </a-button>
                </div>
              </div>

              <!-- 2. Tài khoản Admin -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#FFF4E5"><UserOutlined style="color:#CF7C07" /></div>
                  <div>
                    <h4>{{ t('auto.txt_fd51d996') }}</h4>
                    <p>{{ t('auto.txt_583282b8') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-field-row two-col">
                    <div>
                      <label>{{ t('auto.txt_623825de') }}</label>
                      <a-input v-model:value="settings.adminFullName" :placeholder="t('auto.txt_f200d91f')" />
                    </div>
                    <div>
                      <label>Email</label>
                      <a-input v-model:value="settings.adminEmail" placeholder="admin@smartlib.net" />
                    </div>
                  </div>
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_c76ba836') }}</label>
                    <div class="logo-upload-area">
                      <div class="logo-preview avatar-preview">
                        <span v-if="!settings.adminAvatar" style="font-size:22px;font-weight:700;color:#1E5652">{{ (settings.adminFullName || 'A').charAt(0).toUpperCase() }}</span>
                        <img v-else :src="settings.adminAvatar" alt="avatar" style="width:48px;height:48px;object-fit:cover;border-radius:50%;" />
                      </div>
                      <input ref="avatarInputRef" type="file" accept="image/*" style="display:none" @change="onAvatarChange" />
                      <a-button class="btn-upload-logo" @click="(avatarInputRef as HTMLInputElement)?.click()"><UploadOutlined /> {{ t('auto.txt_fcac0c11') }}</a-button>
                    </div>
                  </div>
                  <div class="settings-card-footer" style="border:none;padding:0">
                    <a-button type="primary" class="btn-settings-save" :loading="savingProfile" @click="saveSettings('admin')">
                      <SaveOutlined /> {{ t('auto.txt_7c2e305f') }}
                    </a-button>
                  </div>
                  <div class="settings-divider">{{ t('auto.txt_ff6fe7b1') }}</div>
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_d9487313') }}</label>
                    <a-input-password v-model:value="settings.currentPassword" :placeholder="t('auto.txt_22bfd9b8')" />
                  </div>
                  <div class="settings-field-row two-col">
                    <div>
                      <label>{{ t('auto.txt_ccef959a') }}</label>
                      <a-input-password v-model:value="settings.newPassword" :placeholder="t('auto.txt_ccef959a')" />
                    </div>
                    <div>
                      <label>{{ t('auto.txt_d393faae') }}</label>
                      <a-input-password v-model:value="settings.confirmPassword" :placeholder="t('auto.txt_eee7a769')" />
                    </div>
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save btn-danger-save" :loading="changingPassword" @click="handleChangePassword">
                    <LockOutlined /> {{ t('auto.txt_ff6fe7b1') }}
                  </a-button>
                </div>
              </div>

              <!-- 3. Cài đặt thẻ thư viện -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#EBF4FF"><IdcardOutlined style="color:#3182CE" /></div>
                  <div>
                    <h4>{{ t('auto.txt_03c9183a') }}</h4>
                    <p>{{ t('auto.txt_f91ceb28') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_c23dd68f') }}</label>
                    <a-select v-model:value="settings.cardDuration" style="width:100%">
                      <a-select-option value="1">{{ t('auto.txt_d3c3f22b') }}</a-select-option>
                      <a-select-option value="2">{{ t('auto.txt_c8b30bdd') }}</a-select-option>
                      <a-select-option value="3">{{ t('auto.txt_364c8183') }}</a-select-option>
                      <a-select-option value="5">{{ t('auto.txt_99744649') }}</a-select-option>
                    </a-select>
                  </div>
                  <div class="settings-field-row two-col">
                    <div>
                      <label>{{ t('auto.txt_582846d5') }}</label>
                      <a-input-number v-model:value="settings.maxBooksPerLoan" :min="1" :max="20" style="width:100%" />
                    </div>
                    <div>
                      <label>{{ t('auto.txt_59a5d82c') }}</label>
                      <a-input-number v-model:value="settings.maxLoanDays" :min="1" :max="90" style="width:100%" />
                    </div>
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save" @click="saveSettings('card')">
                    <SaveOutlined /> {{ t('auto.txt_999302b3') }}
                  </a-button>
                </div>
              </div>

              <!-- 4. Cài đặt tiền phạt -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#FEF2F2"><DollarOutlined style="color:#EF4444" /></div>
                  <div>
                    <h4>{{ t('auto.txt_b1dec2e8') }}</h4>
                    <p>{{ t('auto.txt_dcd6f911') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-field-row">
                    <label>{{ t('auto.txt_a4d1f838') }}</label>
                    <a-input-number
                      v-model:value="settings.lateFeePerDay"
                      :min="0" :step="1000"
                      :formatter="(v: number) => `${v}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')"
                      style="width:100%"
                    />
                  </div>
                  <div class="settings-field-row two-col">
                    <div>
                      <label>{{ t('auto.txt_19e22d13') }}</label>
                      <a-input-number
                        v-model:value="settings.lostBookFee"
                        :min="0" :step="10000"
                        :formatter="(v: number) => `${v}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')"
                        style="width:100%"
                      />
                    </div>
                    <div>
                      <label>{{ t('auto.txt_c40ed254') }}</label>
                      <a-input-number
                        v-model:value="settings.damagedBookFee"
                        :min="0" :step="10000"
                        :formatter="(v: number) => `${v}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')"
                        style="width:100%"
                      />
                    </div>
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save" @click="saveSettings('fee')">
                    <SaveOutlined /> {{ t('auto.txt_999302b3') }}
                  </a-button>
                </div>
              </div>

              <!-- 5. Cài đặt thông báo -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#F0FFF4"><BellOutlined style="color:#38A169" /></div>
                  <div>
                    <h4>{{ t('auto.txt_8c6f9073') }}</h4>
                    <p>{{ t('auto.txt_9243437d') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-toggle-row">
                    <div>
                      <span class="toggle-label">{{ t('auto.txt_50368d86') }}</span>
                      <span class="toggle-desc">{{ t('auto.txt_9eba830e') }}</span>
                    </div>
                    <a-switch v-model:checked="settings.emailReminder" />
                  </div>
                  <div class="settings-toggle-row">
                    <div>
                      <span class="toggle-label">{{ t('auto.txt_9057a108') }}</span>
                      <span class="toggle-desc">{{ t('auto.txt_2f6315c0') }}</span>
                    </div>
                    <a-switch v-model:checked="settings.systemNotification" />
                  </div>
                  <div class="settings-field-row" style="margin-top:8px">
                    <label>{{ t('auto.txt_fd5ba7c4') }}</label>
                    <a-input-number v-model:value="settings.reminderDaysBefore" :min="1" :max="14" style="width:100%" />
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save" @click="saveSettings('notify')">
                    <SaveOutlined /> {{ t('auto.txt_999302b3') }}
                  </a-button>
                </div>
              </div>

              <!-- 6. Sao lưu dữ liệu -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#EEF2FF"><CloudUploadOutlined style="color:#6366F1" /></div>
                  <div>
                    <h4>{{ t('auto.txt_a764e4b3') }}</h4>
                    <p>{{ t('auto.txt_36f619f7') }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="backup-action-row">
                    <div class="backup-info">
                      <div class="backup-info-icon"><DatabaseOutlined /></div>
                      <div>
                        <span class="backup-title">{{ t('auto.txt_e7a0d600') }}</span>
                        <span class="backup-desc">{{ t('auto.txt_183fb6d3') }}</span>
                      </div>
                    </div>
                    <a-button class="btn-backup" :loading="backingUp" @click="handleBackup">
                      <DownloadOutlined /> {{ t('auto.txt_cfe3f9bf') }}
                    </a-button>
                  </div>
                  <div class="backup-action-row" style="margin-top:12px">
                    <div class="backup-info">
                      <div class="backup-info-icon restore"><UploadOutlined /></div>
                      <div>
                        <span class="backup-title">{{ t('auto.txt_6b858fe0') }}</span>
                        <span class="backup-desc">{{ t('auto.txt_b083ce20') }}</span>
                      </div>
                    </div>
                    <a-button class="btn-restore" @click="handleRestore">
                      <UploadOutlined /> {{ t('auto.txt_682697e7') }}
                    </a-button>
                  </div>
                  <div class="backup-warning">
                    <ExclamationCircleOutlined /> {{ t('auto.txt_782f13ac') }}
                  </div>
                </div>
              </div>

              <!-- 7. Ngôn ngữ hệ thống -->
              <div class="settings-card">
                <div class="settings-card-header">
                  <div class="settings-card-icon" style="background:#F0F4FF"><GlobalOutlined style="color:#4F6CF7" /></div>
                  <div>
                    <h4>{{ t('settings.lang.label') }}</h4>
                    <p>{{ selectedLocale === 'vi' ? 'Đang dùng Tiếng Việt' : 'Currently using English' }}</p>
                  </div>
                </div>
                <div class="settings-fields">
                  <div class="settings-field-row">
                    <label>{{ t('settings.lang.label') }}</label>
                    <a-radio-group v-model:value="selectedLocale" size="large" button-style="solid">
                      <a-radio-button value="vi">🇻🇳 {{ t('settings.lang.vi') }}</a-radio-button>
                      <a-radio-button value="en">🇬🇧 {{ t('settings.lang.en') }}</a-radio-button>
                    </a-radio-group>
                  </div>
                </div>
                <div class="settings-card-footer">
                  <a-button type="primary" class="btn-settings-save" @click="saveLanguage">
                    <SaveOutlined /> {{ t('settings.lang.save') }}
                  </a-button>
                </div>
              </div>

            </div>
            <!-- Footer -->
            <div style="text-align:center;padding:40px 24px 20px;color:rgba(0,0,0,0.45);font-size:14px;">
              SmartLib Library Management System ©2026 Created by Admin
            </div>
          </div>
        </template>
        <template v-if="false && currentRole === 'Librarian' && currentTab === 'loan-processing'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row">
              <h3><SyncOutlined /> {{ t('auto.txt_2196afbb') }}</h3>
              <span class="pending-badge-count">{{ mockLoans.filter(l => l.status === 'Chờ duyệt').length }} pending</span>
            </div>

            <div class="white-panel table-panel">
              <table class="pastel-table">
                <thead>
                  <tr>
                    <th>{{ t('auto.txt_d8ee31f7') }}</th>
                    <th>{{ t('auto.txt_d3bc2c9c') }}</th>
                    <th>{{ t('auto.txt_dbce656a') }}</th>
                    <th>{{ t('auto.txt_f01dcd24') }}</th>
                    <th>{{ t('auto.txt_e1f22674') }}</th>
                    <th>{{ t('auto.txt_42b670cd') }}</th>
                    <th>{{ t('auto.txt_9822be11') }}</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="loan in mockLoans" :key="loan.id">
                    <td class="text-teal font-bold">#{{ loan.code }}</td>
                    <td class="font-bold text-dark">{{ loan.readerName }}</td>
                    <td><span class="card-number-badge">{{ loan.cardNo }}</span></td>
                    <td>{{ loan.bookTitle }}</td>
                    <td>{{ loan.requestDate }}</td>
                    <td>
                      <span class="status-badge" :class="loan.status.toLowerCase().replace(' ', '-')">
                        {{ loan.status }}
                      </span>
                    </td>
                    <td>
                      <div class="action-buttons" v-if="loan.status === 'Chờ duyệt'">
                        <button class="btn-approve" @click="approveLoan(loan.id)"><CheckOutlined /> {{ t('auto.txt_a4db4d12') }}</button>
                        <button class="btn-reject" @click="rejectLoan(loan.id)"><CloseOutlined /> {{ t('auto.txt_2119d87d') }}</button>
                      </div>
                      <span v-else class="text-muted">{{ t('auto.txt_10fb2c48') }}</span>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </template>

        <!-- ==================== 5. LIBRARIAN - SEARCH STATUS TAB ==================== -->
        <template v-if="false && currentRole === 'Librarian' && currentTab === 'search-status'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row">
              <h3><SearchOutlined /> {{ t('auto.txt_772271c9') }}</h3>
            </div>

            <div class="white-panel search-panel">
              <div class="search-input-group">
                <a-input v-model:value="searchCardNo" placeholder="Nhập Mã Thẻ thư viện (Ví dụ: LIB-577064)..." class="search-input-pill" />
                <a-button type="primary" class="btn-search-pill" @click="handleSearchCard">{{ t('auto.txt_8929ef31') }}</a-button>
              </div>

              <!-- Search Results -->
              <div v-if="searchResult" class="search-results-box animate-fade-in">
                <div class="result-header">
                  <div class="user-info-large">
                    <h3>{{ searchResult.name }}</h3>
                    <span class="card-no">{{ searchResult.cardNo }}</span>
                  </div>
                  <span class="status-badge instock">{{ t('auto.txt_93faf48c') }}</span>
                </div>

                <div class="details-grid-custom">
                  <div class="detail-item-box">
                    <span class="lbl">{{ t('auto.txt_2d017d38') }}</span>
                    <span class="val">{{ searchResult.borrowedBooks }} cuốn</span>
                  </div>
                  <div class="detail-item-box">
                    <span class="lbl">{{ t('auto.txt_8d6a5a83') }}</span>
                    <span class="val text-danger">{{ searchResult.overdueBooks }} cuốn</span>
                  </div>
                  <div class="detail-item-box">
                    <span class="lbl">{{ t('auto.txt_ece15c59') }}</span>
                    <span class="val text-gold">{{ searchResult.penaltyAmt }} VND</span>
                  </div>
                </div>

                <div class="active-loans-list">
                  <h4><ReadOutlined /> {{ t('auto.txt_3fa0943c') }}</h4>
                  <div class="loan-item-row" v-for="b in searchResult.activeLoans" :key="b.title">
                    <span class="title">{{ b.title }}</span>
                    <span class="date">Hạn trả: {{ b.dueDate }}</span>
                    <span class="badge-status-loan" :class="b.status">{{ b.status === 'overdue' ? 'Quá hạn' : 'Đang mượn' }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </template>

        <!-- ==================== 6. LIBRARIAN - CONFIRM RETURN TAB ==================== -->
        <template v-if="false && currentRole === 'Librarian' && currentTab === 'confirm-return'">
          <div class="tab-content animate-fade-in">
            <div class="content-header-row">
              <h3><InboxOutlined /> {{ t('auto.txt_f923e7c4') }}</h3>
            </div>

            <div class="white-panel return-panel">
              <div class="steps-flow">
                <div class="step-item" :class="{ completed: returnStep >= 1 }">
                  <span class="step-num">1</span>
                  <span class="step-txt">{{ t('auto.txt_cc0b1c24') }}</span>
                </div>
                <div class="step-item" :class="{ completed: returnStep >= 2 }">
                  <span class="step-num">2</span>
                  <span class="step-txt">{{ t('auto.txt_208f42e3') }}</span>
                </div>
                <div class="step-item" :class="{ completed: returnStep >= 3 }">
                  <span class="step-num">3</span>
                  <span class="step-txt">{{ t('auto.txt_d9c855e8') }}</span>
                </div>
              </div>

              <!-- Step 1 Layout -->
              <div v-if="returnStep === 1" class="step-content-box">
                <p>{{ t('auto.txt_142e98dc') }}</p>
                <div class="input-inline">
                  <a-input v-model:value="returnBookCode" placeholder="Nhập mã sách (e.g. MS-OOP-001)..." class="pill-input-custom" />
                  <a-button type="primary" class="btn-next-step" @click="returnStep = 2">{{ t('auto.txt_555f1f22') }} <ArrowRightOutlined /></a-button>
                </div>
              </div>

              <!-- Step 2 Layout -->
              <div v-if="returnStep === 2" class="step-content-box">
                <p>{{ t('auto.txt_2266320e') }}</p>
                <div class="checkbox-group-custom">
                  <a-checkbox v-model:checked="bookCondition.intact">{{ t('auto.txt_f958fb22') }}</a-checkbox>
                  <a-checkbox v-model:checked="bookCondition.clean">{{ t('auto.txt_1c810fc4') }}</a-checkbox>
                  <a-checkbox v-model:checked="bookCondition.noLate">{{ t('auto.txt_890cdb9d') }}</a-checkbox>
                </div>
                <div class="step-actions mt-6">
                  <a-button class="btn-prev" @click="returnStep = 1"><ArrowLeftOutlined /> Quay lại</a-button>
                  <a-button type="primary" class="btn-next-step" @click="handleConfirmReturn">{{ t('auto.txt_12b318ea') }}</a-button>
                </div>
              </div>

              <!-- Step 3 Layout -->
              <div v-if="returnStep === 3" class="step-content-box text-center">
                <div class="success-icon-large"><CheckCircleOutlined /></div>
                <h3>{{ t('auto.txt_f400e4ad') }}</h3>
                <p class="text-teal">{{ t('auto.txt_127b35a4') }}</p>
                <a-button type="primary" class="btn-restart" @click="resetReturnWizard">{{ t('auto.txt_e29866e5') }}</a-button>
              </div>
            </div>
          </div>
        </template>
      </main>
    </template>

    <!-- ==================== DETAIL MODALS ==================== -->

    <!-- READERS DETAIL MODAL -->
    <a-modal v-model:open="showReadersModal" title="<TeamOutlined /> Danh sách Độc giả" width="750px" :footer="null" class="detail-modal">
      <div v-if="detailLoading" class="modal-loading"><LoadingOutlined /> {{ t('auto.txt_8c4d542c') }}</div>
      <div v-else-if="readersList.length === 0" class="modal-empty">{{ t('auto.txt_706b4d81') }}</div>
      <table v-else class="pastel-table modal-table">
        <thead>
          <tr>
            <th>{{ t('auto.txt_bb345e8e') }}</th>
            <th>Username</th>
            <th>Email</th>
            <th>{{ t('auto.txt_dbce656a') }}</th>
            <th>{{ t('auto.txt_42b670cd') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="reader in readersList" :key="reader.Id || reader.id">
            <td class="font-bold text-dark">{{ reader.FullName || reader.fullName }}</td>
            <td>{{ reader.Username || reader.username }}</td>
            <td>{{ reader.Email || reader.email }}</td>
            <td><span class="card-number-badge">{{ reader.LibraryCard?.CardNumber || reader.libraryCard?.cardNumber || 'Chưa cấp' }}</span></td>
            <td>
              <span class="status-badge" :class="(reader.IsActive || reader.isActive) ? 'instock' : 'outstock'">
                {{ (reader.IsActive || reader.isActive) ? 'Hoạt động' : 'Bị khóa' }}
              </span>
            </td>
          </tr>
        </tbody>
      </table>
    </a-modal>

    <!-- CARDS DETAIL MODAL -->
    <a-modal v-model:open="showCardsModal" title="<IdcardOutlined /> Danh sách Thẻ thư viện" width="750px" :footer="null" class="detail-modal">
      <div v-if="detailLoading" class="modal-loading"><LoadingOutlined /> {{ t('auto.txt_8c4d542c') }}</div>
      <div v-else-if="cardsList.length === 0" class="modal-empty">{{ t('auto.txt_8cae9afb') }}</div>
      <table v-else class="pastel-table modal-table">
        <thead>
          <tr>
            <th>{{ t('auto.txt_dbce656a') }}</th>
            <th>{{ t('auto.txt_b721fd50') }}</th>
            <th>{{ t('auto.txt_c72178e9') }}</th>
            <th>{{ t('auto.txt_a584d378') }}</th>
            <th>{{ t('auto.txt_42b670cd') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="card in cardsList" :key="card.CardNumber || card.cardNumber">
            <td class="text-teal font-bold">{{ card.CardNumber || card.cardNumber }}</td>
            <td class="font-bold text-dark">{{ card.OwnerName || card.ownerName }}</td>
            <td>{{ formatDate(card.IssueDate || card.issueDate) }}</td>
            <td>{{ formatDate(card.ExpiryDate || card.expiryDate) }}</td>
            <td>
              <span class="status-badge" :class="(card.Status || card.status) === 'Active' ? 'instock' : 'outstock'">
                {{ card.Status || card.status }}
              </span>
            </td>
          </tr>
        </tbody>
      </table>
    </a-modal>

    <!-- BORROWS DETAIL MODAL -->
    <a-modal v-model:open="showBorrowsModal" title="<SyncOutlined /> Chi tiết Lượt mượn" width="850px" :footer="null" class="detail-modal">
      <div v-if="!transactionsList || transactionsList.length === 0" class="modal-pending">
        <div class="pending-icon"><SyncOutlined /></div>
        <h3>{{ t('dashboard.overview.no_data') }} lượt mượn</h3>
        <p>{{ t('auto.txt_e78f2e1e') }}</p>
      </div>
      <table v-else class="pastel-table modal-table">
        <thead>
          <tr>
            <th>{{ t('auto.txt_d8ee31f7') }}</th>
            <th>{{ t('auto.txt_dbce656a') }}</th>
            <th>{{ t('auto.txt_b208cc4c') }}</th>
            <th>{{ t('auto.txt_42b670cd') }}</th>
            <th>{{ t('auto.txt_e7f5ae9e') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="t in transactionsList" :key="t.Id || t.id">
            <td class="text-teal font-bold">#{{ t.Id || t.id }}</td>
            <td>{{ t.CardNumber || t.cardNumber }}</td>
            <td class="font-bold text-dark">{{ t.TenSach || t.tenSach }}</td>
            <td>
              <span class="status-badge" :class="(t.Status || t.status).toLowerCase()">
                {{ t.Status || t.status }}
              </span>
            </td>
            <td style="color:#ef4444;font-weight:600;">
              {{ (t.FineAmount || t.fineAmount) > 0 ? (t.FineAmount || t.fineAmount).toLocaleString() + 'đ' : '-' }}
            </td>
          </tr>
        </tbody>
      </table>
    </a-modal>

    <!-- BOOKS DETAIL MODAL -->
    <a-modal v-model:open="showBooksModal" title="<ReadOutlined /> Chi tiết Tổng sách" width="500px" :footer="null" class="detail-modal">
      <div class="modal-pending">
        <div class="pending-icon"><LinkOutlined /></div>
        <h3>{{ t('auto.txt_660d6b7b') }}</h3>
        <p>{{ t('auto.txt_4704b606') }}</p>
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, computed, reactive, h, resolveComponent, watch } from 'vue'
import { useRouter } from 'vue-router'
import { message, Modal } from 'ant-design-vue'
import { apiClient, logout } from '@/utils/apiClient'
import { useLibraryStore } from '@/stores/libraryStore'
import { useI18nStore } from '@/stores/i18nStore'
import { getAccessToken } from '@/utils/auth'
import { N1_BOOKS_URL, N2_LIBRARIAN_LOANS_URL, N2_LIBRARIAN_PAYMENTS_URL, N2_LIBRARIAN_FINES_URL, N2_LIBRARIAN_REVIEWS_URL, buildHandoffUrl, createAuthHandoffCode } from '@/utils/authHandoff'
import { exportReadersToXlsx, importXlsxFile, normalizeExcelRows } from '@/utils/excelHelper.js'
import { formatVnDateTime, formatVnDate, parseUtcDate } from '@/utils/dateTime'
import { StarOutlined, LockOutlined, UserOutlined, DashboardOutlined, BookOutlined, SettingOutlined, TeamOutlined, IdcardOutlined, SyncOutlined, SearchOutlined, InboxOutlined, LogoutOutlined, BulbOutlined, BellOutlined, ReadOutlined, FrownOutlined, EyeOutlined, UnlockOutlined, StopOutlined, CheckCircleOutlined, ArrowRightOutlined, ArrowLeftOutlined, LoadingOutlined, LinkOutlined, SaveOutlined, CheckOutlined, CloseOutlined, WifiOutlined, FileExcelOutlined, DownloadOutlined, EditOutlined, UploadOutlined, PlusOutlined, DeleteOutlined, LoginOutlined, DownOutlined, HistoryOutlined, WarningOutlined, ExclamationCircleOutlined, DollarOutlined, BarChartOutlined, AlertOutlined, BankOutlined, CloudUploadOutlined, DatabaseOutlined, GlobalOutlined } from '@ant-design/icons-vue';

const router = useRouter()
const i18n = useI18nStore()
const t = (key: Parameters<typeof i18n.t>[0]) => i18n.t(key)
const loading = ref(false)
const importLoading = ref(false)
const stats = ref<any>(null)
const totalBooks = ref(0)

// Get state from localStorage
const currentRole = ref(localStorage.getItem('role') || 'Admin')
const currentUser = ref(localStorage.getItem('username') || 'Nirmal')
const currentEmail = ref(localStorage.getItem('email') || '')
const accessToken = ref(getAccessToken() || '')
const booksIframeSrc = ref('')
const loansIframeSrc = ref('')
const paymentsIframeSrc = ref('')
const finesIframeSrc = ref('')
const reviewsIframeSrc = ref('')

async function loadBooksIframe() {
  booksIframeSrc.value = ''
  try {
    const code = await createAuthHandoffCode()
    booksIframeSrc.value = buildHandoffUrl(N1_BOOKS_URL, code)
  } catch {
    message.error('Không thể tải giao diện quản lý sách. Vui lòng thử lại.')
  }
}

async function loadLoansIframe() {
  loansIframeSrc.value = ''
  try {
    const code = await createAuthHandoffCode()
    loansIframeSrc.value = buildHandoffUrl(N2_LIBRARIAN_LOANS_URL, code)
  } catch {
    message.error('Không thể tải giao diện xử lý mượn trả. Vui lòng thử lại.')
  }
}

async function loadPaymentsIframe() {
  paymentsIframeSrc.value = ''
  try {
    const code = await createAuthHandoffCode()
    paymentsIframeSrc.value = buildHandoffUrl(N2_LIBRARIAN_PAYMENTS_URL, code)
  } catch {
    message.error('Không thể tải giao diện quản lý doanh thu. Vui lòng thử lại.')
  }
}

async function loadFinesIframe() {
  finesIframeSrc.value = ''
  try {
    const code = await createAuthHandoffCode()
    finesIframeSrc.value = buildHandoffUrl(N2_LIBRARIAN_FINES_URL, code)
  } catch {
    message.error('Không thể tải giao diện quản lý phí phạt. Vui lòng thử lại.')
  }
}

async function loadReviewsIframe() {
  reviewsIframeSrc.value = ''
  try {
    const code = await createAuthHandoffCode()
    reviewsIframeSrc.value = buildHandoffUrl(N2_LIBRARIAN_REVIEWS_URL, code)
  } catch {
    message.error('Không thể tải giao diện quản lý đánh giá. Vui lòng thử lại.')
  }
}

// Authorize guard
const isAuthorized = computed(() => {
  return (currentRole.value === 'Admin' || currentRole.value === 'Librarian')
})

const activeCardPercentage = computed(() => {
  const total = getCardStat('TotalCards')
  const active = getCardStat('ActiveCards')
  if (total === 0) return 0
  return Math.round((active / total) * 100)
})

const lockedCardPercentage = computed(() => {
  const total = getCardStat('TotalCards')
  if (total === 0) return 0
  return 100 - activeCardPercentage.value
})

// --- ApexCharts Setup ---
const userGrowthSeries = computed(() => {
  const trends = circulationStats.value?.borrowingTrends || []
  if (trends.length > 0) {
    return [{ name: 'Lượt mượn sách', data: trends.map((t: any) => t.borrows || 0) }]
  }
  return [{ name: 'Lượt mượn sách', data: [] }]
})

const userGrowthOptions = computed(() => {
  const trends = circulationStats.value?.borrowingTrends || []
  let categories = trends.length > 0 ? trends.map((t: any) => t.month || '') : []

  return {
    chart: {
      type: 'bar',
      toolbar: { show: false },
      fontFamily: 'Inter, sans-serif',
      animations: {
        enabled: true,
        easing: 'easeinout',
        speed: 800,
        animateGradually: {
          enabled: true,
          delay: 150
        },
        dynamicAnimation: {
          enabled: true,
          speed: 350
        }
      }
    },
    colors: ['#1E5652'],
    plotOptions: {
      bar: {
        borderRadius: 6,
        columnWidth: '50%',
        dataLabels: { position: 'top' }
      }
    },
    dataLabels: { 
      enabled: true,
      offsetY: -20,
      style: { 
        fontSize: '11px', 
        fontWeight: 600, 
        colors: ['#114B44'] 
      }
    },
    xaxis: {
      categories: categories,
      axisBorder: { show: false },
      axisTicks: { show: false },
      labels: { style: { colors: '#6C7A78', fontSize: '12px', fontWeight: 500 } }
    },
    yaxis: {
      labels: {
        formatter: (val: number) => {
          return val >= 1000 ? (val / 1000) + 'K' : val
        },
        style: { colors: '#6C7A78', fontSize: '11px', fontWeight: 500 }
      }
    },
    grid: { 
      borderColor: '#f0f0f0',
      strokeDashArray: 4,
      yaxis: { lines: { show: true } },
      xaxis: { lines: { show: false } }
    },
    legend: { show: false },
    tooltip: { 
      theme: 'light',
      y: { formatter: (val: number) => val + ' lượt' }
    },
    states: {
      hover: {
        filter: {
          type: 'lighten',
          value: 0.1
        }
      }
    }
  }
})

// Activity Series for Right Panel — reactive từ CardStats
const activitySeries = computed(() => [
  getCardStat('ActiveCards'),
  getCardStat('LockedCards'),
  getCardStat('ExpiredCards'),
])

const activityOptions = computed(() => {
  return {
    chart: { 
      type: 'donut', 
      fontFamily: 'Inter, sans-serif',
      toolbar: { show: false },
      animations: {
        enabled: true,
        easing: 'easeinout',
        speed: 800,
        animateGradually: {
          enabled: true,
          delay: 150
        },
        dynamicAnimation: {
          enabled: true,
          speed: 350
        }
      }
    },
    labels: ['Hoạt động', 'Bị khóa', 'Hết hạn'],
    colors: ['#1E5652', '#CF7C07', '#9ca3af'],
    plotOptions: {
      pie: {
        expandOnClick: true,
        donut: {
          size: '65%',
          labels: {
            show: true,
            name: { show: true, fontSize: '14px', color: '#6C7A78', fontWeight: 600 },
            value: { show: true, fontSize: '20px', fontWeight: 700, color: '#114B44' },
            total: {
              show: true,
              label: 'Tổng số thẻ',
              color: '#6C7A78',
              fontSize: '12px',
              fontWeight: 600,
              formatter: function (w: any) {
                return w.globals.seriesTotals.reduce((a: any, b: any) => {
                  return a + b
                }, 0)
              }
            }
          }
        }
      }
    },
    dataLabels: { enabled: false },
    stroke: { show: true, colors: ['#ffffff'], width: 3 },
    legend: { 
      position: 'bottom', 
      horizontalAlign: 'center',
      markers: { width: 12, height: 12, radius: 12 },
      itemMargin: { horizontal: 10, vertical: 5 },
      fontSize: '12px',
      fontWeight: 600
    },
    tooltip: {
      theme: 'light',
      y: { formatter: (val: number) => val + ' thẻ' }
    },
    states: {
      hover: {
        filter: {
          type: 'lighten',
          value: 0.1
        }
      },
      active: {
        filter: {
          type: 'darken',
          value: 0.1
        }
      }
    }
  }
})

// Tab active state
const currentTab = ref(currentRole.value === 'Admin' ? 'overview' : 'rules')

const usersMenuOpen = ref(false)
const isUsersTab = computed(() => ['readers-list', 'staff-list'].includes(currentTab.value))

const financeMenuOpen = ref(false)
const isFinanceTab = computed(() => ['finance-revenue', 'finance-penalty'].includes(currentTab.value))

// ── Settings state ───────────────────────────────────────────
const backingUp = ref(false)
const savingProfile = ref(false)
const changingPassword = ref(false)
const logoInputRef = ref<HTMLInputElement | null>(null)
const avatarInputRef = ref<HTMLInputElement | null>(null)

const libraryStore = useLibraryStore()

const settings = ref({
  // Thư viện — bind 2 chiều với store
  get libraryName()    { return libraryStore.name },
  set libraryName(v)   { libraryStore.name = v },
  get libraryAddress() { return libraryStore.address },
  set libraryAddress(v){ libraryStore.address = v },
  get libraryPhone()   { return libraryStore.phone },
  set libraryPhone(v)  { libraryStore.phone = v },
  get libraryEmail()   { return libraryStore.email },
  set libraryEmail(v)  { libraryStore.email = v },
  get libraryDesc()    { return libraryStore.description },
  set libraryDesc(v)   { libraryStore.description = v },
  get logoUrl()        { return libraryStore.logoUrl },
  set logoUrl(v)       { libraryStore.logoUrl = v },
  // Admin
  adminFullName: currentUser.value || '',
  adminEmail: currentEmail.value || localStorage.getItem('email') || '',
  adminAvatar: localStorage.getItem('adminAvatar') || '',
  currentPassword: '',
  newPassword: '',
  confirmPassword: '',
  // Thẻ
  cardDuration: '1',
  maxBooksPerLoan: 3,
  maxLoanDays: 14,
  // Phạt
  lateFeePerDay: 2000,
  lostBookFee: 200000,
  damagedBookFee: 100000,
  // Thông báo
  emailReminder: true,
  systemNotification: true,
  reminderDaysBefore: 3,
})

const saveSettings = async (section: string) => {
  if (section === 'library') {
    libraryStore.update({
      name:        settings.value.libraryName,
      address:     settings.value.libraryAddress,
      phone:       settings.value.libraryPhone,
      email:       settings.value.libraryEmail,
      description: settings.value.libraryDesc,
      logoUrl:     settings.value.logoUrl,
    })
    message.success('Đã lưu thông tin thư viện thành công!')
  } else if (section === 'admin') {
    savingProfile.value = true
    try {
      await apiClient.put('/api/identity/User/profile', {
        FullName: settings.value.adminFullName,
        Email: settings.value.adminEmail,
      })
      localStorage.setItem('username', settings.value.adminFullName)
      localStorage.setItem('email', settings.value.adminEmail)
      currentUser.value = settings.value.adminFullName
      message.success('Đã cập nhật thông tin cá nhân thành công!')
    } catch (err: any) {
      message.error(err?.response?.data?.Message || 'Cập nhật thất bại!')
    } finally {
      savingProfile.value = false
    }
  } else {
    message.success(`Đã lưu cài đặt ${
      section === 'card'   ? 'thẻ thư viện' :
      section === 'fee'    ? 'tiền phạt' : 'thông báo'
    } thành công!`)
  }
}

const selectedLocale = ref(i18n.locale)

const saveLanguage = () => {
  i18n.setLocale(selectedLocale.value)
  message.success(i18n.t('settings.lang.saved'))
}

const handleChangePassword = async () => {
  if (!settings.value.currentPassword || !settings.value.newPassword) {
    message.warning('Vui lòng nhập đầy đủ mật khẩu!')
    return
  }
  if (settings.value.newPassword !== settings.value.confirmPassword) {
    message.error('Mật khẩu mới và xác nhận không khớp!')
    return
  }
  changingPassword.value = true
  try {
    await apiClient.post('/api/identity/User/change-password', {
      CurrentPassword: settings.value.currentPassword,
      NewPassword: settings.value.newPassword,
    })
    message.success('Đổi mật khẩu thành công!')
    settings.value.currentPassword = ''
    settings.value.newPassword = ''
    settings.value.confirmPassword = ''
  } catch (err: any) {
    const errors = err?.response?.data?.Errors
    if (errors?.length) {
      errors.forEach((e: any) => message.error(e.Description || e.description))
    } else {
      message.error(err?.response?.data?.Message || 'Đổi mật khẩu thất bại!')
    }
  } finally {
    changingPassword.value = false
  }
}

const onLogoChange = (e: Event) => {
  const file = (e.target as HTMLInputElement).files?.[0]
  if (!file) return
  const reader = new FileReader()
  reader.onload = (ev) => {
    const base64 = ev.target?.result as string
    settings.value.logoUrl = base64
    libraryStore.update({ logoUrl: base64 })
    message.success('Đã tải lên logo thư viện!')
  }
  reader.readAsDataURL(file)
}

const onAvatarChange = (e: Event) => {
  const file = (e.target as HTMLInputElement).files?.[0]
  if (!file) return
  const reader = new FileReader()
  reader.onload = (ev) => {
    const base64 = ev.target?.result as string
    settings.value.adminAvatar = base64
    localStorage.setItem('adminAvatar', base64)
    message.success('Đã cập nhật ảnh đại diện!')
  }
  reader.readAsDataURL(file)
}

const handleBackup = async () => {
  backingUp.value = true
  setTimeout(() => {
    backingUp.value = false
    message.success('Đã tạo bản sao lưu thành công!')
  }, 1500)
}

const handleRestore = () => {
  message.info('Tính năng khôi phục sẽ sẵn sàng khi backend hỗ trợ.')
}

// Bulk selection state
const selectedReaderIds = ref<string[]>([])
const selectedStaffIds = ref<string[]>([])
const bulkDeleteReadersLoading = ref(false)
const bulkDeleteStaffLoading = ref(false)

const readerRowSelection = computed(() => ({
  selectedRowKeys: selectedReaderIds.value,
  onChange: (keys: string[]) => { selectedReaderIds.value = keys },
}))

const staffRowSelection = computed(() => ({
  selectedRowKeys: selectedStaffIds.value,
  onChange: (keys: string[]) => { selectedStaffIds.value = keys },
}))

const ALERT_EVENT_TYPES = new Set([
  'circulation.borrow.requested',
  'circulation.return.requested',
  'circulation.renewal.requested',
  'circulation.overdue',
  'card.expiring',
  'document.pending',
  'system.error',
])

const staffList = ref<any[]>([])
const staffLoading = ref(false)
const staffSearchQuery = ref('')
const staffRoleFilter = ref('all')
type LogItem = {
  id: string
  eventType: string
  category: string
  icon: any
  title: string
  message: string
  timeLabel: string
  timestamp?: string
  actionTab?: string
}

const showNotifications = ref(false)
const notificationsLoading = ref(false)
const activityLogsLoading = ref(false)
const logFilterType = ref('all')
const activityLogPage = ref(1)
const activityLogPageSize = ref(20)
const notifications = ref<LogItem[]>([])
const activityLogs = ref<LogItem[]>([])
const showCreateStaffModal = ref(false)
const createStaffLoading = ref(false)
const staffForm = reactive({
  username: '',
  email: '',
  fullName: '',
  phoneNumber: '',
  password: ''
})
const staffColumns = [
  {
    title: 'STT',
    key: 'stt',
    width: 60,
    align: 'center',
    fixed: 'left',
  },
  {
    title: 'Họ và Tên',
    key: 'fullName',
    width: 220,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const name = record.FullName || record.fullName || ''
      return name.toString().toLowerCase().includes((value as string).toLowerCase())
    },
    sorter: (a: any, b: any) =>
      (a.FullName || a.fullName || '').localeCompare(b.FullName || b.fullName || ''),
  },
  {
    title: 'Tên đăng nhập',
    key: 'username',
    width: 160,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const u = record.Username || record.username || ''
      return u.toString().toLowerCase().includes((value as string).toLowerCase())
    },
    sorter: (a: any, b: any) =>
      (a.Username || a.username || '').localeCompare(b.Username || b.username || ''),
  },
  {
    title: 'Email',
    key: 'email',
    width: 220,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const em = record.Email || record.email || ''
      return em.toString().toLowerCase().includes((value as string).toLowerCase())
    },
  },
  {
    title: 'Vai trò',
    key: 'role',
    width: 120,
    filters: [
      { text: 'Admin', value: 'Admin' },
      { text: 'Librarian', value: 'Librarian' },
    ],
    onFilter: (value: any, record: any) => {
      const r = record.Role || record.role || ''
      return r === value
    }
  },
  {
    title: 'Trạng thái',
    key: 'isActive',
    width: 130,
    filters: [
      { text: 'Hoạt động', value: true },
      { text: 'Bị khóa', value: false },
    ],
    onFilter: (value: any, record: any) => {
      const isActive = record.IsActive ?? record.isActive ?? false
      return isActive === value
    }
  },
  {
    title: 'Ngày tạo',
    key: 'createdAt',
    width: 130,
    sorter: (a: any, b: any) => {
      const dateA = new Date(a.CreatedAt || a.createdAt || 0).getTime()
      const dateB = new Date(b.CreatedAt || b.createdAt || 0).getTime()
      return dateA - dateB
    },
  },
  {
    title: 'Thao tác',
    key: 'actions',
    align: 'center',
    width: 220,
    fixed: 'right',
  },
]

const filteredStaffList = computed(() => {
  const q = staffSearchQuery.value.trim().toLowerCase()
  return staffList.value.filter((user: any) => {
    const fullName = (user.FullName || user.fullName || '').toLowerCase()
    const username = (user.Username || user.username || '').toLowerCase()
    const email = (user.Email || user.email || '').toLowerCase()
    const role = user.Role || user.role

    const matchesSearch = !q || fullName.includes(q) || username.includes(q) || email.includes(q)
    const matchesRole = staffRoleFilter.value === 'all' || role === staffRoleFilter.value
    return matchesSearch && matchesRole
  })
})

const fetchStaffData = async () => {
  staffLoading.value = true
  try {
    const res = await apiClient.get('/api/identity/User/staff')
    staffList.value = res.data
  } catch {
    staffList.value = []
    message.error('Không tải được danh sách nhân viên')
  } finally {
    staffLoading.value = false
  }
}

const createStaff = async () => {
  if (!staffForm.username || !staffForm.email || !staffForm.fullName || !staffForm.password) {
    message.warning('Vui lòng điền đủ thông tin bắt buộc')
    return
  }
  if (staffForm.password.length < 6) {
    message.warning('Mật khẩu tối thiểu 6 ký tự')
    return
  }
  createStaffLoading.value = true
  try {
    await apiClient.post('/api/identity/User/staff', {
      Username: staffForm.username,
      Email: staffForm.email,
      FullName: staffForm.fullName,
      PhoneNumber: staffForm.phoneNumber || null,
      Password: staffForm.password
    })
    message.success('Tạo tài khoản Thủ thư thành công!')
    showCreateStaffModal.value = false
    staffForm.username = ''
    staffForm.email = ''
    staffForm.fullName = ''
    staffForm.phoneNumber = ''
    staffForm.password = ''
    await fetchStaffData()
  } catch (err: any) {
    // Hiển thị chi tiết lỗi từ ASP.NET Identity (password complexity, duplicate, etc.)
    const errors = err?.response?.data?.Errors
    if (errors?.length) {
      errors.forEach((e: any) => message.error(e.Description || e.description || String(e)))
    } else {
      message.error(err?.response?.data?.Message || 'Tạo thủ thư thất bại')
    }
  } finally {
    createStaffLoading.value = false
  }
}

const demoteLibrarian = async (user: any) => {
  const userId = user.Id || user.id
  const username = user.Username || user.username
  try {
    await apiClient.put(`/api/identity/User/users/${userId}/role`, { Role: 'Reader' })
    message.success(`Đã hạ ${username} xuống Reader`)
    await fetchStaffData()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Cập nhật role thất bại')
  }
}

const promoteToLibrarian = async (user: any) => {
  const userId = user.Id || user.id
  const username = user.Username || user.username
  const fullName = user.FullName || user.fullName
  try {
    await apiClient.put(`/api/identity/User/users/${userId}/role`, { Role: 'Librarian' })
    message.success(`Đã nâng ${fullName || username} lên Thủ thư!`)
    await fetchReadersData()
    await fetchStaffData()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Cập nhật role thất bại')
  }
}

const bulkDeleteReaders = async () => {
  if (!selectedReaderIds.value.length) return
  bulkDeleteReadersLoading.value = true
  try {
    const res = await apiClient.post('/api/identity/User/readers/bulk-delete', { UserIds: selectedReaderIds.value })
    message.success(res.data?.Message || 'Xóa thành công')
    selectedReaderIds.value = []
    await fetchReadersData()
    fetchActivityLogs()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Xóa thất bại')
  } finally {
    bulkDeleteReadersLoading.value = false
  }
}

const bulkDeleteStaff = async () => {
  if (!selectedStaffIds.value.length) return
  bulkDeleteStaffLoading.value = true
  try {
    const res = await apiClient.post('/api/identity/User/staff/bulk-delete', { UserIds: selectedStaffIds.value })
    message.success(res.data?.Message || 'Xóa thành công')
    selectedStaffIds.value = []
    await fetchStaffData()
    fetchActivityLogs()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Xóa thất bại')
  } finally {
    bulkDeleteStaffLoading.value = false
  }
}

const deleteReaderSingle = async (record: any) => {
  const userId = record.Id || record.id
  const name = record.FullName || record.fullName
  try {
    await apiClient.delete(`/api/identity/User/users/${userId}`)
    message.success(`Đã xóa ${name}`)
    await fetchReadersData()
    fetchActivityLogs()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Xóa thất bại')
  }
}

const deleteStaffSingle = async (record: any) => {
  const userId = record.Id || record.id
  const name = record.FullName || record.fullName
  try {
    await apiClient.delete(`/api/identity/User/users/${userId}`)
    message.success(`Đã xóa ${name}`)
    await fetchStaffData()
    fetchActivityLogs()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Xóa thất bại')
  }
}

function parseLogPayload(raw: unknown): Record<string, any> {
  if (!raw) return {}
  if (typeof raw === 'string') {
    try {
      return JSON.parse(raw)
    } catch {
      return {}
    }
  }
  if (typeof raw === 'object') return raw as Record<string, any>
  return {}
}

function buildLogItem(item: any, eventType: string, payload: Record<string, any>, timeLabel: string, extractedTimestamp?: string): LogItem {
  const fullName = payload.fullName || payload.FullName || payload.ownerName || payload.username || payload.Username || 'Người dùng'
  const bookTitle = payload.bookTitle || payload.BookTitle || payload.tenSach || payload.TenSach || 'Sách'
  const cardNumber = payload.cardNumber || payload.CardNumber || '—'
  const expiryDate = payload.expiryDate || payload.ExpiryDate

  let category = 'system'
  let icon: any = BellOutlined
  let title = 'Hoạt động hệ thống'
  let message = payload.message || eventType

  switch (eventType) {
    case 'circulation.borrow.requested':
      category = 'borrow'
      icon = SyncOutlined
      title = 'Yêu cầu mượn sách mới'
      message = `Thẻ ${cardNumber} yêu cầu mượn "${bookTitle}"`
      break
    case 'circulation.renewal.requested':
      category = 'renewal'
      icon = SyncOutlined
      title = 'Yêu cầu gia hạn mới'
      message = `Thẻ ${cardNumber} yêu cầu gia hạn "${bookTitle}"`
      break
    case 'circulation.overdue':
      category = 'overdue'
      icon = WarningOutlined
      title = 'Sách quá hạn trả'
      message = `"${bookTitle}" — thẻ ${cardNumber}`
      break
    case 'card.expiring':
      category = 'card-expire'
      icon = IdcardOutlined
      title = 'Thẻ sắp hết hạn'
      message = `Thẻ ${cardNumber} (${fullName}) hết hạn ${expiryDate ? formatVnDate(String(expiryDate)) : 'sớm'}`
      break
    case 'document.pending':
      category = 'pending'
      icon = InboxOutlined
      title = 'Tài liệu chờ duyệt'
      message = `Thẻ ${cardNumber} — "${bookTitle}" chờ xử lý`
      break
    case 'system.error':
      category = 'error'
      icon = ExclamationCircleOutlined
      title = 'Lỗi hệ thống'
      message = payload.message || 'Có lỗi xảy ra trong hệ thống'
      break
    case 'user.registered':
      category = 'register'
      icon = UserOutlined
      title = 'Đăng ký tài khoản'
      message = `${fullName} đăng ký mới${cardNumber !== '—' ? ` — thẻ ${cardNumber}` : ''}`
      break
    case 'user.login':
      category = 'login'
      icon = LoginOutlined
      title = 'Đăng nhập'
      message = `${fullName} đăng nhập hệ thống`
      break
    case 'user.logout':
      category = 'logout'
      icon = LogoutOutlined
      title = 'Đăng xuất'
      message = `${fullName} đăng xuất`
      break
    case 'user.locked':
      category = 'lock'
      icon = LockOutlined
      title = 'Khóa tài khoản'
      message = `Đã khóa tài khoản ${fullName}${payload.cardStatus ? ` — thẻ ${payload.cardStatus}` : ''}`
      break
    case 'user.unlocked':
      category = 'unlock'
      icon = UnlockOutlined
      title = 'Mở khóa tài khoản'
      message = `Đã mở khóa tài khoản ${fullName}`
      break
    case 'card.locked':
      category = 'lock'
      icon = LockOutlined
      title = 'Khóa thẻ thư viện'
      message = `Thẻ ${cardNumber} bị khóa — ${fullName}`
      break
    case 'card.unlocked':
      category = 'unlock'
      icon = UnlockOutlined
      title = 'Mở khóa thẻ'
      message = `Thẻ ${cardNumber} được mở khóa`
      break
    case 'staff.created':
      category = 'staff'
      icon = TeamOutlined
      title = 'Tạo Thủ thư'
      message = `Tạo tài khoản Thủ thư: ${fullName}`
      break
    case 'user.deleted':
      category = 'delete'
      icon = DeleteOutlined
      title = 'Xóa tài khoản'
      message = `Xóa ${fullName} (${payload.role || payload.Role || ''})`
      break
    case 'circulation.return.requested':
      category = 'return'
      icon = InboxOutlined
      title = 'Yêu cầu trả sách'
      message = `Thẻ ${cardNumber} yêu cầu trả "${bookTitle}"`
      break
    case 'circulation.borrowed':
      category = 'borrow'
      icon = BookOutlined
      title = 'Mượn sách'
      message = `"${bookTitle}" — thẻ ${cardNumber}`
      break
    case 'circulation.returned':
      category = 'return'
      icon = CheckCircleOutlined
      title = 'Trả sách'
      message = `"${bookTitle}" — thẻ ${cardNumber}`
      break
    case 'circulation.updated':
      category = 'borrow'
      icon = SyncOutlined
      title = 'Cập nhật phiếu mượn'
      message = `"${bookTitle}" — thẻ ${cardNumber} (${payload.status || ''})`
      break
    case 'book.added':
    case 'book.created':
      category = 'book'
      icon = BookOutlined
      title = 'Thêm sách'
      message = payload.title ? `Thêm "${payload.title}" vào kho` : 'Thêm sách mới vào Catalog'
      break
    case 'book.updated':
      category = 'book'
      icon = EditOutlined
      title = 'Sửa sách'
      message = payload.title ? `Cập nhật "${payload.title}"` : 'Cập nhật thông tin sách'
      break
    case 'book.deleted':
      category = 'delete'
      icon = DeleteOutlined
      title = 'Xóa sách'
      message = payload.title ? `Xóa "${payload.title}" khỏi kho` : 'Xóa sách khỏi Catalog'
      break
    default:
      break
  }

  const timestamp = extractedTimestamp || item.Timestamp || item.timestamp || undefined

  return {
    id: String(item.Id || item.id || `${eventType}-${timeLabel}`),
    eventType,
    category,
    icon,
    title,
    message,
    timeLabel,
    timestamp: timestamp ? String(timestamp) : undefined,
  }
}

function mapTransactionEventType(status: string): string {
  const normalized = (status || '').trim().toLowerCase()
  switch (normalized) {
    case 'pending':
      return 'circulation.borrow.requested'
    case 'returnpending':
      return 'circulation.return.requested'
    case 'borrowed':
      return 'circulation.borrowed'
    case 'returned':
      return 'circulation.returned'
    case 'overdue':
      return 'circulation.overdue'
    case 'renewalpending':
    case 'renewpending':
      return 'circulation.renewal.requested'
    default:
      return 'circulation.updated'
  }
}

function extractLogTimestamp(source: Record<string, any>): string | undefined {
  const raw =
    source.Timestamp ?? source.timestamp
    ?? source.UpdatedAt ?? source.updatedAt
    ?? source.CreatedAt ?? source.createdAt
    ?? source.BorrowDate ?? source.borrowDate
    ?? source.NgayMuon ?? source.ngayMuon
    ?? source.ReturnDate ?? source.returnDate
  if (!raw) return undefined
  return String(raw)
}

function formatLogTime(timestamp?: string | null): string {
  if (!timestamp) return '—'
  const formatted = formatVnDateTime(timestamp)
  return formatted === '-' ? '—' : formatted
}

function mapTransactionToLogItem(tx: any): LogItem {
  const status = tx.Status || tx.status || ''
  const eventType = mapTransactionEventType(status)
  const payload = {
    bookTitle: tx.TenSach || tx.tenSach || tx.bookTitle,
    cardNumber: tx.CardNumber || tx.cardNumber,
    status,
  }
  const timestamp = extractLogTimestamp(tx)
  const timeLabel = formatLogTime(timestamp)
  return buildLogItem(
    { id: tx.Id || tx.id, Timestamp: timestamp },
    eventType,
    payload,
    timeLabel,
    timestamp
  )
}

function getAlertActionTab(eventType: string): string | undefined {
  switch (eventType) {
    case 'circulation.borrow.requested':
    case 'circulation.return.requested':
    case 'circulation.renewal.requested':
    case 'circulation.overdue':
    case 'document.pending':
      return 'rules'
    case 'card.expiring':
      return 'cards-list'
    case 'system.error':
      return 'system-logs'
    default:
      return undefined
  }
}

function mapAlertItem(item: any): LogItem {
  const eventType = item.EventType || item.eventType || ''
  const payload = parseLogPayload(item.Data ?? item.data)
  const timestamp = extractLogTimestamp(item)
  const timeLabel = formatLogTime(timestamp)
  const log = buildLogItem(item, eventType, payload, timeLabel, timestamp)
  return { ...log, actionTab: getAlertActionTab(eventType) }
}

function mergeActivityLogs(apiLogs: LogItem[], transactionLogs: LogItem[]): LogItem[] {
  const merged = new Map<string, LogItem>()
  for (const log of [...apiLogs, ...transactionLogs]) {
    const key = `${log.eventType}|${log.message}|${log.timestamp || log.timeLabel}`
    if (!merged.has(key)) merged.set(key, log)
  }
  return Array.from(merged.values()).sort((a, b) => {
    const ta = parseUtcDate(a.timestamp)?.getTime() ?? 0
    const tb = parseUtcDate(b.timestamp)?.getTime() ?? 0
    return tb - ta
  })
}

function mapActivityLogItem(item: any): LogItem {
  const eventType = item.EventType || item.eventType || ''
  const payload = parseLogPayload(item.Data ?? item.data)
  const timestamp = extractLogTimestamp(item)
  const timeLabel = formatLogTime(timestamp)
  return buildLogItem(item, eventType, payload, timeLabel, timestamp)
}

const dashboardActivityLogs = computed(() => activityLogs.value.slice(0, 8))

const filteredActivityLogs = computed(() => {
  const list = activityLogs.value
  const f = logFilterType.value
  if (f === 'all') return list
  if (f === 'auth') {
    return list.filter((i) =>
      ['user.login', 'user.logout', 'user.registered'].includes(i.eventType)
    )
  }
  if (f === 'user') {
    return list.filter((i) =>
      (i.eventType.startsWith('user.') && !['user.login', 'user.logout', 'user.registered'].includes(i.eventType))
      || i.eventType.startsWith('card.')
      || i.eventType === 'staff.created'
    )
  }
  if (f === 'book') return list.filter((i) => i.eventType.startsWith('book.'))
  if (f === 'circulation') {
    return list.filter((i) =>
      i.eventType.startsWith('circulation.') || i.eventType === 'document.pending'
    )
  }
  return list
})

const paginatedActivityLogs = computed(() => {
  const list = filteredActivityLogs.value
  const start = (activityLogPage.value - 1) * activityLogPageSize.value
  return list.slice(start, start + activityLogPageSize.value)
})

const activityLogTotal = computed(() => filteredActivityLogs.value.length)

const fetchNotifications = async () => {
  notificationsLoading.value = true
  try {
    const res = await apiClient.get('/api/identity/Report/notifications')
    const raw = Array.isArray(res.data) ? res.data : (res.data?.Items || [])
    let items = raw
      .filter((item: any) => ALERT_EVENT_TYPES.has(item.EventType || item.eventType || ''))
      .map(mapAlertItem)

    // Dedupe by message to avoid backend duplicates
    const merged = new Map<string, LogItem>()
    for (const log of items) {
      const key = `${log.eventType}|${log.message}`
      if (!merged.has(key)) merged.set(key, log)
    }
    items = Array.from(merged.values()).sort((a, b) => {
      const ta = a.timestamp ? new Date(a.timestamp).getTime() : 0
      const tb = b.timestamp ? new Date(b.timestamp).getTime() : 0
      return tb - ta
    })

    notifications.value = items
  } catch {
    notifications.value = []
  } finally {
    notificationsLoading.value = false
  }
}

const fetchActivityLogs = async (forcedFilter?: string) => {
  activityLogsLoading.value = true
  try {
    const filterType = forcedFilter ?? logFilterType.value
    const res = await apiClient.get('/api/identity/Report/activity-logs', {
      params: {
        page: 1,
        pageSize: 500,
        type: filterType === 'all' ? undefined : filterType,
      },
    })
    const payload = res.data
    const rawItems = Array.isArray(payload)
      ? payload
      : (payload?.Items || payload?.items || [])
    let logs = rawItems.map(mapActivityLogItem)

    // Dedupe: chỉ loại bỏ log trùng hoàn toàn (cùng type + message + timestamp)
    // Giữ nguyên các event cùng loại nhưng khác thời điểm (vd: login nhiều lần)
    const merged = new Map<string, LogItem>()
    for (const log of logs) {
      const key = `${log.eventType}|${log.message}|${log.timestamp ?? log.timeLabel}`
      if (!merged.has(key)) merged.set(key, log)
    }
    logs = Array.from(merged.values()).sort((a, b) => {
      const ta = parseUtcDate(a.timestamp)?.getTime() ?? 0
      const tb = parseUtcDate(b.timestamp)?.getTime() ?? 0
      return tb - ta
    })

    activityLogs.value = logs
    if (activityLogPage.value > 1 && !paginatedActivityLogs.value.length) {
      activityLogPage.value = 1
    }
  } catch {
    activityLogs.value = []
  } finally {
    activityLogsLoading.value = false
  }
}

const onNotificationsOpen = (open: boolean) => {
  if (open) fetchNotifications()
}

const handleNotificationClick = (item: LogItem) => {
  showNotifications.value = false
  if (!item.actionTab) return
  switchTab(item.actionTab)
  if (item.actionTab === 'rules') {
    message.info('Đang mở trang Quản lý mượn trả để xử lý phiếu...')
  }
}

let notificationPollTimer: ReturnType<typeof setInterval> | null = null
let activityLogPollTimer: ReturnType<typeof setInterval> | null = null

const prefetchSpotlightData = async () => {
  try {
    const [readersRes, cardsRes] = await Promise.allSettled([
      apiClient.get('/api/identity/User/readers'),
      apiClient.get('/api/identity/User/cards'),
    ])
    if (readersRes.status === 'fulfilled') readersList.value = readersRes.value.data
    if (cardsRes.status === 'fulfilled') cardsList.value = cardsRes.value.data
  } catch {
    // Spotlight can still work after tab navigation
  }
}

const switchTab = (tabName: string) => {
  currentTab.value = tabName
  if (tabName === 'readers-list' || tabName === 'staff-list') {
    usersMenuOpen.value = true
  }
  if (tabName === 'finance-revenue' || tabName === 'finance-penalty') {
    financeMenuOpen.value = true
  }
  if (tabName === 'readers-list') {
    fetchReadersData()
  } else if (tabName === 'cards-list') {
    fetchCardsData()
  } else if (tabName === 'staff-list') {
    fetchStaffData()
  } else if (tabName === 'overview') {
    fetchActivityLogs('all')
  } else if (tabName === 'system-logs') {
    fetchActivityLogs()
  } else if (tabName === 'finance-revenue') {
    loadPaymentsIframe()
  } else if (tabName === 'finance-penalty') {
    loadFinesIframe()
  } else if (tabName === 'books') {
    loadBooksIframe()
  } else if (tabName === 'rules') {
    loadLoansIframe()
  } else if (tabName === 'reviews') {
    loadReviewsIframe()
  }
}

const goHome = () => {
  router.push('/')
}

const handleLogout = async () => {
  await logout()
  message.success('Đăng xuất thành công!')
  router.push('/')
}

// -------------------------------------------------------------
// [DATA MOCKING & API INTEGRATIONS]
// -------------------------------------------------------------

// N1 - Catalog Books Mock List
const mockBooks = ref([
  { id: 1, code: 'MS-OOP-001', title: 'Lập Trình Hướng Đối Tượng C#', author: 'Nguyễn Văn A', genre: 'Giáo trình', qty: 15 },
  { id: 2, code: 'MS-CC-002', title: 'Clean Code - Mã Sạch', author: 'Robert C. Martin', genre: 'Lập trình', qty: 8 },
  { id: 3, code: 'MS-MS-003', title: 'Kiến Trúc Microservices Thực Tế', author: 'Trần Văn B', genre: 'Kiến trúc PM', qty: 3 },
  { id: 4, code: 'MS-JS-004', title: 'Javascript Nâng Cao', author: 'Lê Hoàng C', genre: 'Lập trình', qty: 0 }
])

const showAddBookModal = () => {
  message.info('Catalog Service (N1) đang hoạt động. Tính năng tạo trực tiếp trên UI admin đang được kết nối!')
}

const editBook = (book: any) => {
  message.success(`Đang mở chỉnh sửa sách: ${book.title}`)
}

const deleteBook = (id: number) => {
  mockBooks.value = mockBooks.value.filter(b => b.id !== id)
  message.success('Xóa sách thành công khỏi Catalog!')
}

// Librarian - Xử lý Phiếu mượn trả
const mockLoans = ref([
  { id: 1, code: 'PH-9921', readerName: 'Lê Thùy Dung', cardNo: 'LIB-577064', bookTitle: 'Clean Code - Mã Sạch', requestDate: '18/05/2026', status: 'Chờ duyệt' },
  { id: 2, code: 'PH-9922', readerName: 'Nguyễn Hoàng Nam', cardNo: 'LIB-128941', bookTitle: 'Lập Trình Hướng Đối Tượng C#', requestDate: '17/05/2026', status: 'Chờ duyệt' },
  { id: 3, code: 'PH-9810', readerName: 'Phạm Minh Đức', cardNo: 'LIB-338291', bookTitle: 'Javascript Nâng Cao', requestDate: '15/05/2026', status: 'Đã phê duyệt' }
])

const approveLoan = (id: number) => {
  const loan = mockLoans.value.find(l => l.id === id)
  if (loan) {
    loan.status = 'Đã phê duyệt'
    message.success(`Đã phê duyệt cho Độc giả ${loan.readerName} mượn sách!`)
  }
}

const rejectLoan = (id: number) => {
  const loan = mockLoans.value.find(l => l.id === id)
  if (loan) {
    loan.status = 'Từ chối'
    message.warning(`Đã từ chối phiếu mượn của ${loan.readerName}.`)
  }
}

// Librarian - Tra cứu tình trạng
const searchCardNo = ref('')
const searchResult = ref<any>(null)

const handleSearchCard = () => {
  if (!searchCardNo.value) {
    message.error('Vui lòng nhập mã thẻ để tra cứu!')
    return
  }
  
  message.loading('Đang tra cứu cơ sở dữ liệu...', 0.5)
  setTimeout(() => {
    searchResult.value = {
      name: 'Lê Thùy Dung',
      cardNo: searchCardNo.value,
      borrowedBooks: 2,
      overdueBooks: 0,
      penaltyAmt: 0,
      activeLoans: [
        { title: 'Clean Code - Mã Sạch', dueDate: '01/06/2026', status: 'active' },
        { title: 'Kiến Trúc Microservices Thực Tế', dueDate: '25/05/2026', status: 'active' }
      ]
    }
    message.success('Tìm thấy thông tin độc giả!')
  }, 500)
}

// Librarian - Xác nhận trả sách
const returnStep = ref(1)
const returnBookCode = ref('')
const bookCondition = reactive({
  intact: true,
  clean: true,
  noLate: true
})

const handleConfirmReturn = () => {
  returnStep.value = 3
}

const resetReturnWizard = () => {
  returnStep.value = 1
  returnBookCode.value = ''
  bookCondition.intact = true
  bookCondition.clean = true
  bookCondition.noLate = true
}

// Circulation Data
const circulationStats = ref<any>({
  totalBorrows: 0,
  totalReturns: 0,
  activeBorrows: 0,
  overdueBorrows: 0,
  totalFines: 0,
  unpaidFines: 0,
  topBorrowedBooks: [],
  borrowingTrends: []
})

const transactionsList = ref<any[]>([])

function extractTransactionItems(data: unknown): any[] {
  if (Array.isArray(data)) return data
  if (data && typeof data === 'object') {
    const payload = data as Record<string, unknown>
    const items = payload.items ?? payload.Items
    if (Array.isArray(items)) return items
  }
  return []
}

function normalizeCirculationStats(raw: any, identityStats: any, transactions: any[]) {
  const monthly = identityStats?.MonthlyBorrowStats || identityStats?.monthlyBorrowStats || []
  const totalFromMonthly = monthly.reduce(
    (sum: number, m: any) => sum + (m.BorrowCount ?? m.borrowCount ?? 0),
    0
  )

  let totalBorrows = raw?.totalBorrows ?? raw?.TotalBorrows ?? 0
  if (!totalBorrows && totalFromMonthly) totalBorrows = totalFromMonthly
  if (!totalBorrows && transactions.length) totalBorrows = transactions.length

  let borrowingTrends = raw?.borrowingTrends ?? raw?.BorrowingTrends ?? []
  if (!borrowingTrends.length && monthly.length) {
    borrowingTrends = monthly.map((m: any) => ({
      month: m.Month ?? m.month ?? '',
      borrows: m.BorrowCount ?? m.borrowCount ?? 0,
    }))
  }

  let topBorrowedBooks = raw?.topBorrowedBooks ?? raw?.TopBorrowedBooks ?? []
  if (!topBorrowedBooks.length) {
    const popular = identityStats?.TopPopularBooks ?? identityStats?.topPopularBooks ?? []
    const maxBorrow = Math.max(...popular.map((p: any) => p.BorrowTimes ?? p.borrowTimes ?? 0), 1)
    topBorrowedBooks = popular.map((p: any) => {
      const borrowCount = p.BorrowTimes ?? p.borrowTimes ?? 0
      return {
        bookName: p.BookTitle ?? p.bookTitle ?? '',
        borrowCount,
        percentage: Math.round((borrowCount / maxBorrow) * 100),
      }
    })
  }

  return {
    totalBorrows,
    totalReturns: raw?.totalReturns ?? raw?.TotalReturns ?? 0,
    activeBorrows: raw?.activeBorrows ?? raw?.ActiveBorrows ?? 0,
    overdueBorrows: raw?.overdueBorrows ?? raw?.OverdueBorrows ?? 0,
    totalFines: raw?.totalFines ?? raw?.TotalFines ?? 0,
    unpaidFines: raw?.unpaidFines ?? raw?.UnpaidFines ?? 0,
    topBorrowedBooks,
    borrowingTrends,
  }
}

const fetchTransactionsData = async () => {
  try {
    const res = await apiClient.get('/api/circulation/transactions', { params: { pageSize: 200 } })
    transactionsList.value = extractTransactionItems(res.data)
  } catch (err) {
    console.error('Fetch Transactions Error:', err)
    transactionsList.value = []
  }
}

// Area Chart for Revenue Stream
const revenueSeries = computed(() => {
  const fines = circulationStats.value?.totalFines || 0
  if (!fines) {
    return [{ name: 'Doanh thu', data: [0, 0, 0, 0, 0, 0] }]
  }
  return [{
    name: 'Doanh thu',
    data: [fines * 0.1, fines * 0.25, fines * 0.3, fines * 0.5, fines * 0.8, fines]
  }]
})

const revenueOptions = computed(() => {
  const trends = circulationStats.value?.borrowingTrends || []
  let categories = trends.length > 0 ? trends.map((t: any) => t.month || '') : ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun']

  return {
    chart: { type: 'area', fontFamily: 'Inter, sans-serif', toolbar: { show: false } },
    colors: ['#CF7C07'], // Gold theme for revenue
    dataLabels: { enabled: false },
    stroke: { curve: 'smooth', width: 3 },
    xaxis: {
      categories: categories,
      axisBorder: { show: false },
      axisTicks: { show: false },
      labels: { style: { colors: '#6C7A78', fontSize: '11px', fontFamily: 'Inter, sans-serif' } }
    },
    yaxis: { show: false },
    grid: {
      borderColor: 'rgba(0,0,0,0.05)',
      strokeDashArray: 4,
      yaxis: { lines: { show: true } },
      xaxis: { lines: { show: false } }
    },
    fill: {
      type: 'gradient',
      gradient: { shadeIntensity: 1, opacityFrom: 0.45, opacityTo: 0.05, stops: [0, 100] }
    },
    tooltip: {
      theme: 'light',
      y: { formatter: (val: number) => val.toLocaleString() + ' VNĐ' }
    }
  }
})

// Books Categories Chart Data (Donut)
const booksCategoriesData = ref<[string, number][]>([])
const bookCategoryColors = [
  '#1E5652', '#CF7C07', '#2a9d8f', '#e9c46a', '#f4a261',
  '#e76f51', '#264653', '#457b9d', '#1d3557', '#6d597a', '#b5651d',
]

const bookCategoryDonutOptions = computed(() => {
  const totalTitles = booksCategoriesData.value.reduce((sum, item) => sum + item[1], 0)
  return {
    chart: {
      type: 'donut',
      fontFamily: 'Inter, sans-serif',
      toolbar: { show: false },
      animations: { enabled: true, easing: 'easeinout', speed: 700 },
    },
    labels: booksCategoriesData.value.map(item => item[0]),
    colors: bookCategoryColors,
    stroke: { show: true, colors: ['#ffffff'], width: 3 },
    dataLabels: { enabled: false },
    legend: { show: false },
    plotOptions: {
      pie: {
        expandOnClick: false,
        donut: {
          size: '72%',
          labels: {
            show: true,
            name: {
              show: true,
              fontSize: '12px',
              color: '#6C7A78',
              fontWeight: 600,
              offsetY: -6,
            },
            value: {
              show: true,
              fontSize: '28px',
              fontWeight: 800,
              color: '#114B44',
              offsetY: 6,
            },
            total: {
              show: true,
              showAlways: true,
              label: 'Tổng số',
              color: '#6C7A78',
              fontSize: '12px',
              fontWeight: 600,
              formatter: () => String(totalTitles),
            },
          },
        },
      },
    },
    tooltip: {
      theme: 'light',
      fixed: { enabled: false },
      y: { formatter: (val: number) => `${val} đầu sách` },
    },
    states: {
      hover: { filter: { type: 'lighten', value: 0.08 } },
      active: { filter: { type: 'darken', value: 0.06 } },
    },
  }
})

const bookCategoryDonutSeries = computed(() => {
  return booksCategoriesData.value.map(item => item[1])
})

// Report Data call
const fetchDashboardData = async () => {
  if (!accessToken.value) return

  loading.value = true
  try {
    const [dashRes, booksRes, circRes, txRes] = await Promise.allSettled([
      apiClient.get('/api/identity/Report/dashboard'),
      apiClient.get('/api/catalog/books'),
      apiClient.get('/api/circulation/reports/dashboard'),
      apiClient.get('/api/circulation/transactions', { params: { pageSize: 200 } }),
    ])

    let identityStats: any = null

    if (dashRes.status === 'fulfilled') {
      stats.value = dashRes.value.data
      identityStats = dashRes.value.data
    } else {
      stats.value = {
        UserStats: { TotalUsers: 0, ActiveUsers: 0, InactiveUsers: 0 },
        CardStats: { TotalCards: 0, ActiveCards: 0, LockedCards: 0, ExpiredCards: 0 },
        MonthlyBorrowStats: [],
        TopPopularBooks: [],
      }
    }

    if (booksRes.status === 'fulfilled') {
      const booksData = booksRes.value.data
      if (Array.isArray(booksData)) {
        totalBooks.value = booksData.reduce((acc: number, b: any) => acc + (b.soLuong || 0), 0)

        const catMap: Record<string, number> = {}
        booksData.forEach((b: any) => {
          let genre = b.theLoai ? b.theLoai.split(',')[0].trim() : 'Khác'
          if (!genre) genre = 'Khác'
          catMap[genre] = (catMap[genre] || 0) + 1
        })

        const sortedCats = Object.entries(catMap).sort((a, b) => b[1] - a[1])
        booksCategoriesData.value = sortedCats
      } else {
        totalBooks.value = 0
        booksCategoriesData.value = []
      }
    }

    const transactions =
      txRes.status === 'fulfilled' ? extractTransactionItems(txRes.value.data) : []
    transactionsList.value = transactions

    const circRaw = circRes.status === 'fulfilled' ? circRes.value.data : null
    circulationStats.value = normalizeCirculationStats(circRaw, identityStats, transactions)
  } catch (error: any) {
    console.error('Fetch Dashboard Error:', error)
  } finally {
    loading.value = false
  }
}

// ----- Detail Modals State -----
const showReadersModal = ref(false)
const showCardsModal = ref(false)
const showBorrowsModal = ref(false)
const showBooksModal = ref(false)
const detailLoading = ref(false)
const readersList = ref<any[]>([])
const cardsList = ref<any[]>([])

// ----- Import / Export / Edit Reader State -----
const showEditReaderDrawer = ref(false)
const editReaderLoading = ref(false)
const editReaderForm = reactive({
  id: '',
  fullName: '',
  email: '',
  phoneNumber: ''
})
const fileInputRef = ref<HTMLInputElement | null>(null)

const openEditReaderDrawer = (reader: any) => {
  editReaderForm.id = reader.Id || reader.id
  editReaderForm.fullName = reader.FullName || reader.fullName
  editReaderForm.email = reader.Email || reader.email
  editReaderForm.phoneNumber = reader.PhoneNumber || reader.phoneNumber || ''
  showEditReaderDrawer.value = true
}

const saveReaderEdit = async () => {
  if (!editReaderForm.fullName || !editReaderForm.email) {
    message.warning('Vui lòng nhập đầy đủ họ tên và email')
    return
  }
  editReaderLoading.value = true
  try {
    await apiClient.put(`/api/identity/User/users/${editReaderForm.id}`, {
      FullName: editReaderForm.fullName,
      Email: editReaderForm.email,
      PhoneNumber: editReaderForm.phoneNumber || null
    })
    message.success('Cập nhật thông tin độc giả thành công!')
    showEditReaderDrawer.value = false
    await fetchReadersData()
  } catch (err: any) {
    message.error(err.response?.data?.Message || 'Lỗi khi cập nhật độc giả')
  } finally {
    editReaderLoading.value = false
  }
}

const triggerFileUpload = () => {
  if (fileInputRef.value) fileInputRef.value.click()
}

// Updated Excel import/export logic

type ExcelImportRow = Record<string, unknown>
type BulkImportReader = {
  Username: string
  Email: string
  FullName: string
  Password: string
  PhoneNumber?: string
}

// Updated import function to handle .xlsx files
const importReadersFromExcel = async (event: Event) => {
  const target = event.target as HTMLInputElement
  if (!target.files || target.files.length === 0) return
  const file = target.files[0]
  if (!file || !file.name.endsWith('.xlsx')) {
    message.error('Vui lòng chọn file định dạng .xlsx')
    target.value = ''
    return
  }

  importLoading.value = true
  try {
    const rawData: Array<any> = await importXlsxFile(file)
    const data = normalizeExcelRows(rawData)
    if (!data.length) {
      message.error('File Excel không có dữ liệu')
      return
    }

    const readers = data
      .map((row: ExcelImportRow) => ({
        Username: String(row.username || row.Username || '').trim(),
        Email: String(row.email || row.Email || '').trim(),
        FullName: String(row.fullname || row.FullName || row.fullName || '').trim(),
        Password: String(row.password || row.Password || '').trim(),
        PhoneNumber: String(row.phonenumber || row.PhoneNumber || row.phoneNumber || '').trim() || undefined,
      }))
      .filter((row: BulkImportReader) => row.Username || row.Email || row.FullName)

    if (!readers.length) {
      message.error('Không tìm thấy dòng hợp lệ. Cần các cột: Username, Email, FullName, Password')
      return
    }

    const missingRequired = readers.filter(
      (row: BulkImportReader) => !row.Username || !row.Email || !row.FullName || !row.Password
    )
    if (missingRequired.length) {
      message.error(`${missingRequired.length} dòng thiếu Username/Email/FullName/Password`)
      return
    }

    message.loading({ content: `Đang import ${readers.length} độc giả...`, key: 'importExcel', duration: 0 })

    const res = await apiClient.post('/api/identity/User/readers/bulk-import', { Readers: readers })
    const { SuccessCount, successCount, ErrorCount, errorCount, Errors, errors, Message, message: msg } = res.data
    const ok = SuccessCount ?? successCount ?? 0
    const fail = ErrorCount ?? errorCount ?? 0
    const errList: string[] = Errors ?? errors ?? []

    message.success({
      content: Message || msg || `Import hoàn tất: ${ok} thành công, ${fail} thất bại.`,
      key: 'importExcel',
      duration: 5,
    })

    if (errList.length) {
      Modal.warning({
        title: `Chi tiết lỗi import (${fail})`,
        content: errList.slice(0, 30).join('\n') + (errList.length > 30 ? `\n... và ${errList.length - 30} lỗi khác` : ''),
        width: 560,
      })
    }

    await fetchReadersData()
    await fetchDashboardData()
    fetchActivityLogs()
  } catch (err: any) {
    const apiMsg = err.response?.data?.Message || err.response?.data?.message
    message.error({ content: apiMsg || 'Lỗi khi import file Excel', key: 'importExcel' })
  } finally {
    importLoading.value = false
    target.value = ''
  }
}

// Updated export function to use Excel helper
const exportReadersToExcel = () => {
  if (!filteredReaders.value || filteredReaders.value.length === 0) {
    message.warning('Không có dữ liệu để xuất!')
    return
  }
  const blob = exportReadersToXlsx(filteredReaders.value)
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.setAttribute('href', url)
  const ts = new Date().toISOString().replace(/[:T]/g, '-').slice(0, 19)
  link.setAttribute('download', `danh_sach_doc_gia_${ts}.xlsx`)
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  URL.revokeObjectURL(url)
}

// UI button updates (replace CSV button labels and accept attribute)
// In template section, change accept=".csv" to accept=".xlsx" and button text.
// This will be handled in the template modifications below.


// ----- Reader Detail Drawer -----
const selectedReader = ref<any>(null)
const openReaderDrawer = (reader: any) => {
  router.push(`/reader/${reader.Id || reader.id}`)
}

const formatDate = (dateStr: string) => formatVnDate(dateStr)

// Helper: handle both camelCase (from .NET JSON) and PascalCase
const getUserStat = (key: string) => {
  if (!stats.value) return 0
  const us = stats.value.UserStats || stats.value.userStats
  if (!us) return 0
  const camelKey = key.charAt(0).toLowerCase() + key.slice(1)
  return us[key] ?? us[camelKey] ?? 0
}

const getCardStat = (key: string) => {
  if (!stats.value) return 0
  const cs = stats.value.CardStats || stats.value.cardStats
  if (!cs) return 0
  const camelKey = key.charAt(0).toLowerCase() + key.slice(1)
  return cs[key] ?? cs[camelKey] ?? 0
}

function syncCardStats() {
  if (!stats.value) return
  const cs = stats.value.CardStats || stats.value.cardStats
  if (!cs) return
  const list = cardsList.value || []
  const countBy = (status: string) =>
    list.filter((c: any) => (c.Status || c.status) === status).length
  cs.TotalCards = list.length
  cs.ActiveCards = countBy('Active')
  cs.LockedCards = countBy('Locked')
  cs.ExpiredCards = countBy('Expired')
  if (stats.value.cardStats) {
    stats.value.cardStats = { ...cs }
  }
  if (stats.value.CardStats) {
    stats.value.CardStats = { ...cs }
  }
}

function syncUserStats() {
  if (!stats.value) return
  const us = stats.value.UserStats || stats.value.userStats
  if (!us) return
  const list = readersList.value || []
  us.TotalUsers = list.length
  us.ActiveUsers = list.filter((r: any) => r.IsActive ?? r.isActive).length
  us.InactiveUsers = us.TotalUsers - us.ActiveUsers
}

const readersSearchQuery = ref('')
const readersStatusFilter = ref('all')
const cardsSearchQuery = ref('')
const cardsStatusFilter = ref('all')

const globalSearchQuery = ref('')
const showSpotlight = ref(false)

const spotlightReaders = computed(() => {
  const q = globalSearchQuery.value.trim().toLowerCase()
  if (!q || !readersList.value) return []
  return readersList.value.filter((r: any) => {
    const name = (r.FullName || r.fullName || '').toLowerCase()
    const email = (r.Email || r.email || '').toLowerCase()
    const username = (r.Username || r.username || '').toLowerCase()
    return name.includes(q) || email.includes(q) || username.includes(q)
  }).slice(0, 3).map((r: any) => ({
    type: 'reader',
    title: r.FullName || r.fullName,
    desc: `@${r.Username || r.username} - ${r.Email || r.email}`,
    item: r
  }))
})

const spotlightCards = computed(() => {
  const q = globalSearchQuery.value.trim().toLowerCase()
  if (!q || !cardsList.value) return []
  return cardsList.value.filter((c: any) => {
    const num = (c.CardNumber || c.cardNumber || '').toLowerCase()
    const owner = (c.OwnerName || c.ownerName || '').toLowerCase()
    return num.includes(q) || owner.includes(q)
  }).slice(0, 3).map((c: any) => ({
    type: 'card',
    title: c.CardNumber || c.cardNumber,
    desc: `Chủ thẻ: ${c.OwnerName || c.ownerName}`,
    item: c
  }))
})

const spotlightResults = computed(() => [...spotlightReaders.value, ...spotlightCards.value])

const handleSpotlightSelect = (result: any) => {
  showSpotlight.value = false
  globalSearchQuery.value = ''
  if (result.type === 'reader') {
    switchTab('readers-list')
    readersSearchQuery.value = result.title
    setTimeout(() => {
      openReaderDrawer(result.item)
    }, 100)
  } else if (result.type === 'card') {
    switchTab('cards-list')
    cardsSearchQuery.value = result.title
  }
}

const handleGlobalSearch = async () => {
  const q = globalSearchQuery.value.trim()
  if (!q) return

  if (!readersList.value.length || !cardsList.value.length) {
    await prefetchSpotlightData()
  }

  const qLower = q.toLowerCase()
  const cardMatch = cardsList.value.find((c: any) => {
    const num = (c.CardNumber || c.cardNumber || '').toLowerCase()
    const owner = (c.OwnerName || c.ownerName || '').toLowerCase()
    return num.includes(qLower) || owner.includes(qLower)
  })

  if (cardMatch) {
    switchTab('cards-list')
    cardsSearchQuery.value = q
  } else {
    switchTab('readers-list')
    readersSearchQuery.value = q
  }

  globalSearchQuery.value = ''
  showSpotlight.value = false
}

const readersCurrentPage = ref(1)
const readersPageSize = ref(15)
const cardsCurrentPage = ref(1)
const cardsPageSize = ref(9)

const onReadersPageChange = (page: number, size: number) => {
  readersCurrentPage.value = page
  readersPageSize.value = size
}

// Readers columns — AntD customFilterDropdown (icon + search input + Search/Reset/close)
const readersColumns = [
  {
    title: 'STT',
    dataIndex: 'stt',
    key: 'stt',
    width: 60,
    align: 'center',
    fixed: 'left',
  },
  {
    title: 'Họ và Tên',
    dataIndex: 'fullName',
    key: 'fullName',
    width: 220,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const name = record.FullName || record.fullName || ''
      return name.toString().toLowerCase().includes((value as string).toLowerCase())
    },
    sorter: (a: any, b: any) => {
      const nameA = a.FullName || a.fullName || ''
      const nameB = b.FullName || b.fullName || ''
      return nameA.localeCompare(nameB)
    }
  },
  {
    title: 'Tên Đăng nhập',
    dataIndex: 'username',
    key: 'username',
    width: 160,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const u = record.Username || record.username || ''
      return u.toString().toLowerCase().includes((value as string).toLowerCase())
    },
    sorter: (a: any, b: any) => {
      const uA = a.Username || a.username || ''
      const uB = b.Username || b.username || ''
      return uA.localeCompare(uB)
    }
  },
  {
    title: 'Email',
    dataIndex: 'email',
    key: 'email',
    width: 220,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const em = record.Email || record.email || ''
      return em.toString().toLowerCase().includes((value as string).toLowerCase())
    },
  },
  {
    title: 'Mã Thẻ',
    dataIndex: 'cardNumber',
    key: 'cardNumber',
    width: 140,
    customFilterDropdown: true,
    onFilter: (value: any, record: any) => {
      const c = record.LibraryCard?.CardNumber || record.libraryCard?.cardNumber || ''
      return c.toString().toLowerCase().includes((value as string).toLowerCase())
    },
  },
  {
    title: 'Trạng Thái',
    dataIndex: 'status',
    key: 'status',
    width: 130,
    filters: [
      { text: 'Hoạt động', value: true },
      { text: 'Bị khóa', value: false },
    ],
    onFilter: (value: any, record: any) => {
      const isActive = record.IsActive ?? record.isActive ?? false
      return isActive === value
    }
  },
  {
    title: 'Thao tác',
    key: 'action',
    align: 'center',
    width: 240,
    fixed: 'right',
  }
]

const filteredReaders = computed(() => {
  if (!readersList.value) return []
  const q = readersSearchQuery.value.trim().toLowerCase()
  return readersList.value.filter((r: any) => {
    const fullName = (r.FullName || r.fullName || '').toLowerCase()
    const username = (r.Username || r.username || '').toLowerCase()
    const email = (r.Email || r.email || '').toLowerCase()
    const cardNumber = (r.LibraryCard?.CardNumber || r.libraryCard?.cardNumber || '').toLowerCase()
    const isActive = r.IsActive ?? r.isActive ?? false

    const matchesSearch = !q || fullName.includes(q) || username.includes(q) || email.includes(q) || cardNumber.includes(q)
    let matchesStatus = true
    if (readersStatusFilter.value === 'active') matchesStatus = isActive
    if (readersStatusFilter.value === 'locked') matchesStatus = !isActive
    return matchesSearch && matchesStatus
  })
})


const filteredCards = computed(() => {
  if (!cardsList.value) return []
  return cardsList.value.filter((c: any) => {
    const cardNum = (c.CardNumber || c.cardNumber || '').toLowerCase()
    const owner = (c.OwnerName || c.ownerName || '').toLowerCase()
    const q = cardsSearchQuery.value.toLowerCase()
    
    const matchesSearch = cardNum.includes(q) || owner.includes(q)
    
    const status = (c.Status || c.status || '').toLowerCase()
    let matchesStatus = true
    if (cardsStatusFilter.value === 'active') {
      matchesStatus = status === 'active'
    } else if (cardsStatusFilter.value === 'locked') {
      matchesStatus = status === 'locked'
    } else if (cardsStatusFilter.value === 'expired') {
      matchesStatus = status === 'expired'
    }
    
    return matchesSearch && matchesStatus
  })
})

const paginatedReaders = computed(() => {
  const start = (readersCurrentPage.value - 1) * readersPageSize.value
  return filteredReaders.value.slice(start, start + readersPageSize.value)
})

const paginatedCards = computed(() => {
  const start = (cardsCurrentPage.value - 1) * cardsPageSize.value
  return filteredCards.value.slice(start, start + cardsPageSize.value)
})

const toggleUserStatus = async (user: any) => {
  const wasActive = user.IsActive ?? user.isActive ?? true
  message.loading('Đang cập nhật trạng thái người dùng...', 0.5)
  try {
    const res = await apiClient.put(`/api/identity/User/users/${user.Id || user.id}/toggle-status`)
    const isNowActive = res.data?.IsActive ?? !wasActive
    const cardStatus = res.data?.CardStatus || (isNowActive ? 'Active' : 'Locked')

    if (user.IsActive !== undefined) user.IsActive = isNowActive
    if (user.isActive !== undefined) user.isActive = isNowActive

    // Khóa/mở TK → đồng bộ thẻ trong danh sách độc giả
    const readerCard = user.LibraryCard || user.libraryCard
    if (readerCard) {
      readerCard.Status = cardStatus
      readerCard.status = cardStatus
    }

    // Đồng bộ cardsList cho biểu đồ thống kê
    const cardNumber = readerCard?.CardNumber || readerCard?.cardNumber
    if (cardNumber) {
      const cardInList = cardsList.value.find(
        (c: any) => (c.CardNumber || c.cardNumber) === cardNumber
      )
      if (cardInList) {
        cardInList.Status = cardStatus
        cardInList.status = cardStatus
      }
    }

    syncUserStats()
    syncCardStats()

    message.success(
      isNowActive
        ? 'Đã mở khóa tài khoản và thẻ thư viện'
        : 'Đã khóa tài khoản — thẻ thư viện cũng bị khóa theo'
    )
    fetchActivityLogs()
  } catch (err: any) {
    console.error(err)
    message.error(err.response?.data?.Message || 'Cập nhật trạng thái thất bại!')
  }
}

const toggleCardStatus = async (card: any) => {
  const newStatus = (card.Status || card.status) === 'Active' ? 'Locked' : 'Active'
  message.loading('Đang cập nhật trạng thái thẻ...', 0.5)
  try {
    await apiClient.put(`/api/identity/User/cards/${card.CardNumber || card.cardNumber}/status`, {
      Status: newStatus
    })
    card.Status = newStatus
    card.status = newStatus

    // Chỉ khóa thẻ — không đổi trạng thái tài khoản độc giả
    syncCardStats()

    message.success(
      newStatus === 'Locked'
        ? 'Đã khóa thẻ — độc giả không thể mượn sách (tài khoản vẫn hoạt động)'
        : 'Đã mở khóa thẻ — độc giả có thể mượn sách trở lại'
    )
    fetchActivityLogs()
  } catch (err: any) {
    console.error(err)
    message.error(err.response?.data?.Message || 'Cập nhật trạng thái thẻ thất bại!')
  }
}

const formatCardNumber = (numStr: string) => {
  if (!numStr) return 'LIB - XXXX - XXXX'
  const cleaned = numStr.replace(/-/g, ' ')
  return cleaned.toUpperCase()
}

const fetchReadersData = async () => {
  detailLoading.value = true
  try {
    const res = await apiClient.get('/api/identity/User/readers')
    readersList.value = res.data
    syncUserStats()
  } catch (err) {
    console.error('Fetch readers error:', err)
    readersList.value = []
  } finally {
    detailLoading.value = false
  }
}

const fetchCardsData = async () => {
  detailLoading.value = true
  try {
    const res = await apiClient.get('/api/identity/User/cards')
    cardsList.value = res.data
    syncCardStats()
  } catch (err) {
    console.error('Fetch cards error:', err)
    cardsList.value = []
  } finally {
    detailLoading.value = false
  }
}

const openDetailModal = (type: string) => {
  if (type === 'readers') {
    switchTab('readers-list')
  } else if (type === 'cards') {
    switchTab('cards-list')
  } else if (type === 'borrows') {
    fetchTransactionsData()
    showBorrowsModal.value = true
  } else if (type === 'books') {
    showBooksModal.value = true
  }
}

watch(logFilterType, () => {
  activityLogPage.value = 1
  fetchActivityLogs()
})

watch(
  () => currentTab.value,
  (tab) => {
    if (tab === 'books') loadBooksIframe()
    if (tab === 'rules') loadLoansIframe()
    if (tab === 'finance-revenue') loadPaymentsIframe()
    if (tab === 'finance-penalty') loadFinesIframe()
    if (tab === 'system-logs') fetchActivityLogs()
  }
)

onMounted(() => {
  if (!isAuthorized.value) {
    message.error('Bạn cần tài khoản Admin hoặc Thủ thư để xem trang này!')
    setTimeout(() => {
      router.push('/')
    }, 2000)
  } else {
    fetchDashboardData()
    prefetchSpotlightData()
    fetchNotifications()
    fetchActivityLogs(currentTab.value === 'overview' ? 'all' : undefined)
    notificationPollTimer = setInterval(fetchNotifications, 30000)
    activityLogPollTimer = setInterval(() => fetchActivityLogs(currentTab.value === 'overview' ? 'all' : undefined), 30000)
    if (currentTab.value === 'books') loadBooksIframe()
    if (currentTab.value === 'rules') loadLoansIframe()
    if (currentTab.value === 'finance-revenue') loadPaymentsIframe()
    if (currentTab.value === 'finance-penalty') loadFinesIframe()
  }
})

onUnmounted(() => {
  if (notificationPollTimer) clearInterval(notificationPollTimer)
  if (activityLogPollTimer) clearInterval(activityLogPollTimer)
})
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Outfit:wght@300;400;500;600;700&family=Inter:wght@300;400;500;600;700&display=swap');

.books-iframe-loading {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  color: #1E5652;
  font-weight: 500;
}

.finance-coming-soon {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 80px 24px;
  margin-top: 24px;
  background: white;
  border-radius: 16px;
  border: 1px solid #E8EDE9;
  box-shadow: 0 2px 12px rgba(17,75,68,0.05);
  text-align: center;
  color: #4a5568;
}
.finance-coming-soon .fcs-icon {
  font-size: 56px;
  margin-bottom: 16px;
  opacity: 0.7;
}
.finance-coming-soon h3 {
  font-size: 20px;
  font-weight: 700;
  color: #114B44;
  margin: 0 0 8px;
}
.finance-coming-soon p {
  font-size: 14px;
  color: #718096;
  margin: 0;
  max-width: 400px;
  line-height: 1.6;
}

/* ── SETTINGS ─────────────────────────────────────────────── */
.settings-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 20px;
  padding: 0 0 8px;
}
@media (max-width: 900px) { .settings-grid { grid-template-columns: 1fr; } }

.settings-card {
  background: #ffffff;
  border-radius: 16px;
  border: 1px solid #E8EDE9;
  box-shadow: 0 2px 10px rgba(17,75,68,0.04);
  overflow: hidden;
  display: flex;
  flex-direction: column;
}
.settings-card-header {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 20px 24px 16px;
  border-bottom: 1px solid #F3F4F6;
}
.settings-card-icon {
  width: 44px; height: 44px;
  border-radius: 12px;
  display: flex; align-items: center; justify-content: center;
  font-size: 20px;
  flex-shrink: 0;
}
.settings-card-header h4 {
  margin: 0 0 3px;
  font-size: 15px;
  font-weight: 700;
  color: #1a202c;
}
.settings-card-header p {
  margin: 0;
  font-size: 12px;
  color: #9CA3AF;
}
.settings-fields {
  padding: 20px 24px;
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 14px;
}
.settings-field-row { display: flex; flex-direction: column; gap: 6px; }
.settings-field-row label {
  font-size: 12px;
  font-weight: 600;
  color: #6B7280;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}
.settings-field-row.two-col {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
}
.settings-field-row.two-col > div { display: flex; flex-direction: column; gap: 6px; }
.settings-field-row.two-col label {
  font-size: 12px;
  font-weight: 600;
  color: #6B7280;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}
.settings-divider {
  font-size: 12px;
  font-weight: 700;
  color: #9CA3AF;
  text-transform: uppercase;
  letter-spacing: 0.8px;
  padding: 6px 0 2px;
  border-top: 1px solid #F3F4F6;
  margin-top: 4px;
}
.settings-card-footer {
  padding: 16px 24px;
  border-top: 1px solid #F3F4F6;
  display: flex;
  justify-content: flex-end;
}
.btn-settings-save {
  background: linear-gradient(135deg, #1E5652, #114B44) !important;
  border: none !important;
  border-radius: 8px !important;
  font-weight: 600 !important;
  display: inline-flex !important;
  align-items: center !important;
  gap: 6px !important;
}
.btn-danger-save {
  background: linear-gradient(135deg, #DC2626, #991B1B) !important;
}

/* Logo / avatar upload */
.logo-upload-area {
  display: flex;
  align-items: center;
  gap: 14px;
}
.logo-preview {
  width: 56px; height: 56px;
  border-radius: 12px;
  background: #F3F4F6;
  border: 1px dashed #D1D5DB;
  display: flex; align-items: center; justify-content: center;
  flex-shrink: 0;
}
.avatar-preview { border-radius: 50% !important; }
.btn-upload-logo {
  border-radius: 8px !important;
  font-size: 13px !important;
  color: #1E5652 !important;
  border-color: #1E5652 !important;
}

/* Toggle rows */
.settings-toggle-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
  padding: 12px 0;
  border-bottom: 1px solid #F9FAFB;
}
.settings-toggle-row:last-of-type { border-bottom: none; }
.toggle-label {
  display: block;
  font-size: 14px;
  font-weight: 600;
  color: #374151;
}
.toggle-desc {
  display: block;
  font-size: 12px;
  color: #9CA3AF;
  margin-top: 2px;
}

/* Backup */
.backup-action-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  padding: 14px;
  background: #F9FAFB;
  border-radius: 10px;
  border: 1px solid #E5E7EB;
}
.backup-info { display: flex; align-items: center; gap: 12px; }
.backup-info-icon {
  width: 40px; height: 40px;
  border-radius: 10px;
  background: #EEF2FF;
  color: #6366F1;
  font-size: 18px;
  display: flex; align-items: center; justify-content: center;
  flex-shrink: 0;
}
.backup-info-icon.restore { background: #F0FFF4; color: #38A169; }
.backup-title { display: block; font-size: 13px; font-weight: 600; color: #374151; }
.backup-desc  { display: block; font-size: 12px; color: #9CA3AF; margin-top: 2px; }
.btn-backup  { border-radius: 8px !important; color: #6366F1 !important; border-color: #6366F1 !important; font-size:13px !important; }
.btn-restore { border-radius: 8px !important; color: #38A169 !important; border-color: #38A169 !important; font-size:13px !important; }
.backup-warning {
  font-size: 12px;
  color: #D97706;
  background: #FFFBEB;
  border: 1px solid #FCD34D;
  border-radius: 8px;
  padding: 10px 14px;
  display: flex;
  align-items: flex-start;
  gap: 8px;
  line-height: 1.5;
  margin-top: 4px;
}

.dashboard-container {
  display: flex;
  min-height: 100vh;
  /* Perfect mix-match with general application background #FAF9F6 */
  background-color: #FAF9F6;
  font-family: 'Outfit', 'Inter', sans-serif;
  color: #2d3748;
  overflow-x: hidden;
}

/* Access Denied Overlay */
.access-denied {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100vw;
  height: 100vh;
  background: rgba(255, 255, 255, 0.9);
  position: fixed;
  top: 0;
  left: 0;
  z-index: 9999;
}

.denied-card {
  background: white;
  border: 1px solid rgba(0,0,0,0.1);
  padding: 40px;
  border-radius: 24px;
  text-align: center;
  max-width: 480px;
  box-shadow: 0 20px 40px rgba(0,0,0,0.05);
}

.denied-icon {
  font-size: 64px;
  margin-bottom: 20px;
}

.denied-card h2 {
  font-weight: 700;
  color: #1E5652;
}

.btn-home {
  background-color: #CF7C07 !important;
  border-color: #CF7C07 !important;
  border-radius: 20px !important;
  font-weight: 600 !important;
  height: auto !important;
}

/* LEFT SIDEBAR */
.dashboard-sidebar {
  width: 260px;
  background: linear-gradient(160deg, #e8f5e9 0%, #eef7ee 60%, #e0f2e0 100%);
  border-right: 1px solid rgba(17, 75, 68, 0.1);
  padding: 32px 20px 28px 20px;
  display: flex;
  flex-direction: column;
  color: #114B44;
  flex-shrink: 0;
  box-shadow: 2px 0 16px rgba(17,75,68,0.06);
}

/* SmartLib Brand Logo */
.sidebar-brand {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 28px;
  padding-bottom: 20px;
  border-bottom: 1px solid rgba(17, 75, 68, 0.1);
}

.brand-icon {
  width: 52px;
  height: 52px;
  border-radius: 14px;
  background: linear-gradient(135deg, #1E5652 0%, #114B44 100%);
  display: flex;
  justify-content: center;
  align-items: center;
  flex-shrink: 0;
  box-shadow: 0 6px 20px rgba(30,86,82,0.35);
  animation: floatBrand 3s ease-in-out infinite;
}

@keyframes floatBrand {
  0% { transform: translateY(0px); }
  50% { transform: translateY(-4px); }
  100% { transform: translateY(0px); }
}

.brand-icon .anticon {
  color: #F2C94C !important; /* Màu vàng Gold cho nổi bật */
  animation: pulseBrand 2s ease-in-out infinite;
}

@keyframes pulseBrand {
  0% { transform: scale(1); }
  50% { transform: scale(1.15); }
  100% { transform: scale(1); }
}

.brand-text {
  display: flex;
  flex-direction: column;
}

.brand-name {
  font-size: 22px;
  font-weight: 800;
  color: #114B44;
  letter-spacing: 0.5px;
  line-height: 1.1;
}

.brand-sub {
  font-size: 11px;
  color: #5C6765;
  font-weight: 600;
  letter-spacing: 0.5px;
}

.menu-group-label {
  font-size: 10px;
  color: #8C9996;
  font-weight: 800;
  letter-spacing: 1.5px;
  margin: 20px 0 10px 10px;
}

.sidebar-menu {
  display: flex;
  flex-direction: column;
  gap: 4px;
  flex-grow: 1;
}

.menu-item {
  display: flex;
  align-items: center;
  gap: 12px;
  margin: 2px 8px;
  padding: 11px 16px;
  border-radius: 10px;
  color: #1E5652;
  font-weight: 600;
  font-size: 14px;
  transition: all 0.2s ease;
  cursor: pointer;
}

.menu-item:hover {
  background: rgba(30, 86, 82, 0.08);
  color: #114B44;
}

.menu-item.active {
  background: #1E5652 !important;
  color: #ffffff !important;
  box-shadow: 0 2px 10px rgba(30, 86, 82, 0.2);
  font-weight: 700;
}

.menu-submenu {
  margin: 2px 0;
}

.menu-item.submenu-toggle {
  justify-content: flex-start;
  position: relative;
}

.submenu-label {
  flex: 1;
}

.submenu-chevron {
  margin-left: auto;
  font-size: 11px;
  transition: transform 0.25s ease;
  opacity: 0.6;
  color: #1E5652;
}

.submenu-chevron.open {
  transform: rotate(180deg);
  opacity: 1;
}

.submenu-items {
  display: flex;
  flex-direction: column;
  gap: 2px;
  margin: 4px 12px 4px 44px;
  padding: 6px 0 6px 12px;
  border-left: 2px solid rgba(30, 86, 82, 0.2);
}

.subitem-dot {
  width: 6px;
  height: 6px;
  border-radius: 50%;
  background: #9ca3af;
  flex-shrink: 0;
  transition: background 0.2s;
}

.menu-subitem {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 8px 12px;
  border-radius: 8px;
  color: #5C6765;
  font-weight: 600;
  font-size: 13px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.menu-subitem:hover {
  background: rgba(30, 86, 82, 0.08);
  color: #114B44;
}

.menu-subitem:hover .subitem-dot {
  background: #1E5652;
}

.menu-subitem.active {
  background: rgba(30, 86, 82, 0.1);
  color: #1E5652;
  font-weight: 700;
}

.menu-subitem.active .subitem-dot {
  background: #1E5652;
  box-shadow: 0 0 0 2px rgba(30, 86, 82, 0.2);
}

/* Submenu transition */
.submenu-slide-enter-active,
.submenu-slide-leave-active {
  transition: all 0.25s ease;
  overflow: hidden;
}
.submenu-slide-enter-from,
.submenu-slide-leave-to {
  opacity: 0;
  max-height: 0;
  transform: translateY(-6px);
}
.submenu-slide-enter-to,
.submenu-slide-leave-from {
  opacity: 1;
  max-height: 120px;
}

.activity-log-panel {
  margin-top: 24px;
  padding: 24px;
}

.activity-log-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.activity-log-header h4 {
  margin: 0;
  font-size: 16px;
  font-weight: 700;
  color: #114B44;
  display: flex;
  align-items: center;
  gap: 8px;
}

.activity-log-list {
  display: flex;
  flex-direction: column;
  gap: 10px;
  max-height: 360px;
  overflow-y: auto;
}

.activity-log-item {
  display: flex;
  align-items: flex-start;
  gap: 12px;
  padding: 12px 14px;
  border-radius: 10px;
  background: #FAF9F6;
  border: 1px solid rgba(17, 75, 68, 0.08);
}

.activity-log-icon {
  width: 34px;
  height: 34px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  flex-shrink: 0;
  font-size: 14px;
}

.activity-log-body {
  flex: 1;
  min-width: 0;
}

.activity-log-title {
  font-weight: 700;
  color: #114B44;
  font-size: 14px;
}

.activity-log-desc {
  color: #6C7A78;
  font-size: 13px;
  margin-top: 2px;
}

.activity-log-time {
  color: #9ca3af;
  font-size: 12px;
  white-space: nowrap;
  flex-shrink: 0;
}

.activity-log-empty {
  text-align: center;
  color: #6C7A78;
  padding: 32px 16px;
  font-size: 14px;
}

.sidebar-footer {
  border-top: 1px solid rgba(17, 75, 68, 0.1);
  padding-top: 25px;
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.role-badge-container {
  display: flex;
  justify-content: center;
}

.role-tag {
  font-size: 11px;
  padding: 4px 12px;
  border-radius: 20px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.role-tag.admin {
  background-color: rgba(207, 124, 7, 0.15);
  color: #CF7C07;
  border: 1px solid rgba(207, 124, 7, 0.3);
}

.role-tag.librarian {
  background-color: rgba(30, 86, 82, 0.1);
  color: #1E5652;
  border: 1px solid rgba(30, 86, 82, 0.2);
}

.btn-logout {
  background: transparent;
  border: 1.5px solid rgba(17, 75, 68, 0.15);
  color: #5C6765;
  padding: 12px;
  border-radius: 16px;
  font-weight: 700;
  font-size: 13px;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
  transition: all 0.3s ease;
}

.btn-logout:hover {
  background: rgba(255, 91, 91, 0.15);
  color: #ff5b5b;
  border-color: rgba(255, 91, 91, 0.3);
}

/* MAIN CONTENT AREA (Harmonized background aligned with system Ivory #FAF9F6) */
.dashboard-main {
  flex-grow: 1;
  padding: 28px 32px;
  overflow-y: auto;
  max-height: 100vh;
  display: flex;
  flex-direction: column;
  gap: 24px;
  background-color: #FAF9F6;
}

/* TOP BAR (Layout from Nirmal design reference) */
.dashboard-topbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  padding: 16px 30px;
  border-radius: 20px;
  box-shadow: 0 4px 20px rgba(0,0,0,0.015);
  border: 1.5px solid rgba(30, 86, 82, 0.05);
}

.welcome-text h2 {
  font-weight: 700;
  font-size: 22px;
  color: #114B44;
  margin: 0;
}

.topbar-actions {
  display: flex;
  align-items: center;
  gap: 15px;
}

.search-box-pill {
  display: flex;
  align-items: center;
  background: #F4F3ED; /* Matches off-white cream background */
  border-radius: 8px;
  padding: 8px 16px;
  gap: 8px;
  width: 260px;
  border: 1px solid rgba(0, 0, 0, 0.06);
}

.search-box-pill input {
  border: none;
  background: transparent;
  outline: none;
  font-size: 13px;
  color: #4b5563;
  width: 100%;
}

.action-icon-btn {
  width: 38px;
  height: 38px;
  border-radius: 8px;
  background: #F4F3ED;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: all 0.2s ease;
  font-size: 16px;
  color: #1E5652;
  border: 1px solid rgba(0, 0, 0, 0.06);
}

.action-icon-btn:hover {
  background: #e5e7eb;
}

/* CONTENT HEADER */
.content-header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.content-header-row h3 {
  font-size: 20px;
  font-weight: 700;
  color: #114B44;
  margin: 0;
}

.mgmt-hero-header {
  background: linear-gradient(135deg, #1E5652 0%, #114B44 100%);
  padding: 24px;
  border-radius: 16px;
  color: #ffffff;
  margin-bottom: 24px;
  box-shadow: 0 10px 30px rgba(17, 75, 68, 0.2);
}

.mgmt-hero-header .btn-hero-action {
  background: #ffffff;
  color: #1E5652;
  border-color: #ffffff;
  font-weight: 700;
  height: auto;
  padding: 8px 16px;
  border-radius: 20px;
}

.year-badge {
  font-size: 13px;
  font-weight: 700;
  color: #CF7C07;
  background: rgba(207, 124, 7, 0.1);
  padding: 4px 12px;
  border-radius: 12px;
}

/* OVER VIEW CARDS (Cohesive brand colors) */
.overview-cards-row {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  margin-bottom: 10px;
}

.ov-card {
  background: #ffffff;
  border-radius: 8px;
  padding: 24px;
  display: flex;
  align-items: center;
  gap: 20px;
  box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.03), 0 1px 6px -1px rgba(0, 0, 0, 0.02), 0 2px 4px 0 rgba(0, 0, 0, 0.02);
  position: relative;
  transition: all 0.3s ease;
  border: 1px solid rgba(30, 86, 82, 0.08);
}

.ov-card.clickable {
  cursor: pointer;
}

.ov-card:hover {
  transform: translateY(-2px);
  box-shadow: 0 1px 2px -2px rgba(0,0,0,0.16), 0 3px 6px 0 rgba(0,0,0,0.12), 0 5px 12px 4px rgba(0,0,0,0.09);
}

.ov-card.brand-border { border-left: 4px solid #1E5652; }

/* Highlighted card exactly matching the reference outline look but in SmartLib Warm Gold */
.ov-card.gold-highlight {
  border: 1px solid rgba(207, 124, 7, 0.3);
  background: #fffdf9;
}

.ov-icon-wrapper {
  width: 54px;
  height: 54px;
  border-radius: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 26px;
}

.teal-bg { background: rgba(30, 86, 82, 0.08); color: #1E5652; }
.gold-bg { background: rgba(207, 124, 7, 0.1); color: #CF7C07; }

.ov-value {
  font-size: 26px;
  font-weight: 700;
  color: #114B44;
  line-height: 1.2;
}

.ov-label {
  font-size: 13px;
  color: #6C7A78;
  font-weight: 600;
}

/* Dashboard overview layout */
.dashboard-hero {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
  padding: 18px 24px;
  background: linear-gradient(135deg, #1E5652 0%, #114B44 100%);
  border-radius: 14px;
  color: white;
  box-shadow: 0 6px 24px rgba(17, 75, 68, 0.18);
}

.dashboard-hero h3 {
  margin: 0 0 6px;
  font-size: 24px;
  font-weight: 800;
  color: white;
}

.dashboard-hero-sub {
  margin: 0;
  font-size: 14px;
  color: rgba(255, 255, 255, 0.8);
}

.dashboard-hero .year-badge {
  background: rgba(255, 255, 255, 0.15);
  border: 1px solid rgba(255, 255, 255, 0.25);
  color: white;
}

.dashboard-main-grid {
  display: grid;
  grid-template-columns: 1.5fr 1fr;
  grid-template-rows: auto auto;
  gap: 20px;
}

.chart-card {
  padding: 20px 24px;
  min-height: 0;
}

.chart-card--wide {
  grid-column: 1 / 2;
  grid-row: 1 / 2;
}

.chart-card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}

.chart-card-header h4 {
  margin: 0;
  font-size: 15px;
  font-weight: 700;
  color: #114B44;
  display: flex;
  align-items: center;
  gap: 8px;
}

.chart-card-hint {
  font-size: 12px;
  color: #6C7A78;
  font-weight: 600;
}

.chart-link {
  font-size: 12px;
  color: #1E5652;
  font-weight: 600;
  cursor: pointer;
}

.chart-link:hover {
  text-decoration: underline;
}

.donut-wrap {
  display: flex;
  justify-content: center;
  align-items: center;
}

.book-category-layout {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.book-category-chart-wrap {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 4px 0 0;
}

.book-category-legend {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 8px 14px;
  padding: 2px 4px 0;
  max-height: 168px;
  overflow-y: auto;
}

.book-category-legend-item {
  display: flex;
  align-items: center;
  gap: 8px;
  min-width: 0;
  font-size: 12px;
}

.book-category-legend-dot {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  flex-shrink: 0;
}

.book-category-legend-name {
  flex: 1;
  min-width: 0;
  color: #374151;
  font-weight: 500;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.book-category-legend-count {
  color: #114B44;
  font-weight: 700;
  flex-shrink: 0;
}

.book-category-empty {
  text-align: center;
  color: #6C7A78;
  font-size: 13px;
  font-weight: 500;
  padding: 24px 0 8px;
}

.system-log-pagination {
  display: flex;
  justify-content: flex-end;
  padding: 16px 20px 8px;
}

.card-stat-mini {
  display: flex;
  flex-wrap: wrap;
  gap: 12px;
  justify-content: center;
  margin-top: 8px;
  font-size: 12px;
  font-weight: 600;
  color: #5C6765;
}

.card-stat-mini .dot {
  display: inline-block;
  width: 8px;
  height: 8px;
  border-radius: 50%;
  margin-right: 4px;
}

.dot-active { background: #1E5652; }
.dot-locked { background: #CF7C07; }
.dot-expired { background: #9ca3af; }

.table-action-row {
  display: flex;
  gap: 8px;
  justify-content: center;
  flex-wrap: nowrap;
}

.btn-edit-inline {
  background: rgba(207, 124, 7, 0.1);
  color: #CF7C07;
  border: 1px solid rgba(207, 124, 7, 0.2);
}

.system-log-filters {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 16px 20px;
  margin-bottom: 16px;
}

.system-log-table {
  padding: 0;
  overflow: hidden;
}

.system-log-row {
  display: flex;
  align-items: flex-start;
  gap: 14px;
  padding: 16px 20px;
  border-bottom: 1px solid rgba(17, 75, 68, 0.06);
  transition: background 0.15s ease;
}

.system-log-row:hover {
  background: #FAF9F6;
}

.system-log-row:last-child {
  border-bottom: none;
}

.system-log-icon {
  width: 36px;
  height: 36px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: white;
  flex-shrink: 0;
  font-size: 15px;
  background: #1E5652;
}

.system-log-icon.login { background: #2563eb; }
.system-log-icon.logout { background: #6b7280; }
.system-log-icon.borrow,
.system-log-icon.renewal { background: #CF7C07; }
.system-log-icon.return { background: #059669; }
.system-log-icon.overdue,
.system-log-icon.error { background: #dc2626; }
.system-log-icon.lock { background: #b45309; }
.system-log-icon.unlock { background: #1E5652; }
.system-log-icon.book { background: #7c3aed; }
.system-log-icon.card-expire { background: #d97706; }
.system-log-icon.pending { background: #0891b2; }

.system-log-body {
  flex: 1;
  min-width: 0;
}

.system-log-title {
  font-weight: 700;
  color: #114B44;
  font-size: 14px;
}

.system-log-desc {
  color: #6C7A78;
  font-size: 13px;
  margin-top: 2px;
}

.system-log-meta {
  font-size: 11px;
  color: #9ca3af;
  margin-top: 4px;
  font-family: monospace;
}

.system-log-time {
  font-size: 12px;
  color: #9ca3af;
  white-space: nowrap;
  flex-shrink: 0;
}

.info-tooltip {
  position: absolute;
  top: 10px;
  right: 12px;
  width: 16px;
  height: 16px;
  border-radius: 50%;
  background: rgba(0,0,0,0.03);
  color: #718096;
  font-size: 10px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  font-weight: bold;
}

/* GRAPHICS GRID */
.graphics-grid {
  display: grid;
  grid-template-columns: 1.3fr 1fr;
  gap: 25px;
}

.graphics-left-col {
  display: flex;
  flex-direction: column;
  gap: 25px;
}

.mid-panels-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 25px;
}

.white-panel {
  background: #ffffff;
  border-radius: 8px;
  padding: 24px;
  box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.03), 0 1px 6px -1px rgba(0, 0, 0, 0.02), 0 2px 4px 0 rgba(0, 0, 0, 0.02);
  border: 1px solid rgba(30, 86, 82, 0.08);
}

.white-panel h4 {
  font-size: 16px;
  font-weight: 700;
  color: #114B44;
  margin: 0 0 20px 0;
}

/* User Count Panel */
.user-stats-visual {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px 0;
}

.user-icon-circle {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  background: rgba(30, 86, 82, 0.08);
  color: #1E5652;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 22px;
  margin-bottom: 12px;
}

.large-number {
  font-size: 32px;
  font-weight: 700;
  color: #114B44;
}

.sub-label {
  font-size: 12px;
  color: #6C7A78;
  font-weight: 600;
}

/* Donut Panel */
.pie-chart-container {
  display: flex;
  align-items: center;
  gap: 20px;
  justify-content: center;
}

.svg-pie {
  position: relative;
  width: 90px;
  height: 90px;
}

.circular-chart {
  display: block;
  width: 100%;
  height: 100%;
}

.circle-bg {
  fill: none;
  stroke: #F4F3ED;
  stroke-width: 3.8;
}

.circle {
  fill: none;
  stroke: #1E5652;
  stroke-width: 3.8;
  stroke-linecap: round;
  transition: stroke-dasharray 0.3s ease;
}

.pie-percentage {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-weight: 700;
  font-size: 16px;
  color: #114B44;
}

.pie-legend {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.legend-item {
  font-size: 12px;
  font-weight: 700;
  color: #5c6765;
  display: flex;
  align-items: center;
  gap: 6px;
}

.legend-item .dot {
  width: 8px;
  height: 8px;
  border-radius: 50%;
}

.legend-item .dot.active { background: #1E5652; }
.legend-item .dot.locked { background: #F4F3ED; border: 1px solid rgba(0,0,0,0.05); }

/* Area Chart Panel */
.area-chart-panel {
  position: relative;
}

.chart-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.chart-header h4 { margin: 0; }

.svg-area-chart-container {
  position: relative;
  width: 100%;
}

.area-chart-svg {
  width: 100%;
  height: auto;
}

/* Indicators styled professionally */
.indicator-tag {
  position: absolute;
  padding: 4px 8px;
  border-radius: 6px;
  font-size: 10px;
  font-weight: 700;
  color: white;
  box-shadow: 0 5px 10px rgba(0,0,0,0.1);
}

.indicator-tag.peak {
  background: #CF7C07;
}

.indicator-tag.active-tag {
  background: #1E5652;
}

.chart-months {
  display: flex;
  justify-content: space-between;
  margin-top: 10px;
  padding: 0 10px;
}

.chart-months span {
  font-size: 11px;
  color: #6C7A78;
  font-weight: 700;
}

/* Top Borrowed Books — ranked list */
.top-books-panel {
  padding: 24px;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.top-books-header h4 {
  margin: 0 0 4px;
  font-size: 16px;
  font-weight: 700;
  color: #114B44;
  display: flex;
  align-items: center;
  gap: 8px;
}

.top-books-sub {
  margin: 0;
  font-size: 12px;
  color: #6C7A78;
}

.top-books-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
  margin-top: 20px;
  flex: 1;
}

.top-book-item {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 14px 16px;
  border-radius: 14px;
  background: #FAF9F6;
  border: 1px solid rgba(17, 75, 68, 0.08);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.top-book-item:hover {
  transform: translateY(-1px);
  box-shadow: 0 6px 20px rgba(17, 75, 68, 0.08);
}

.top-book-rank {
  width: 36px;
  height: 36px;
  border-radius: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: 800;
  font-size: 15px;
  flex-shrink: 0;
  background: rgba(30, 86, 82, 0.12);
  color: #1E5652;
}

.top-book-item.rank-1 .top-book-rank {
  background: linear-gradient(135deg, #CF7C07, #f0a030);
  color: white;
  box-shadow: 0 4px 12px rgba(207, 124, 7, 0.35);
}

.top-book-item.rank-2 .top-book-rank {
  background: linear-gradient(135deg, #6b7280, #9ca3af);
  color: white;
}

.top-book-item.rank-3 .top-book-rank {
  background: linear-gradient(135deg, #b45309, #d97706);
  color: white;
}

.top-book-content {
  flex: 1;
  min-width: 0;
}

.top-book-row {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  gap: 12px;
  margin-bottom: 8px;
}

.top-book-name {
  font-size: 14px;
  font-weight: 700;
  color: #114B44;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.top-book-count {
  font-size: 12px;
  font-weight: 700;
  color: #1E5652;
  background: rgba(30, 86, 82, 0.1);
  padding: 3px 10px;
  border-radius: 20px;
  white-space: nowrap;
  flex-shrink: 0;
}

.top-book-track {
  width: 100%;
  height: 6px;
  background: rgba(17, 75, 68, 0.08);
  border-radius: 6px;
  overflow: hidden;
}

.top-book-bar {
  height: 100%;
  border-radius: 6px;
  background: linear-gradient(90deg, #1E5652, #2a9d8f);
  transition: width 0.6s ease;
}

.top-book-item.rank-1 .top-book-bar {
  background: linear-gradient(90deg, #CF7C07, #f0b429);
}

.top-books-empty {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 8px;
  padding: 40px 16px;
  color: #6C7A78;
  font-size: 14px;
}

.top-books-empty .anticon {
  font-size: 28px;
  opacity: 0.4;
}

.bar-teal-primary { background: #1E5652; }
.bar-gold { background: #CF7C07; }
.bar-teal-secondary { background: rgba(30, 86, 82, 0.7); }
.bar-teal-muted { background: rgba(30, 86, 82, 0.4); }
.bar-gold-muted { background: rgba(207, 124, 7, 0.4); }

/* Brand Aligned Table */
.pastel-table {
  width: 100%;
  border-collapse: collapse;
}

.pastel-table th {
  background: rgba(30, 86, 82, 0.04);
  text-align: left;
  padding: 16px;
  font-weight: 700;
  color: #1E5652;
  border-bottom: 1.5px solid rgba(30, 86, 82, 0.1);
}

.pastel-table td {
  padding: 16px;
  border-bottom: 1px solid rgba(0,0,0,0.03);
  font-size: 14px;
}

.pastel-table tr:hover {
  background: rgba(30, 86, 82, 0.02);
}

.btn-action-pastel {
  background: linear-gradient(135deg, #1E5652 0%, #CF7C07 100%) !important;
  border: none !important;
  color: #FAF9F6 !important;
  font-weight: 700 !important;
  border-radius: 20px !important;
  height: auto !important;
  padding: 8px 24px !important;
  box-shadow: 0 6px 15px rgba(30, 86, 82, 0.15) !important;
}

.btn-action-pastel:hover {
  transform: translateY(-2px);
  box-shadow: 0 10px 20px rgba(30, 86, 82, 0.2) !important;
}

.genre-badge {
  background: rgba(30, 86, 82, 0.08);
  color: #1E5652;
  padding: 4px 10px;
  border-radius: 8px;
  font-size: 11px;
  font-weight: 700;
}

.status-badge {
  font-size: 11px;
  font-weight: 700;
  padding: 4px 10px;
  border-radius: 8px;
}

.status-badge.instock, .status-badge.đã-phê-duyệt {
  background: rgba(46, 204, 113, 0.15);
  color: #27ae60;
}

.status-badge.outstock, .status-badge.chờ-duyệt {
  background: rgba(207, 124, 7, 0.15);
  color: #CF7C07;
}

.status-badge.từ-chối {
  background: rgba(231, 76, 60, 0.15);
  color: #c0392b;
}

.card-number-badge {
  background: rgba(30, 86, 82, 0.05);
  border: 1px solid rgba(30, 86, 82, 0.1);
  font-family: monospace;
  padding: 2px 8px;
  border-radius: 6px;
  font-size: 12px;
  color: #1E5652;
  font-weight: bold;
}

/* Custom Buttons for inline actions */
.btn-edit-inline, .btn-delete-inline, .btn-approve, .btn-reject {
  border: none;
  background: rgba(0,0,0,0.03);
  padding: 6px 12px;
  border-radius: 8px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn-edit-inline:hover { background: rgba(30, 86, 82, 0.1); }
.btn-delete-inline { background: rgba(231, 76, 60, 0.1); color: #c0392b; }
.btn-delete-inline:hover { background: rgba(231, 76, 60, 0.18); }

.staff-bulk-actions {
  display: flex;
  gap: 10px;
  align-items: center;
  margin-left: auto;
  flex-wrap: wrap;
}

.notification-dot {
  position: absolute;
  top: 4px;
  right: 4px;
  min-width: 16px;
  height: 16px;
  padding: 0 4px;
  border-radius: 999px;
  background: #ef4444;
  color: white;
  font-size: 10px;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
}

.action-icon-btn.notifications {
  position: relative;
}

.notification-panel {
  width: 360px;
  max-height: 420px;
}

.notification-panel-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 8px;
  font-weight: 700;
  color: #114B44;
  margin-bottom: 12px;
  padding-bottom: 8px;
  border-bottom: 1px solid #eef2f2;
}

.notification-panel-sub {
  margin: 4px 0 0;
  font-size: 11px;
  font-weight: 500;
  color: #6b7280;
  line-height: 1.35;
}

.notification-list {
  max-height: 360px;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.notification-card {
  width: 100%;
  text-align: left;
  cursor: pointer;
  border-radius: 10px;
  background: #ffffff;
  border: 1px solid rgba(17, 75, 68, 0.1);
  border-left: 4px solid #1E5652;
  box-shadow: 0 1px 3px rgba(17, 75, 68, 0.06);
  transition: box-shadow 0.15s ease, transform 0.15s ease;
  font: inherit;
}

.notification-card:hover {
  box-shadow: 0 2px 8px rgba(17, 75, 68, 0.12);
  transform: translateY(-1px);
}

.notification-card--borrow,
.notification-card--renewal { border-left-color: #CF7C07; }
.notification-card--return { border-left-color: #059669; }
.notification-card--overdue,
.notification-card--error { border-left-color: #dc2626; }
.notification-card--login { border-left-color: #2563eb; }
.notification-card--logout { border-left-color: #6b7280; }
.notification-card--book { border-left-color: #7c3aed; }
.notification-card--card-expire { border-left-color: #d97706; }
.notification-card--pending { border-left-color: #0891b2; }

.notification-card-content {
  padding: 10px 12px;
}

.notification-card-top {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 10px;
}

.notification-card-title {
  font-weight: 700;
  color: #114B44;
  font-size: 13px;
  line-height: 1.3;
}

.notification-card-time {
  color: #6b7280;
  font-size: 11px;
  white-space: nowrap;
  flex-shrink: 0;
  font-variant-numeric: tabular-nums;
}

.notification-card-desc {
  color: #4b5563;
  font-size: 12px;
  margin: 4px 0 0;
  line-height: 1.45;
}

.notification-card-action {
  display: inline-block;
  margin-top: 6px;
  font-size: 11px;
  font-weight: 700;
  color: #1E5652;
}

.dashboard-topbar--compact {
  padding: 14px 28px;
  min-height: 64px;
}

.topbar-page-spacer {
  flex: 1;
}

.notification-empty {
  text-align: center;
  color: #9ca3af;
  padding: 24px 12px;
}
.btn-delete-inline:hover { background: rgba(231, 76, 60, 0.1); color: #c0392b; }
.btn-approve { background: rgba(46, 204, 113, 0.15); color: #27ae60; }
.btn-reject { background: rgba(231, 76, 60, 0.1); color: #c0392b; }

.btn-approve:hover { transform: scale(1.05); }
.btn-reject:hover { transform: scale(1.05); }

/* Rules page inputs */
.num-input {
  width: 100% !important;
  background: #ffffff !important;
  border: 1.5px solid rgba(30, 86, 82, 0.15) !important;
  color: #114B44 !important;
  border-radius: 12px !important;
  height: 40px !important;
  display: flex;
  align-items: center;
}

.form-group-custom {
  display: flex;
  flex-direction: column;
  gap: 8px;
  margin-bottom: 20px;
}

.form-group-custom label {
  font-size: 13px;
  font-weight: 700;
  color: #114B44;
}

/* Search Results UI */
.search-input-pill {
  border: 1.5px solid rgba(30, 86, 82, 0.15) !important;
  border-radius: 20px !important;
  height: 45px !important;
}

.btn-search-pill {
  background: linear-gradient(135deg, #1E5652 0%, #CF7C07 100%) !important;
  border: none !important;
  border-radius: 20px !important;
  height: 45px !important;
  font-weight: 700 !important;
}

.search-results-box {
  background: #ffffff;
  border: 1px solid rgba(30, 86, 82, 0.1);
  border-radius: 16px;
  padding: 25px;
  margin-top: 25px;
}

.result-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1.5px solid rgba(30, 86, 82, 0.08);
  padding-bottom: 15px;
  margin-bottom: 20px;
}

.user-info-large h3 { font-size: 18px; font-weight: 700; color: #114B44; }
.user-info-large .card-no { font-family: monospace; font-size: 12px; color: #6C7A78; }

.details-grid-custom {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 15px;
  margin-bottom: 25px;
}

.detail-item-box {
  background: #F4F3ED;
  border: 1px solid rgba(0,0,0,0.02);
  padding: 15px;
  border-radius: 12px;
  display: flex;
  flex-direction: column;
}

.detail-item-box .lbl { font-size: 11px; color: #6C7A78; }
.detail-item-box .val { font-size: 18px; font-weight: 700; color: #114B44; }

.active-loans-list h4 {
  font-size: 13px;
  font-weight: 700;
  color: #114B44;
  margin-bottom: 15px;
}

.loan-item-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  padding: 12px 18px;
  border-radius: 10px;
  margin-bottom: 8px;
  border: 1.5px solid rgba(30, 86, 82, 0.05);
}

.loan-item-row .title { font-weight: 700; font-size: 13px; color: #114B44; }

/* Return Sách Steps Flow */
.steps-flow {
  display: flex;
  justify-content: space-around;
  margin-bottom: 40px;
}

.step-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;
}

.step-num {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  background: #F4F3ED;
  display: flex;
  justify-content: center;
  align-items: center;
  font-weight: 700;
  color: #6C7A78;
}

.step-txt {
  font-size: 12px;
  font-weight: 700;
  color: #6C7A78;
}

.step-item.completed .step-num {
  background: #1E5652;
  color: white;
}

.step-item.completed .step-txt {
  color: #1E5652;
}

.step-content-box {
  background: #ffffff;
  border: 1.5px solid rgba(30, 86, 82, 0.05);
  padding: 30px;
  border-radius: 16px;
}

.pill-input-custom {
  border: 1.5px solid rgba(30, 86, 82, 0.15) !important;
  border-radius: 12px !important;
  height: 42px !important;
}

.btn-next-step {
  background: linear-gradient(135deg, #1E5652 0%, #CF7C07 100%) !important;
  border: none !important;
  border-radius: 12px !important;
  font-weight: 700 !important;
  height: 42px !important;
}

.checkbox-group-custom {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.checkbox-group-custom .ant-checkbox-wrapper {
  color: #114B44 !important;
  font-weight: 600;
}

.success-icon-large {
  font-size: 64px;
}

.btn-restart {
  background: #1E5652 !important;
  border: none !important;
  border-radius: 12px !important;
  font-weight: 700 !important;
}

/* Animations */
.animate-fade-in {
  animation: fadeIn 0.4s ease-out;
}

@keyframes fadeIn {
  from { opacity: 0; transform: translateY(10px); }
  to { opacity: 1; transform: translateY(0); }
}

/* Responsive adjustment */
@media (max-width: 1200px) {
  .overview-cards-row { grid-template-columns: repeat(2, 1fr); }
  .graphics-grid { grid-template-columns: 1fr; }
  .dashboard-main-grid { grid-template-columns: 1fr; }
  .chart-card--wide { grid-column: auto; }
}

@media (max-width: 768px) {
  .dashboard-container { flex-direction: column; }
  .dashboard-sidebar { width: 100%; border-right: none; }
}

/* Detail Modal Styles */
.modal-table {
  width: 100%;
}

.modal-loading,
.modal-empty {
  text-align: center;
  padding: 40px 20px;
  font-size: 15px;
  color: #7a8583;
  font-weight: 600;
}

.modal-pending {
  text-align: center;
  padding: 40px 20px;
}

.modal-pending .pending-icon {
  font-size: 48px;
  margin-bottom: 16px;
}

.modal-pending h3 {
  font-size: 18px;
  font-weight: 700;
  color: #114B44;
  margin-bottom: 8px;
}

.modal-pending p {
  font-size: 13px;
  color: #7a8583;
  line-height: 1.6;
}

/* ==========================================================================
   NEW TABS STYLING (Readers & Realistic Library Cards)
   ========================================================================== */

/* Subtitle Title */
.title-with-subtitle h3 {
  font-size: 22px;
  font-weight: 700;
  color: #114B44;
  margin: 0 0 4px 0;
}
.title-with-subtitle .subtitle-text {
  font-size: 13px;
  color: #6C7A78;
  margin: 0;
}

/* Hero header overrides — must sit after .title-with-subtitle rules */
.content-header-row.mgmt-hero-header .title-with-subtitle h3 {
  color: #ffffff;
  font-size: 24px;
  font-weight: 800;
  margin: 0 0 8px 0;
}

.content-header-row.mgmt-hero-header .title-with-subtitle h3 .anticon {
  color: #ffffff;
}

.content-header-row.mgmt-hero-header .title-with-subtitle .subtitle-text {
  color: rgba(255, 255, 255, 0.88);
  font-size: 14px;
}

.content-header-row.mgmt-hero-header .stats-pill {
  background: rgba(255, 255, 255, 0.15);
  color: #ffffff;
  border: 1px solid rgba(255, 255, 255, 0.28);
  font-weight: 600;
  padding: 8px 16px;
  border-radius: 20px;
  white-space: nowrap;
}

.stats-pill {
  background: rgba(30, 86, 82, 0.08);
  color: #1E5652;
  font-size: 13px;
  font-weight: 700;
  padding: 6px 16px;
  border-radius: 20px;
  border: 1px solid rgba(30, 86, 82, 0.15);
}

/* Search Filter Panel */
.search-filter-panel {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 20px;
  background: #ffffff;
  padding: 20px 30px;
  border-radius: 20px;
  box-shadow: 0 4px 20px rgba(0,0,0,0.015);
  margin-bottom: 25px;
}

.search-filter-panel .search-input-pill {
  flex-grow: 1;
  max-width: 480px;
  display: flex;
  align-items: center;
  background: #F4F3ED;
  border-radius: 20px;
  padding: 6px 16px;
  gap: 10px;
  border: 1px solid rgba(30, 86, 82, 0.1);
}

.search-filter-panel .search-input-pill input {
  border: none;
  background: transparent;
  outline: none;
  font-size: 14px;
  color: #2d3748;
  width: 100%;
}

.filter-group {
  display: flex;
  align-items: center;
  gap: 10px;
}

.filter-label {
  font-size: 13px;
  font-weight: 700;
  color: #114B44;
}

.custom-select-pill {
  background: #F4F3ED;
  border: 1px solid rgba(30, 86, 82, 0.1);
  padding: 8px 16px;
  border-radius: 20px;
  outline: none;
  font-size: 13px;
  font-weight: 600;
  color: #114B44;
  cursor: pointer;
  transition: all 0.2s ease;
}

.custom-select-pill:hover {
  border-color: #1E5652;
}

/* Loading and Spinner */
.page-loading-spinner-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 60px 20px;
  gap: 15px;
}

.spinner-spinner {
  width: 40px;
  height: 40px;
  border: 4px solid rgba(30, 86, 82, 0.1);
  border-left-color: #1E5652;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.empty-state-card {
  background: white;
  border: 1.5px dashed rgba(30, 86, 82, 0.15);
  border-radius: 24px;
  padding: 50px 30px;
  text-align: center;
  max-width: 500px;
  margin: 40px auto;
}

.empty-icon {
  font-size: 54px;
  margin-bottom: 15px;
}

.empty-state-card h4 {
  font-size: 18px;
  font-weight: 700;
  color: #114B44;
  margin: 0 0 8px 0;
}

.empty-state-card p {
  font-size: 14px;
  color: #6C7A78;
  margin: 0;
}

/* Readers Grid Layout */
.readers-grid-layout {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 25px;
  margin-top: 10px;
}

.reader-card-new {
  background: white;
  border-radius: 20px;
  border: 1.5px solid rgba(30, 86, 82, 0.05);
  box-shadow: 0 10px 25px rgba(0,0,0,0.015);
  overflow: hidden;
  position: relative;
  transition: all 0.3s cubic-bezier(0.25, 0.8, 0.25, 1);
  display: flex;
  flex-direction: column;
}

.reader-card-new:hover {
  transform: translateY(-5px);
  box-shadow: 0 15px 35px rgba(30, 86, 82, 0.08);
  border-color: rgba(30, 86, 82, 0.15);
}

.reader-card-top-decoration {
  height: 60px;
  background: linear-gradient(135deg, #114B44 0%, #1E5652 100%);
}

.reader-card-avatar-wrapper {
  position: absolute;
  top: 25px;
  left: 50%;
  transform: translateX(-50%);
}

.avatar-ring {
  width: 70px;
  height: 70px;
  border-radius: 50%;
  background: #ffffff;
  border: 3.5px solid #ffffff;
  box-shadow: 0 6px 15px rgba(0,0,0,0.1);
  display: flex;
  justify-content: center;
  align-items: center;
}

.avatar-ring .avatar-emoji {
  font-size: 34px;
}

.reader-card-body {
  padding: 50px 24px 20px 24px;
  text-align: center;
  flex-grow: 1;
}

.reader-card-name {
  font-size: 17px;
  font-weight: 700;
  color: #114B44;
  margin: 0 0 2px 0;
}

.reader-card-username {
  font-size: 13px;
  color: #CF7C07;
  font-weight: 600;
  display: inline-block;
  margin-bottom: 20px;
}

.reader-card-meta {
  display: flex;
  flex-direction: column;
  gap: 10px;
  text-align: left;
  border-top: 1px solid rgba(30, 86, 82, 0.06);
  padding-top: 15px;
}

.meta-item {
  display: flex;
  align-items: center;
  gap: 10px;
}

.meta-item .m-icon {
  font-size: 15px;
}

.meta-item .m-text {
  font-size: 13px;
  color: #4A5568;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  width: 100%;
}

.reader-card-footer {
  padding: 16px 24px;
  background: #FDFDFB;
  border-top: 1px solid rgba(30, 86, 82, 0.05);
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.btn-action-small {
  border: none;
  padding: 6px 14px;
  border-radius: 12px;
  font-size: 12px;
  font-weight: 700;
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn-action-small.btn-lock {
  background: rgba(231, 76, 60, 0.1);
  color: #e74c3c;
}

.btn-action-small.btn-lock:hover {
  background: #e74c3c;
  color: white;
}

.btn-action-small.btn-unlock {
  background: rgba(46, 204, 113, 0.1);
  color: #2ecc71;
}

.btn-action-small.btn-unlock:hover {
  background: #2ecc71;
  color: white;
}

/* View Detail Button */
.btn-view-detail {
  display: inline-flex;
  align-items: center;
  gap: 5px;
  padding: 5px 12px;
  border-radius: 8px;
  border: 1px solid #1a6b5e;
  background: rgba(26,107,94,0.08);
  color: #1a6b5e;
  font-size: 12px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  white-space: nowrap;
}
.btn-view-detail:hover {
  background: #1a6b5e;
  color: white;
}

/* Reader Detail Drawer */
.drawer-profile-wrapper { padding: 4px 0; }

.drawer-profile-header {
  display: flex;
  align-items: center;
  gap: 14px;
  padding: 20px;
  background: linear-gradient(135deg, #1a6b5e 0%, #2a9d8f 100%);
  border-radius: 14px;
  margin-bottom: 20px;
}
.drawer-avatar {
  width: 56px; height: 56px;
  border-radius: 50%;
  background: rgba(255,255,255,0.2);
  display: flex; align-items: center; justify-content: center;
  font-size: 26px;
  flex-shrink: 0;
}
.drawer-header-info h2 { margin: 0; font-size: 18px; color: #fff; font-weight: 700; }
.drawer-username { font-size: 13px; color: rgba(255,255,255,0.75); }

.drawer-section {
  background: #fff;
  border-radius: 12px;
  border: 1px solid #e8f4f2;
  padding: 16px 18px;
  margin-bottom: 14px;
}
.drawer-section-title {
  font-size: 13px;
  font-weight: 700;
  color: #1a6b5e;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  margin-bottom: 12px;
  padding-bottom: 8px;
  border-bottom: 1px solid #e8f4f2;
}
.drawer-info-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 7px 0;
  border-bottom: 1px dashed #f0f0f0;
}
.drawer-info-row:last-child { border-bottom: none; }
.drawer-info-label { font-size: 13px; color: #9ca3af; }
.drawer-info-value { font-size: 13px; font-weight: 600; color: #1f2937; }

.library-card-visual {
  display: flex;
  align-items: center;
  gap: 12px;
  background: linear-gradient(135deg, #f0fdf9, #e8f4f2);
  border-radius: 10px;
  padding: 12px 14px;
  margin-bottom: 12px;
  border: 1px solid #c6ece7;
}
.lc-chip { font-size: 24px; }
.lc-info { flex: 1; }
.lc-number { font-size: 16px; font-weight: 700; color: #1a6b5e; letter-spacing: 1px; }
.lc-label { font-size: 11px; color: #9ca3af; margin-top: 2px; }

.drawer-empty-card {
  text-align: center;
  padding: 20px;
  color: #9ca3af;
  font-size: 14px;
  background: #fafafa;
  border-radius: 10px;
  border: 1px dashed #e5e7eb;
}
.drawer-actions { margin-top: 8px; }

/* Cards Grid Layout */
.cards-grid-layout {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(330px, 1fr));
  gap: 30px;
  margin-top: 10px;
}

/* REALISTIC CREDIT CARD */
.library-credit-card {
  perspective: 1000px;
  position: relative;
  border-radius: 20px;
  overflow: hidden;
  box-shadow: 0 10px 30px rgba(0,0,0,0.06);
  transition: all 0.4s cubic-bezier(0.165, 0.84, 0.44, 1);
  aspect-ratio: 1.586;
  background-size: cover;
  border: 1px solid rgba(255,255,255,0.1);
}

.library-credit-card:hover {
  transform: translateY(-8px) scale(1.02);
  box-shadow: 0 20px 40px rgba(17, 75, 68, 0.15);
}

/* Realistic Credit Card Colors depending on status */
.library-credit-card.active {
  background: linear-gradient(135deg, #114B44 0%, #1E5652 45%, #2D8A80 100%);
}

.library-credit-card.locked {
  background: linear-gradient(135deg, #4A0E17 0%, #7B1113 45%, #A81C1C 100%);
}

.library-credit-card.expired {
  background: linear-gradient(135deg, #2D3748 0%, #4A5568 50%, #718096 100%);
}

.card-inner-glass {
  width: 100%;
  height: 100%;
  padding: 24px;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  background: rgba(255, 255, 255, 0.03);
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
}

.card-header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.card-brand-logo {
  font-size: 18px;
  font-weight: 800;
  color: #ffffff;
  letter-spacing: 0.5px;
  text-shadow: 0 2px 4px rgba(0,0,0,0.2);
}

.card-status-dot-badge {
  font-size: 11px;
  font-weight: 700;
  color: #ffffff;
  background: rgba(255, 255, 255, 0.15);
  padding: 4px 10px;
  border-radius: 12px;
  display: flex;
  align-items: center;
  gap: 6px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.pulse-dot {
  width: 7px;
  height: 7px;
  border-radius: 50%;
  background: #2ecc71;
  display: inline-block;
  box-shadow: 0 0 8px #2ecc71;
}

.library-credit-card.locked .pulse-dot {
  background: #e74c3c;
  box-shadow: 0 0 8px #e74c3c;
}

.library-credit-card.expired .pulse-dot {
  background: #f1c40f;
  box-shadow: 0 0 8px #f1c40f;
}

/* Metallic gold chip */
.card-chip-row {
  display: flex;
  align-items: center;
  gap: 15px;
  margin-top: 10px;
}

.gold-chip {
  width: 44px;
  height: 32px;
  background: linear-gradient(135deg, #f5d061 0%, #e6b31e 50%, #c4960d 100%);
  border-radius: 6px;
  box-shadow: inset 0 1px 1px rgba(255,255,255,0.3), 0 2px 4px rgba(0,0,0,0.1);
  position: relative;
}

.gold-chip::before {
  content: '';
  position: absolute;
  top: 4px;
  left: 6px;
  right: 6px;
  bottom: 4px;
  border: 1px solid rgba(0,0,0,0.12);
  border-radius: 4px;
}

.wifi-icon {
  color: rgba(255, 255, 255, 0.6);
  font-size: 16px;
}

/* Card NumberDisplay */
.card-number-display {
  font-family: monospace;
  font-size: 19px;
  color: #ffffff;
  letter-spacing: 2px;
  margin: 15px 0;
  text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
  font-weight: 600;
}

.card-details-row {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
}

.detail-col {
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.detail-lbl {
  font-size: 9px;
  color: rgba(255, 255, 255, 0.5);
  font-weight: 700;
  letter-spacing: 1px;
}

.detail-val {
  font-size: 13px;
  color: #ffffff;
  font-weight: 700;
  letter-spacing: 0.5px;
  text-shadow: 0 1px 2px rgba(0,0,0,0.2);
}

.text-right {
  text-align: right;
}

/* Card action panel displayed directly under the realistic credit card */
.card-action-underlay {
  background: white;
  padding: 16px 20px;
  border-top: 1px solid rgba(30, 86, 82, 0.05);
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-radius: 0 0 20px 20px;
  margin-top: -20px; /* Overlaps card container bottom nicely */
  z-index: 1;
  position: relative;
  box-shadow: 0 8px 20px rgba(0,0,0,0.02);
}

/* Adjust card container gap to show action underlay perfectly */
.cards-grid-layout > div {
  display: flex;
  flex-direction: column;
}

.issue-date-label {
  font-size: 11px;
  color: #6C7A78;
  font-weight: 600;
}

.btn-action-card-toggle {
  border: none;
  background: #F4F3ED;
  color: #114B44;
  padding: 6px 12px;
  border-radius: 12px;
  font-size: 11px;
  font-weight: 700;
  cursor: pointer;
  transition: all 0.2s ease;
  border: 1px solid rgba(30, 86, 82, 0.08);
}

.btn-action-card-toggle:hover {
  background: #114B44;
  color: white;
  border-color: #114B44;
}
.spotlight-container {
  position: relative;
}

.search-box-pill.spotlight-active {
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
  border-bottom: 1px solid transparent;
  box-shadow: 0 0 0 2px rgba(26, 107, 94, 0.2);
}

.spotlight-dropdown {
  position: absolute;
  top: 100%;
  left: 0;
  width: 320px;
  background: white;
  border-radius: 0 0 12px 12px;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
  border: 1px solid rgba(0,0,0,0.08);
  border-top: none;
  z-index: 1000;
  overflow: hidden;
  animation: slideDown 0.2s ease;
}

@keyframes slideDown {
  from { opacity: 0; transform: translateY(-10px); }
  to { opacity: 1; transform: translateY(0); }
}

.spotlight-empty {
  padding: 20px;
  text-align: center;
  color: #8c8c8c;
  font-size: 14px;
}

.spotlight-group-title {
  font-size: 11px;
  font-weight: 700;
  color: #9ca3af;
  padding: 12px 16px 4px;
  letter-spacing: 0.5px;
}

.spotlight-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 10px 16px;
  cursor: pointer;
  transition: all 0.2s;
  text-align: left;
}

.spotlight-item:hover {
  background: #f8fafc;
}

.spotlight-item .sl-icon {
  width: 32px;
  height: 32px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  flex-shrink: 0;
}

.spotlight-item .sl-icon.teal { background: #e6f7f5; color: #1a6b5e; }
.spotlight-item .sl-icon.gold { background: #fff8e6; color: #d4a373; }

.spotlight-item .sl-info {
  flex: 1;
  overflow: hidden;
}

.spotlight-item .sl-title {
  font-weight: 600;
  color: #1f2937;
  font-size: 14px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 1.4;
}

.spotlight-item .sl-desc {
  font-size: 12px;
  color: #6b7280;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 1.4;
}

.spotlight-container {
  position: relative;
}

.search-box-pill.spotlight-active {
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
  border-bottom: 1px solid transparent;
  box-shadow: 0 0 0 2px rgba(26, 107, 94, 0.2);
}

.spotlight-dropdown {
  position: absolute;
  top: 100%;
  left: 0;
  width: 320px;
  background: white;
  border-radius: 0 0 12px 12px;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
  border: 1px solid rgba(0,0,0,0.08);
  border-top: none;
  z-index: 1000;
  overflow: hidden;
  animation: slideDown 0.2s ease;
}

@keyframes slideDown {
  from { opacity: 0; transform: translateY(-10px); }
  to { opacity: 1; transform: translateY(0); }
}

.spotlight-empty {
  padding: 20px;
  text-align: center;
  color: #8c8c8c;
  font-size: 14px;
}

.spotlight-group-title {
  font-size: 11px;
  font-weight: 700;
  color: #9ca3af;
  padding: 12px 16px 4px;
  letter-spacing: 0.5px;
}

.spotlight-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 10px 16px;
  cursor: pointer;
  transition: all 0.2s;
  text-align: left;
}

.spotlight-item:hover {
  background: #f8fafc;
}

.spotlight-item .sl-icon {
  width: 32px;
  height: 32px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 16px;
  flex-shrink: 0;
}

.spotlight-item .sl-icon.teal { background: #e6f7f5; color: #1a6b5e; }
.spotlight-item .sl-icon.gold { background: #fff8e6; color: #d4a373; }

.spotlight-item .sl-info {
  flex: 1;
  overflow: hidden;
}

.spotlight-item .sl-title {
  font-weight: 600;
  color: #1f2937;
  font-size: 14px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 1.4;
}

.spotlight-item .sl-desc {
  font-size: 12px;
  color: #6b7280;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  line-height: 1.4;
}


.sidebar-logo {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 0 0 30px 0;
  margin-bottom: 20px;
  border-bottom: 1px solid rgba(255,255,255,0.15);
}
.logo-icon { font-size: 28px; }
.logo-text {
  font-size: 22px;
  font-weight: 800;
  color: #ffffff;
  letter-spacing: 1px;
}
.topbar-user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  background: rgba(17, 75, 68, 0.06);
  border: 1px solid rgba(17, 75, 68, 0.12);
  border-radius: 20px;
  padding: 6px 16px 6px 10px;
  cursor: pointer;
  transition: all 0.2s ease;
}
.topbar-user-info:hover {
  background: rgba(17, 75, 68, 0.1);
}
.topbar-avatar {
  width: 32px; height: 32px;
  border-radius: 50%;
  background: linear-gradient(135deg, #1E5652, #114B44);
  display: flex; align-items: center; justify-content: center;
  color: white; font-size: 16px;
}
.topbar-user-detail { display: flex; flex-direction: column; }
.topbar-username { font-size: 13px; font-weight: 700; color: #114B44; }
.topbar-role { font-size: 11px; color: #6C7A78; }

/* Dashboard Log Panel */
.dashboard-log-panel {
  margin-top: 4px;
}

.dashboard-log-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 16px;
  padding-bottom: 14px;
  border-bottom: 1px solid rgba(17, 75, 68, 0.07);
}

.dashboard-log-header h4 {
  margin: 0 0 4px;
  font-size: 15px;
  font-weight: 700;
  color: #114B44;
  display: flex;
  align-items: center;
  gap: 8px;
}

.dashboard-log-sub {
  margin: 0;
  font-size: 12px;
  color: #9ca3af;
}

.dashboard-log-actions {
  display: flex;
  gap: 4px;
  align-items: center;
  flex-shrink: 0;
}

.dashboard-log-table {
  max-height: 360px;
  overflow-y: auto;
}

/* Bulk action bar */
.bulk-action-bar {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 10px 16px;
  background: #fff8e6;
  border: 1px solid rgba(207, 124, 7, 0.25);
  border-radius: 10px;
  margin-bottom: 12px;
  animation: slideDown 0.2s ease;
}

.bulk-count {
  font-size: 13px;
  font-weight: 700;
  color: #92540a;
  flex: 1;
}
</style>
