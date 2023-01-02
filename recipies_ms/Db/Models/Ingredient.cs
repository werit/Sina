using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipies_ms.Db.Models
{
    [Table("ingredient")]
    public class Ingredient
    {
        [Key]
        [Column("ingredient_key")]
        public Guid IngredientKey { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Column("ingredient_name")]
        public string IngredientName { get; set; }

        [Column("ingredient_note")]
        public string Note { get; set; }

        [Column("recipe_ingredient")]
        public ICollection<RecipeIngredientItem> recipeIngredientItem { get; set; }

    }
}