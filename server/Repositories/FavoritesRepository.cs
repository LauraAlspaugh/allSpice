



namespace allSpice.Repositories;
public class FavoritesRepository
{
    private readonly IDbConnection _db;
    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal RecipeFavorite CreateFavorite(Favorite favoriteData)
    {
        string sql = @"
    INSERT INTO 
    favorites(recipeId, accountId)
    VALUES(@recipeId, @accountId);

    SELECT
    fav.*,
    rec.*,
    acc.*
    FROM favorites fav
    JOIN recipes rec ON fav.recipeId = rec.id
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE fav.id = LAST_INSERT_ID();
    ";
        RecipeFavorite recipeFavorites = _db.Query<Favorite, RecipeFavorite, Account, RecipeFavorite>(sql, (favorite, recipeFavorite, account) =>
        {
            recipeFavorite.FavoriteId = favorite.Id;
            recipeFavorite.AccountId = favorite.AccountId;
            recipeFavorite.Creator = account;

            return recipeFavorite;

        }, favoriteData).FirstOrDefault();
        return recipeFavorites;
    }

    internal void DestroyFavorite(int favoriteId)
    {
        string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
        _db.Execute(sql, new { favoriteId });
    }

    internal Favorite GetFavoriteById(int favoriteId)
    {
        string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
        Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
        return favorite;
    }

    internal List<RecipeFavorite> GetRecipeFavoritesByAccountId(string userId)
    {
        string sql = @"
    SELECT 
    fav.*,
    rec.*,
    acc.*
    FROM favorites fav
    JOIN recipes rec ON fav.recipeId = rec.id
    JOIN accounts acc ON acc.id = rec.creatorId
    WHERE fav.accountId = @userId;
    ";
        List<RecipeFavorite> recipeFavorites = _db.Query<Favorite, RecipeFavorite, Account, RecipeFavorite>(sql, (favorite, recipeFavorite, account) =>
        {
            recipeFavorite.FavoriteId = favorite.Id;
            recipeFavorite.AccountId = favorite.AccountId;
            recipeFavorite.Creator = account;

            return recipeFavorite;

        }, new { userId }).ToList();
        return recipeFavorites;
    }
}