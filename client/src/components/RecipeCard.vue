<template>
    <div class=" recipe-card mb-3">
        <!-- <section class="row">
            <div class="col-4 "> -->
        <img type="button" data-bs-toggle="modal" data-bs-target="#exampleModal" class="img-fluid" :src="recipeProp.img"
            alt="recipe image">
        <p class="fs-5 text-center">{{ recipeProp.title }}</p>
        <p class="text-center ">{{ recipeProp.category }}</p>
        <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
            Launch demo modal
        </button> -->
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-fullscreen">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Recipe Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    {{ recipeProp.title }}
                    <img :src="recipeProp.img" alt="">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>
            </div>
        </div>
    </div>
    <!-- </section>
    </div> -->
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe.js';
import RecipeDetails from './RecipeDetails.vue';
export default {
    props: { recipeProp: { type: Recipe, required: true } },
    setup() {
        return {
            recipes: computed(() => AppState.recipes)
        };
    },
    components: { RecipeDetails }
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