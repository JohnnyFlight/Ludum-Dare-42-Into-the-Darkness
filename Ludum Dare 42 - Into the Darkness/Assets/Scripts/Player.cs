using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 2.5f;
    
    public List<FuelTank> fuelTanks;

    public float startingFuel = 100.0f;

    List<Recipe> CraftableRecipes;

    public enum PlayerState {
        Active,
        Inventory,
        Crafting,
        Dead
    }

    PlayerState state;

    //  How many units away can a torch be lit from
    private float lightTorchRange = 1.0f;

    public Inventory inventory;

	// Use this for initialization
	void Start ()
    {
        state = PlayerState.Active;

        CraftableRecipes = new List<Recipe>();
        //  TODO: Find a better solution for this, so it doesn't need to be maintained constantly
        CraftableRecipes.Add(new Recipes.MechRightArm());
        CraftableRecipes.Add(new Recipes.PoweredDrill());
        CraftableRecipes.Add(new Recipes.PoweredQuarry());
        CraftableRecipes.Add(new Recipes.PoweredSaw());

        CraftableRecipes.Add(new Recipes.CrankDrill());
        CraftableRecipes.Add(new Recipes.CrankQuarry());
        CraftableRecipes.Add(new Recipes.CrankSaw());

        CraftableRecipes.Add(new Recipes.Grinder());
        CraftableRecipes.Add(new Recipes.Loom());
        CraftableRecipes.Add(new Recipes.Mill());
        CraftableRecipes.Add(new Recipes.Smelter());

        //  Mech Parts
        CraftableRecipes.Add(new Recipes.MechLeftLeg());
        CraftableRecipes.Add(new Recipes.MechRightLeg());
        CraftableRecipes.Add(new Recipes.MechLeftArm());
        CraftableRecipes.Add(new Recipes.MechHead());
        CraftableRecipes.Add(new Recipes.MechTorso());
        
        //  Full Mechs
        CraftableRecipes.Add(new Recipes.Mech());
        CraftableRecipes.Add(new Recipes.LightMech());

        fuelTanks = new List<FuelTank>();
        fuelTanks.Add(new FuelTank(GameManager.FuelType.Regular, startingFuel));
        fuelTanks.Add(new FuelTank(GameManager.FuelType.Regular, 0.0f));

        inventory = new Inventory();
        
        inventory.AddItem(new InventoryItem(InventoryItem.Type.Gizmo));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronIngot));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronIngot));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperWire));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.LightStone));

        inventory.AddItem(new InventoryItem(InventoryItem.Type.AdvancedGizmo));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronIngot));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronIngot));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.IronIngot));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperWire));
        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperWire));
    }

    bool ManualGather() {

        int maxX = GameManager.instance.Width;
        int maxY = GameManager.instance.Height;

        
        int nodeX = Mathf.RoundToInt(this.transform.position.x);
        if ((nodeX >= maxX)||(nodeX < 0))
        {
            return false;
        }

        int nodeY = Mathf.RoundToInt(this.transform.position.y);
        if ((nodeY >= maxY) || (nodeY < 0))
        {
            return false;
        }

        Nodes TargetNode = CellManager.instance.Rows[nodeX][nodeY].GetComponent<Nodes>() as Nodes;
        GameManager.instance.CreateResource(TargetNode.GetResourceType(), TargetNode.transform.position, 1.0f);
        
        return true;
    }
    
    bool isInLight()
    {
        LightSource[] lightsArray = FindObjectsOfType<LightSource>();

        for (int sourceLoop = 0; sourceLoop < lightsArray.Length; sourceLoop++)
        {

            if (Vector2.Distance((lightsArray[sourceLoop].transform.position), (this.transform.position)) < lightsArray[sourceLoop].radius)
            {
                return true;
            }
        }

        return false;
    }

    void FixedUpdate () {
        if (state != PlayerState.Active) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ManualGather();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            ToggleCrafting();
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            AttemptCrank();
        }

        if (!isInLight() && state != PlayerState.Dead) {
            //  Start Match
            Match match = GetComponentInChildren<Match>();
            match.addFuel(5, GameManager.FuelType.Regular);
        }
    }

    void AttemptCrank() {
        CrankGatherer nearest = GameManager.instance.GetNearestCrank(this.gameObject.transform.position, 1.0f);

        if (nearest == null)
        {
            //  TODO: Other stuff here, like start a shrug animation?
            return;
        }

        nearest.IncreaseCrankTurn();
    }

    private void ToggleCrafting()
    {
        switch (state)
        {
            case PlayerState.Crafting:
                state = PlayerState.Active;
                break;
            case PlayerState.Active:
                state = PlayerState.Crafting;
                break;
        }
    }

    private void ToggleInventory()
    {
        switch (state)
        {
            case PlayerState.Inventory:
                state = PlayerState.Active;
                break;
            case PlayerState.Active:
                state = PlayerState.Inventory;
                break;
        }
    }

    private void OnGUI()
    {
        switch (state)
        {
            case PlayerState.Inventory:
                GUIInventory();
                break;
            case PlayerState.Crafting:
                GUICrafting();
                break;
            case PlayerState.Dead:
                GUIDead();
                break;
        }
    }

    private void GUIDead()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "U R DED LMAO");
    }

    private void GUICrafting()
    {
        //  Iterate through recipes
        //  If can craft draw button to craft
        //  Otherwise don't

        GUI.skin = GameManager.instance.guiSkin;

        float buttonWidth = 300.0f;
        float x = Screen.width / 2 - buttonWidth / 2;
        float buttonHeight = 60.0f;
        float buttonSpacing = 5.0f;

        float totalHeight = 0.0f;

        Vector2 scrollPosition = Vector2.zero;

        GUI.BeginScrollView(new Rect(0f, 0f, Screen.width, Screen.height * 20), scrollPosition, new Rect(0f, 0f, Screen.width, Screen.height));

        for (var i = 0; i < CraftableRecipes.Count; i++)
        {
            buttonHeight = 20.0f;

            string recipeText = $"{CraftableRecipes[i].Name} Ingredients:";
            Recipe.Requirement[] reqs = CraftableRecipes[i].GetRequirements();

            foreach (Recipe.Requirement req in reqs)
            {
                recipeText += $"\n\t{req.count} x {req.type}";
                buttonHeight += 20.0f;
            }

            if (CraftableRecipes[i].CanCraft(inventory))
            {
                if (GUI.Button(new Rect(x, totalHeight, buttonWidth, buttonHeight), recipeText))
                {
                    InventoryItem result = CraftableRecipes[i].Craft(inventory);
                    inventory.AddItem(result);
                    GameManager.instance.ItemMade(result.type);
                }
            }
            else
            {
                GUI.Label(new Rect(x, totalHeight, buttonWidth, buttonHeight), recipeText);
            }

            totalHeight += buttonHeight + buttonSpacing;
        }

        GUI.EndScrollView();
    }

    private void GUIInventory()
    {
        //  Show a grid of inventory items and drop them if clicking on the button
        InventoryItem[] inventoryItems = inventory.GetArray();

        float buttonWidth = 100;
        float x = Screen.width / 2 - buttonWidth / 2;

        for (var i = 0; i < inventoryItems.Length; i++)
        {
            if (GUI.Button(new Rect(x, 25 * i, buttonWidth, 20), inventoryItems[i].type.ToString()))
            {
                if (inventoryItems[i].Drop(transform.position))
                {
                    inventory.RemoveItem(inventoryItems[i].type);
                }
            }
        }
    }

    private void AttemptLightTorch()
    {
        Torch nearest = GameManager.instance.GetNearestTorch(this.gameObject.transform.position, lightTorchRange);

        if (nearest == null)
        {
            //  TODO: Other stuff here, like start a shrug animation?
            return;
        }

        //  Does the player have a matching fuel type?
        FuelTank emptiest = GetEmptiestNonEmptyFuelTankOfType(nearest.type);

        //  If so, get the emptiest tank with that type and put it in the torch
        if (emptiest != null)
        {
            float leftover = nearest.addFuel(emptiest.quantity, emptiest.type);
            emptiest.SetFuel(leftover, emptiest.type);
        }
    }

    public FuelTank GetEmptiestNonEmptyFuelTankOfType(GameManager.FuelType type)
    {
        FuelTank emptiest = null;
        float leastAmountOfFuel = float.PositiveInfinity;

        foreach (FuelTank tank in fuelTanks)
        {
            if (tank.type != type) continue;

            if (tank.quantity < leastAmountOfFuel && tank.quantity > 0)
            {
                emptiest = tank;
                leastAmountOfFuel = tank.quantity;
            }
        }

        return emptiest;
    }

    //  If tank is of a different type, then return that too
    public FuelTank GetEmptiestFuelTankOfType(GameManager.FuelType type)
    {
        FuelTank emptiest = null;
        float leastAmountOfFuel = float.PositiveInfinity;

        foreach (FuelTank tank in fuelTanks)
        {
            if (tank.type != type && tank.quantity > 0.0f) continue;

            if (tank.quantity < leastAmountOfFuel)
            {
                emptiest = tank;
                leastAmountOfFuel = tank.quantity;
            }
        }

        return emptiest;
    }

    public void MatchExtinguished()
    {
        //  If in dark then die
        if (!isInLight())
        {
            Debug.Log("ded");
            state = PlayerState.Dead;
        }
        else
        {
            
        }
    }
}
