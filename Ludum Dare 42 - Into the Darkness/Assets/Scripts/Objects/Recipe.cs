using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe 
{
    public struct Requirement
    {
        public Requirement(InventoryItem.Type type, uint count)
        {
            this.type = type;
            this.count = count;
        }

        public InventoryItem.Type type { get; private set; }
        public uint count { get; private set; }
    }

    //  Requirements is a list of ResourceTypes with a number
    protected List<Requirement> Requirements;
    protected InventoryItem.Type Result;

    public string Name { get; protected set; }

    //  This is so yo can only instantiate a Base Class of Recipe
    protected Recipe(InventoryItem.Type result)
    {
        Name = "NoNameFound";    

        Requirements = new List<Requirement>();
        Result = result;
    }

    public bool CanCraft(Inventory inv)
    {
        //  Return true if all criteria are met
        foreach (Requirement req in Requirements)
        {
            if (!inv.Contains(req.type, req.count)) 
                return false;
        }

        return true;
    }

    public InventoryItem Craft(Inventory inv)
    {
        //  Remove items from list and return item
        if (!CanCraft(inv)) return null;

        //  Remove requirements
        foreach (Requirement req in Requirements)
        {
            inv.RemoveItem(req.type, req.count);
        }

        return new InventoryItem(Result);
    }

    protected virtual InventoryItem GetResult()
    {
        return new InventoryItem(Result);
    }

    //  Returns a cloned array to prevent modification
    public Requirement[] GetRequirements()
    {
        return Requirements.ToArray();
    }
}
