using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;

namespace GroceryManagementTool.APIs;

public interface IRecipesService
{
    /// <summary>
    /// Create one Recipe
    /// </summary>
    public Task<Recipe> CreateRecipe(RecipeCreateInput recipe);

    /// <summary>
    /// Delete one Recipe
    /// </summary>
    public Task DeleteRecipe(RecipeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Recipes
    /// </summary>
    public Task<List<Recipe>> Recipes(RecipeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Recipe records
    /// </summary>
    public Task<MetadataDto> RecipesMeta(RecipeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Recipe
    /// </summary>
    public Task<Recipe> Recipe(RecipeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Recipe
    /// </summary>
    public Task UpdateRecipe(RecipeWhereUniqueInput uniqueId, RecipeUpdateInput updateDto);
}
