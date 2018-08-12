using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    public Inventory inventory;
    public Dictionary<InventoryItem.Type, Recipe> recipes;

    bool running = false;

    float processTimeSeconds = 5.0f;

    float currentTimeSeconds = 0.0f;

    protected string onStateSpriteName = "";
    protected string offStateSpriteName = "";

    // Use this for initialization
    void Start() {
        recipes = new Dictionary<InventoryItem.Type, Recipe>();
        
        Setup();
        
        //  If any child class doesn't initialise an inventory
        if (inventory == null)
        {
            //  Create a default one
            List<InventoryItem.Type> filter = new List<InventoryItem.Type>();
            inventory = new Inventory(filter, Inventory.FilterMode.IncludeFilter, 5);
        }
	}

    protected virtual void Setup()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (!running)
        {
            if (!inventory.IsEmpty())
            {
                StartRunning();
            }
        }
        else
        {
            currentTimeSeconds += Time.deltaTime;

            if (currentTimeSeconds > processTimeSeconds)
            {
                MakeItem();

                if (inventory.IsEmpty())
                {
                    StopRunning();
                }
                else
                {
                    currentTimeSeconds -= processTimeSeconds;
                }
            }
        }
	}

    void StartRunning()
    {
        running = true;
        currentTimeSeconds = 0.0f;

        ChangeSprite(onStateSpriteName);
    }

    void StopRunning()
    {
        running = false;

        ChangeSprite(offStateSpriteName);
    }

    void ChangeSprite(string name)
    {
        Sprite spr = Resources.Load<Sprite>("Sprites/Machines/" + name);
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.sprite = spr;
    }

    void MakeItem()
    {
        //  Get top inventory item and make associated recipe
        InventoryItem result = recipes[inventory.GetArray()[0].type].Craft(inventory);

        if (result != null)
        {
            GameManager.instance.CreateResource(result.type, this.transform.position, 1);
        }
    }
}
