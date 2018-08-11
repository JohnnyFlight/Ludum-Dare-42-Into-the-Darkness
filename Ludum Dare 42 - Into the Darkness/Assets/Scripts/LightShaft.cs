using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShaft : MonoBehaviour {

    public float lightRadius = 1.5f;

    [SerializeField]
    private bool lit = true;

	// Use this for initialization
	void Start () {
        SetLightSourceRadius(lightRadius);
        SetLightSourceActive(lit);
	}

    void Deactivate()
    {
        SetLightSourceActive(false);
    }

    void Activate()
    {
        SetLightSourceActive(true);

        //  TODO: Get GameManager to spawn a resource here
    }

    void SetLightSourceActive(bool active)
    {
        LightSource[] ls = GetComponentsInChildren<LightSource>(true);
        ls[0]?.gameObject.SetActive(active);
    }

    void SetLightSourceRadius(float radius)
    {
        LightSource ls = GetComponentInChildren<LightSource>();
        ls?.setRadius(lightRadius);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
