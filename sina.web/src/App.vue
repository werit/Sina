<template>
  <v-app>
    <v-navigation-drawer app clipped permanent expand-on-hover>
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
                <v-list-item v-for="item in menuItems"
          :key="item.title"
          :to="item.link">
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

 <v-app-bar
      app
      clipped-left
      color="primary"
      dark
    >
      <div class="d-flex align-center">
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />

        <v-img
          alt="Vuetify Name"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>

      <v-spacer></v-spacer>

      <v-btn
        href="https://github.com/vuetifyjs/vuetify/releases/latest"
        target="_blank"
        text
      >
        <span class="mr-2">Latest Release</span>
        <v-icon>mdi-open-in-new</v-icon>
      </v-btn>
    </v-app-bar>
    <v-main>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>

export default {
  name: 'App',

  data: () => ({
    menuItems: [
      { icon: 'mdi-bowl-mix', title: 'Recipes', link: '/recipes' },
      { icon: 'mdi-basket', title: 'Shopping', link: '/shopping' },
      { icon: 'mdi-application', title: 'Planning', link: '/planning' },
    ],
  }),
};
</script>
