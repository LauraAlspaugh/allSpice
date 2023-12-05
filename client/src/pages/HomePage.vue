<template>
  <div class="container-fluid">
    <section class="row home-background p-5">
      <div class="col-12 text-center">
        <p class="text-center p-1 recipe-name">Cherish your family and their cooking.</p>
        <button class=" rounded-circle recipe-button p-2" type="button" data-bs-toggle="modal"
          data-bs-target="#newRecipeModal">
          <i class="mdi mdi-plus fs-2" title="create a recipe"></i></button>

      </div>

    </section>
    <section class="row">
      <div class="col-12 mt-3 p-3 d-flex rounded-pill justify-content-around">
        <button class="btn btn-outline-dark w-100 mx-3" v-for="filter in filters" :key="filter"
          @click="filterRecipes(filter)">{{ filter
          }}</button>

      </div>
    </section>
    <div class="section-row ">
      <div class=" md-12 col-md-12 mt-3" v-if="!isMobile">

        <div class="d-flex rounded-pill justify-content-around">
          <button class="btn btn-outline-dark w-100 mx-3" @click="changeCategory('')">All</button>
          <button class="btn btn-outline-dark  w-100 mx-3" @click="changeCategory(category)"
            v-for="category in categories" :key="category">
            {{ category }}
          </button>
        </div>
      </div>
      <div class="dropdown" v-else>
        <button class="dropdown-toggle" data-bs-toggle="dropdown" role="button" aria-expanded="false"> Dropdown</button>
        <ul class="dropdown-menu">
          <li @click="changeCategory('')" class="dropdown-item">All</li>
          <li @click="changeCategory(category)" v-for="category in categories" :key="category" class="dropdown-item">
            {{ category }}</li>
        </ul>
      </div>
    </div>
    <section class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-12 col-md-4 p-4 d-flex justify-content-center ">
        <RecipeCard :recipeProp="recipe" />
      </div>
    </section>
  </div>
  <NewRecipeModal />
</template>

<script>
import { computed, onMounted, ref } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import RecipeCard from '../components/RecipeCard.vue';
import NewRecipeModal from '../components/NewRecipeModal.vue';
// import { accountService } from '../services/AccountService.js';

export default {
  setup() {
    const categories = ["Mexican", "Soup", "Italian", "Specialty Coffee", "Cheese"];
    const filters = ["Home", "Created", "Favorites"];
    const filteredCategory = ref("");
    // const filtersCategory = ref("");
    onMounted(() => {
      getRecipes();
      // getFavorites()
    });
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    };
    // async function getFavorites() {
    //   try {
    //     await accountService.getFavorites()
    //   } catch (error) {
    //     logger.error(error)
    //     Pop.error(error)

    //   }
    // }
    return {
      categories,
      filters,
      filteredCategory,

      isMobile: computed(() => {
        let isMobile = false
        if (window.innerWidth < 768) {
          isMobile = true
        }
        return isMobile
      }),
      recipes: computed(() => AppState.filteredRecipes),
      recipes: computed(() => {
        if (filteredCategory.value) {
          return AppState.recipes.filter((recipe) => recipe.category == filteredCategory.value);

        } else {
          return AppState.recipes
        }
      }),
      // filteredRecipes: computed(() => {
      //   if (filtersCategory.value) {
      //     return AppState.recipes.filter((recipe) => recipe.filter == filtersCategory.value);
      //   } else {
      //     return AppState.recipes
      //   }
      // }),
      filterRecipes(filter) {
        recipesService.filterRecipes(filter)
      },
      changeCategory(category) {
        logger.log(category)
        filteredCategory.value = category
      }
    }
  },
  components: { RecipeCard, NewRecipeModal }
}
</script>

<style scoped lang="scss">
.recipe-name {
  font-family: 'Pinyon Script', cursive;
  font-size: 50px;
  color: #7F8C8D;
  // backdrop-filter: blur(6px);
  text-shadow: 2px 4px 4px rgb(87, 82, 82);
}

.home-background {
  // background-image: url("https://i0.wp.com/www.faithanddoubt.com/wp-content/uploads/hossein-farahani-pqJ21tErTgI-unsplash-edited-scaled.jpg?fit=2560%2C1437&ssl=1");
  background-image: url("https://images.unsplash.com/photo-1567769541715-8c71fe49fd43?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTMwfHxmb29kfGVufDB8fDB8fHww");

  background-position: center;
  background-repeat: no-repeat;
  // object-fit: cover;
  background-size: cover;
  border-bottom: #7F8C8D solid 6px;
}

.recipe-button {
  background-color: #D5D8DC;
  border-radius: 50%;
  padding: 100px 45px;
}
</style>
