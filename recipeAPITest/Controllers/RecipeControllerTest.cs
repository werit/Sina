using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using recipeAPITest.TestUtils;
using recipeAPITest.TestUtils.Fixtures;
using recipies_ms.Db;
using recipies_ms.Db.Models;
using recipies_ms.Web.Dto;
using Xunit;

namespace recipeAPITest.Controllers
{
    [Collection(CommonFixtures.DatabaseCollectionName)]
    public class RecipeControllerTest : IClassFixture<CompleteWebApplicationFactory>, IDisposable
    {
        private readonly HttpClient client;
        private readonly CompleteWebApplicationFactory factory;

        public RecipeControllerTest(CompleteWebApplicationFactory factory)
        {
            client = factory.CreateClient();
            this.factory = factory;

            DatabaseUtilsForTesting.ClearDatabase(factory);
        }

        public static IEnumerable<object[]> GetRecipiesToCreate()
        {
            yield return new object[]
            {
                new RecipeItemCreateDto
                {
                    RecipeName = "Pie", RecipeDescription = "Monty", Ingredients =
                        new List<RecipeIngredientItemCreateDto>
                        {
                            new(Guid.NewGuid(), 3.3f, "cup", "Mix it"),
                            new(Guid.NewGuid(), 100f, "ml", "Mix it"),
                            new(Guid.NewGuid(), 2, "lb", "You can add more or less. It is up to you.")
                        }
                }
            };
            yield return new object[]
            {
                new RecipeItemCreateDto
                {
                    RecipeName = "Home wine", RecipeDescription = "Bre it", Ingredients =
                        new List<RecipeIngredientItemCreateDto>
                        {
                            new(Guid.NewGuid(), 2, "cup", null),
                            new(Guid.NewGuid(), 1.5f, "cup", "")
                        }
                }
            };
            yield return new object[]
            {
                new RecipeItemCreateDto {RecipeName = "Home wine", RecipeDescription = "Bre it", Ingredients = null}
            };
            yield return new object[]
            {
                new RecipeItemCreateDto
                {
                    RecipeName = "Home wine", RecipeDescription = "Bre it",
                    Ingredients = new List<RecipeIngredientItemCreateDto>()
                }
            };
        }


        [Trait(TestCategories.TraitName, TestCategories.EndToEndTest)]
        [Theory]
        [MemberData(nameof(GetRecipiesToCreate))]
        public async Task AddRecipeTest(RecipeItemCreateDto recipeForCreation)
        {
            const string uri = "api/v1/system/recipe/add";
            var byteContent = GenerateByteArrayContent(recipeForCreation);

            using var response = await client.PostAsync(uri, byteContent, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            using var scope = factory.Services.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<RecipeContext>();
            var recipeItem = await context.Recipes.Include(ri => ri.Ingredient).FirstAsync();

            Assert.Equal(recipeForCreation.RecipeName, recipeItem.RecipeName);
            Assert.Equal(recipeForCreation.RecipeDescription, recipeItem.RecipeDescription);
            Assert.Equal(recipeForCreation.Ingredients?.Count ?? 0,
                recipeItem.Ingredient.Count);
        }

        private static ByteArrayContent GenerateByteArrayContent(RecipeItemCreateDto data)
        {
            var myContent = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

        public static IEnumerable<object[]> GetRecipeItems()
        {
            yield return new object[]
            {
                "Pie", "Monty", new RecipeIngredientItemDto[]
                {
                    new(Guid.Empty, 600f, "ml", "milk"),
                    new(Guid.Empty, 700.2f, "lb", "You can add more or lees. It si up to you.")
                }
            };
            yield return new object[]
            {
                "Home wine", "Bre it", new RecipeIngredientItemDto[]
                {
                    new(Guid.Empty, 1.5f, "pcs", "no"),
                    new(Guid.Empty, 5f, "cup", null),
                    new(Guid.Empty, 5.2f, "cup", "")
                }
            };
        }


        [Trait(TestCategories.TraitName, TestCategories.EndToEndTest)]
        [Theory]
        [MemberData(nameof(GetRecipeItems))]
        public async Task GetRecipesTest(string recipeName, string recipeDescription,
            RecipeIngredientItemDto[] sourceIngredients)
        {
            var recipeKey = Guid.NewGuid();
            await SaveRecipeItemToDb(recipeName, recipeDescription, sourceIngredients, recipeKey);

            const string uri = "api/v1/system/recipe";


            using var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var resultRecipeItemDto = JsonConvert.DeserializeObject<IEnumerable<RecipeItemDto>>(responseBody).First();

            Assert.Equal(recipeName, resultRecipeItemDto.RecipeName);
            Assert.Equal(recipeDescription, resultRecipeItemDto.RecipeDescription);
            Assert.Equal(recipeKey, resultRecipeItemDto.RecipeKey);
            Assert.Equal(sourceIngredients.Length, resultRecipeItemDto.Ingredients.Count);
        }

        private async Task SaveRecipeItemToDb(string recipeName, string recipeDescription,
            IEnumerable<RecipeIngredientItemDto> sourceIngredients, Guid recipeKey)
        {
            var recipeItem =
                GenerateRecipeItemFromRecipeIngredientItemDto(recipeKey, recipeName, recipeDescription, sourceIngredients);
            using var scope = factory.Services.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<RecipeContext>();

            await context.Recipes.AddAsync(recipeItem);
            await context.SaveChangesAsync();
        }

        private static RecipeItem GenerateRecipeItemFromRecipeIngredientItemDto(Guid recipeKey, string recipeName,
            string recipeDescription, IEnumerable<RecipeIngredientItemDto> sourceIngredients)
        {
            var recipeItem = new RecipeItem
            {
                RecipeName = recipeName, RecipeDescription = recipeDescription, RecipeKey = recipeKey, Ingredient =
                    sourceIngredients.Select(ing => new RecipeIngredientItem
                    {
                        IngredientId = Guid.NewGuid(), RecipeItemId = recipeKey, Amount = ing.Amount, Unit = ing.Unit,
                        IngredientRecipeNote = ing.Note
                    }).ToList()
            };
            return recipeItem;
        }

        public void Dispose()
        {
            DatabaseUtilsForTesting.ClearDatabase(factory);
            client?.Dispose();
        }
    }
}