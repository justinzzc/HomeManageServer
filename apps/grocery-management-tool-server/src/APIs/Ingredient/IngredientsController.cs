using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[ApiController()]
public class IngredientsController : IngredientsControllerBase
{
    public IngredientsController(IIngredientsService service)
        : base(service) { }
}
