<template>
  <div id="app">
    <div class="container">
      <calendar :schedule-items="scheduleItems" />
      <dashboard :selected-items="selectedItems" />
    </div>
  </div>
</template>

<script>
import '@/assets/styles/main.css'
import Calendar from './components/Calendar.vue'
import Dashboard from './components/Dashboard.vue'
import axios from 'axios'

export default {
  name: 'app',
  components: {
    Calendar,
    Dashboard
  },

  data () {
    return {
      scheduleItems: []
    }
  },

  mounted: function () {
    axios.get('http://localhost:5000/api/schedule').then(({data}) => {
      data.forEach(item => item.selected = false)
      this.scheduleItems = data
    })
  },

  // methods: {
  //   getItemsForDay: function (dayString) {
  //     return this.scheduleItems.filter(item => item.dayOfWeek === dayString)
  //   }
  // },

  computed: {
    selectedItems: function () {
      return this.scheduleItems.filter(item => item.selected === true)
    }
  }
}
</script>

<style lang="scss">

  body {
    font-family: 'Roboto Condensed', sans-serif;
    background-color: #f9f9f9;
    color: #333;
  }

  #app {
    min-height: 100vh;
    display: grid;
    grid-template-rows: 1fr;
  }

  .header {
    text-align: center;
    padding: 1rem;
  }

  .container {

    @media (min-width: 1920px) {
      display: grid;
      grid-template-columns: 1fr 20rem;
    }

  }

</style>
