using System.Collections.Generic;

public class Mill : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.Vine);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.Wood, new Recipes.Lumber());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Mill/MillOn";
        offStateSpriteName = "Mill/MillOff";
    }
}