<template>
  <div class="header-section">
    <h1>
      Koyomi
      <font-awesome-icon icon="calendar-alt" />
    </h1>
    <p>This is an app that allows you to create a Google Calendar with the weekly air times of all the anime you're currently watching.</p>
  </div>
</template>

<script>
  export default {
    name: "header-section"
  }
</script>

<style scoped lang="scss">
  h1 {
    text-align: center;
    margin-bottom: 1rem;
    font-weight: 500;
  }

  p {
    line-height: 1.35;
    color: #777;
  }
</style>