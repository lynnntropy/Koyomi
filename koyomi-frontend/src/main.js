import Vue from 'vue'
import axios from 'axios'

import App from './App.vue'
import store from './store'

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

import vueMoment from 'vue-moment'

Vue.config.productionTip = false

Vue.use(vueMoment)

new Vue({
  store,
  render: h => h(App)
}).$mount('#app')
