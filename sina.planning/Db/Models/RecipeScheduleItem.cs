using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sina.planning.Db.Models
{
    [Table("recipe_schedule")]
    public class RecipeScheduleItem: IRecipeScheduleEntity
    {
        [Key]
        [Column("recipe_schedule_key")]
        public Guid RecipeScheduleKey { get; set; }

        [Column("recipe_schedule_time")]
        public DateTime RecipeScheduleTime { get; set; }
        
        [Column("recipe_key")]
        public Guid RecipeKey { get; set; }
        
        [Column("recipe_name")]
        public string RecipeName { get; set; }
        
        [Column("recipe_portions")]
        public float RecipePortions { get; set; }

        [Column("valid_from")]
        public DateTime ValidFrom { get; set; }
        
        [Column("valid_to")]
        public DateTime ValidTo { get; set; }
        
        [Column("current_flag")]
        public char CurrentFlag { get; set; }
        
    }
}