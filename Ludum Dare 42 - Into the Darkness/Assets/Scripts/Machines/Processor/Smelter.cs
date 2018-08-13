using System.Collections.Generic;

public class Smelter : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.CopperOre);
        filter.Add(InventoryItem.Type.IronOre);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        recipes.Add(InventoryItem.Type.IronOre, new Recipes.IronIngot());
        recipes.Add(InventoryItem.Type.CopperOre, new Recipes.CopperIngot());

        inventory = new Inventory(filter, filterMode);
        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperOre));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronOre));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperOre));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronOre));

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
    }
}
