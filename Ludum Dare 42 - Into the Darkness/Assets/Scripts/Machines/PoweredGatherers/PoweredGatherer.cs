using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoweredGatherer : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        
    }

    protected override bool ContinueRunning() {
        return true;
    }


    protected override Recipe GetRecipe()
    {
        return recipes[InventoryItem.Type.Nothing];
    }
}
