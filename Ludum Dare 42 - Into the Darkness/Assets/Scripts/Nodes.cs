using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nodes : MonoBehaviour {

    [SerializeField]
    InventoryItem.Type MyResource;

    public GameObject StoneFab;
    public GameObject WoodFab;
    public GameObject VinesFab;
    public GameObject IronOreFab;
    public GameObject CopperOreFab;

    // Use this for initialization

    void Awake () {
		
	}
    public void InitializeNode(InventoryItem.Type Type) {
        MyResource = Type;
    }


    int OnHit() {

        return 0;
    }

    public InventoryItem.Type GetResourceType() {

        return MyResource;
    }

    public void SetType(InventoryItem.Type newType) {

        MyResource = newType;
    }

    public void SetPosition(float x, float y) {
        this.gameObject.transform.position.Set(x, y, 0.0f);
    }

    public void CreateResource()
    {

        GameObject go = new GameObject();

        Resource newInstance = go.AddComponent(typeof(Resource)) as Resource;

        newInstance.SetType(MyResource);

        switch (MyResource)
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
            default:
                break;
        }
        Vector3 NewPosition;

        NewPosition.x = this.transform.position.x + Random.Range(-1.0f, 1.0f);
        NewPosition.y = this.transform.position.y + Random.Range(-1.0f, 1.0f);
        NewPosition.z = this.transform.position.z - 0.1f;

        go.transform.position = NewPosition;
    }

    // Update is called once per frame
    void Update () {
       /*if (Input.GetKeyUp(KeyCode.X))
        {
            GameManager.instance.CreateResource(MyResource, this.transform.position);
        }*/
    }
}
