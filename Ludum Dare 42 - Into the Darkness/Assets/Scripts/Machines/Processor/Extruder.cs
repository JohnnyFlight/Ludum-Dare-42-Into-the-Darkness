using System.Collections.Generic;

public class Extruder : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.CopperIngot);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.CopperIngot, new Recipes.CopperWire());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Smelter/ExtruderOff";
        offStateSpriteName = "Smelter/ExtruderOff";
    }
}