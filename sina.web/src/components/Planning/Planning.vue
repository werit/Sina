<template>
  <div>
    <v-list v-for="item in dayItems"
          :key="item.title">
    <v-btn text>
      <span class="mr-4">{{ item.day }}</span>
    </v-btn>
    <v-btn v-for="food in foodItems[item.title]" :key="food.title">
       <v-icon left>{{ food.icon }}</v-icon>
      <span class="mr-2">{{ food.title }}, {{ food.count }}x</span>
    </v-btn>
    </v-list>
  </div>
</template>

<script>
export default {
  name: 'Planning',
  data: () => ({
    dayItems: [
      { title: 'monday', day: 'MO' },
      { title: 'tuesday', day: 'TU' },
      { title: 'wednesday', day: 'WE' },
      { title: 'thursday', day: 'TH' },
      { title: 'friday', day: 'FR' },
      { title: 'saturday', day: 'SA' },
      { title: 'sunday', day: 'SU' },
    ],
    foodItems:
      {
        monday: [
          { title: 'ramen', icon: 'mdi-bowl', count: 12 },
          { title: 'poke', icon: 'mdi-bowl-mix-outline', count: 3 },
        ],
        tuesday: [
          { title: 'Pho-bo', icon: 'mdi-bowl', count: 27 },
          { title: 'poke-salat', icon: 'mdi-bowl-mix-outline', count: 10 },
        ],
        wednesday: [
          { title: 'Bun-cha', icon: 'mdi-bowl', count: 11 },
          { title: 'green-fact-salat', icon: 'mdi-bowl-mix-outline', count: 20 },
        ],
      },
  }),
  methods: {
  },
};
</script>
