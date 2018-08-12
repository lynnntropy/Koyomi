<template>
  <div class="send-to-google-section">
    <div class="button" @click="$emit('create-calendar')" :class="{ disabled: calendarApiState === 'working' }">
      <span v-if="calendarApiState !== 'working'">
        <font-awesome-icon icon="calendar-plus" /> Create calendar
      </span>
      <span v-else>
        <font-awesome-icon icon="clock" /> Working...
      </span>
    </div>
    <p v-if="showFinishedMessage">
      Finished! <a href="https://calendar.google.com/" target="_blank" rel="noopener">Take a look at your calendar.</a>
    </p>
  </div>
</template>

<script>
  export default {
    name: "send-to-google-section",
    props: ['calendarApiState'],

    data () {
      return {
        showFinishedMessage: false
      }
    },

    watch: {
      calendarApiState: function(newState, oldState) {
        if (newState === 'idle' && oldState === 'working') {
          this.showFinishedMessage = true
        }
      }
    }
  }
</script>

<style scoped lang="scss">
  p {
    margin-top: .5rem;
    line-height: 1.35;
    color: #777;
  }

   a {
     color: #8BC34A;
   }
</style>