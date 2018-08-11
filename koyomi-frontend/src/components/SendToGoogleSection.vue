<template>
  <div class="send-to-google-section">
    <div class="button" @click="$emit('create-calendar')" :class="{ disabled: calendarApiState === 'working' }">
      {{ calendarApiState === 'working' ? 'Working...' : 'Create calendar '}}
    </div>
    <p v-if="showFinishedMessage">
      Finished! <a href="https://calendar.google.com/" target="_blank" rel="noopener">Open your calendar.</a>
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