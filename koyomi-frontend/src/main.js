import Vue from 'vue'
import axios from 'axios'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCalendarAlt } from '@fortawesome/free-solid-svg-icons'
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons'
import { faSignInAlt } from '@fortawesome/free-solid-svg-icons'
import { faCalendarPlus } from '@fortawesome/free-solid-svg-icons'
import { faClock } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import VueAnalytics from 'vue-analytics'
import App from './App.vue'

axios.defaults.baseURL = process.env.VUE_APP_API_BASE_URL;

library.add(faCalendarAlt, faSignOutAlt, faSignInAlt, faCalendarPlus, faClock)
Vue.component('font-awesome-icon', FontAwesomeIcon)

import vueMoment from 'vue-moment'

Vue.config.productionTip = false

Vue.use(vueMoment)

Vue.use(VueAnalytics, {
  id: 'UA-25952845-9'
})

new Vue({
  render: h => h(App)
}).$mount('#app')
