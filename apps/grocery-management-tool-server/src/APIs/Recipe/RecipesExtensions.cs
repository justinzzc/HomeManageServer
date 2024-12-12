using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.Infrastructure.Models;

namespace GroceryManagementTool.APIs.Extensions;

public static class RecipesExtensions
{
    public static Recipe ToDto(this RecipeDbModel model)
    {
        return new Recipe
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RecipeDbModel ToModel(
        this RecipeUpdateInput updateDto,
        RecipeWhereUniqueInput uniqueId
    )
    {
        var recipe = new RecipeDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            recipe.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            recipe.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return recipe;
    }
}
