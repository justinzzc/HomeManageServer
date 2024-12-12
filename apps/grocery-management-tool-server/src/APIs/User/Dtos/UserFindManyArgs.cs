using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
