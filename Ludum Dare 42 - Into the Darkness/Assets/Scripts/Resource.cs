using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    InventoryItem.Type MyType;
    SpriteRenderer MyRenderer;

    //  Types of Resource that don't deplete in dark
    static readonly IList<InventoryItem.Type> DarkExclusions = new ReadOnlyCollection<InventoryItem.Type>
        (new List<InventoryItem.Type>{
            InventoryItem.Type.LightStone,
            InventoryItem.Type.Gizmo,
            InventoryItem.Type.AdvancedGizmo
        });

    // Use this for initialization
    void Start () {
        
    }

    public void SetSprite(String name)
    {
        Sprite spr = Resources.Load<Sprite>("Sprites/Pickups/" + name);
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (rend == null)
        {
            rend = gameObject.AddComponent<SpriteRenderer>();
        }
        rend.sprite = spr;
    }

    public void SetType(InventoryItem.Type Type) {
        MyType = Type;
    }

    //  Don't need to right click so don't need any hacky shenanigans to allow for that
    public void OnMouseDown()
    {
        if (AddItemToInventory())
        {
            Destroy(this.gameObject);
        }
    }

    private bool AddItemToInventory()
    {
        //  Get Player
        Player player = FindObjectOfType<Player>();

        //  Hacky edge case for fuel
        if (MyType == InventoryItem.Type.Fuel)
        {
            //  Add fuel to player

            //  If all fuel returned then return false

            //  Other return true
            return true;
        }

        return player.inventory.AddItem(new InventoryItem(MyType));
    }

    bool inLight() {
        if (!DepletesInDark()) return true;

        LightSource[] lightsArray = FindObjectsOfType<LightSource>();
        bool inLight = false;

        for (int sourceLoop = 0; sourceLoop < lightsArray.Length; sourceLoop++)
        {

            if (Vector2.Distance((lightsArray[sourceLoop].transform.position), (this.transform.position)) < lightsArray[sourceLoop].radius)
            {
                inLight = true;
                break;
            }
        }

        return inLight;
    }

    bool DepletesInDark()
    {
        return !DarkExclusions.Contains(MyType);
    }

    void inDark() {

        if (Random.Range(0, 100) == 0) {
            Destroy(this.gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
        if (inLight() == false) {
            inDark();
        }
	}
}
