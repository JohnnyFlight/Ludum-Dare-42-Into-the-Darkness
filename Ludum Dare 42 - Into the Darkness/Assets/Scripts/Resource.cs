using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    InventoryItem.Type MyType;

    // Use this for initialization
    void Start () {
        
	}

    public void SetSprite(String name)
    {
        Sprite spr = Resources.Load<Sprite>("Sprites/Pickups/" + name);
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (rend == null)
        {
            rend = gameObject.AddComponent<SpriteRenderer>();
        }
        rend.sprite = spr;
    }

    public void SetType(InventoryItem.Type Type) {
        MyType = Type;
    }

    bool inLight() {
        LightSource[] lightsArray = FindObjectsOfType<LightSource>();
        bool inLight = false;

        for (int sourceLoop = 0; sourceLoop < lightsArray.Length; sourceLoop++)
        {

            if (Vector3.Distance((lightsArray[sourceLoop].transform.position), (this.transform.position)) < lightsArray[sourceLoop].radius)
            {
                inLight = true;
                break;
            }
        }

        return inLight;
    }

    void inDark() {

        if (Random.Range(0, 100) == 0) {
            Destroy(this.gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
        if (inLight() == false) {
            inDark();
        }
	}
}
