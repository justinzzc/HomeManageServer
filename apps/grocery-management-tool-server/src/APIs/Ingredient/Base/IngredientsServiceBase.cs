using GroceryManagementTool.APIs;
using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.APIs.Errors;
using GroceryManagementTool.APIs.Extensions;
using GroceryManagementTool.Infrastructure;
using GroceryManagementTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryManagementTool.APIs;

public abstract class IngredientsServiceBase : IIngredientsService
{
    protected readonly GroceryManagementToolDbContext _context;

    public IngredientsServiceBase(GroceryManagementToolDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Ingredient
    /// </summary>
    public async Task<Ingredient> CreateIngredient(IngredientCreateInput createDto)
    {
        var ingredient = new IngredientDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            ingredient.Id = createDto.Id;
        }

        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<IngredientDbModel>(ingredient.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Ingredient
    /// </summary>
    public async Task DeleteIngredient(IngredientWhereUniqueInput uniqueId)
    {
        var ingredient = await _context.Ingredients.FindAsync(uniqueId.Id);
        if (ingredient == null)
        {
            throw new NotFoundException();
        }

        _context.Ingredients.Remove(ingredient);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Ingredients
    /// </summary>
    public async Task<List<Ingredient>> Ingredients(IngredientFindManyArgs findManyArgs)
    {
        var ingredients = await _context
            .Ingredients.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return ingredients.ConvertAll(ingredient => ingredient.ToDto());
    }

    /// <summary>
    /// Meta data about Ingredient records
    /// </summary>
    public async Task<MetadataDto> IngredientsMeta(IngredientFindManyArgs findManyArgs)
    {
        var count = await _context.Ingredients.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Ingredient
    /// </summary>
    public async Task<Ingredient> Ingredient(IngredientWhereUniqueInput uniqueId)
    {
        var ingredients = await this.Ingredients(
            new IngredientFindManyArgs { Where = new IngredientWhereInput { Id = uniqueId.Id } }
        );
        var ingredient = ingredients.FirstOrDefault();
        if (ingredient == null)
        {
            throw new NotFoundException();
        }

        return ingredient;
    }

    /// <summary>
    /// Update one Ingredient
    /// </summary>
    public async Task UpdateIngredient(
        IngredientWhereUniqueInput uniqueId,
        IngredientUpdateInput updateDto
    )
    {
        var ingredient = updateDto.ToModel(uniqueId);

        _context.Entry(ingredient).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Ingredients.Any(e => e.Id == ingredient.Id))
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
