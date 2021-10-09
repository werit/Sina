<template>
  <v-row
    align="center"
    justify="space-around"
  >
    <v-btn v-for="btn in btns" :key="btn.title" elevation="2" outlined class="ma-2 pa-2">{{
      btn.name
    }}</v-btn>
  </v-row>
</template>

<script>
export default {
  name: 'RecipesCrud',
  data: () => ({
    btns: [
      { title: 'add', name: 'Add' },
      { title: 'edit', name: 'Edit' },
      { title: 'delete', name: 'Delete' },
    ],
  }),
  methods: {},
};
</script>
