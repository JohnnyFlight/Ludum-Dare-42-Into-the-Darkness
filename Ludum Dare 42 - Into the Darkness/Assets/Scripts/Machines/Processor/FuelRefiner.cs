﻿using System.Collections.Generic;

public class FuelRefiner : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.Coal);
        filter.Add(InventoryItem.Type.Fuel);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.Coal, new Recipes.Fuel());
        recipes.Add(InventoryItem.Type.Fuel, new Recipes.RefinedFuel());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Smelter/RefinerOn";
        offStateSpriteName = "Smelter/RefinerOff";
    }
}
