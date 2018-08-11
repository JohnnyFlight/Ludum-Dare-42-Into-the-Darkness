using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    private Resource.ResourceType _type;
    public Resource.ResourceType type
    {
        get { return _type;  }
        private set { }
    }

    public InventoryItem(Resource.ResourceType type)
    {
        _type = type;
    }
}
