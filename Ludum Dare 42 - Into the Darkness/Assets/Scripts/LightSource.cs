using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

    public float radius = 10.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setRadius(float newRadius) {
        radius = newRadius;

        //  Scale sprite to have a width and height of twice radius
        gameObject.transform.localScale = new Vector3(2.0f * radius, 2.0f * radius, 1.0f);
    }
}
