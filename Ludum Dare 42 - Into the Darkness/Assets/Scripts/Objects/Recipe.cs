using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe 
{
    public List<Tuple<Resource.ResourceType, uint>> Requirements;

    public Recipe()
    {
        
    }

    public bool CanCraft(IEnumerable<InventoryItem> items)
    {
        //  Return true if all criteria are met
        return true;
    }

    public InventoryItem Craft(IEnumerable<InventoryItem> items)
    {
        //  Remove items from list and return item
        return null;
    }
}
