using System;
using System.Collections.Generic;
using System.Linq;
using recipies_ms.Db.Models;

namespace recipies_ms.Web.Dto
{
    public record RecipeIngredientItemCreateDto(Guid ingredientId, float Amount, string Unit,
        string Note);

    public record RecipeIngredientItemDto(Guid RecipeIngredientKey, float Amount, string Unit, string Note);

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

            if (recipeItemCreateDto.Ingredients != null && recipeItemCreateDto.Ingredients.Any(x =>
                    string.IsNullOrEmpty(x.Unit)))
            {
                throw new ArgumentException(
                    $"{nameof(RecipeIngredientItemCreateDto.Unit)} must be filled with non empty value.");
            }

            var recipeKey = Guid.NewGuid();
            return new RecipeItem
            {
                RecipeKey = recipeKey,
                RecipeName = recipeItemCreateDto.RecipeName,
                RecipeDescription = recipeItemCreateDto.RecipeDescription,
                Ingredient = recipeItemCreateDto.Ingredients?.Select(x => new RecipeIngredientItem
                {
                    Amount = x.Amount,
                    IngredientId = x.ingredientId,
                    Unit = x.Unit,
                    IngredientRecipeNote = x.Note,
                    RecipeItemId = recipeKey
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
                    new RecipeIngredientItemDto(x.IngredientId, x.Amount, x.Unit,
                        x.IngredientRecipeNote)).ToList()
            };
        }

        public static RecipeItemDto ToRecipeItemDto(this IRecipeEntity recipeItem)
        {
            throw new NotImplementedException();
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
                    new RecipeIngredientItem
                    {
                        RecipeItemId = recipeItem.RecipeKey, Amount = x.Amount, Unit = x.Unit,
                        IngredientId = x.RecipeIngredientKey,
                        IngredientRecipeNote = x.Note
                    }).ToList()
            };
        }
    }
}