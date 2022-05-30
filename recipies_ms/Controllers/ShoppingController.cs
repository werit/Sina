using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipies_ms.Db;
using recipies_ms.Db.Models;
using recipies_ms.Web.Dto;
using sina.messaging.contracts;

namespace recipies_ms.Controllers
{
    [ApiController]
    [Route("api/v1/system/shopping")]
    public class ShoppingController : ControllerBase
    {
        private readonly ILogger<ShoppingController> logger;
        private readonly IRecipeDbContext<RecipeItem> dbContext;

        public ShoppingController(ILogger<ShoppingController> logger, IRecipeDbContext<RecipeItem> dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }


        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(RecipeScheduleCreated), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RecipeScheduleCreated), StatusCodes.Status404NotFound)]
        [HttpGet("{from:datetime},{to:datetime}")]
        public async Task<ActionResult<IEnumerable<RecipeScheduleCreated>>> GetSchedulesInTimeFrameAsync(DateTime from,
            DateTime to,
            CancellationToken cancellationToken)
        {
            logger.Log(LogLevel.Debug,
                $"Request for {nameof(GetSchedulesInTimeFrameAsync)} with parameters {nameof(from)}: {from} and {nameof(to)}: {to}.");
            var scheduleCreateds = (await dbContext.GetSchedulesBetweenTimeAsync(from, to, cancellationToken));
            if (scheduleCreateds == null)
            {
                return NotFound();
            }

            return Ok(scheduleCreateds);
        }
    }
}