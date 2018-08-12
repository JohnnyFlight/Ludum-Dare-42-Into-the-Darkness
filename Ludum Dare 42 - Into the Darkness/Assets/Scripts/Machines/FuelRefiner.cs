﻿using System.Collections.Generic;

public class FuelRefiner : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.Coal);
        filter.Add(InventoryItem.Type.Fuel);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.IronOre, new Recipes.IronIngot());
        recipes.Add(InventoryItem.Type.CopperOre, new Recipes.CopperIngot());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }
}
