using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour {

    public enum ResourceType {Stone, Iron, Copper, Wood, Vine,};
    ResourceType MyResource;

    public Resource Stone;
    public Resource Wood;
    public Resource Vines;
    public Resource IronOre;
    public Resource CopperOre;


	// Use this for initialization

	void Awake () {
		
	}
    public void InitializeNode(ResourceType Type) {
        MyResource = Type;
    }
    int OnHit() {


        return 0;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
