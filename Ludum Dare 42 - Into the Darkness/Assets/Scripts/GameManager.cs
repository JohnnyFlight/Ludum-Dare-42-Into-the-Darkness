using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum FuelType {
        Regular,
        Refined
    }

    public Text fuelText;
    public Text inventoryText;

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;
    public CaveFloorManager MyFloor;

    float dayLengthSeconds = 5.0f * 60f;
    public int daysPassed = 0;
    float dayCounter = 0.0f;
    public CellManager MyCells;

    public int Width;
    public int Height;

    // Use this for initialization
    void Awake() {

        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        MyFloor = GetComponent<CaveFloorManager>();
        MyCells= GetComponent<CellManager>();
        InitializeGame();
    }

    void InitializeGame() {
        //MyFloor.FloorCreate(Width, Height);
        MyCells.CellCreate(Width, Height);
    }
    
	// Update is called once per frame
	void Update () {
        UpdateTime(Time.deltaTime);

        UpdateUI();
	}

    private void UpdateUI()
    {
        UpdateFuelText();
        UpdateInventoryText();
    }

    private void UpdateInventoryText()
    {
        //  TODO: Just provide a linked instance of the player to save on the lookup?
        Player player = FindObjectOfType<Player>();

        String message = "Items: ";
        InventoryItem[] items = player.inventory.GetArray();

        foreach (InventoryItem item in items)
        {
            message += $"\n\n{item.type.ToString()}";
        }

        if (inventoryText != null)
            inventoryText.text = message;
    }

    void UpdateFuelText()
    {
        //  Get Player
        Player player = FindObjectOfType<Player>();

        String message = "Fuel:";
        for (int i = 0; i < player.fuelTanks.Count; i++)
        {
            message += $"\n\nTank {i + 1}\nType: {player.fuelTanks[i].type.ToString()}\nAmount: {player.fuelTanks[i].quantity}";
        }

        if (fuelText != null)
            fuelText.text = message;
    }

    private void UpdateTime(float deltaTime)
    {
        //  Check to see if passing halfway mark
        if (dayCounter <= dayLengthSeconds / 2f && dayCounter + deltaTime > dayLengthSeconds / 2)
        {
            ChangeLightShaftState(false);
        }

        dayCounter += deltaTime;
        //  Check to see if passing end of day
        if (dayCounter >= dayLengthSeconds)
        {
            dayCounter -= dayLengthSeconds;
            daysPassed++;

            ChangeLightShaftState(true);
        }
    }

    private void ChangeLightShaftState(bool active)
    {
        //  FindObjectsOfType only returns active GameObjects,
        //  But since the shafts will never be inactive (only the associated LightSource will be deactivated)
        LightShaft[] shafts = FindObjectsOfType<LightShaft>();

        foreach (LightShaft shaft in shafts)
        {
            if (active)
                shaft.Activate();
            else
                shaft.Deactivate();
        }
    }

    public Torch GetNearestTorch(Vector3 position, float range)
    {
        Torch[] torches = FindObjectsOfType<Torch>();

        if (torches == null) return null;

        Torch nearestTorch = null;
        float nearestDistance = float.PositiveInfinity;

        foreach (Torch torch in torches)
        {
            float distance = Vector2.Distance(torch.transform.position, position);

            if (distance < nearestDistance)
            {
                nearestTorch = torch;
                nearestDistance = distance;
            }
        }

        //  Only return if it's in range
        return (nearestDistance <= range) ? nearestTorch : null;
    }

    public void CreateResource(InventoryItem.Type type, Vector3 position, float range = 0f)
    {

        GameObject go = new GameObject();

        go.name = type.ToString();

        Resource newInstance = go.AddComponent<Resource>();
        
        newInstance.SetType(type);

        switch (type)
        {
            case InventoryItem.Type.Stone:
                newInstance.SetSprite("Stone");
                break;
            case InventoryItem.Type.IronOre:
                newInstance.SetSprite("IronOre");
                break;
            case InventoryItem.Type.CopperOre:
                newInstance.SetSprite("CopperOre");
                break;
            case InventoryItem.Type.Wood:
                newInstance.SetSprite("Wood");
                break;
            case InventoryItem.Type.Vine:
                newInstance.SetSprite("Vine");
                break;
            case InventoryItem.Type.Fuel:
                newInstance.SetSprite("fuel");
                break;
            case InventoryItem.Type.RefinedFuel:
                newInstance.SetSprite("refined_fuel");
                break;
            default:
                break;
        }
        Vector3 NewPosition = position + (Vector3)UnityEngine.Random.insideUnitCircle * range;
        NewPosition.z = -0.1f;

        go.transform.position = NewPosition;
    }
}
