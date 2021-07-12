﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipies_ms.Db.Models
{
    [Table("recipe")]
    public class RecipeItem
    {
        [Key]
        [Column("recipe_key")]
        public Guid RecipeKey { get; set; }

        [Column("recipe_name")]
        public string RecipeName { get; set; }

        [Column("recipe_desc")]
        public string RecipeDescription { get; set; }
    }
}