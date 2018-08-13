using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum FuelType
    {
        Regular,
        Refined
    }

    public Text fuelText;
    public Text inventoryText;

    public GUISkin guiSkin;

    //  This is the resource prefab
    public GameObject res;

    public float DarknessIntensifyBurnRateMultiplier = 1.1f;

    protected static IList<InventoryItem.Type> ItemsThatIntensifyDarkness = new ReadOnlyCollection<InventoryItem.Type>
        (new List<InventoryItem.Type>{
            InventoryItem.Type.MechHead,
            InventoryItem.Type.MechTorso,
            InventoryItem.Type.MechLeftLeg,
            InventoryItem.Type.MechRightLeg,
            InventoryItem.Type.MechLeftArm,
            InventoryItem.Type.MechRightArm
        });

    //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager instance = null;
    public CaveFloorManager MyFloor;

    float dayLengthSeconds = 5.0f * 6f;
    public int daysPassed = 0;
    float dayCounter = 0.0f;
    public CellManager MyCells;

    public int Width;
    public int Height;

    // Use this for initialization
    void Awake()
    {
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
        MyCells = GetComponent<CellManager>();
        InitializeGame();
    }

    void InitializeGame()
    {
        MyFloor.FloorCreate(Width, Height);
        MyCells.CellCreate(Width, Height);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(Time.deltaTime);

        UpdateUI();
    }

    private void UpdateUI()
    {
        UpdateFuelText();
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

    public CrankGatherer GetNearestCrank(Vector3 position, float range)
    {
        CrankGatherer[] cranks = FindObjectsOfType<CrankGatherer>();

        if (cranks == null) return null;

        CrankGatherer nearestCrank = null;
        float nearestDistance = float.PositiveInfinity;

        foreach (CrankGatherer crank in cranks)
        {
            float distance = Vector2.Distance(crank.transform.position, position);

            if (distance < nearestDistance)
            {
                nearestCrank = crank;
                nearestDistance = distance;
            }
        }

        //  Only return if it's in range
        return (nearestDistance <= range) ? nearestCrank : null;
    }

    public void CreateResource(InventoryItem.Type type, Vector3 position, float range = 0f)
    {
        if (type == InventoryItem.Type.Nothing) return;

        GameObject go = Instantiate(res);
        go.name = type.ToString();

        Resource newInstance = go.GetComponent<Resource>();
        newInstance.SetType(type);

        switch (type)
        {
            case InventoryItem.Type.Stone:
                newInstance.SetSprite("Stone");
                break;
            case InventoryItem.Type.IronOre:
                newInstance.SetSprite("IronOre");
                break;
            case InventoryItem.Type.IronIngot:
                newInstance.SetSprite("IronIngot");
                break;
            case InventoryItem.Type.CopperOre:
                newInstance.SetSprite("CopperOre");
                break;
            case InventoryItem.Type.CopperIngot:
                newInstance.SetSprite("CopperIngot");
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
            case InventoryItem.Type.CopperWire:
                newInstance.SetSprite("CopperWire");
                break;
            case InventoryItem.Type.LightStone:
                newInstance.SetSprite("LightStone");
                break;
            case InventoryItem.Type.Gizmo:
                newInstance.SetSprite("Gizmo");
                break;
            case InventoryItem.Type.AdvancedGizmo:
                newInstance.SetSprite("AdvancedGizmo");
                break;
            default:
                break;
        }
        Vector3 NewPosition = position + (Vector3)UnityEngine.Random.insideUnitCircle * range;
        NewPosition.z = -0.1f;

        go.transform.position = NewPosition;
    }

    public bool PlaceMachine(InventoryItem.Type type, Vector3 pos)
    {
        int maxX = Width;
        int maxY = Height;


        int nodeX = Mathf.RoundToInt(pos.x);
        if ((nodeX >= maxX) || (nodeX < 0))
        {
            return false;
        }

        int nodeY = Mathf.RoundToInt(pos.y);
        if ((nodeY >= maxY) || (nodeY < 0))
        {
            return false;
        }

        Nodes TargetNode = CellManager.instance.Rows[nodeX][nodeY].GetComponent<Nodes>() as Nodes;

        //  This is hacky and dumb but it will work
        GameObject go = GetMachineFromType(type);

        go.name = type.ToString();
        Machine machine = go.GetComponent<Machine>();

        if (!machine.IsCompatibleWith(TargetNode.GetResourceType()))
        {
            Destroy(machine);
            return false;
        }

        Vector3 position = TargetNode.transform.position;
        position.z = -0.05f;
        machine.transform.position = position;

        return true;
    }

    GameObject GetMachineFromType(InventoryItem.Type type)
    {
        switch (type)
        {
            case InventoryItem.Type.PoweredDrill:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/PoweredDrill"));
            case InventoryItem.Type.PoweredQuarry:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/PoweredQuarry"));
            case InventoryItem.Type.PoweredChopper:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/Saw"));
            case InventoryItem.Type.Smelter:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/Smelter"));
            case InventoryItem.Type.Grinder:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/Grinder"));
            case InventoryItem.Type.Loom:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/Loom"));
            case InventoryItem.Type.Mill:
                return (GameObject)Instantiate(Resources.Load("Prefabs/MachinePrefabs/Mill"));
            default:
                return null;
        }
    }

    public bool PlaceLightStone(Vector3 location)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/SetLightStone"));
        go.transform.position = (Vector3)(Vector2)location + new Vector3(0, 0, -0.1f);

        return true;
    }

    public void ItemMade(InventoryItem.Type type)
    {
        if (ItemsThatIntensifyDarkness.Contains(type))
        {
            IntensifyDarkness();
        }
    }

    private void IntensifyDarkness()
    {
        Debug.Log("Darkness intensifies!");
        //  For now just add a multiplier to the burn rate of all torches
        Torch[] torches = FindObjectsOfType<Torch>();

        foreach (Torch torch in torches)
        {
            torch.fuelBurnRatePerSecond *= DarknessIntensifyBurnRateMultiplier;
        }
    }
}
