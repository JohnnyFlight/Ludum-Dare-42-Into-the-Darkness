using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaveFloorManager : MonoBehaviour {


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
                GameObject instance = Instantiate(toInstantiate, new Vector3(widthLoop, heightLoop, 0f), Quaternion.identity) as GameObject;
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
