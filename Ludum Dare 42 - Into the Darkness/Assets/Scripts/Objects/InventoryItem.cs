using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    private Nodes.ResourceType _type;
    public Nodes.ResourceType type
    {
        get { return _type;  }
        private set { }
    }

    public InventoryItem(Nodes.ResourceType type)
    {
        _type = type;
    }
}
