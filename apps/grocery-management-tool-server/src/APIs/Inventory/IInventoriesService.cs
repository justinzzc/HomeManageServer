using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;

namespace GroceryManagementTool.APIs;

public interface IInventoriesService
{
    /// <summary>
    /// Create one Inventory
    /// </summary>
    public Task<Inventory> CreateInventory(InventoryCreateInput inventory);

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    public Task DeleteInventory(InventoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Inventories
    /// </summary>
    public Task<List<Inventory>> Inventories(InventoryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    public Task<MetadataDto> InventoriesMeta(InventoryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Inventory
    /// </summary>
    public Task<Inventory> Inventory(InventoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Inventory
    /// </summary>
    public Task UpdateInventory(InventoryWhereUniqueInput uniqueId, InventoryUpdateInput updateDto);
}
