using System;
using sina.planning.Db.Models;

namespace sina.planning.Web.Dto
{
    public class ScheduleItemCreatedDto
    {
        public DateTime RecipeScheduleTime { get; set; }
        
        public Guid RecipeKey { get; set; }
        
        public string RecipeName { get; set; }
        
        public float RecipePortions { get; set; }
    }

    public static class ScheduleItemCreatedDtoExtension
    {
        public static RecipeScheduleItem ToRecipeScheduleItem(this ScheduleItemCreatedDto scheduleItemCreatedDto)
        {
            return new RecipeScheduleItem
            {
                RecipeScheduleKey = Guid.NewGuid(),
                RecipeScheduleTime = scheduleItemCreatedDto.RecipeScheduleTime,
                RecipeKey= scheduleItemCreatedDto.RecipeKey,
                RecipeName = scheduleItemCreatedDto.RecipeName,
                RecipePortions = scheduleItemCreatedDto.RecipePortions
            };
        }
    }
}