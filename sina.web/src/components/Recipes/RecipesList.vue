<template>
    <div>
<v-list>
        <v-list-group
          v-for="recipe in recipeItems"
          :key="recipe.title"
          v-model="recipe.active"
          :prepend-icon="recipe.img"
          no-action
        >
          <template v-slot:activator>
            <v-list-item-content>
              <v-list-item-title v-text="recipe.name"></v-list-item-title>
            </v-list-item-content>
          </template>
          <!-- v-for="ingredient in recipe.ingredients"
        :key="ingredient.ingTitle" -->
          <v-card
            elevation="3"
            outlined
            shaped
            max-width="500"
            class="pa-2 ma-2"
          >
            <!-- <v-row>
            <v-icon right>{{ recipe.img }}</v-icon>
            <v-card-title>{{ recipe.name }}</v-card-title>
          </v-row> -->
            <v-list two-line subheader>
              <v-list-item
                v-for="ingredient in recipe.ingredients"
                :key="ingredient.ingTitle"
              >
                <v-list-item-content>
                  <v-list-item-title
                    >{{ ingredient.amount }}{{ ingredient.unit }}
                    {{ ingredient.ingName }}</v-list-item-title
                  >
                  <v-list-item-subtitle>{{
                    ingredient.note
                  }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
              <!-- <v-subheader>Ingredients</v-subheader>
          <v-list-item
            v-for="ingredient in recipe.ingredients"
            :key="ingredient.ingTitle"
          >
            <v-list-item-content>
              <v-list-item-title
                >{{ ingredient.amount }}{{ ingredient.unit }}
                {{ ingredient.ingName }}</v-list-item-title
              >
              <v-list-item-subtitle>{{ ingredient.note }}</v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item> -->
            </v-list>
          </v-card>
        </v-list-group>
      </v-list>
    </div>
</template>

<script>
export default {
  name: 'RecipesList',
  data: () => ({
    recipeItems: [
      {
        title: 'kura-bazant',
        name: 'Kura a la Bazant',
        img: 'mdi-food-drumstick',
        ingredients: [
          {
            ingTitle: 'kura',
            ingName: 'Kura',
            amount: 1,
            unit: 'kg',
            note: 'bez drobkov',
          },
          {
            ingTitle: 'cibula',
            ingName: 'Cibula',
            amount: 3,
            unit: 'ks',
            note: 'vacsia',
          },
        ],
      },
      {
        title: 'ramen',
        name: 'Basic Ramen',
        img: 'mdi-bowl-mix-outline',
        ingredients: [
          {
            ingTitle: 'broth',
            ingName: 'Rich broth',
            amount: 1,
            unit: 'l',
            note: 'Clean broth coocked over a long period of time.',
          },
          {
            ingTitle: 'noodles',
            ingName: 'Noodles',
            amount: 300,
            unit: 'g',
            note: 'Wide rice noodles.',
          },
        ],
      },
    ],
  }),
  methods: {},
};
</script>
