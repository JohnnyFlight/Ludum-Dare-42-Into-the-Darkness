using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public enum Type
    {
        Stone, IronOre, CopperOre, Wood, Vine,
        IronIngot, CopperIngot, Lumber, Rope,
        CopperWire
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
