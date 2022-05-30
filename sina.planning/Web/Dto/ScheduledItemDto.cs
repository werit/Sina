using System;
using sina.planning.Db.Models;

namespace sina.planning.Web.Dto
{
    public class ScheduledItemDto
    {
        public ScheduledItemDto(RecipeScheduleItem scheduleItem)
        {
            RecipeScheduleKey = scheduleItem.RecipeScheduleKey; 
            RecipeScheduleTime = scheduleItem.RecipeScheduleTime;
        }
        
        public Guid RecipeScheduleKey { get; set; }

        public DateTime RecipeScheduleTime { get; set; }
    }
}