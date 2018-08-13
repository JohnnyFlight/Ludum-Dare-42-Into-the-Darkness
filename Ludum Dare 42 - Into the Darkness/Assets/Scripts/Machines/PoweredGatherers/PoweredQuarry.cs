using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredQuarry : PoweredGatherer {

    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes = new Dictionary<InventoryItem.Type, Recipe>();
        recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherStone());
        running = true;

        compatibleTypes = new List<InventoryItem.Type>();
        compatibleTypes.Add(InventoryItem.Type.Stone);

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }
}