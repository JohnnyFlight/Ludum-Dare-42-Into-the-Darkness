using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaveFloorManager : MonoBehaviour {

    public static CaveFloorManager instance = null;//Static instance of CaveFloorManager which allows it to be accessed by any other script

    int Width;
    int Height;

    public GameObject[] FloorPrefabs;

    public void FloorCreate(int x, int y) {

        Width = x;
        Height = y;

        for (int widthLoop = 0; widthLoop < Width; widthLoop++)
        {
            for (int heightLoop = 0; heightLoop < Height; heightLoop++)
            {
                GameObject toInstantiate = FloorPrefabs[Random.Range(0, FloorPrefabs.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(widthLoop, heightLoop, 0.1f), Quaternion.identity) as GameObject;
            }
        }
    }


    // Use this for initialization
    void Awake () {

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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
