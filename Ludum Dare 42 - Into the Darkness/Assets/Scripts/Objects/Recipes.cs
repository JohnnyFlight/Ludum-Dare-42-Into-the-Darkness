using System;
using System.Collections.Generic;

namespace Recipes
{
    class CopperIngot : Recipe
    {
        public CopperIngot() : base(InventoryItem.Type.CopperOre)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.CopperOre, 1));
        }
    }

    class IronIngot : Recipe
    {
        public IronIngot() : base(InventoryItem.Type.IronOre)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.IronOre, 1));
        }
    }
}
