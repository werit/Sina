<template>
  <div>
    <v-card color="grey lighten-4" flat height="50px" tile>
      <v-toolbar dense>
        <v-app-bar-nav-icon></v-app-bar-nav-icon>

        <v-toolbar-title>Search</v-toolbar-title>

        <v-spacer></v-spacer>

        <v-btn icon>
          <v-icon>mdi-magnify</v-icon>
        </v-btn>

        <v-btn icon>
          <v-icon>mdi-heart</v-icon>
        </v-btn>

        <v-btn icon>
          <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
      </v-toolbar>
    </v-card>
    <v-container>
      <v-row>
        <div>
          <v-container grid-list-md>
            <recipes-list />
          </v-container>
          <recipes-crud />
        </div>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import RecipesList from './RecipesList2.vue';
import RecipesCrud from './RecipesCrud.vue';

export default {
  name: 'Recipes',
  data: () => ({}),
  components: { RecipesList, RecipesCrud },
  methods: {},
};
</script>
