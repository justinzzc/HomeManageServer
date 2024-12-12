using GroceryManagementTool.APIs;
using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RecipesControllerBase : ControllerBase
{
    protected readonly IRecipesService _service;

    public RecipesControllerBase(IRecipesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Recipe
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Recipe>> CreateRecipe(RecipeCreateInput input)
    {
        var recipe = await _service.CreateRecipe(input);

        return CreatedAtAction(nameof(Recipe), new { id = recipe.Id }, recipe);
    }

    /// <summary>
    /// Delete one Recipe
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRecipe([FromRoute()] RecipeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRecipe(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Recipes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Recipe>>> Recipes([FromQuery()] RecipeFindManyArgs filter)
    {
        return Ok(await _service.Recipes(filter));
    }

    /// <summary>
    /// Meta data about Recipe records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RecipesMeta(
        [FromQuery()] RecipeFindManyArgs filter
    )
    {
        return Ok(await _service.RecipesMeta(filter));
    }

    /// <summary>
    /// Get one Recipe
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Recipe>> Recipe([FromRoute()] RecipeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Recipe(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Recipe
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRecipe(
        [FromRoute()] RecipeWhereUniqueInput uniqueId,
        [FromQuery()] RecipeUpdateInput recipeUpdateDto
    )
    {
        try
        {
            await _service.UpdateRecipe(uniqueId, recipeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
