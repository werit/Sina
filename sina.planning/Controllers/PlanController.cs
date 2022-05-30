using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipies_ms.Web.Dto;
using sina.planning.Db;
using sina.planning.Db.Models;
using sina.planning.Web.Dto;
using System.Linq;

namespace sina.planning.Controllers
{
    [ApiController]
    [Route("api/v1/system/plan")]
    public class PlanController : ControllerBase
    {
        private readonly ILogger<PlanController> logger;
        private readonly IRecipeSchedulingDbContext<RecipeScheduleItem> dbContext;

        public PlanController(ILogger<PlanController> logger, IRecipeSchedulingDbContext<RecipeScheduleItem> dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [HttpPost("add")]
        public async Task<ActionResult<RecipeScheduleItem>> AddRecipeAsync(ScheduleItemCreatedDto recipeItemCreateDto,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(recipeItemCreateDto.RecipeName))
            {
                return BadRequest(new ResponseMessageDto
                    {Message = $"{nameof(recipeItemCreateDto.RecipeName)} cannot be empty."});
            }

            logger.LogInformation("Creating new schedule.");
            var recipeItemDto =
                (await dbContext.AddRecipeScheduleAsync(recipeItemCreateDto.ToRecipeScheduleItem(), cancellationToken));
            logger.LogInformation("New schedule created.");
            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetScheduleByIdAsync), new {id = recipeItemDto.RecipeKey}, recipeItemDto);
        }

        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RecipeScheduleItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RecipeScheduleItem), StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RecipeScheduleItem>> GetScheduleByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            var scheduleItemDto = await dbContext.GetScheduleByKeyAsync(id, cancellationToken);
            if (scheduleItemDto == null)
            {
                return NotFound();
            }

            return Ok(scheduleItemDto);
        }
        
        
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ScheduledItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ScheduledItemDto), StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduledItemDto>>> GetRecipesAsync(CancellationToken cancellationToken)
        {
            var scheduledItemDtos =
                (await dbContext.GetSchedulesAsync(cancellationToken))?.Select(scheduleItem =>
                    new ScheduledItemDto(scheduleItem));
            if (scheduledItemDtos == null)
            {
                return NotFound();
            }

            return Ok(scheduledItemDtos);
        }
    }
}