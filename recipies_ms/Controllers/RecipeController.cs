using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipies_ms.Db;
using recipies_ms.Web.Dto;

namespace recipies_ms.Controllers
{
    [ApiController]
    [Route("api/v1/system/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> logger;
        private readonly IRecipeDbContext dbContext;

        public RecipeController(ILogger<RecipeController> logger, IRecipeDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [HttpPost("add")]
        public async Task<ActionResult<RecipeItemDto>> AddRecipe(RecipeItemCreateDto recipeItemCreateDto,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(recipeItemCreateDto.RecipeName))
            {
                return BadRequest(new ResponseMessageDto
                    {Message = $"{nameof(recipeItemCreateDto.RecipeName)} cannot be empty."});
            }

            var recipeItemDto = (await dbContext.AddRecipeAsync(recipeItemCreateDto.ToRecipeItem(), cancellationToken))
                .ToRecipeItemDto();
            return CreatedAtAction(nameof(GetRecipeById), new {id = recipeItemDto.RecipeKey},
                recipeItemDto);
        }

        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> PutRecipeItem(Guid id, RecipeItemDto recipeItem,
            CancellationToken cancellationToken)
        {
            if (recipeItem?.RecipeKey == null || id != recipeItem.RecipeKey)
            {
                return BadRequest(
                    $"{nameof(recipeItem)} is either empty or {nameof(id)} does not correlate to {nameof(recipeItem.RecipeKey)}");
            }

            var updateStatus = await dbContext.UpdateRecipeAsync(recipeItem.ToRecipeItem(), cancellationToken);

            switch (updateStatus)
            {
                case RecordUpdateStatus.Updated:
                    return NoContent();
                case RecordUpdateStatus.NotFound:
                    return NotFound();
                default:
                    logger.LogError("This place should not be reachable.");
                    return NoContent();
            }
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RecipeItemDto), StatusCodes.Status200OK)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RecipeItemDto>> GetRecipeById(Guid id,
            CancellationToken cancellationToken)
        {
            var recipeItemDto = (await dbContext.GetRecipeByKeyAsync(id, cancellationToken))?.ToRecipeItemDto();
            if (recipeItemDto == null)
            {
                return NotFound();
            }
            return Ok(recipeItemDto);
        }
    }
}