using System;
using System.Collections.Generic;
using System.Linq;
using recipies_ms.Db.Models;

namespace recipies_ms.Web.Dto
{
    public record RecipeIngredientItemCreateDto(float Amount, string Unit, string Ingredient, string Note);

    public record RecipeIngredientItemDto(Guid RecipeIngredientKey, float Amount, string Unit, string Ingredient,
        string Note);

    public class RecipeItemDto
    {
        public Guid RecipeKey { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public ICollection<RecipeIngredientItemDto> Ingredients { get; set; }
    }

    public class RecipeItemCreateDto
    {
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public ICollection<RecipeIngredientItemCreateDto> Ingredients { get; set; }
    }

    public static class RecipeItemDtoExtension
    {
        public static RecipeItem ToRecipeItem(this RecipeItemCreateDto recipeItemCreateDto)
        {
            if (string.IsNullOrEmpty(recipeItemCreateDto?.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItemCreateDto.RecipeName)} cannot be null or empty.");
            }

            if (recipeItemCreateDto.Ingredients!= null && recipeItemCreateDto.Ingredients.Any(x =>
                string.IsNullOrEmpty(x.Ingredient) || string.IsNullOrEmpty(x.Unit)))
            {
                throw new ArgumentException(
                    $"{nameof(RecipeIngredientItemCreateDto.Ingredient)} and {nameof(RecipeIngredientItemCreateDto.Unit)} must be filled with non empty value.");
            }

            var recipeKey = Guid.NewGuid();
            return new RecipeItem
            {
                RecipeKey = recipeKey,
                RecipeName = recipeItemCreateDto.RecipeName,
                RecipeDescription = recipeItemCreateDto.RecipeDescription,
                Ingredient = recipeItemCreateDto.Ingredients?.Select(x => new RecipeIngredientItem
                {
                    Amount = x.Amount, Ingredient = x.Ingredient, Note = x.Note, Unit = x.Unit,
                    IngredientKey = Guid.NewGuid(), RecipeItemId = recipeKey
                }).ToList()
            };
        }

        public static RecipeItemDto ToRecipeItemDto(this RecipeItem recipeItem)
        {
            if (string.IsNullOrEmpty(recipeItem?.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItem.RecipeName)} cannot be null or empty.");
            }

            return new RecipeItemDto
            {
                RecipeKey = recipeItem.RecipeKey,
                RecipeName = recipeItem.RecipeName,
                RecipeDescription = recipeItem.RecipeDescription,
                Ingredients = recipeItem.Ingredient?.Select(x =>
                    new RecipeIngredientItemDto(x.IngredientKey, x.Amount, x.Unit, x.Ingredient, x.Note)).ToList()
            };
        }

        public static RecipeItem ToRecipeItem(this RecipeItemDto recipeItem)
        {
            if (string.IsNullOrEmpty(recipeItem?.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItem.RecipeName)} cannot be null or empty.");
            }

            return new RecipeItem
            {
                RecipeKey = recipeItem.RecipeKey,
                RecipeName = recipeItem.RecipeName,
                RecipeDescription = recipeItem.RecipeDescription,
                Ingredient = recipeItem.Ingredients.Select(x =>
                    new RecipeIngredientItem()
                    {
                        RecipeItemId = recipeItem.RecipeKey, Amount = x.Amount, Unit = x.Unit,
                        IngredientKey = x.RecipeIngredientKey, Ingredient = x.Ingredient, Note = x.Note
                    }).ToList()
            };
        }
    }
}