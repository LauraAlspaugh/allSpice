


namespace allSpice.Services;
public class IngredientsService
{
    private readonly IngredientsRepository _ingredientsRepository;
    public IngredientsService(IngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
        return ingredient;
    }
    internal Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
        if (ingredient == null)
        {
            throw new Exception("not a valid ingredient id!");
        }
        return ingredient;
    }
    internal string DestroyIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        if (ingredient.CreatorId != userId)
        {
            throw new Exception("this is not your ingredient to destroy! ");
        }
        _ingredientsRepository.DestroyIngredient(ingredientId);
        return "this ingredient has been deleted!";
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsByRecipeId(recipeId);
        return ingredients;
    }
}
