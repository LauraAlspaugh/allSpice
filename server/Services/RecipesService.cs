


namespace allSpice.Services;

public class RecipesService
{
    private readonly RecipesRepository _recipesRepository;
    public RecipesService(RecipesRepository recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        Recipe recipe = _recipesRepository.CreateRecipe(recipeData);
        return recipe;
    }


    internal string DestroyRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId)
        {
            throw new Exception("not your recipe to destroy!");
        }
        _recipesRepository.DestroyRecipe(recipeId);
        return "this had been destroyed!";
    }

    internal Recipe EditRecipe(int recipeId, Recipe recipeData, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);
        if (recipe.CreatorId != userId)
        {
            throw new Exception("not your recipe to destroy!");
        }
        recipe.Title = recipeData.Title ?? recipe.Title;
        recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
        recipe.Img = recipeData.Img ?? recipe.Img;
        recipe.Category = recipeData.Category ?? recipe.Category;
        _recipesRepository.EditRecipe(recipe);
        return recipe;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
        if (recipe == null)
        {
            throw new Exception("invalid id");
        }
        return recipe;
    }

    internal List<Recipe> GetRecipes()
    {
        List<Recipe> recipes = _recipesRepository.GetRecipes();
        return recipes;
    }
}