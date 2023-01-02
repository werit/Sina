using System;
using recipies_ms.Db.Models;

namespace recipies_ms.Web.Dto.IngredientDtos
{
    public class IngredientCreateDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        
    }

    public static class IngredientCreateDtoExtension
    {
        public static Ingredient ToIngredient(this IngredientCreateDto ingredientCreateDto)
        {
            return new Ingredient
            {
                IngredientKey = Guid.NewGuid(),
                IngredientName = ingredientCreateDto.Name,
                Note = ingredientCreateDto.Note,
                recipeIngredientItem = null
            };
        }
    }
}