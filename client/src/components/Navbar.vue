<template>
  <nav class="navbar navbar-expand-sm navbar-dark nav-color px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <i class="mdi mdi-shaker-outline fs-1"><span class="plan-title fs-1 p-0">All Spice</span></i>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
          <!-- <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link> -->
        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <!-- <div class="search-container">
        <form action="/action_page.php">
          <input type="text" placeholder="Search.." name="search">
          <button type="submit"><i class="mdi mdi-magnify "></i></button>
        </form>
      </div> -->
      <div>
        <form class="d-flex" @submit.prevent="getRecipesWithSearchQuery(editable)">

          <!-- <label for="recipeTitle" class="form-label"></label> -->
          <input v-model="editable" type="text" class="form-control" id="recipeTitle">
          <button type="submit"> <i class="mdi mdi-magnify"></i></button>

        </form>
      </div>
      <div>
        <button class="btn text-light" @click="toggleTheme"><i class="mdi"
            :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
      </div>
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { recipesService } from '../services/RecipesService.js';
export default {
  setup() {
    const editable = ref('');

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      editable,
      recipes: computed(() => AppState.recipes),
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      },
      getRecipesWithSearchQuery(editable) {
        logger.log(editable)
        recipesService.getRecipesWithSearchQuery(editable)
      },
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

.nav-color {
  /* background-color: #7F8C8D; */
  background-color: #7F8C8D
}

.plan-title {
  font-family: 'Pinyon Script', cursive;

  color: #D5D8DC
}
</style>
