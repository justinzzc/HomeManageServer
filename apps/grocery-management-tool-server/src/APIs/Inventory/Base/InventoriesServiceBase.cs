using GroceryManagementTool.APIs;
using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.APIs.Errors;
using GroceryManagementTool.APIs.Extensions;
using GroceryManagementTool.Infrastructure;
using GroceryManagementTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryManagementTool.APIs;

public abstract class InventoriesServiceBase : IInventoriesService
{
    protected readonly GroceryManagementToolDbContext _context;

    public InventoriesServiceBase(GroceryManagementToolDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Inventory
    /// </summary>
    public async Task<Inventory> CreateInventory(InventoryCreateInput createDto)
    {
        var inventory = new InventoryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            inventory.Id = createDto.Id;
        }

        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InventoryDbModel>(inventory.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Inventory
    /// </summary>
    public async Task DeleteInventory(InventoryWhereUniqueInput uniqueId)
    {
        var inventory = await _context.Inventories.FindAsync(uniqueId.Id);
        if (inventory == null)
        {
            throw new NotFoundException();
        }

        _context.Inventories.Remove(inventory);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Inventories
    /// </summary>
    public async Task<List<Inventory>> Inventories(InventoryFindManyArgs findManyArgs)
    {
        var inventories = await _context
            .Inventories.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return inventories.ConvertAll(inventory => inventory.ToDto());
    }

    /// <summary>
    /// Meta data about Inventory records
    /// </summary>
    public async Task<MetadataDto> InventoriesMeta(InventoryFindManyArgs findManyArgs)
    {
        var count = await _context.Inventories.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Inventory
    /// </summary>
    public async Task<Inventory> Inventory(InventoryWhereUniqueInput uniqueId)
    {
        var inventories = await this.Inventories(
            new InventoryFindManyArgs { Where = new InventoryWhereInput { Id = uniqueId.Id } }
        );
        var inventory = inventories.FirstOrDefault();
        if (inventory == null)
        {
            throw new NotFoundException();
        }

        return inventory;
    }

    /// <summary>
    /// Update one Inventory
    /// </summary>
    public async Task UpdateInventory(
        InventoryWhereUniqueInput uniqueId,
        InventoryUpdateInput updateDto
    )
    {
        var inventory = updateDto.ToModel(uniqueId);

        _context.Entry(inventory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Inventories.Any(e => e.Id == inventory.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
