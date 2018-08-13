using System.Collections.Generic;
using UnityEngine;

namespace Recipes
{
    class CopperIngot : Recipe
    {
        public CopperIngot() : base(InventoryItem.Type.CopperIngot)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.CopperOre, 1));
        }
    }

    class IronIngot : Recipe
    {
        public IronIngot() : base(InventoryItem.Type.IronIngot)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.IronOre, 1));
        }
    }

    class Fuel : Recipe
    {
        public Fuel() : base(InventoryItem.Type.Fuel)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Coal, 1));
        }
    }

    class RefinedFuel : Recipe
    {
        public RefinedFuel() : base(InventoryItem.Type.RefinedFuel)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Fuel, 1));
        }
    }

    class CopperWire : Recipe
    {
        public CopperWire() : base(InventoryItem.Type.CopperWire)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class GatherStone : Recipe
    {
        public GatherStone() : base(InventoryItem.Type.Stone)
        {
            
        }
    }

    class GatherWood : Recipe
    {
        public GatherWood() : base(InventoryItem.Type.Wood)
        {

        }
    }

    class GatherVine : Recipe
    {
        public GatherVine() : base(InventoryItem.Type.Vine)
        {

        }
    }

    class GatherIronOre : Recipe
    {
        public GatherIronOre() : base(InventoryItem.Type.IronOre)
        {

        }
    }

    class GatherCopperOre : Recipe
    {
        public GatherCopperOre() : base(InventoryItem.Type.CopperOre)
        {

        }
    }

    class GroundStone : Recipe
    {
        public GroundStone() : base(InventoryItem.Type.Stone)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Stone, 1));
        }

        protected override InventoryItem GetResult()
        {
            int r = Random.Range(0, 100);

            //  Need c# 7 to declare expressions in case statements, so I'll just use messy if statements for now
            if (r == 0) // 1%
                return new InventoryItem(InventoryItem.Type.LightStone);
            else if (r <= 5) // 5%
                return new InventoryItem(InventoryItem.Type.Gizmo);
            else if (r <= 25) // 20%
                return new InventoryItem(InventoryItem.Type.IronOre);
            else if (r <= 45) // 20%
                return new InventoryItem(InventoryItem.Type.CopperOre);
            else // 54%
                return new InventoryItem(InventoryItem.Type.Coal);
        }
    }

    class Rope : Recipe {
        public Rope() : base(InventoryItem.Type.Rope)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Vine, 1));
        }
    }

    class Lumber : Recipe
    {
        public Lumber() : base(InventoryItem.Type.Lumber)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Wood, 1));
        }
    }
}
