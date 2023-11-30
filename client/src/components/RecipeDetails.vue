<template>
    <div class="col-12">

    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from '../models/Recipe.js';
export default {

    setup() {

        return {
            recipes: computed(() => AppState.recipes)
        }
    }
};
</script>


<style lang="scss" scoped></style>