using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nodes : MonoBehaviour {

    public enum ResourceType {Stone,    IronOre,    CopperOre,      Wood,   Vine,
                                        IronIngot,  CopperIngot,    Lumber, Rope,
                                                    CopperWire};
    ResourceType MyResource;

	// Use this for initialization

	void Awake () {
		
	}
    public void InitializeNode(ResourceType Type) {
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
            case ResourceType.Stone:
                newInstance.SetSprite("Stone");
                break;
            case ResourceType.IronOre:
                newInstance.SetSprite("IronOre");
                break;
            case ResourceType.CopperOre:
                newInstance.SetSprite("CopperOre");
                break;
            case ResourceType.Wood:
                newInstance.SetSprite("Wood");
                break;
            case ResourceType.Vine:
                newInstance.SetSprite("Vine");
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
