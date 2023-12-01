import { AppState } from "../AppState.js"
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
}
export const recipesService = new RecipesService()