using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredQuarry : PoweredGatherer {

    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherStone());
        running = true;

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }

}