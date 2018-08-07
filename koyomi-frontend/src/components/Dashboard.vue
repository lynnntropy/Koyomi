<template>
  <div class="dashboard">
    <div class="list">
      <h1>Selected shows</h1>
      <div class="selected-items">
        <div v-for="item in selectedItems" :key="item.title" class="item">
          <div class="title">{{ item.title }}</div>
          <div class="time">{{ item.dayOfWeek }} &ndash; {{ item.nextEpisodeTime | moment("h:mm a") }}</div>
        </div>
        <div v-if="!selectedItems.length" class="no-items-message">
          You haven't added any shows! Click or tap shows from the schedule
          to add them to your calendar.
        </div>
      </div>
    </div>
    <div class="google">
      <div v-if="loggedIn">
        <div @click="logOut" class="button">Log out</div>
      </div>
      <div v-else class="login-form">
        <p>Log in with your Google account to add shows to your Google Calendar.</p>
        <div @click="logIn" class="button">Log in</div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "dashboard",
    props: ['selectedItems'],

    data () {
      return {
        loggedIn: false
      }
    },

    mounted () {
      this.loadGapi()
    },

    methods: {

      loadGapi: function () {
        let script = document.createElement('script')

        script.onload = () => {
          gapi.load('client:auth2', () => {
            console.log('gapi loaded')
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
          console.log('gapi initialized')

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

        if (isLoggedIn) {
          gapi.client.calendar.calendarList.list().then(response => console.log(response))
        }
      }
    }
  }
</script>

<style scoped lang="scss">

  ::-webkit-scrollbar {
    width: 0.35rem;
    background: transparent;
  }

  ::-webkit-scrollbar-thumb {
    background: #ddd;
  }

  .dashboard {
    background-color: #fff;
    border-left: 1px solid #eee;

    display: grid;
    grid-template-rows: 1fr 10rem;
    height: 100vh;

    .list {
      padding: 1rem;
      overflow-y: auto;
      min-height: 0;
      min-width: 0;

      h1 {
        font-size: 1rem;
        color: #444;
        font-weight: 700;
      }

      .selected-items {
        padding-top: .5rem;

        .item {
          padding: .75rem 0;

          &:not(:last-of-type) {
            border-bottom: 1px solid #f9f9f9;
          }

          .title {
            margin-bottom: 0.25rem;
            font-size: 1rem;
            color: #333;
            line-height: 1.25;
          }

          .time {
            color: #777;
            font-size: .9rem;
          }
        }

        .no-items-message {
          padding: .75rem 0;
          color: #888;
          line-height: 1.35;
        }
      }
    }

    .google {
      background-color: #f2f2f2;

      .login-form {
        padding: 2rem;
        text-align: center;

        p {
          color: #666;
          margin-bottom: .5rem;
          line-height: 1.35;
        }

        .button {
          display: inline-block;
          background-color: #8BC34A;
          padding: .5rem .75rem;
          color: #fff;
          border-radius: .25rem;
          cursor: pointer;
        }
      }
    }
  }
</style>