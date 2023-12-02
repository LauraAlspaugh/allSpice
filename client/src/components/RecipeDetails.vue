<template>
    <div class="container-fluid">
        <section class="row">
            <div v-if="recipe" class="col-12 text-center">

                <p class="fs-2 recipe-name">{{ recipe.title }}</p>
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
                <p class="fs-5">{{ recipe.instructions }} <i @click="destroyInstructions()" class="mdi mdi-close fs-4"
                        type="button"></i> </p>
                <form @submit.prevent="addInstructions()">
                    <div class="mb-3">
                        <label for="instructions" class="form-label"></label>
                        <input v-model="editable2.instructions" type="text" class="form-control" id="instructions"
                            placeholder="Add Instructions" aria-describedby="emailHelp">
                    </div>

                    <button type="submit" class="btn btn-outline-dark">Submit</button>
                </form>

            </div>

            <div class="col-3 p-4 recipe-details">
                <p class="fs-4 text-center">Ingredients</p>
                <p v-for="ingredient in ingredients" :key="ingredient.id" class="ingredient-card">{{ ingredient.quantity
                }} {{ ingredient.name }} <i @click="destroyIngredient()" class="mdi mdi-close fs-4" type="button"></i>
                </p>




                <form @submit.prevent="addIngredient()">
                    <div class="mb-3">
                        <label for="quantity" class="form-label"></label>
                        <input v-model="editable.quantity" type="text" class="form-control" id="quantity"
                            placeholder="Amount" aria-describedby="emailHelp">
                    </div>
                    <div class="mb-3">
                        <label for="name" class="form-label"></label>
                        <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Name"
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
        const editable2 = ref({})
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
            editable2,
            recipe: computed(() => AppState.activeRecipe),
            recipes: computed(() => AppState.recipes),
            ingredients: computed(() => AppState.ingredients),
            ingredient: computed(() => AppState.activeIngredient),
            account: computed(() => AppState.account),
            async addInstructions() {
                try {
                    const instructionData = editable2.value
                    instructionData.recipeId = AppState.activeRecipe.id
                    await recipesService.addInstructions(instructionData, instructionData.recipeId)

                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async addIngredient() {
                try {
                    const ingredientData = editable.value
                    ingredientData.recipeId = AppState.activeRecipe.id
                    await ingredientsService.addIngredient(ingredientData)
                    editable.value = {}
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async destroyIngredient() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this Ingredient? ')
                    if (!wantstoDestroy) {
                        return
                    } const ingredientId = AppState.activeRecipe.ingredients.id
                    await ingredientsService.destroyIngredient(ingredientId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            },
            async destroyInstructions() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy these instructions? ')
                    if (!wantstoDestroy) {
                        return
                    } const instructionsId = AppState.activeRecipe.instructions.id
                    await recipesService.destroyInstructions(instructionsId)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }
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

.ingredient-card {
    padding: 3px;

}

.recipe-name {
    color: #7F8C8D;
    font-family: 'Pinyon Script', cursive;
}
</style>