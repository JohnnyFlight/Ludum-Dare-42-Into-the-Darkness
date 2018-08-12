using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {
    public Inventory inventory;

    //  TODO: Change the key to be a list of Requirements to allow for more complex recipes
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

    private void OnMouseOver()
    {
        //  Left click
        if (Input.GetMouseButtonDown(0))
        {
            //  Check if Machine inventory is full before trying to put something in it
            if (inventory.IsFull()) return;

            //  Get Player
            Player player = FindObjectOfType<Player>();

            if (player == null) return;

            //  Get requirements from recipes
            //  Due to the simple way we're handling recipes in this class, we can just use the key of the dictionary
            List<InventoryItem.Type> inputs = new List<InventoryItem.Type>(recipes.Keys);
        
            //  See if any of the requirements are in the Player's inventory
            foreach (InventoryItem.Type input in inputs)
            {
                if (player.inventory.Contains(input))
                {
                    inventory.AddItem(new InventoryItem(input));
                    player.inventory.RemoveItem(input);
                    return;
                }
            }
        }
        //  Right click - Add Fuel
        else if (Input.GetMouseButtonDown(1))
        {
            
        }
    }

    // Update is called once per frame
    void Update () {
		if (!running)
        {
            if (ContinueRunning())
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

                if (!ContinueRunning())
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

    protected virtual bool ContinueRunning()
    {
        return !inventory.IsEmpty();
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
        InventoryItem result = GetRecipe().Craft(inventory);

        if (result != null)
        {
            GameManager.instance.CreateResource(result.type, this.transform.position, 1);
        }
    }

    protected virtual Recipe GetRecipe()
    {
        return recipes[inventory.GetArray()[0].type];
    }
}
