<template>
    <div class=" recipe-card mb-3 ">
        <!-- <section class="row">
            <div class="col-4 "> -->
        <img @click="setActiveRecipe(recipeProp)" type="button" data-bs-toggle="modal" data-bs-target="#recipeModal"
            class="img-fluid" :src="recipeProp.img" alt="recipe image" :title="recipeProp.title">
        <span v-if="isFavRecipe" @click.stop="unfavoriteRecipe(isFavRecipe.favoriteId)" role="button"><i
                class="fs-2 mdi mdi-heart text-center" title="unfavorite this recipe"></i></span>
        <span v-else @click.stop="favoriteRecipe(recipeProp.id)" role="button"><i
                class="fs-2 mdi mdi-heart-outline text-center" title="favorite this recipe"></i></span>
        <i class="mdi mdi-close fs-2 text-center" title="delete this recipe" type="button" @click="destroyRecipe()"></i>
        <p class="fs-3 text-center recipe-name" :title="recipeProp.title">{{ recipeProp.title }}</p>
        <p class="text-center " :title="recipeProp.category">{{ recipeProp.category }}</p>
        <div class="text-center">
            <span class="justify-content-center d-flex">
                <i v-for="star in [...Array(stars)].map((n, i) => i + 1)" @click="addStar(star)"
                    class="mdi mdi-star fs-4 star-icon"></i>
                <i v-for="star in [...Array(5 - stars)].map((n, i) => i + 1 + stars)" @click="addStar(star)"
                    class="mdi mdi-star-outline fs-4 star-icon"></i>


            </span>
        </div>
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
            isFavRecipe: computed(() => AppState.myFavoriteRecipes.find((recipe) => recipe.id == props.recipeProp.id || recipe.recipeId == props.recipeProp.id)),
            recipes: computed(() => AppState.recipes),
            ingredients: computed(() => AppState.ingredients),
            stars: computed(() => {
                return Math.floor(Math.random() * 5)
            }),
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



            addStar(star) {
                logger.log('star', star)
            },

            async unfavoriteRecipe(favoriteId) {
                try {

                    const recipeId = props.recipeProp.id;
                    logger.log('we are trying to unfavorite this recipe', recipeId)
                    await recipesService.unfavoriteRecipe(favoriteId);
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
    border: 2px solid #7F8C8D;
    padding: 5px;
    border-radius: 7px;
    /* From https://css.glass */
    background: rgba(187, 174, 174, 0.62);
    border-radius: 16px;
    box-shadow: 0 4px 30px rgba(0, 0, 0, 0.1);
    backdrop-filter: blur(3.6px);
    -webkit-backdrop-filter: blur(3.6px);
    border: 1px solid #7F8C8D(30, 23, 23, 0.78);
}

img {
    border-radius: 7px;
    width: 22rem;
    height: 18rem;
    object-fit: cover;
    position: center;
    ;
}

.recipe-name {
    font-family: 'Pinyon Script', cursive;
    color: #7F8C8D;
    text-shadow: 2px 1px 2px rgb(87, 82, 82);
}

.star-icon {
    color: #7F8C8D;
}

.star {
    font-size: 2vh;
}

.one {
    color: rgb(255, 0, 0);
}

.two {
    color: rgb(255, 106, 0);
}

.three {
    color: rgb(251, 255, 120);
}

.four {
    color: rgb(255, 255, 0);
}

.five {
    color: rgb(24, 159, 14);
}
</style>