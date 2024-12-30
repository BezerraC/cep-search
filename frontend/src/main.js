import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css';

// App Component
import App from './App.vue'

// Views
import HomeView from './views/HomeView.vue'

// Configuração do Vuetify
const vuetify = createVuetify({
  components,
  directives,
  theme: {
    defaultTheme: 'light',
  },
})

// Configuração do Vue Router
const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: HomeView }
  ]
})

// Criação do app Vue
createApp(App)
  .use(router)
  .use(vuetify)
  .mount('#app')
