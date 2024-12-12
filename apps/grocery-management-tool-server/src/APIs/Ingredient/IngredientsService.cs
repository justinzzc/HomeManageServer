using GroceryManagementTool.Infrastructure;

namespace GroceryManagementTool.APIs;

public class IngredientsService : IngredientsServiceBase
{
    public IngredientsService(GroceryManagementToolDbContext context)
        : base(context) { }
}
