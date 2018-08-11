using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    // Use this for initialization

    int Width;
    int Height;

    ArrayList Rows;

    struct cell {

        public int x;
        public int y;
        public GameObject TileInfo;

    }

    public void CellCreate(int x, int y) {
        Width = x;
        Height = y;

        Rows = new ArrayList();
        
        for (int rowsLoop = 0; rowsLoop < Width; rowsLoop++)
        {
            ArrayList Columns = new ArrayList();
            for (int columnsLoop = 0; columnsLoop < Height; columnsLoop++)
            {
                cell newCell;
                newCell.x = rowsLoop;
                newCell.y = columnsLoop;
                newCell.TileInfo = null;
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
