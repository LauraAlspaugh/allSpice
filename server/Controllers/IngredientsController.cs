namespace allSpice.Controllers;
[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;
    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            ingredientData.CreatorId = userInfo.Id;
            Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
            return Ok(ingredient);

        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
    [Authorize]
    [HttpDelete("{ingredientId}")]
    public async Task<ActionResult<string>> DestroyIngredient(int ingredientId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            string message = _ingredientsService.DestroyIngredient(ingredientId, userId);
            return Ok(message);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);

        }
    }
    [Authorize]
    [HttpPut("{ingredientId}")]
    public async Task<ActionResult<Ingredient>> EditIngredient(int ingredientId, [FromBody] Ingredient ingredientData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string userId = userInfo.Id;
            Ingredient ingredient = _ingredientsService.EditIngredient(ingredientId, ingredientData, userId);
            return Ok(ingredient);
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }

}