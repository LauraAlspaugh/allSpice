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
}