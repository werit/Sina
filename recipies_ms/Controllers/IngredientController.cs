﻿using System;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using recipies_ms.Db;
using recipies_ms.Web.Dto;
using recipies_ms.Web.Dto.IngredientDtos;

namespace recipies_ms.Controllers
{
    [ApiController]
    [Route("api/v1/system/ingredeint")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> logger;
        private readonly IRecipeIngredient dbContext;

        public IngredientController(ILogger<IngredientController> logger, IRecipeIngredient dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }
        
        
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseMessageDto), StatusCodes.Status400BadRequest)]
        [HttpPost("add")]
        public async Task<ActionResult<IngredientReturnDto>> AddIngredientAsync(IngredientCreateDto ingredientCreateDto,
            CancellationToken cancellationToken)
        {
            logger.LogDebug($"Checking new ingredient with name: '{ingredientCreateDto.Name}'.");

            if (string.IsNullOrEmpty(ingredientCreateDto.Name))
            {
                return BadRequest(new ResponseMessageDto
                    { Message = $"{nameof(ingredientCreateDto.Name)} cannot be empty." });
            }

            logger.LogDebug($"Creating new ingredient with name: '{ingredientCreateDto.Name}'.");

            var recipeItemDto =
                new IngredientReturnDto(
                    await dbContext.AddIngredientAsync(ingredientCreateDto.ToIngredient(), cancellationToken));
            
            logger.LogDebug(
                $"Finished creation of new ingredient with name: '{ingredientCreateDto.Name}' " +
                $"and id: '{recipeItemDto.IngredientKey}'.");
            
            // ReSharper disable once Mvc.ActionNotResolved
            return CreatedAtAction(nameof(GetIngredientByIdAsync), new { id = recipeItemDto.IngredientKey },
                recipeItemDto);
        }
        
        
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IngredientReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IngredientReturnDto), StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IngredientReturnDto>> GetIngredientByIdAsync(Guid id,
            CancellationToken cancellationToken)
        {
            var ingredientItemDto = await dbContext.GetIngredientByKeyAsync(id, cancellationToken);
            if (ingredientItemDto == null)
            {
                return NotFound();
            }

            return Ok(new IngredientReturnDto(ingredientItemDto));
        }
        
    }
}