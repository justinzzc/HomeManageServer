using GroceryManagementTool.Infrastructure;

namespace GroceryManagementTool.APIs;

public class InventoriesService : InventoriesServiceBase
{
    public InventoriesService(GroceryManagementToolDbContext context)
        : base(context) { }
}
