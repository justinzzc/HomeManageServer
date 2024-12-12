using GroceryManagementTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryManagementTool.Infrastructure;

public class GroceryManagementToolDbContext : DbContext
{
    public GroceryManagementToolDbContext(DbContextOptions<GroceryManagementToolDbContext> options)
        : base(options) { }

    public DbSet<InventoryDbModel> Inventories { get; set; }

    public DbSet<IngredientDbModel> Ingredients { get; set; }

    public DbSet<RecipeDbModel> Recipes { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
