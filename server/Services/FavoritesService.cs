


namespace allSpice.Services;
public class FavoritesService
{
    private readonly FavoritesRepository _favoritesRepository;
    private readonly Auth0Provider _auth0Provider;
    public FavoritesService(FavoritesRepository favoritesRepository, Auth0Provider auth0Provider)
    {
        _favoritesRepository = favoritesRepository;
        _auth0Provider = auth0Provider;
    }

    internal Favorite CreateFavorite(Favorite favoriteData)
    {
        Favorite favorite = _favoritesRepository.CreateFavorite(favoriteData);
        return favorite;
    }
    internal Favorite GetFavoriteById(int favoriteId)
    {
        Favorite favorite = _favoritesRepository.GetFavoriteById(favoriteId);
        if (favorite == null)
        {
            throw new Exception("not a valid id to destroy!");
        }
        return favorite;
    }
    internal string DestroyFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(favoriteId);
        if (favorite.AccountId != userId)
        {
            throw new Exception("not yours to destroy!");
        }
        _favoritesRepository.DestroyFavorite(favoriteId);
        return "It really is gone bro!";
    }

    internal List<RecipeFavorite> GetRecipeFavoritesByAccountId(string userId)
    {
        List<RecipeFavorite> recipeFavorites = _favoritesRepository.GetRecipeFavoritesByAccountId(userId);
        return recipeFavorites;
    }
}