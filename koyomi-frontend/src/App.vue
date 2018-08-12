<template>
  <div id="app">
    <media :query="{ minWidth: 768 }">
      <desktop-layout
        :schedule-items="scheduleItems"
        :selected-items="selectedItems"
        :logged-in="loggedIn"
        :calendar-api-state="calendarApiState"
        @log-in="logIn"
        @log-out="logOut"
        @create-calendar="createKoyomiCalendar" />
    </media>
    <media :query="{ maxWidth: 768 }">
      <mobile-layout
        :schedule-items="scheduleItems"
        :selected-items="selectedItems"
        :logged-in="loggedIn"
        :calendar-api-state="calendarApiState"
        @log-in="logIn"
        @log-out="logOut"
        @create-calendar="createKoyomiCalendar" />
    </media>
  </div>
</template>

<script>
import '@/assets/styles/main.css'
import DesktopLayout from './components/layouts/DesktopLayout.vue'
import MobileLayout from './components/layouts/MobileLayout.vue'
import Media from 'vue-media'
import axios from 'axios'
import moment from 'moment'

export default {
  name: 'app',
  components: {
    DesktopLayout,
    MobileLayout,
    Media
  },

  data () {
    return {
      scheduleItems: [],
      loggedIn: false,
      calendarApiState: 'idle'
    }
  },

  mounted: function () {

    axios.get('/schedule').then(({data}) => {
      data.forEach(item => item.selected = false)
      this.scheduleItems = data
    })

    this.loadGapi()

  },

  computed: {
    selectedItems: function () {
      return this.scheduleItems.filter(item => item.selected === true)
    }
  },

  methods: {

    loadGapi: function () {
      let script = document.createElement('script')

      script.onload = () => {
        gapi.load('client:auth2', () => {
          this.initializeGapi()
        })
      }

      script.src = 'https://apis.google.com/js/api.js'
      document.head.appendChild(script)
    },

    initializeGapi: function () {
      gapi.client.init({
        apiKey: 'AIzaSyBRc2qr_ZsE62W9Jsgql1UWHXwYt1FnJbM',
        clientId: '92861593872-7e0lpudsqkhdjop0umnop0o6o6qer7ev.apps.googleusercontent.com',
        discoveryDocs: ['https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest'],
        scope: 'https://www.googleapis.com/auth/calendar'
      }).then(() => {
        // Register the login state listener.
        gapi.auth2.getAuthInstance().isSignedIn.listen(this.handleLoginStateChange)

        // Handle the initial login state.
        this.handleLoginStateChange(gapi.auth2.getAuthInstance().isSignedIn.get())
      })
    },

    logIn: function () {
      gapi.auth2.getAuthInstance().signIn()
    },

    logOut: function () {
      gapi.auth2.getAuthInstance().signOut()
    },

    handleLoginStateChange: function (isLoggedIn) {
      this.loggedIn = isLoggedIn
    },

    createKoyomiCalendar: async function() {

      this.calendarApiState = 'working'

      // Delete any existing Koyomi calendars.

      const calendarListResponse = await gapi.client.calendar.calendarList.list()

      for (let i = 0; i < calendarListResponse.result.items.length; i++) {
        let item = calendarListResponse.result.items[i]
        if (item.summary === 'Koyomi') {
          await gapi.client.calendar.calendars.delete({calendarId: item.id})
        }
      }

      // Create a new Koyomi calendar.

      const calendarInsertResponse = await gapi.client.calendar.calendars.insert({summary: 'Koyomi'})
      const koyomiCalendarId = calendarInsertResponse.result.id

      // Create events in the new calendar
      // for each selected show.

      // Traditional for loop necessary because
      // you can't `await` inside a `forEach()`.

      for (let i = 0; i < this.selectedItems.length; i++) {
        let item = this.selectedItems[i]

        console.log('creating event for: ' + item.title)
        await gapi.client.calendar.events.insert({
          calendarId: koyomiCalendarId,
          resource: {
            summary: item.title,
            start: {
              dateTime: item.nextEpisodeTime,
              timeZone: 'America/Los_Angeles'
            },
            end: {
              dateTime: moment(item.nextEpisodeTime).add(30, 'minutes').toISOString(true),
              timeZone: 'America/Los_Angeles'
            },
            recurrence: ['RRULE:FREQ=WEEKLY']
          }
        })
      }

      this.calendarApiState = 'idle'
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

  ::selection {
    background: #a3e554;
    color: #fff;
  }

  #app {
    min-height: 100vh;
  }

  .button {
    display: inline-block;
    background-color: #8BC34A;
    padding: .5rem .75rem;
    color: #fff;
    border-radius: .25rem;
    cursor: pointer;

    &.disabled {
      cursor: not-allowed;
      pointer-events: none;
      opacity: 0.5;
    }
  }

  .button-sm {
    display: inline-block;
    background-color: #8BC34A;
    padding: .35rem .5rem;
    color: #fff;
    border-radius: .25rem;
    cursor: pointer;
    font-size: .8rem;
  }

  .button-secondary {
    background-color: #ddd;
    color: #666;
  }

</style>
