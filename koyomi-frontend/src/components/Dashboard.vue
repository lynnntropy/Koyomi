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
          You haven't selected any shows! Select shows from the schedule
          to add them to your calendar.
        </div>
      </div>
    </div>
    <div class="google">

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
      min-height: 0;  /* NEW */
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
    }
  }
</style>