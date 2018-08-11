using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    // Use this for initialization

    int Width;
    int Height;

    ArrayList Rows;


    public void CellCreate(int x, int y) {
        Width = x;
        Height = y;

        Rows = new ArrayList();
        
        for (int rowsLoop = 0; rowsLoop < Width; rowsLoop++)
        {
            ArrayList Columns = new ArrayList();
            for (int columnsLoop = 0; columnsLoop < Height; columnsLoop++)
            {
                GameObject newCell = new GameObject();
                Columns.Add(newCell);
            }
            Rows.Add(Columns);

        }
    }


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
