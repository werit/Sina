using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MomentApi.Web.Dto;
using recipies_ms.Db;
using recipies_ms.Db.Models;

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
        public async Task<IActionResult> AddRecipe([FromQuery] string recipeName, [FromQuery] string recipeDescription,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(recipeName))
            {
                return BadRequest(new ResponseMessageDto
                    {Message = $"{nameof(recipeName)} cannot be empty."});
            }

            return Created("", await dbContext.AddRecipeAsync(recipeName, recipeDescription, cancellationToken));
        }

        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> PutRecipeItem(Guid id, RecipeItem recipeItem,
            CancellationToken cancellationToken)
        {
            if (recipeItem?.RecipeKey == null || id != recipeItem.RecipeKey)
            {
                return BadRequest(
                    $"{nameof(recipeItem)} is either empty or {nameof(id)} does not correlate to {nameof(recipeItem.RecipeKey)}");
            }

            var updateStatus = await dbContext.UpdateRecipeAsync(recipeItem, cancellationToken);

            switch (updateStatus)
            {
                case UpdateStatus.Updated:
                    return NoContent();
                case UpdateStatus.NotFound:
                    return NotFound();
                default:
                    logger.LogError("This place should not be reachable.");
                    return NoContent();
            }
        }
    }
}