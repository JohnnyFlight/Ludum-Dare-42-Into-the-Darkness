using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

    public float radius = 5.0f;

    Light MyLight;

	// Use this for initialization
	void Start () {
        if (MyLight == null)
            SetupLight();
    }

    void SetupLight()
    {
        MyLight = this.gameObject.AddComponent<Light>();
        MyLight.type = LightType.Spot;
        MyLight.spotAngle = 60.0f;
        MyLight.range = 12;
        MyLight.intensity = 20;
        MyLight.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10.0f);
    }
	
	// Update is called once per frame
	void Update () {
        int i = 0;
	}

    public void setRadius(float newRadius) {
        radius = newRadius;

        if (MyLight == null) SetupLight();
        
        MyLight.spotAngle = radius*12.0f;

        //  Scale sprite to have a width and height of twice radius
        gameObject.transform.localScale = new Vector3(2.0f * radius, 2.0f * radius, 1.0f);
    }
}
