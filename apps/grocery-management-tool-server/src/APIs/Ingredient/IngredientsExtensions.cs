using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.Infrastructure.Models;

namespace GroceryManagementTool.APIs.Extensions;

public static class IngredientsExtensions
{
    public static Ingredient ToDto(this IngredientDbModel model)
    {
        return new Ingredient
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static IngredientDbModel ToModel(
        this IngredientUpdateInput updateDto,
        IngredientWhereUniqueInput uniqueId
    )
    {
        var ingredient = new IngredientDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            ingredient.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            ingredient.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return ingredient;
    }
}
