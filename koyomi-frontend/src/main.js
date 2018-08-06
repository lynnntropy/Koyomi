import Vue from 'vue'
import App from './App.vue'
import store from './store'

import vueMoment from 'vue-moment'

Vue.config.productionTip = false

Vue.use(vueMoment)

new Vue({
  store,
  render: h => h(App)
}).$mount('#app')
