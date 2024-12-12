using GroceryManagementTool.APIs;
using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.APIs.Errors;
using GroceryManagementTool.APIs.Extensions;
using GroceryManagementTool.Infrastructure;
using GroceryManagementTool.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryManagementTool.APIs;

public abstract class RecipesServiceBase : IRecipesService
{
    protected readonly GroceryManagementToolDbContext _context;

    public RecipesServiceBase(GroceryManagementToolDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Recipe
    /// </summary>
    public async Task<Recipe> CreateRecipe(RecipeCreateInput createDto)
    {
        var recipe = new RecipeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            recipe.Id = createDto.Id;
        }

        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RecipeDbModel>(recipe.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Recipe
    /// </summary>
    public async Task DeleteRecipe(RecipeWhereUniqueInput uniqueId)
    {
        var recipe = await _context.Recipes.FindAsync(uniqueId.Id);
        if (recipe == null)
        {
            throw new NotFoundException();
        }

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Recipes
    /// </summary>
    public async Task<List<Recipe>> Recipes(RecipeFindManyArgs findManyArgs)
    {
        var recipes = await _context
            .Recipes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return recipes.ConvertAll(recipe => recipe.ToDto());
    }

    /// <summary>
    /// Meta data about Recipe records
    /// </summary>
    public async Task<MetadataDto> RecipesMeta(RecipeFindManyArgs findManyArgs)
    {
        var count = await _context.Recipes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Recipe
    /// </summary>
    public async Task<Recipe> Recipe(RecipeWhereUniqueInput uniqueId)
    {
        var recipes = await this.Recipes(
            new RecipeFindManyArgs { Where = new RecipeWhereInput { Id = uniqueId.Id } }
        );
        var recipe = recipes.FirstOrDefault();
        if (recipe == null)
        {
            throw new NotFoundException();
        }

        return recipe;
    }

    /// <summary>
    /// Update one Recipe
    /// </summary>
    public async Task UpdateRecipe(RecipeWhereUniqueInput uniqueId, RecipeUpdateInput updateDto)
    {
        var recipe = updateDto.ToModel(uniqueId);

        _context.Entry(recipe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Recipes.Any(e => e.Id == recipe.Id))
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
