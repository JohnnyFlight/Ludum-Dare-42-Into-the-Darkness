using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveSpeed = 2.5f;
    
    public List<FuelTank> fuelTanks;

    public float startingFuel = 100.0f;

    //  How many units away can a torch be lit from
    private float lightTorchRange = 1.0f;

    public Inventory inventory;

	// Use this for initialization
	void Start ()
    {
        fuelTanks = new List<FuelTank>();
        fuelTanks.Add(new FuelTank(GameManager.FuelType.Regular, startingFuel));
        fuelTanks.Add(new FuelTank(GameManager.FuelType.Regular, 0.0f));

        inventory = new Inventory();

        inventory.AddItem(new InventoryItem(InventoryItem.Type.CopperOre));

        Recipe r = new Recipes.CopperIngot();
        if (r.CanCraft(inventory)) {
            inventory.AddItem(r.Craft(inventory));
        }
    }
    
    bool isInLight()
    {
        LightSource[] lightsArray = FindObjectsOfType<LightSource>();

        for (int sourceLoop = 0; sourceLoop < lightsArray.Length; sourceLoop++)
        {

            if (Vector3.Distance((lightsArray[sourceLoop].transform.position), (this.transform.position)) < lightsArray[sourceLoop].radius)
            {
                return true;
            }
        }

        return false;
    }

    void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            AttemptLightTorch();
        }

        if (!isInLight()) {
            Debug.Log("ded");
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

    FuelTank GetEmptiestNonEmptyFuelTankOfType(GameManager.FuelType type) {
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
}
