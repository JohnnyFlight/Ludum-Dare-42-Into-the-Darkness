using System.Collections.Generic;
using UnityEngine;

namespace Recipes
{
    //  Production / Refinement recipes

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

    class Lumber : Recipe {
        public Lumber() : base(InventoryItem.Type.Lumber)
        {
            Requirements.Add(new Requirement(InventoryItem.Type.Wood, 1));
        }
    }

    //  Machine Recipes

    class PoweredDrill : Recipe
    {
        public PoweredDrill() : base(InventoryItem.Type.PoweredDrill)
        {
            Name = "Powered Drill";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 1));
        }
    }

    class PoweredQuarry : Recipe
    {
        public PoweredQuarry() : base(InventoryItem.Type.PoweredQuarry)
        {
            Name = "Powered Quarry";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class PoweredSaw : Recipe
    {
        public PoweredSaw() : base(InventoryItem.Type.PoweredChopper)
        {
            Name = "Powered Saw";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class CrankDrill : Recipe
    {
        public CrankDrill() : base(InventoryItem.Type.CrankDrill)
        {
            Name = "Crank Drill";

            Requirements.Add(new Requirement(InventoryItem.Type.Wood, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.IronOre, 1));
        }
    }

    class CrankQuarry : Recipe
    {
        public CrankQuarry() : base(InventoryItem.Type.CrankQuarry)
        {
            Name = "Crank Quarry";

            Requirements.Add(new Requirement(InventoryItem.Type.Wood, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.IronOre, 1));
        }
    }

    class CrankSaw : Recipe
    {
        public CrankSaw() : base(InventoryItem.Type.CrankSaw)
        {
            Name = "Crank Saw";

            Requirements.Add(new Requirement(InventoryItem.Type.Wood, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronOre, 2));
        }
    }

    class Loom : Recipe
    {
        public Loom() : base(InventoryItem.Type.Loom)
        {
            Name = "Loom";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class Mill : Recipe
    {
        public Mill() : base(InventoryItem.Type.Mill)
        {
            Name = "Mill";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class Grinder : Recipe
    {
        public Grinder() : base(InventoryItem.Type.Grinder)
        {
            Name = "Grinder";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    class Smelter : Recipe
    {
        public Smelter() : base(InventoryItem.Type.Smelter)
        {
            Name = "Smelter";

            Requirements.Add(new Requirement(InventoryItem.Type.Gizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 2));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperIngot, 1));
        }
    }

    //  Mech Part recipes

    class MechLeftLeg : Recipe
    {
        public MechLeftLeg() : base(InventoryItem.Type.MechLeftLeg)
        {
            Name = "Left Leg of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    class MechRightLeg : Recipe
    {
        public MechRightLeg() : base(InventoryItem.Type.MechRightLeg)
        {
            Name = "Right Leg of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    class MechTorso : Recipe
    {
        public MechTorso() : base(InventoryItem.Type.MechTorso)
        {
            Name = "Torso of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    class MechHead : Recipe
    {
        public MechHead() : base(InventoryItem.Type.MechHead)
        {
            Name = "Head of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    class MechLeftArm : Recipe
    {
        public MechLeftArm() : base(InventoryItem.Type.MechLeftArm)
        {
            Name = "Left Arm of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    class MechRightArm : Recipe
    {
        public MechRightArm() : base(InventoryItem.Type.MechRightArm)
        {
            Name = "Right Arm of Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.AdvancedGizmo, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.IronIngot, 3));
            Requirements.Add(new Requirement(InventoryItem.Type.CopperWire, 2));
        }
    }

    //  Mech Recipes

    class Mech: Recipe
    {
        public Mech() : base(InventoryItem.Type.MechRightArm)
        {
            Name = "Mech";
            
            Requirements.Add(new Requirement(InventoryItem.Type.MechLeftLeg, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechRightLeg, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechLeftArm, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechRightArm, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechTorso, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechHead, 1));
        }
    }

    class LightMech : Recipe
    {
        public LightMech() : base(InventoryItem.Type.MechRightArm)
        {
            Name = "Light Mech";

            Requirements.Add(new Requirement(InventoryItem.Type.MechLeftLeg, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechRightLeg, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechLeftArm, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechRightArm, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechTorso, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.MechHead, 1));
            Requirements.Add(new Requirement(InventoryItem.Type.LightStone, 1));
        }
    }
}
