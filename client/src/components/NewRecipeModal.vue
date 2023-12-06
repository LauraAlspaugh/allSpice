<template>
    <!-- <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
        Launch demo modal
    </button> -->

    <!-- Modal -->
    <div class="modal fade" id="newRecipeModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center text-light" id="exampleModalLabel">Create a Recipe</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="createRecipe()">
                        <div class="mb-3">
                            <label for="title" class="form-label">Title</label>
                            <input v-model="editable.title" type="text" class="form-control" id="title"
                                aria-describedby="emailHelp" maxlength="255" required>

                        </div>
                        <div class="mb-3">
                            <label for="imgUrl" class="form-label">Image Url</label>
                            <input v-model="editable.img" type="text" class="form-control" id="imgUrl"
                                aria-describedby="emailHelp" maxlength="1000" required>

                        </div>
                        <div class="mb-3">
                            <label for="category" class="form-label">Category</label>
                            <input v-model="editable.category" type="text" class="form-control" id="category"
                                aria-describedby="emailHelp">

                        </div>
                        <div class="mb-3">
                            <label for="instructions" class="form-label">Instructions</label>
                            <textarea v-model="editable.instructions" type="text" class="form-control" id="instructions"
                                rows="5" maxlength="255" required></textarea>
                        </div>

                        <button type="submit" class="btn btn-outline-dark">Submit</button>
                    </form>
                </div>
                <div class="modal-footer">
                    <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button> -->
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { recipesService } from '../services/RecipesService.js';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})


        return {
            editable,
            recipes: computed(() => AppState.recipes),
            async createRecipe() {
                try {
                    const recipeData = editable.value
                    const recipe = await recipesService.createRecipe(recipeData)
                    Pop.success('Recipe created!')
                    Modal.getOrCreateInstance('#newRecipeModal').hide()
                    editable.value = {}
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
.modal-footer {
    background-color: #7F8C8D;
}

.modal-header {
    background-color: #7F8C8D;
}
</style>