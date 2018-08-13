using System.Collections.Generic;

public class Loom : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.Vine);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.Vine, new Recipes.Rope());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Loom/LoomOn";
        offStateSpriteName = "Loom/LoomOff";
    }
}