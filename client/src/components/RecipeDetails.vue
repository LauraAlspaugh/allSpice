<template>
    <div class="container-fluid">
        <section class="row">
            <div v-if="recipe" class="col-12 text-center">

                <p class="fs-2 recipe-name" :title="recipe.title">{{ recipe.title }}</p>
            </div>
        </section>
        <section class="row">
            <div class="col-12 mb-2 col-md-4 me-5">
                <div v-if="recipe">
                    <img :src="recipe.img" title="recipe image" alt="recipe image">
                </div>
            </div>
            <div v-if="recipe" class="col-12 mb-2 col-md-3 p-4 recipe-details me-5">
                <p class="fs-4 text-center instruction-name">Instructions</p>
                <p class="fs-5">{{ recipe.instructions }} </p>
                <form @submit.prevent="addInstructions()">
                    <div class="mb-3">
                        <label for="instructions" class="form-label"></label>
                        <input v-model="editable2.instructions" type="text" class="form-control" id="instructions"
                            placeholder="Add Instructions" aria-describedby="emailHelp" maxlength="255">
                    </div>

                    <button type="submit" class="btn btn-outline-dark">Submit</button>
                </form>
                <!-- <div class=" absolute">
                    <span><i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>

                    </span>

                </div> -->
            </div>

            <div class="col-12 mb-2 col-md-3 p-4 recipe-details">
                <p class="fs-4 text-center instruction-name">Ingredients</p>
                <p v-for="ingredient in ingredients" :key="ingredient.id" class="ingredient-card">{{ ingredient.quantity
                }} {{ ingredient.name }} <i @click="destroyIngredient(ingredient.id)" class="mdi mdi-close fs-4"
                        type="button" title="delete this ingredient"></i>
                    <i class="mdi mdi-pencil fs-4 btn" type="button" title="edit this ingredient"
                        @click="enableEdit(ingredient)"></i>
                </p>





                <form @submit.prevent="routeSubmit()">
                    <div class="mb-3">
                        <label for="quantity" class="form-label"></label>
                        <input v-model="editable.quantity" type="text" class="form-control" id="quantity"
                            placeholder="Amount" aria-describedby="emailHelp" maxlength="255" required>

                    </div>
                    <div class="mb-3">
                        <label for="name" class="form-label"></label>
                        <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Name"
                            aria-describedby="emailHelp" maxlength="255" required>

                    </div>

                    <button type="submit" class="btn btn-outline-dark">Submit</button>
                </form>
                <!-- <div class=" absolute">
                    <span><i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>
                        <i class="mdi mdi-chevron-double-right fs-1 chevron-icon"></i>

                    </span>

                </div> -->
            </div>


        </section>

    </div>
    <EditModal />
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
import EditModal from './EditModal.vue';
import { accountService } from '../services/AccountService.js';
export default {
    setup() {
        const route = useRoute();
        onMounted(() => {
            // getIngredientsByRecipeId()
            // getFavorites()
        });
        const editable = ref({});
        const editable2 = ref({});
        async function getIngredientsByRecipeId() {
            try {
                // const recipeId = AppState.activeRecipe.id
                // await ingredientsService.getIngredientsByRecipeId(recipeId)
            }
            catch (error) {
                logger.error(error);
                Pop.error(error);
            }
        };
        async function getFavorites() {
            try {
                await accountService.getFavorites()
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
                    const instructionData = editable2.value;
                    instructionData.recipeId = AppState.activeRecipe.id;
                    await recipesService.addInstructions(instructionData, instructionData.recipeId);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },
            async addIngredient() {
                try {
                    const ingredientData = editable.value;
                    ingredientData.recipeId = AppState.activeRecipe.id;
                    await ingredientsService.addIngredient(ingredientData);
                    editable.value = {};
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },
            async editIngredient() {
                try {
                    const ingredientData = editable.value;
                    await ingredientsService.editIngredient(ingredientData);
                    editable.value = {};
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },
            enableEdit(ingredient) {
                editable.value = ingredient
            },
            routeSubmit() {
                if (editable.value.id) {
                    this.editIngredient()
                } else this.addIngredient()
            },
            async destroyIngredient(ingredientId) {
                try {
                    if (await Pop.confirm('Are you sure you want to destroy this Ingredient? ')) {

                        logger.log('deleting ingredient with following id ? ', ingredientId);
                        await ingredientsService.destroyIngredient(ingredientId);
                    }
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            },
            async destroyInstructions(recipeInstructions) {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy these instructions? ');
                    if (!wantstoDestroy) {
                        return;
                    }
                    const instructionData = editable2.value;
                    instructionData.recipeId = AppState.activeRecipe.id;
                    await recipesService.destroyInstructions(instructionData, instructionData.recipeId);
                }
                catch (error) {
                    logger.error(error);
                    Pop.error(error);
                }
            }
        };
    },
    components: { EditModal }
};
</script>


<style lang="scss" scoped>
img {
    position: center;
    object-fit: cover;
    width: 100%;
    height: 100%px;
    border: solid #7F8C8D 3px;
    border-radius: 5px;
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

.instruction-name {
    border-bottom: solid #7F8C8D 3px;

    width: 100%;
}

.chevron-icon {
    color: #7F8C8D;
}

.absolute {
    position: absolute;
    width: 50%;
    bottom: 2px;

}
</style>