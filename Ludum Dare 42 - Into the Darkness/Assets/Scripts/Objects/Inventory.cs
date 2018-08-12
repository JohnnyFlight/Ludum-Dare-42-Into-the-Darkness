using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField]
    private List<InventoryItem> Items;
    private List<InventoryItem.Type> Filter;

    public enum FilterMode
    {
        NoFilter,
        ExcludeFilter,
        IncludeFilter
    }

    private FilterMode filterMode;

    public uint Capacity { get; private set; }

    public Inventory(List<InventoryItem.Type> filter = null, FilterMode filterMode = FilterMode.NoFilter, uint capacity = 10)
    {
        Items = new List<InventoryItem>();

        this.filterMode = filterMode;

        Filter = filter ?? new List<InventoryItem.Type>();

        Capacity = capacity;
    }

    public bool Contains(InventoryItem.Type type, uint amount = 1) {
        return Items.FindAll(item => item.type == type).Count >= amount;
    }

    public bool AddItem(InventoryItem item) {
        if (Items.Count >= Capacity) return false;

        switch (filterMode)
        {
            case FilterMode.ExcludeFilter:
                if (Filter.Contains(item.type)) return false;
                break;
            case FilterMode.IncludeFilter:
                if (!Filter.Contains(item.type)) return false;
                break;
            default:
                break;
        }
        
        Items.Add(item);
        return true;
    }

    //  doNothingIfNotEnough means that if there are less items in the Inventory than requested, don't remove any
    //  eg. If you want to remove 2 Copper, but the Inventory only has 1, if doNothingIfNotEnough is false, it will still remove the 1 copper
    //  Returns number of removed items
    public uint RemoveItem(InventoryItem.Type type, uint count = 1, bool doNothingIfNotEnough = true)
    {
        if (Contains(type, count))
        {
            for (var i = 0; i < count; i++)
            {
                Items.Remove(Items.Find(item => item.type == type));
            }

            return count;
        }
        else
        {
            if (doNothingIfNotEnough) return 0;

            for (var i = 0; i < count; i++)
            {
                if (!Items.Remove(Items.Find(item => item.type == type))) return (uint)i;
            }

            return count;
        }
    }
    
    //  Returns a copy of the items to prevent modification
    public InventoryItem[] GetArray() {
        return Items.ToArray();
    }

    public bool IsEmpty()
    {
        return Items.Count == 0;
    }

    public bool IsFull()
    {
        return Items.Count >= Capacity;
    }
}
