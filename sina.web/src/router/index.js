import Vue from 'vue';
import VueRouter from 'vue-router';
import Home from '../views/Home.vue';
import Recipes from '../components/Recipes/Recipes.vue';
import CreateRecipes from '../components/Recipes/CreateRecipe.vue';
import Shopping from '../components/Shopping/Shopping.vue';
import Planning from '../components/Planning/Planning.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue'),
  },
  {
    path: '/recipes',
    name: 'Recipes',
    component: Recipes,
  },
  {
    path: '/recipes/new',
    name: 'CreateRecipes',
    component: CreateRecipes,
  },
  {
    path: '/shopping',
    name: 'Shopping',
    component: Shopping,
  },
  {
    path: '/planning',
    name: 'Planning',
    component: Planning,
  },
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

export default router;
