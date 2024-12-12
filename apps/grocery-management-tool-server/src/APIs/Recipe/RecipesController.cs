using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[ApiController()]
public class RecipesController : RecipesControllerBase
{
    public RecipesController(IRecipesService service)
        : base(service) { }
}
