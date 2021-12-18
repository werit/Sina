using System;

namespace sina.planning.Web.Dto
{
    // ReSharper disable once InconsistentNaming
    public class RecipeScheduleCreateDto
    {
        public DateTime RecipeScheduleTime { get; set; }
        public Guid RecipeKey { get; set; }
        public float RecipePortions { get; set; }
    }
}