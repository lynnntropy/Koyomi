<template>
  <div class="dashboard">
    <div class="list">
      <h1>Selected shows</h1>
      <div class="selected-items">
        <div v-for="item in selectedItems" :key="item.title" class="item">
          <div class="title">{{ item.title }}</div>
          <div class="time">{{ item.nextEpisodeTime | moment("dddd &ndash; h:mm a") }}</div>
        </div>
        <div v-if="!selectedItems.length" class="no-items-message">
          You haven't added any shows! Click or tap shows from the schedule
          to add them to the list.
        </div>
      </div>
    </div>
    <div class="google">
      <div v-if="selectedItems.length" class="google-container">
        <div v-if="loggedIn" class="logged-in">
          <div @click="logOut" class="button-sm button-secondary logout-button">Log out</div>

          <p>Click the button to create (or recreate) a Koyomi calendar with your anime events.</p>
          <div class="button" @click="createKoyomiCalendar" v-if="!loading">
            Create calendar
          </div>
          <div class="button button-disabled" v-else>
            Working...
          </div>
          <p v-if="finished" class="done-message">
            Done! <a href="https://calendar.google.com/" target="_blank" rel="noopener">Check out your calendar.</a>
          </p>
        </div>
        <div v-else class="login-form">
          <p>Log in with your Google account to add shows to your Google Calendar.</p>
          <div @click="logIn" class="button">Log in</div>
        </div>
      </div>
      <div v-else class="empty-message">
        <p>Select some shows first.</p>
      </div>
    </div>
  </div>
</template>

<script>
  import moment from 'moment'

  export default {
    name: "dashboard",
    props: ['selectedItems'],

    data () {
      return {
        loggedIn: false,
        loading: false,
        finished: false
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

        this.loading = true

        // Delete any existing Koyomi calendars.

        const calendarListResponse = await gapi.client.calendar.calendarList.list()

        for (let i = 0; i < calendarListResponse .result.items.length; i++) {
          let item = calendarListResponse .result.items[i]
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

        this.loading = false
        this.finished = true
      }

    }
  }
</script>

<style scoped lang="scss">

  a {
    color: #8BC34A;
  }

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

      .google-container {
        height: 100%;
      }

      .empty-message {
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
      }

      .button {
        display: inline-block;
        background-color: #8BC34A;
        padding: .5rem .75rem;
        color: #fff;
        border-radius: .25rem;
        cursor: pointer;
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

      .button-disabled {
        cursor: not-allowed;
        opacity: 0.5;
      }

      .button-secondary {
        background-color: #ddd;
        color: #666;
      }

      p {
        color: #666;
        margin-bottom: .5rem;
        line-height: 1.35;
      }

      .login-form {
        padding: 2rem;
        text-align: center;
      }

      .logged-in {
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        height: 100%;
        text-align: center;
        padding: 1rem;

        .logout-button {
          position: absolute;
          right: .35rem;
          bottom: .35rem;
        }

        .done-message {
          margin-top: .5rem;
          font-size: .9rem;
        }
      }
    }
  }
</style>