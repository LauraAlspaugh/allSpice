<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-12 text-center">
        <p class="text-center p-2 recipe-name">Cherish your family and their cooking.</p>
        <button class="rounded pill p-2" type="button" data-bs-toggle="modal" data-bs-target="#newRecipeModal">Create
          Recipe</button>

      </div>

    </section>
    <section class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4 p-4">
        <RecipeCard :recipeProp="recipe" />
      </div>
    </section>
  </div>
  <NewRecipeModal />
</template>

<script>
import { computed, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import RecipeCard from '../components/RecipeCard.vue';
import NewRecipeModal from '../components/NewRecipeModal.vue';

export default {
  setup() {
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
      recipes: computed(() => AppState.recipes)
    };
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
