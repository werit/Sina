using System;
using recipies_ms.Db.Models;

namespace recipies_ms.Web.Dto.IngredientDtos
{
    public class IngredientReturnDto
    {
        public Guid IngredientKey { get; }
        public  string IngredientName { get; }
        public  string IngredientNote { get; }
        
        public IngredientReturnDto(Ingredient ingredient)
        {
            IngredientKey = ingredient.IngredientKey;
            IngredientName = ingredient.IngredientName;
            IngredientNote = ingredient.Note;
        }
    }
}