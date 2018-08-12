using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nodes : MonoBehaviour {

    InventoryItem.Type MyResource;

	// Use this for initialization

	void Awake () {
		
	}
    public void InitializeNode(InventoryItem.Type Type) {
        MyResource = Type;
    }


    int OnHit() {

        return 0;
    }


    public void SetType(ResourceType newType) {

        MyResource = newType;
    }


    void CreateResource (){

        Resource newInstance = gameObject.AddComponent(typeof(Resource)) as Resource;

        newInstance.SetType(MyResource);

        switch (MyResource)
        {
            case InventoryItem.Type.Stone:
                newInstance = StoneFab;
                break;
            case InventoryItem.Type.IronOre:
                newInstance = IronOreFab;
                break;
            case InventoryItem.Type.CopperOre:
                newInstance = CopperOreFab;
                break;
            case InventoryItem.Type.Wood:
                newInstance = WoodFab;
                break;
            case InventoryItem.Type.Vine:
                newInstance = VinesFab;
                break;
            default:
                break;
        }
        Vector3 NewPosition;

        NewPosition.x = this.transform.position.x + Random.Range(-1.0f, 1.0f);
        NewPosition.y = this.transform.position.y + Random.Range(-1.0f, 1.0f);
        NewPosition.z = this.transform.position.z - 0.1f;

        Resource instance = Instantiate(newInstance, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.X))
        {
            CreateResource();
        }
    }
}
