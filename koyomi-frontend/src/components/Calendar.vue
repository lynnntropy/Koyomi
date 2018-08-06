<template>
  <div class="calendar">
    <div v-for="day in days" :key="day" class="day">
      <h1>
        {{day}}
      </h1>
      <div v-for="item in getItemsForDay(day)" :key="item.title" class="item" :class="{ 'selected': item.selected }" @click="item.selected = !item.selected">
        <div class="title">{{ item.title }}</div>
        <div class="time">{{ item.nextEpisodeTime | moment("h:mm a") }} &ndash; {{ item.nextEpisodeTime | moment("from") }}</div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "calendar",
    props: ['scheduleItems'],

    data () {
      return {
        days: [
          'Monday',
          'Tuesday',
          'Wednesday',
          'Thursday',
          'Friday',
          'Saturday',
          'Sunday'
        ]
      }
    },

    methods: {
      getItemsForDay: function (dayString) {
        return this.scheduleItems.filter(item => item.dayOfWeek === dayString)
      }
    },
  }
</script>

<style scoped lang="scss">
  .calendar {

    @media (max-width: 1919px) {

      h1 {
        margin-top: 1rem;
        margin-bottom: .5rem;
        margin-left: 1rem;

        font-size: 1.5rem;
        color: #333;
      }

      .item {
        padding: 1rem;

        .title {
          margin-bottom: 0.25rem;
        }

        .time {
          color: #777;
          font-size: 1rem;
        }

        cursor: pointer;
        transition: background-color .2s ease;

        &:hover {
          background-color: #f6f6f6;
        }

      }

    }

    @media (min-width: 1920px) {

      display: grid;
      grid-template-columns: repeat(7, 1fr);
      padding: 0 1rem;

      h1 {
        margin-top: 1rem;
        margin-bottom: .75rem;
        text-align: center;

        font-size: 1.25rem;
        color: #666;
        font-weight: 500;
      }

      .day {

        padding: 0 .5rem;

        .item {
          padding: .5rem;
          margin-bottom: .5rem;
          background-color: #fff;
          border-radius: .25rem;
          box-shadow: 0 .1rem .25rem rgba(0, 0, 0, .075);
          border-right: .25rem solid rgba(0, 0, 0, 0);
          user-select: none;

          .title {
            margin-bottom: 0.25rem;
            font-size: .85rem;
            color: #333;
          }

          .time {
            color: #777;
            font-size: .8rem;
          }

          cursor: pointer;
          transition: transform .1s ease;

          &:hover {
            transform: translateY(-.1rem);
          }

          &.selected {
            border-right-color: #8BC34A;
          }
        }
      }
    }
  }
</style>