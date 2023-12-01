<template>
    <div class=" recipe-card mb-3">
        <!-- <section class="row">
            <div class="col-4 "> -->
        <img @click="setActiveRecipe(recipeProp)" type="button" data-bs-toggle="modal" data-bs-target="#recipeModal"
            class="img-fluid" :src="recipeProp.img" alt="recipe image">
        <p class="fs-5 text-center">{{ recipeProp.title }}</p>
        <p class="text-center ">{{ recipeProp.category }}</p>
        <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
            Launch demo modal
        </button> -->
    </div>

    <!-- </section>
    </div> -->
    <RecipeModal />
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe.js';
import RecipeDetails from './RecipeDetails.vue';
import RecipeModal from './RecipeModal.vue';
import { useRoute } from 'vue-router';

import { recipesService } from '../services/RecipesService.js';
export default {
    props: { recipeProp: { type: Recipe, required: true } },
    setup() {
        // const route = useRoute();
        return {
            recipes: computed(() => AppState.recipes),
            setActiveRecipe(recipeProp) {
                recipesService.setActiveRecipe(recipeProp)
            }
        };
    },
    components: { RecipeDetails, RecipeModal }
};
</script>


<style lang="scss" scoped>
.recipe-card {
    width: 18rem;
    border: 2px solid black;
    padding: 5px;
    border-radius: 7px;
    /* From https://css.glass */
    background: rgba(187, 174, 174, 0.62);
    border-radius: 16px;
    box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
    backdrop-filter: blur(3.6px);
    -webkit-backdrop-filter: blur(3.6px);
    border: 1px solid rgba(30, 23, 23, 0.78);
}

img {
    border-radius: 7px;
    width: 20rem;
    height: 18rem;
    object-fit: cover;
    position: center;
    ;
}
</style>