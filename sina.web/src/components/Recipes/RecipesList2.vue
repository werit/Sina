<template>
  <div>
    <v-row>
      <div>
        <v-expansion-panels focusable inset style="width: 500px">
          <v-expansion-panel
            v-for="(recipe, index) in recipeItems"
            :key="recipe.title"
          >
            <v-expansion-panel-header
              @click="showing(index)"
              disable-icon-rotate
            >
              <template v-slot:actions>
                <v-icon class="icon">
                  {{ recipe.img }}
                </v-icon>
              </template>
              <span class="header mx-2">{{ recipe.name }}</span>
            </v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-card
                elevation="3"
                outlined
                shaped
                max-width="500"
                class="pa-2 ma-2"
              >
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
                </v-list>
              </v-card>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
        <div class="pa-2 ma-2">
          <recipes-crud />
          </div>
      </div>
      <!-- I could not make it work wito\hout it...it is an insufficiency,
      but I do not knwo how to solve -->
      <p v-if="currentIndex > -1 && shw"></p>
      <recipes-instructions v-if="currentIndex > -1 && recipeItems[currentIndex].show === true">
      </recipes-instructions>
    </v-row>
  </div>
</template>

<script>
import RecipesInstructions from './RecipeInstructions.vue';
import RecipesCrud from './RecipesCrud.vue';

export default {
  name: 'RecipesList',
  data: () => ({
    shw: false,
    currentIndex: -1,
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
      {
        title: 'bun-cha',
        name: 'Bun Cha',
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
          {
            ingTitle: 'zavitky',
            ingName: 'Zavitky',
            amount: 3,
            unit: 'ks',
            note: 'Preferrably fried.',
          },
        ],
      },
    ],
  }),
  components: { RecipesInstructions, RecipesCrud },
  methods: {
    showing(index) {
      if (index === this.currentIndex) {
        this.recipeItems[index].show = !this.recipeItems[index].show;
      } else {
        if (this.currentIndex > -1 && this.recipeItems[this.currentIndex].show === true) {
          this.recipeItems[this.currentIndex].show = false;
        }
        this.recipeItems[index].show = true;
      }
      this.shw = !this.shw;
      this.currentIndex = index;
    },
  },
};
</script>

<style>
.icon {
  order: 0;
}

.header {
  order: 1;
}
</style>
