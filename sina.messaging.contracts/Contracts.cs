using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sina.messaging.contracts
{

    [Table("recipe")]
    public class RecipeItemCreated
    {
        [Key]
        [Column("recipe_id")]
        public Guid RecipeId { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
    
    
    [Table("schedule")]
    public class RecipeScheduleCreated
    {
        [Required]
        [Column("recipe_id")]
        public Guid RecipeId { get; set; }
        
        [Key]
        [Column("schedule_id")]
        public Guid ScheduleId { get; set; }
        
        [Required]
        [Column("schedule_date")]
        public DateTime ScheduleDatetime { get; set; }
        
        [Required]
        [Column("recipe_name")]
        public string RecipeName { get; set; }        
        
        [Required]
        [Column("planned_times")]
        public float PlannedPortions { get; set; }
    }
}