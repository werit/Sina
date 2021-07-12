using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MomentApi.Web.Dto;
using recipies_ms.Db;

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
            if (recipeName == null)
            {
                return BadRequest(new ResponseMessageDto
                    {Message = $"{nameof(recipeName)} cannot be empty."});
            }

            return Created("", await dbContext.AddRecipe(recipeName, recipeDescription, cancellationToken));
        }

    }
}