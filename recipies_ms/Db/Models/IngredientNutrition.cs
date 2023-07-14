using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipies_ms.Db.Models
{
    [Table("ingredient_nutrition")]
    public class IngredientNutrition
    {
        [Key] [Column("nutrition_key")] 
        public Guid NutritionKey { get; set; }
        
        [Column("ingredient_key")] 
        public Ingredient Ingredient { get; set; }

        [Column("amount")]
        public float Amount { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("unit")]
        public string Unit { get; set; }
        
        [Column("fat_content_percentage")]
        public float FatContentPercentageAmount { get; set; }
        
        [Column("saccharides_content_percentage")]
        public float SaccharidesPercentageContent { get; set; }
        
        [Column("protein_content_percentage")]
        public float ProteinPercentageContent { get; set; }        
        
        [Column("kj_energy_content")]
        public float KjEnergyContent { get; set; }      
        
        [Column("salt_content_percentage")]
        public float SaltPercentageContent { get; set; }
        
        [Column("fibre_content_percentage")]
        public float FibrePercentageContent { get; set; }

    }
}