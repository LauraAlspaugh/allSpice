
import { AppState } from "../AppState.js"
import { Account } from "../models/Account.js"
import { Favorite } from "../models/Favorite.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class RecipesService {
    async getRecipes() {
        const res = await api.get('api/recipes')
        logger.log('getting recipes!', res.data)
        AppState.recipes = res.data.map(pojo => new Recipe(pojo))
        AppState.filteredRecipes = res.data.map(recipe => new Recipe(recipe))

    }
    setActiveRecipe(recipeProp) {

        AppState.activeRecipe = recipeProp
    }
    async createRecipe(recipeData) {
        const res = await api.post('api/recipes', recipeData)
        logger.log('created a recipe!', res.data)
        if (AppState.filter === 'Favorites') {
            AppState.recipes.push(new Recipe(res.data))
            return
        }
        if (AppState.filter === 'Home') {
            AppState.recipes.push(new Recipe(res.data))
            return
        }
        const newRecipe = new Recipe(res.data)
        AppState.recipes.push(newRecipe)
        AppState.filteredRecipes.push(new Recipe(res.data))
        return newRecipe
    }
    async favoriteRecipe(recipeId) {
        logger.log(`creating a favorite with ${recipeId}`)
        const res = await api.post('api/favorites', { recipeId });
        logger.log('this is what we are getting back from the favorite post', res.data)
        AppState.favorites.push(new Favorite(res.data));
        AppState.myFavoriteRecipes.push(new Favorite(res.data))
        // const recipe = AppState.recipes.find(recipe => recipe.id == recipeId);
        // recipe.favoriteCount++
    }

    async unfavoriteRecipe(favoriteId) {
        logger.log('trying to unfavorite this.')
        const res = await api.delete(`api/favorites/${favoriteId}`);
        AppState.myFavoriteRecipes = AppState.myFavoriteRecipes.filter(recipe => recipe.favoriteId != favoriteId);

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
    filterRecipes(filter) {

        if (filter == "Home") {
            AppState.filteredRecipes = AppState.recipes
        } logger.log('filter two ')
        if (filter == "Created") {
            AppState.filteredRecipes = AppState.recipes.filter(recipe => recipe.creatorId == AppState.account.id)
        } logger.log('filter three ')
        if (filter == "Favorites") {
            const filteredRecipes = []
            const recipes = AppState.recipes
            for (let i = 0; i < AppState.favorites.length; i++) {
                let favorite = recipes.find(recipe => recipe.id == AppState.favorites[i].id)
                filteredRecipes.push(favorite)
            }
            AppState.filteredRecipes = filteredRecipes

        } logger.log('filter four')
        AppState.filter = filter

    }
}
export const recipesService = new RecipesService()


