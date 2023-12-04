




namespace allSpice.Repositories;
public class IngredientsRepository
{
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
    INSERT INTO ingredients(name, quantity, recipeId, creatorId)
    VALUES (@name, @quantity, @recipeId, @creatorId);
    
    SELECT 
    ing.*, 
    acc.*
    FROM ingredients ing
    JOIN accounts acc ON acc.id = ing.creatorId
    WHERE ing.id = LAST_INSERT_ID();
    ";

        Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, ingredientData).FirstOrDefault();
        return ingredient;
    }

    internal void DestroyIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";
        _db.Execute(sql, new { ingredientId });
    }

    internal Ingredient EditIngredient(Ingredient ingredient)
    {
        string sql = @"
    UPDATE ingredients 
    SET 
    
name = @Name,
quantity = @Quantity
WHERE id = @Id;

SELECT ing.*,
    acc.*
    FROM ingredients ing
    JOIN accounts acc ON ing.creatorId = acc.id
    Where ing.id = @Id;
    ";
        Ingredient newIngredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
    {
        ingredient.Creator = account;
        return ingredient;
    }, ingredient).FirstOrDefault();
        return newIngredient;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
SELECT 
    ing.*, 
    acc.*
    FROM ingredients ing
    JOIN accounts acc ON acc.id = ing.creatorId
    WHERE ing.id = @ingredientId;
";
        Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
    {
        string sql = @"
    SELECT 
    ing.*, 
    acc.*
    FROM ingredients ing
    JOIN accounts acc ON acc.id = ing.creatorId
    WHERE ing.recipeId = @recipeId;
    ";
        List<Ingredient> ingredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
        {
            ingredient.Creator = account;
            return ingredient;
        }, new { recipeId }).ToList();
        return ingredients;
    }
}