using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour {

    public enum TorchState {
        Unlit,
        Lit,
        Ember
    }

    public float fuelAmount = 0.0f;
    public float baseLightSourceRadius = 10.0f;
    public float lightSourceRadius;

    public TorchState state = TorchState.Unlit;

    public float fuelBurnRatePerSecond = 1.0f;
    
    //  This is the point at which the light starts to diminish
    public float lowFuelThreshold = 5.0f;

    public float maxFuel = 20.0f;

	// Use this for initialization
	void Start () {
        lightSourceRadius = baseLightSourceRadius;

        LightSource ls = GetComponentInChildren<LightSource>();
        ls?.setRadius(lightSourceRadius);

        ls?.gameObject.SetActive((state == TorchState.Lit));
	}
	
	// Update is called once per frame
	void Update () {
		if (state == TorchState.Lit)
        {
            depleteFuel();
            
            if (fuelAmount < lowFuelThreshold)
            {
                shrinkLight();
            }
        }
	}

    private void shrinkLight()
    {
        //  Decrease radius linearly
        LightSource ls = GetComponentInChildren<LightSource>();
        ls?.setRadius(getLightRadius());
    }

    private void depleteFuel()
    {
        fuelAmount -= fuelBurnRatePerSecond * Time.deltaTime;

        if (fuelAmount <= 0.0f) {
            fuelAmount = 0.0f;
            changeState(TorchState.Ember);
        }
    }

    private void changeState(TorchState newState)
    {
        if (state == newState) return;

        state = newState;

        switch (state)
        {
            case TorchState.Ember:
                extinguish();
                break;
            case TorchState.Lit:
                lightTorch();
                break;
            default:
                //  A torch will never be able to become unlit
                break;
        }
        
    }

    //  Returns amount of excess fuel
    public float addFuel(float amount) {
        if (amount <= 0.0f) return 0.0f; ;

        fuelAmount += amount;

        if (state == TorchState.Unlit || state == TorchState.Ember)
        {
            this.lightTorch();
        }

        if (fuelAmount > maxFuel) return fuelAmount - maxFuel;
        else return 0.0f;
    }

    private void extinguish()
    {
        //  Deactivate light source
        LightSource ls = GetComponentInChildren<LightSource>();
        ls?.gameObject.SetActive(false);
    }

    private void lightTorch()
    {
        //  TODO: Activate light source and set radius to max
        //  Deactivate light source
        LightSource ls = GetComponentInChildren<LightSource>();
        ls?.gameObject.SetActive(true);
        ls?.setRadius(baseLightSourceRadius);
    }

    float getLightRadius() {
        if (state != TorchState.Lit) return 0;

        if (fuelAmount >= lowFuelThreshold) return baseLightSourceRadius;

        return (fuelAmount / lowFuelThreshold * baseLightSourceRadius);
    }
}
