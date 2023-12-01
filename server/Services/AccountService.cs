
namespace allSpice.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
    original.Bio = editData.Bio?.Length > 0 ? editData.Bio : original.Bio;
    return _repo.Edit(original);
  }

  // internal Account EditAccount(string accountId, Account accountData, string userId)
  // {
  //   Recipe recipe = GetRecipeById(recipeId);
  //       if (recipe.CreatorId != userId)
  //       {
  //           throw new Exception("not your recipe to destroy!");
  //       }
  //       recipe.Title = recipeData.Title ?? recipe.Title;
  //       recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
  //       recipe.Img = recipeData.Img ?? recipe.Img;
  //       recipe.Category = recipeData.Category ?? recipe.Category;
  //       _recipesRepository.EditRecipe(recipe);
  //       return recipe;
  // }
}
