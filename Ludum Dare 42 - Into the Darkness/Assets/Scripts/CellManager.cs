using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour {

    public static CellManager instance = null;//Static instance of CellManager which allows it to be accessed by any other script

    int Width;
    int Height;

    public List<List<GameObject>> Rows;


    public GameObject BaseNode;

    public void CellCreate(int x, int y) {
        Width = x;
        Height = y;
        
        Rows = new List<List<GameObject>>();

        for (int rowsLoop = 0; rowsLoop < Width; rowsLoop++)
        {
            List<GameObject> Columns = new List<GameObject>();
            for (int columnsLoop = 0; columnsLoop < Height; columnsLoop++)
            {
                //Nodes BaseNode.GetComponent<Nodes>;
                Vector3 NewPosition = new Vector3();
                NewPosition.Set(rowsLoop, columnsLoop, -1.0f);
                //GameObject toInstantiate = Instantiate(BaseNode, NewPosition, transform.rotation);
                GameObject toInstantiate = GetPrefabFromType(GetRandomType());
                toInstantiate.transform.position = NewPosition;

                //Nodes InstanceNode = toInstantiate.GetComponent<Nodes>();
                //InstanceNode.SetType( InventoryItem.Type.Stone );
                Columns.Add(toInstantiate);
            }
            Rows.Add(Columns);
            
        }
    }

    InventoryItem.Type GetRandomType()
    {
        int r = Random.Range(0, 100);

        if (r < 2) {
            return InventoryItem.Type.IronOre;
        }
        else if (r < 4)
        {
            return InventoryItem.Type.CopperOre;
        }
        else if (r < 6)
        {
            return InventoryItem.Type.Wood;
        }
        else if (r < 8)
        {
            return InventoryItem.Type.Vine;
        }
        else
        {
            return InventoryItem.Type.Stone;
        }
    }

    GameObject GetPrefabFromType(InventoryItem.Type type)
    {
        switch (type)
        {
            case InventoryItem.Type.Wood:
                return (GameObject)Instantiate(Resources.Load("Prefabs/NodePrefabs/WoodNode"));
            case InventoryItem.Type.Stone:
                return (GameObject)Instantiate(Resources.Load("Prefabs/NodePrefabs/StoneNode"));
            case InventoryItem.Type.Vine:
                return (GameObject)Instantiate(Resources.Load("Prefabs/NodePrefabs/VineNode"));
            case InventoryItem.Type.CopperOre:
                return (GameObject)Instantiate(Resources.Load("Prefabs/NodePrefabs/CopperOreNode"));
            case InventoryItem.Type.IronOre:
                return (GameObject)Instantiate(Resources.Load("Prefabs/NodePrefabs/IronOreNode"));
            default:
                return null;
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
