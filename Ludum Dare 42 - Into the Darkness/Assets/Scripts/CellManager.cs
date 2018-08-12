﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    public static CellManager instance = null;//Static instance of CellManager which allows it to be accessed by any other script

    int Width;
    int Height;

    List<List<Nodes>> Rows;

    public void CellCreate(int x, int y) {
        Width = x;
        Height = y;

        Rows = new List<List<Nodes>>();
        
        for (int rowsLoop = 0; rowsLoop < Width; rowsLoop++)
        {
            List<Nodes> Columns = new List<Nodes>();
            for (int columnsLoop = 0; columnsLoop < Height; columnsLoop++)
            {
                Nodes newCell = gameObject.AddComponent(typeof(Nodes)) as Nodes;
                newCell.transform.position.Set(rowsLoop, columnsLoop, 0.0f);
                newCell.SetType(0);
                Columns.Add(newCell);
            }
            Rows.Add(Columns);
            
        }
    }


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
