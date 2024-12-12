using GroceryManagementTool.Infrastructure;

namespace GroceryManagementTool.APIs;

public class RecipesService : RecipesServiceBase
{
    public RecipesService(GroceryManagementToolDbContext context)
        : base(context) { }
}
