using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipies_ms.Db.Models
{
    [Table("recipe_ingredient_rel")]
    public class RecipeIngredientItem
    {
        [Key]
        [Column("ingredient_key")]

        public Guid IngredientId { get; set; }

        [Column("recipe_key")]
        public Guid RecipeItemId { get; set; }

        [Column("amount")]
        public float Amount { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("unit")]
        public string Unit { get; set; }

        [Column("ingredient_recipe_note")]
        public string IngredientRecipeNote { get; set; }


    }
}