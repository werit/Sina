<template>
  <div class="ma-12 pa-12">
    <!-- <v-toolbar dark class="primary">
      <v-toolbar-side-icon
        @click.stop="sideNav = !sideNav"
        class="hidden-sm-and-up "></v-toolbar-side-icon>
      <v-toolbar-title>
        <router-link to="/" tag="span" style="cursor: pointer">DevMeetup</router-link>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items class="hidden-xs-only">
        <v-btn
          flat
          v-for="item in menuItems"
          :key="item.title"
          :to="item.link">
          <v-icon left dark>{{ item.icon }}</v-icon>
          {{ item.title }}
        </v-btn>
      </v-toolbar-items>
    </v-toolbar>

    <v-navigation-drawer temporary v-model="sideNav">
      <v-list>
        <v-list-tile
          v-for="item in menuItems"
          :key="item.title"
          :to="item.link">
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>{{ item.title }}</v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer> -->

    <v-card>
      <v-container class="lighten-5 mb-6">
        <v-row align="center" no-gutters>
          <v-col cols="2">
            <v-navigation-drawer permanent expand-on-hover>
              <v-list>
                <v-list-item class="px-2">
                  <v-list-item-avatar>
                    <v-img
                      src="https://randomuser.me/api/portraits/women/85.jpg"
                    ></v-img>
                  </v-list-item-avatar>
                </v-list-item>

                <v-list-item link>
                  <v-list-item-content>
                    <v-list-item-title class="text-h6">
                      Sandra Adams
                    </v-list-item-title>
                    <v-list-item-subtitle
                      >sandra_a88@gmail.com</v-list-item-subtitle
                    >
                  </v-list-item-content>
                </v-list-item>
              </v-list>

              <v-divider></v-divider>

              <v-list nav dense>
                <!-- <v-list-tile
          v-for="item in menuItems"
          :key="item.title"
          :to="item.link">
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>{{ item.title }}</v-list-tile-content>
        </v-list-tile> -->
                <!-- <v-list-item link>
                  <v-list-item-icon>
                    <v-icon>mdi-bowl-mix</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Recipes</v-list-item-title>
                </v-list-item> -->
                <v-list-item
                  v-for="item in menuItems"
                  :key="item.title"
                  :to="item.link"
                >
                  <v-list-item-icon>
                    <v-icon>{{ item.icon }}</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>{{ item.title }}</v-list-item-title>
                </v-list-item>
                <!-- <v-list-item link>
                  <v-list-item-icon>
                    <v-icon>mdi-application</v-icon>
                  </v-list-item-icon>
                  <v-list-item-title>Planning</v-list-item-title>
                </v-list-item> -->
              </v-list>
            </v-navigation-drawer>
          </v-col>
          <v-col><recipes /></v-col>
        </v-row>
      </v-container>
    </v-card>
  </div>
</template>

<script>
import Recipes from './Recipes/Recipes.vue';

export default {
  name: 'LeftNavigation',
  data() {
    return {
      menuItems: [
        { icon: 'mdi-bowl-mix', title: 'Recipes', link: '/recipes' },
        { icon: 'mdi-basket', title: 'Shopping', link: '/shopping' },
        { icon: 'mdi-application', title: 'Planning', link: '/planning' },
      ],
    };
  },
  //
  components: {
    Recipes,
  },
};
</script>
