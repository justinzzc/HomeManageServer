using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.Infrastructure.Models;

namespace GroceryManagementTool.APIs.Extensions;

public static class InventoriesExtensions
{
    public static Inventory ToDto(this InventoryDbModel model)
    {
        return new Inventory
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InventoryDbModel ToModel(
        this InventoryUpdateInput updateDto,
        InventoryWhereUniqueInput uniqueId
    )
    {
        var inventory = new InventoryDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            inventory.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            inventory.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return inventory;
    }
}
