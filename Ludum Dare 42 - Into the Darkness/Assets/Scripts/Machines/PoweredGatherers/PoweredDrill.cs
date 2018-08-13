using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDrill : PoweredGatherer
{

    InventoryItem.Type WhatVein = InventoryItem.Type.CopperOre;

    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        running = true;

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
        SetType(WhatVein);
    }

    public bool SetType(InventoryItem.Type ThisVein)
    {
        if ((ThisVein == InventoryItem.Type.IronOre)||(ThisVein == InventoryItem.Type.CopperOre)) {
            WhatVein = ThisVein;

            if (WhatVein == InventoryItem.Type.IronOre)
            {
                recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherIronOre());
            }
            else
            {
                recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherCopperOre());
            }

            return true;
        }
        else {
            return false;
        }
    }
}