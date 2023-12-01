<template>
  <h1>Welcome to All Spice.</h1>
  <button @click="createRecipe()">Create Recipe</button>
  <div class="container-fluid">
    <section class="row">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-4 p-4">
        <RecipeCard :recipeProp="recipe" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import RecipeCard from '../components/RecipeCard.vue';

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
  components: { RecipeCard }
}
</script>

<style scoped lang="scss"></style>
