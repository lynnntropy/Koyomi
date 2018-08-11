<template>
  <div class="mobile-layout">

    <header-section />

    <div class="step-container">
      <h2>Step 1</h2>
      <h3>Select the shows you're watching.</h3>
      <calendar
        class="calendar"
        :schedule-items="scheduleItems" />
    </div>

    <div class="step-container" :class="{ disabled: selectedItems.length === 0 }">
      <h2>Step 2</h2>
      <h3>Log into your Google account. This is secure, and doesn't allow us to see your password.</h3>
      <login-section
        @log-in="$emit('log-in')"
        @log-out="$emit('log-out')"
        :logged-in="loggedIn" />
    </div>

    <div class="step-container" :class="{ disabled: !loggedIn || selectedItems.length === 0 }">
      <h2>Step 3</h2>
      <h3>Create a calendar! If you already have a Koyomi calendar, this will overwrite it.</h3>
      <send-to-google-section
        @create-calendar="$emit('create-calendar')"
        :calendar-api-state="calendarApiState" />
    </div>

  </div>
</template>

<script>
  import Calendar from '../Calendar.vue'
  import HeaderSection from '../HeaderSection.vue'
  import LoginSection from '../LoginSection.vue'
  import SendToGoogleSection from '../SendToGoogleSection.vue'

  export default {
    name: "mobile-layout",
    props: [
      'scheduleItems',
      'selectedItems',
      'loggedIn',
      'calendarApiState'
    ],

    components: {
      Calendar,
      HeaderSection,
      LoginSection,
      SendToGoogleSection
    }


  }
</script>

<style scoped lang="scss">

  $padding: .75rem;

  .mobile-layout {
    background-color: #fff;
    padding: 1.5rem $padding $padding $padding;
  }

  .calendar {
    margin: 0 $padding * -1;
  }

  .step-container {
    margin: 1.5rem 0;

    &.disabled {
      pointer-events: none;
      opacity: 0.25;
    }

    h2 {
      font-size: 1rem;
      margin-bottom: .1rem;
    }

    h3 {
      font-size: 1rem;
      color: #777;
      font-weight: 400;
      line-height: 1.35;
      margin-bottom: .5rem;
    }
  }

</style>