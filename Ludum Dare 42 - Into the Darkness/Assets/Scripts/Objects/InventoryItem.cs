using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public enum Type
    {
        Nothing,
        Stone, 
        IronOre, IronIngot, 
        CopperOre, CopperIngot, CopperWire,
        Wood, Lumber, 
        Vine, Rope,
        Gizmo, AdvancedGizmo,
        Coal, Fuel, RefinedFuel,
        LightStone,
        
        //  Machines
        Smelter,
        Grinder,
        FuelRefiner,
        Loom,
        Mill,

        CrankDrill,
        CrankQuarry,
        CrankSaw,

        PoweredDrill,
        PoweredQuarry,
        PoweredChopper,

        //  Mech parts
        MechLeftLeg,
        MechRightLeg,
        MechTorso,
        MechLeftArm,
        MechRightArm,
        MechHead
    };
    
    public Type type { get; private set; }

    static readonly IList<Type> CannotBeDropped = new ReadOnlyCollection<Type>
        (new List<Type>{
            Type.MechLeftLeg,
            Type.MechRightLeg,
            Type.MechTorso,
            Type.MechLeftArm,
            Type.MechRightArm,
            Type.MechHead
        });

    static readonly IList<Type> Machines = new ReadOnlyCollection<Type>
        (new List<Type>{
            Type.Smelter,
            Type.Grinder,
            Type.FuelRefiner,
            Type.Loom,
            Type.Mill,

            Type.PoweredChopper,
            Type.PoweredDrill,
            Type.PoweredQuarry
        });

    public InventoryItem(Type type)
    {
        this.type = type;
    }

    virtual public bool Drop(Vector3 location)
    {
        //  I should create child classes of Inventory Item to handle this
        //  but for now I'll just use a switch statement for edge cases

        if (CannotBeDropped.Contains(type)) return false;
        else if (Machines.Contains(type)) return GameManager.instance.PlaceMachine(type, location);
        else if (type == Type.LightStone) return GameManager.instance.PlaceLightStone(location);

        GameManager.instance.CreateResource(type, location, 1);

        return true;
    }
}
