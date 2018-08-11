using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FuelTank {

    public GameManager.FuelType type;
    public float quantity;

    public float maxFuel = 100.0f;

    public FuelTank(GameManager.FuelType type, float quantity)
    {
        this.type = type;
        this.quantity = quantity;
    }

    public float addFuel(float amount, GameManager.FuelType type)
    {
        if (amount <= 0) return 0.0f;

        //  If new type override type
        if (type != this.type)
        {
            this.type = type;
            quantity = 0;
        }

        //  Otherwise just add to existing type
        quantity += amount;
        if (quantity > maxFuel) {
            return quantity - maxFuel;
        }
        else
        {
            return 0.0f;
        }
    }
}
