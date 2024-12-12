using GroceryManagementTool.APIs;

namespace GroceryManagementTool;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IIngredientsService, IngredientsService>();
        services.AddScoped<IInventoriesService, InventoriesService>();
        services.AddScoped<IRecipesService, RecipesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
