using System;
using System.Collections.Generic;

namespace Recipes
{
    class CopperIngot : Recipe
    {
        public CopperIngot() : base(InventoryItem.Type.CopperIngot)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.CopperOre, 1));
        }
    }
}
