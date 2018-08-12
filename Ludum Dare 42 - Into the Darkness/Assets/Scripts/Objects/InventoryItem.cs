using System.Collections;
using System.Collections.Generic;
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
        Loom
    };

    private Type _type;
    public Type type
    {
        get { return _type;  }
        private set { }
    }

    public InventoryItem(Type type)
    {
        _type = type;
    }
}
