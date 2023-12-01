<template>
    <div class="container-fluid">
        <section class="row">
            <div v-if="recipe" class="col-12 text-center">

                <p class="fs-2">{{ recipe.title }}</p>
            </div>
        </section>
        <section class="row">
            <div class="col-4 me-5">
                <div v-if="recipe">
                    <img :src="recipe.img" alt="recipe image">
                </div>
            </div>
            <div v-if="recipe" class="col-3 p-4 recipe-details me-5">
                <p class="fs-4 text-center">Instructions</p>
                <p class="fs-5">{{ recipe.instructions }}</p>
                <form @submit.prevent="addInstructions()">
                    <div class="mb-3">
                        <label for="instructions" class="form-label"></label>
                        <input v-model="editable.instructions" type="text" class="form-control" id="instructions"
                            placeholder="Add Instructions" aria-describedby="emailHelp">
                    </div>

                    <button type="submit" class="btn btn-outline-dark">Submit</button>
                </form>

            </div>
            <div v-for="ingredient in ingredients" :key="ingredient.id" class="col-3 p-4 recipe-details">
                <span>{{ ingredient.quantity }} {{ ingredient.name }}</span>


                <!-- <p>{{ ingredient.name }}</p> -->
                <form @submit.prevent="addIngredient()">
                    <div class="mb-3">
                        <label for="ingredients" class="form-label"></label>
                        <input type="text" class="form-control" id="ingredients" placeholder="Add Ingredient"
                            aria-describedby="emailHelp">
                    </div>

                    <button type="submit" class="btn btn-outline-dark">Submit</button>
                </form>
            </div>

        </section>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { ingredientsService } from '../services/IngredientsService.js';
import { Recipe } from '../models/Recipe.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { useRoute } from 'vue-router';
export default {

    setup() {
        const route = useRoute()
        onMounted(() => {
            // getIngredientsByRecipeId()
        })
        const editable = ref({})
        async function getIngredientsByRecipeId() {
            try {
                // const recipeId = AppState.activeRecipe.id
                // await ingredientsService.getIngredientsByRecipeId(recipeId)
            } catch (error) {
                logger.error(error)
                Pop.error(error)

            }
        }

        return {
            editable,
            recipe: computed(() => AppState.activeRecipe),
            recipes: computed(() => AppState.recipes),
            ingredients: computed(() => AppState.ingredients),
            account: computed(() => AppState.account)
        }
    }
};
</script>


<style lang="scss" scoped>
img {
    position: center;
    object-fit: cover;
    width: 100%;
    height: 100%px;
}

.recipe-details {
    border: black solid 2px;
    border-radius: 5px;
}
</style>