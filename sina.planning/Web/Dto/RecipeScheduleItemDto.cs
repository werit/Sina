using System;
using sina.planning.Db.Models;

namespace sina.planning.Web.Dto
{
    public class RecipeScheduleItemDto
    {
        public Guid RecipeScheduleKey { get; set; }
        public DateTime RecipeScheduleTime { get; set; }
        public string RecipeName { get; set; }
        public float RecipePortions { get; set; }
    }

    public static class RecipeScheduleItemExtension
    {
        
        public static RecipeScheduleItemDto ToRecipeScheduleItemDto(this RecipeScheduleItem  recipeScheduleItem)
        {
            var recipeScheduleItemDto = new RecipeScheduleItemDto
            {
                RecipeScheduleKey = recipeScheduleItem.RecipeScheduleKey,
                RecipeScheduleTime = recipeScheduleItem.RecipeScheduleTime,
                RecipeName = recipeScheduleItem.RecipeName,
                RecipePortions = recipeScheduleItem.RecipePortions,
            };
            return recipeScheduleItemDto;
        }
    }
}