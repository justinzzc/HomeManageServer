using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;

namespace GroceryManagementTool.APIs;

public interface IIngredientsService
{
    /// <summary>
    /// Create one Ingredient
    /// </summary>
    public Task<Ingredient> CreateIngredient(IngredientCreateInput ingredient);

    /// <summary>
    /// Delete one Ingredient
    /// </summary>
    public Task DeleteIngredient(IngredientWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Ingredients
    /// </summary>
    public Task<List<Ingredient>> Ingredients(IngredientFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Ingredient records
    /// </summary>
    public Task<MetadataDto> IngredientsMeta(IngredientFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Ingredient
    /// </summary>
    public Task<Ingredient> Ingredient(IngredientWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Ingredient
    /// </summary>
    public Task UpdateIngredient(
        IngredientWhereUniqueInput uniqueId,
        IngredientUpdateInput updateDto
    );
}
