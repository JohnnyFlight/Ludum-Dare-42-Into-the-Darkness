using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDrill : PoweredGatherer
{

    protected override void Setup()
    {
        recipes = new Dictionary<InventoryItem.Type, Recipe>();
        compatibleTypes = new List<InventoryItem.Type>();

        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();

        recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherCopperOre());
        running = true;

        compatibleTypes.Add(InventoryItem.Type.CopperOre);
        compatibleTypes.Add(InventoryItem.Type.IronOre);

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }

}