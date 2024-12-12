using GroceryManagementTool.APIs;
using GroceryManagementTool.APIs.Common;
using GroceryManagementTool.APIs.Dtos;
using GroceryManagementTool.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace GroceryManagementTool.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class IngredientsControllerBase : ControllerBase
{
    protected readonly IIngredientsService _service;

    public IngredientsControllerBase(IIngredientsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Ingredient
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Ingredient>> CreateIngredient(IngredientCreateInput input)
    {
        var ingredient = await _service.CreateIngredient(input);

        return CreatedAtAction(nameof(Ingredient), new { id = ingredient.Id }, ingredient);
    }

    /// <summary>
    /// Delete one Ingredient
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteIngredient(
        [FromRoute()] IngredientWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteIngredient(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Ingredients
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Ingredient>>> Ingredients(
        [FromQuery()] IngredientFindManyArgs filter
    )
    {
        return Ok(await _service.Ingredients(filter));
    }

    /// <summary>
    /// Meta data about Ingredient records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> IngredientsMeta(
        [FromQuery()] IngredientFindManyArgs filter
    )
    {
        return Ok(await _service.IngredientsMeta(filter));
    }

    /// <summary>
    /// Get one Ingredient
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Ingredient>> Ingredient(
        [FromRoute()] IngredientWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Ingredient(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Ingredient
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateIngredient(
        [FromRoute()] IngredientWhereUniqueInput uniqueId,
        [FromQuery()] IngredientUpdateInput ingredientUpdateDto
    )
    {
        try
        {
            await _service.UpdateIngredient(uniqueId, ingredientUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
