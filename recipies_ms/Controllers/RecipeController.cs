using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace recipies_ms.Controllers
{
    [ApiController]
    [Route("api/v1/system/recipe")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;

        public RecipeController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }



    }
}