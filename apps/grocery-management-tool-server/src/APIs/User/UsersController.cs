using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
