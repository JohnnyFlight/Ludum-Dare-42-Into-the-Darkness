using System.Collections.Generic;

public class Grinder : Machine
{
    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        filter.Add(InventoryItem.Type.Stone);

        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;
        
        recipes.Add(InventoryItem.Type.Stone, new Recipes.GroundStone());

        inventory = new Inventory(filter, filterMode);

        onStateSpriteName = "Grinder/GrinderOn";
        offStateSpriteName = "Grinder/GrinderOff";
    }
}
