using GroceryManagementTool.Infrastructure;

namespace GroceryManagementTool.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(GroceryManagementToolDbContext context)
        : base(context) { }
}
