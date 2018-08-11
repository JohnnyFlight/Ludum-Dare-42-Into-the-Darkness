using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        Stone,
        Wood,
        IronOre
    }

	// Use this for initialization
	void Start () {
		
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
            Destroy(this);
        }
    }
	// Update is called once per frame
	void Update () {
        if (inLight() == false) {
            inDark();
        }
	}
}
