using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDrill : PoweredGatherer
{

    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherCopperOre());
        running = true;

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }

}