using System;

namespace sina.messaging.contracts
{

    public class RecipeItemCreated
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
    }
}