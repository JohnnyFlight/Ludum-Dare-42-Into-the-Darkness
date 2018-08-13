using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankSaw : CrankGatherer
{
    InventoryItem.Type HarvestType = InventoryItem.Type.Wood;

    protected override void Setup()
    {
        List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
        Inventory.FilterMode filterMode = Inventory.FilterMode.IncludeFilter;

        running = true;

        onStateSpriteName = "Smelter/smelter_on";
        offStateSpriteName = "Smelter/smelter_off";
        SetType(HarvestType);
    }

    public bool SetType(InventoryItem.Type Harvest)
    {
        if ((Harvest == InventoryItem.Type.Wood) || (Harvest == InventoryItem.Type.Vine))
        {
            HarvestType = Harvest;

            if (HarvestType == InventoryItem.Type.Wood)
            {
                recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherWood());
            }
            else
            {
                recipes.Add(InventoryItem.Type.Nothing, new Recipes.GatherVine());
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
