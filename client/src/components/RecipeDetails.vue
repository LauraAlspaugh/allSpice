<template>
    <div v-if="recipe">
        <p class="fs-3">{{ recipe.title }}</p>
        <p class="fs-5">{{ recipe.instructions }}</p>
        <img :src="recipe.img" alt="">
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe.js';
export default {

    setup() {

        return {
            recipe: computed(() => AppState.activeRecipe)
        }
    }
};
</script>


<style lang="scss" scoped></style>