<template>
  <div v-else class="login-section">
    <div v-if="!loggedIn">
      <div @click="$emit('log-in')" class="button">Log in</div>
    </div>
    <div v-else>
      <div @click="$emit('log-out')" class="button">Log out</div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "login-section",
    props: ['loggedIn']
  }
</script>

<style scoped>

</style>