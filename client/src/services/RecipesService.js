import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log('getting recipes!', res.data)
        AppState.recipes = res.data.map(pojo => new Recipe(pojo))

    }
    setActiveRecipe(recipeProp) {

        AppState.activeRecipe = recipeProp
    }
    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('created a recipe!', res.data)
        const newRecipe = new Recipe(res.data)
        AppState.recipes.push(newRecipe)
        return newRecipe
    }
    async favoriteRecipe(recipeId) {
        logger.log(`creating a favorite with ${recipeId}`)
        const res = await api.post('api/favorites', { recipeId });
        logger.log('this is what we are getting back from the favorite post', res.data)
        AppState.favorites.push(new Favorite(res.data));
        // const recipe = AppState.recipes.find(recipe => recipe.id == recipeId);
        // recipe.favoriteCount++
    }

    async unfavoriteRecipe(favoriteId) {
        logger.log('trying to unfavorite this.')
        const res = await api.delete(`api/favorites/${favoriteId}}`);
        AppState.favorites = AppState.myFavoriteRecipes.filter(fav => fav.recipeId != recipeId);
        recipe.favoriteCount--
        return res.data


    }
    async createLike(recipeId) {

        const res = await api.post(`api/favorites`)
        logger.log(res.data)
        const recipeIndex = AppState.recipes.findIndex(recipes => recipes.id == recipeId)
        if (recipeIndex == -1) { return }
        const newRecipe = new Recipe(res.data)
        AppState.recipes.splice(recipeIndex, 1, newRecipe)

    }

    async addInstructions(recipeData, recipeId) {
        const res = await api.put(`api/recipes/${recipeId}`, recipeData)
        logger.log('adding instructions', res.data)
        const newRecipe = new Recipe(res.data)
        AppState.activeRecipe = newRecipe
        const recipeIndex = AppState.recipes.findIndex(recipe => recipe.id == newRecipe.id)
        if (recipeIndex == -1) {
            throw new Error('no recipe with this id')
        } AppState.recipes.splice(recipeIndex, 1, newRecipe)
    }
    async destroyRecipe(recipeId) {
        const res = await api.delete(`api/recipes/${recipeId}`)
        logger.log('destroying recipe', res.data)
        const recipeIndex = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        if (recipeIndex == -1) { throw new Error('No recipe found with this id') }
        AppState.recipes.splice(recipeIndex, 1)
    }
    async destroyInstructions(recipeData, recipeId) {
        const res = await api.put(`api/recipes/${recipeId}`, recipeData)
        logger.log('destroying instructions', res.data)
        const recipeIndex = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        if (recipeIndex == -1) { throw new Error('No ingredient found with this id') }
        AppState.recipes.splice(recipeIndex, 1)
    }

}
export const recipesService = new RecipesService()

// (recipeIndex, 0, newRecipe)
