<template>
  <div class="calendar">
    <div v-for="day in days" :key="day.id" class="day">
      <h1>
        {{ day.name }}
      </h1>
      <div v-for="item in getItemsForDay(day.id)" :key="item.title" class="item" :class="{ 'selected': item.selected }" @click="item.selected = !item.selected">
        <div class="title">{{ item.title }}</div>
        <div class="time">{{ item.nextEpisodeTime | moment("h:mm a") }} &ndash; {{ item.nextEpisodeTime | moment("from") }}</div>
      </div>
    </div>
  </div>
</template>

<script>
  import moment from 'moment'

  export default {
    name: "calendar",
    props: ['scheduleItems'],

    data () {
      return {
        days: [
          { name: 'Monday', id: 1 },
          { name: 'Tuesday', id: 2 },
          { name: 'Wednesday', id: 3 },
          { name: 'Thursday', id: 4 },
          { name: 'Friday', id: 5 },
          { name: 'Saturday', id: 6 },
          { name: 'Sunday', id:  7}
        ]
      }
    },

    methods: {
      getItemsForDay: function (dayId) {
        return this.scheduleItems.filter(item => {
          return moment(item.nextEpisodeTime).isoWeekday() === dayId
        })
      }
    },
  }
</script>

<style scoped lang="scss">

  ::-webkit-scrollbar {
    width: 0.25rem;
    background: transparent;
  }

  ::-webkit-scrollbar-thumb {
    background: rgba(0, 0, 0, 0.1);
  }

  .calendar {

    @media (min-width: 768px) {
      max-height: 100vh;
      overflow-y: scroll;
    }

    @media (max-width: 1919px) {

      background-color: #fff;

      h1 {
        margin-top: 1rem;
        margin-bottom: .5rem;
        margin-left: 1rem;

        font-size: 1.25rem;
        color: #333;
      }

      .item {
        padding: 1rem;
        border-left: .25rem solid rgba(0, 0, 0, 0);

        .title {
          margin-bottom: 0.25rem;
        }

        .time {
          color: #777;
          font-size: 1rem;
        }

        &.selected {
          border-left-color: #8BC34A;
        }

        cursor: pointer;
        transition: background-color .1s ease;

        &:hover {
          background-color: #f9f9f9;
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
          line-height: 1.3;
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