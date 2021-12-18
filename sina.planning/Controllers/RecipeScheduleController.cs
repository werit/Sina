using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sina.planning.Db;
using sina.planning.Db.Models;
using sina.planning.Web.Dto;

namespace sina.planning.Controllers
{
    [ApiController]
    [Route("api/v1/system/schedule")]
    public class RecipeScheduleController : ControllerBase
    {
        private readonly ILogger<RecipeScheduleController> logger;
        private readonly IRecipeSchedulingDbContext<RecipeScheduleItem> schedulingDbContext;

        public RecipeScheduleController(ILogger<RecipeScheduleController> logger,
            IRecipeSchedulingDbContext<RecipeScheduleItem> schedulingDbContext)
        {
            this.logger = logger;
            this.schedulingDbContext = schedulingDbContext;
        }

        [HttpPost("add")]
        public async Task<ActionResult<RecipeScheduleItemDto>> AddRecipeSchedule(
            RecipeScheduleCreateDto scheduleCreateDto, CancellationToken cancellationToken)
        {
            var scheduleItem = new RecipeScheduleItem
            {
                RecipeKey = scheduleCreateDto.RecipeKey,
                RecipePortions = scheduleCreateDto.RecipePortions,
                RecipeScheduleTime = scheduleCreateDto.RecipeScheduleTime
            };
            var recipeScheduleItemDto =
                (await schedulingDbContext.AddRecipeScheduleAsync(scheduleItem, cancellationToken))
                .ToRecipeScheduleItemDto();
            return Created("",recipeScheduleItemDto);
        }
    }
}