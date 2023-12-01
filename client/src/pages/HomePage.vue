<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 text-center">
        <p class="text-center p-2 recipe-name">Cherish your family and their cooking.</p>
        <button class="rounded pill p-2" type="button" data-bs-toggle="modal" data-bs-target="#newRecipeModal">Create
          Recipe</button>

      </div>

    </section>
    <div class="section-row ">
      <div class=" md-12 col-md-12 mt-3">
        <div class="d-flex rounded-pill justify-content-around">
          <button class="btn btn-outline-dark w-100 mx-3" @click="changeCategory('')">All</button>
          <button class="btn btn-outline-dark  w-100 mx-3" @click="changeCategory(category)"
            v-for="category in categories" :key="category">
            {{ category }}
          </button>
        </div>
      </div>
    </div>
    <section class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4 p-4">
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

export default {
  setup() {
    const categories = ["Mexican", "Soup", "Italian", "Specialty Coffee", "Cheese"];
    const filteredCategory = ref("");
    onMounted(() => {
      getRecipes();
    });
    async function getRecipes() {
      try {
        await recipesService.getRecipes();
      }
      catch (error) {
        logger.error(error);
        Pop.error(error);
      }
    }
    return {
      categories,
      filteredCategory,
      recipes: computed(() => AppState.recipes),
      recipes: computed(() => {
        if (filteredCategory.value) {
          return AppState.recipes.filter((recipe) => recipe.category == filteredCategory.value);

        } else {
          return AppState.recipes
        }
      }),
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
  color: #935116
}
</style>
