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


    public void SetType(InventoryItem.Type newType) {

        MyResource = newType;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.X))
        {
            GameManager.instance.CreateResource(MyResource, this.transform.position);
        }
    }
}
