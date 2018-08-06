<template>
  <div class="dashboard">
    <h1>Selected shows</h1>
    <div class="selected-items">
      <div v-for="item in selectedItems" :key="item.title" class="item">
        <div class="title">{{ item.title }}</div>
        <div class="time">{{ item.dayOfWeek }} &ndash; {{ item.nextEpisodeTime | moment("h:mm a") }}</div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "dashboard",
    props: ['selectedItems']
  }
</script>

<style scoped lang="scss">
  .dashboard {
    background-color: #fff;
    border-left: 1px solid #eee;
    padding: 1rem;

    h1 {
      font-size: 1rem;
      color: #444;
      font-weight: 700;
    }

    .selected-items {
      padding-top: .5rem;

      .item {
        padding: .75rem 0;

        /*padding-bottom: 0.25rem;*/

        &:not(:last-of-type) {
          border-bottom: 1px solid #f9f9f9;
        }

        .title {
          margin-bottom: 0.25rem;
          font-size: 1rem;
          color: #333;
        }

        .time {
          color: #777;
          font-size: .9rem;
        }
      }
    }
  }
</style>