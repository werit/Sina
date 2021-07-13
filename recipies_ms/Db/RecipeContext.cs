﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipies_ms.Db.Models;

namespace recipies_ms.Db
{
    public interface IRecipeDbContext
    {
        Task<RecipeItem> AddRecipeAsync(RecipeItem recipeItem, CancellationToken cancellationToken);
        Task<RecordUpdateStatus> UpdateRecipeAsync(RecipeItem recipeItem, CancellationToken cancellationToken);
        Task<RecipeItem> GetRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken);
    }

    public class RecipeContext : DbContext, IRecipeDbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base(options)
        {
        }

        public DbSet<RecipeItem> Recipes { get; set; }


        public async Task<RecipeItem> AddRecipeAsync(RecipeItem recipeItem,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Recipes.Add(recipeItem);
            await SaveChangesAsync(cancellationToken);
            return recipeItem;
        }

        public async Task<RecordUpdateStatus> UpdateRecipeAsync(RecipeItem recipeItem,
            CancellationToken cancellationToken)
        {
            if (recipeItem?.RecipeKey == null || string.IsNullOrEmpty(recipeItem.RecipeName))
            {
                throw new ArgumentNullException($"{nameof(recipeItem)} cannot be null and neiter its id or name.");
            }

            Entry(recipeItem).State = EntityState.Modified;
            try
            {
                await SaveChangesAsync(cancellationToken);
                return RecordUpdateStatus.Updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeItemExists(recipeItem.RecipeKey))
                {
                    return RecordUpdateStatus.NotFound;
                }

                throw;
            }
        }

        public async Task<RecipeItem> GetRecipeByKeyAsync(Guid recipeKey, CancellationToken cancellationToken)
        {
            return await Recipes.Include(rec => rec.Ingredient)
                .SingleOrDefaultAsync(rec => rec.RecipeKey == recipeKey, cancellationToken: cancellationToken);
        }

        private bool RecipeItemExists(Guid id)
        {
            return Recipes.Any(e => e.RecipeKey == id);
        }
    }
}