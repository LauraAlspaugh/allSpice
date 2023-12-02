<template>
  <div class="container-fluid">
    <section class="row home-background p-5">
      <div class="col-12 text-center">
        <p class="text-center p-1 recipe-name">Cherish your family and their cooking.</p>
        <button class="rounded-circle recipe-button p-2" type="button" data-bs-toggle="modal"
          data-bs-target="#newRecipeModal">
          <i class="mdi mdi-plus fs-2" title="create a recipe"></i></button>

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
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4 p-4 d-flex justify-content-center ">
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
  color: #D5D8DC
}

.home-background {
  background-image: url("https://i0.wp.com/www.faithanddoubt.com/wp-content/uploads/hossein-farahani-pqJ21tErTgI-unsplash-edited-scaled.jpg?fit=2560%2C1437&ssl=1");
  position: center;
  background-repeat: no-repeat;
  object-fit: cover;
}

.recipe-button {
  background-color: #D5D8DC;
}
</style>
