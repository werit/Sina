using System;
using recipies_ms.Db.Models;
using recipies_ms.Db.Models.Enums;

namespace recipies_ms.Web.Dto.IngredientDtos
{
    public class IngredientNutritionReturnDto
    {
        public Guid IngredientNutritionKey { get; }
        public float Amount { get; }
        public SiUnit Unit { get; }
        public float FatContentPercentageAmount { get; }
        public float SaccharidesPercentageContent { get;}
        public float ProteinPercentageContent { get; }
        public float KjEnergyContent { get; }
        public float SaltPercentageContent { get; }
        
        public float FibrePercentageContent { get; } 

        public IngredientNutritionReturnDto(IngredientNutrition ingredientNutrition)
        {
            IngredientNutritionKey = ingredientNutrition.NutritionKey;
            Amount = ingredientNutrition.Amount;
            Unit = ingredientNutrition.Unit;
            FatContentPercentageAmount = ingredientNutrition.FatContentPercentageAmount;
            SaccharidesPercentageContent = ingredientNutrition.SaccharidesPercentageContent;
            ProteinPercentageContent = ingredientNutrition.ProteinPercentageContent;
            KjEnergyContent = ingredientNutrition.KjEnergyContent;
            SaltPercentageContent = ingredientNutrition.SaltPercentageContent;
            FibrePercentageContent = ingredientNutrition.FibrePercentageContent;
        }
        
    }
    public class IngredientReturnDto
    {
        public Guid IngredientKey { get; }
        public  string IngredientName { get; }
        public  string IngredientNote { get; }
        
        public IngredientNutritionReturnDto IngredientNutrition { get; }
        
        
        public IngredientReturnDto(Ingredient ingredient)
        {
            IngredientKey = ingredient.IngredientKey;
            IngredientName = ingredient.IngredientName;
            IngredientNote = ingredient.Note;
            IngredientNutrition = ingredient.ingredientNutritionalValue == null
                ? null
                : new IngredientNutritionReturnDto(ingredient.ingredientNutritionalValue);
        }
    }
}