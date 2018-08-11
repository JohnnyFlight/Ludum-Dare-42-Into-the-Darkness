using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum FuelType {
        Regular,
        Refined
    }

    public static GameManager instance = null;//Static instance of GameManager which allows it to be accessed by any other script.
    public CaveFloorManager MyFloor;

    float dayLengthSeconds = 1f * 60f;
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
        InitializeGame();
    }

    void InitializeGame() {
        MyFloor.FloorCreate(Width, Height);
        MyCells.CellCreate(Width, Height);
    }
    
	// Update is called once per frame
	void Update () {
        UpdateTime(Time.deltaTime);
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
            float distance = Vector3.Distance(torch.transform.position, position);

            if (distance < nearestDistance)
            {
                nearestTorch = torch;
                nearestDistance = distance;
            }
        }

        //  Only return if it's in range
        return (nearestDistance <= range) ? nearestTorch : null;
    }
}
