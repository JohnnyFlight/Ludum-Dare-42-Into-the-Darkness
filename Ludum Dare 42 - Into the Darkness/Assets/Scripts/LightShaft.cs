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

    public void Deactivate()
    {
        SetLightSourceActive(false);
    }

    public void Activate()
    {
        SetLightSourceActive(true);

        //  TODO: Get GameManager to spawn a resource here
        GameManager.instance.CreateResource(InventoryItem.Type.Fuel, this.transform.position, 1.0f);
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
