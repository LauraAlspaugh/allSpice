import { AppState } from "../AppState.js"
import { Ingredient } from "../models/Ingredient.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService {
    async getIngredientsByRecipeId(recipeId) {
        const res = await api.get(`api/recipes/${recipeId}/ingredients`)
        logger.log('getting ingredients!', res.data)
        AppState.ingredients = res.data.map(pojo => new Ingredient(pojo))

    }
    async addIngredient(ingredientData) {
        const res = await api.post('api/ingredients', ingredientData)
        logger.log('adding ingredient!', res.data)
        const newIngredient = new Ingredient(res.data)
        AppState.ingredients.push(newIngredient)
        return newIngredient
    }
}
export const ingredientsService = new IngredientsService()