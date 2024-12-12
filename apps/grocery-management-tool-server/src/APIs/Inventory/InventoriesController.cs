using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[ApiController()]
public class InventoriesController : InventoriesControllerBase
{
    public InventoriesController(IInventoriesService service)
        : base(service) { }
}
