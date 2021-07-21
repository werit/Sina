using System;

namespace recipies_ms.Db.Models
{
    public interface IRecipeEntity
    {
        Guid RecipeKey { get; set; }
    }
}