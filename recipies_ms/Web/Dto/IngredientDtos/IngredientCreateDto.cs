using System;
using recipies_ms.Db.Models;

namespace recipies_ms.Web.Dto.IngredientDtos
{
    public class IngredientPutDto
    {
        public Guid IngredientKey { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        
        public float Amount { get; set; }
        
        public string Unit { get; set; }
        
        public float FatContentPercentageAmount { get; set; }
        
        public float SaccharidesPercentageContent { get; set; }
        
        public float ProteinPercentageContent { get; set; } 
        public float KjEnergyContent { get; set; } 
        public float SaltPercentageContent { get; set; } 
        
        public float FibrePercentageContent { get; set; } 
    }
    
    public class IngredientCreateDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        
        public float Amount { get; set; }
        
        public string Unit { get; set; }
        
        public float FatContentPercentageAmount { get; set; }
        
        public float SaccharidesPercentageContent { get; set; }
        
        public float ProteinPercentageContent { get; set; } 
        public float KjEnergyContent { get; set; } 
        public float SaltPercentageContent { get; set; } 
        
        public float FibrePercentageContent { get; set; } 
    }

    public static class IngredientCreateDtoExtension
    {
        public static Ingredient ToIngredient(this IngredientCreateDto ingredientCreateDto)
        {
            var ingredient = new Ingredient
            {
                IngredientKey = Guid.NewGuid(),
                IngredientName = ingredientCreateDto.Name,
                Note = ingredientCreateDto.Note,
                recipeIngredientItem = null,
                ingredientNutritionalValue = null
            };
            ingredient.ingredientNutritionalValue = new IngredientNutrition
            {
                NutritionKey = Guid.NewGuid(),
                Unit = ingredientCreateDto.Unit,
                Amount = ingredientCreateDto.Amount,
                ProteinPercentageContent = ingredientCreateDto.ProteinPercentageContent,
                FatContentPercentageAmount = ingredientCreateDto.FatContentPercentageAmount,
                SaccharidesPercentageContent = ingredientCreateDto.SaccharidesPercentageContent,
                KjEnergyContent = ingredientCreateDto.KjEnergyContent,
                SaltPercentageContent = ingredientCreateDto.SaltPercentageContent,
                FibrePercentageContent = ingredientCreateDto.FibrePercentageContent,
                Ingredient = ingredient
            };
            return ingredient;
        }

        public static Ingredient ToIngredient(this IngredientPutDto ingredientCreateDto)
        {
            var ingredient = new Ingredient
            {
                IngredientKey = ingredientCreateDto.IngredientKey,
                IngredientName = ingredientCreateDto.Name,
                Note = ingredientCreateDto.Note,
                recipeIngredientItem = null,
                ingredientNutritionalValue = null
            };
            ingredient.ingredientNutritionalValue = new IngredientNutrition
            {
                NutritionKey = ingredientCreateDto.IngredientKey,
                Unit = ingredientCreateDto.Unit,
                Amount = ingredientCreateDto.Amount,
                ProteinPercentageContent = ingredientCreateDto.ProteinPercentageContent,
                FatContentPercentageAmount = ingredientCreateDto.FatContentPercentageAmount,
                SaccharidesPercentageContent = ingredientCreateDto.SaccharidesPercentageContent,
                KjEnergyContent = ingredientCreateDto.KjEnergyContent,
                SaltPercentageContent = ingredientCreateDto.SaltPercentageContent,
                FibrePercentageContent = ingredientCreateDto.FibrePercentageContent,
                Ingredient = ingredient
            };
            return ingredient;
        }
    }
}