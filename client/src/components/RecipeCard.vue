<template>
    <div class=" recipe-card mb-3 ">
        <!-- <section class="row">
            <div class="col-4 "> -->
        <img @click="setActiveRecipe(recipeProp)" type="button" data-bs-toggle="modal" data-bs-target="#recipeModal"
            class="img-fluid" :src="recipeProp.img" alt="recipe image">
        <span v-if="recipe" @click.stop="unfavoriteRecipe()" role="button"><i
                class="fs-2 mdi mdi-heart text-center"></i></span>
        <span v-else @click.stop="favoriteRecipe()" role="button"><i
                class="fs-2 mdi mdi-heart-outline text-center"></i></span>
        <i class="mdi mdi-close fs-2" type="button" @click="destroyRecipe()"></i>
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
import { recipesService } from '../services/RecipesService.js';
import Pop from '../utils/Pop.js';
import { ingredientsService } from '../services/IngredientsService.js';
import { logger } from '../utils/Logger.js';
export default {
    props: { recipeProp: { type: Recipe, required: true } },
    setup(props) {
        // const route = useRoute();
        return {
            myFavoriteRecipes: computed(() => AppState.myFavoriteRecipes),
            isFavRecipe: computed(() => AppState.myFavoriteRecipes.find((recipe) => recipe.recipeId == props.recipeProp.id)),
            recipes: computed(() => AppState.recipes),
            ingredients: computed(() => AppState.ingredients),
            setActiveRecipe(recipeProp) {
                recipesService.setActiveRecipe(recipeProp)
                const recipeId = recipeProp.id
                ingredientsService.getIngredientsByRecipeId(recipeId)
            },
            async favoriteRecipe() {
                try {
                    const recipeId = props.recipeProp.id;
                    logger.log('favorite  recipe with recipe id', recipeId)
                    await recipesService.favoriteRecipe(recipeId);
                }
                catch (error) { Pop.error(error) }
            },
            // async createLike(recipePropId) {
            //     try {
            //         await recipesService.createLike(recipePropId)
            //     } catch (error) {
            //         logger.error(error)
            //         Pop.error(error)

            //     }
            // },

            async unfavoriteRecipe() {
                try {
                    const recipeId = props.recipeProp.id;
                    logger.log('we are trying to unfavorite this recipe', recipeId)
                    await recipesService.unfavoriteRecipe(recipeId);
                }
                catch (error) { Pop.error(error) }
            },
            async destroyRecipe() {
                try {
                    const wantstoDestroy = await Pop.confirm('Are you sure you want to destroy this Recipe? ')
                    if (!wantstoDestroy) {
                        return
                    } await recipesService.destroyRecipe(props.recipeProp.id)
                } catch (error) {
                    logger.error(error)
                    Pop.error(error)

                }
            }

        };
    },
    components: { RecipeDetails, RecipeModal }
};
</script>


<style lang="scss" scoped>
.recipe-card {
    width: 22rem;
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
    width: 22rem;
    height: 18rem;
    object-fit: cover;
    position: center;
    ;
}
</style>