using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankGatherer : Machine {

    public void IncreaseCrankTurn(){
        currentTimeSeconds += Time.deltaTime;
    }

    protected override void Production()
    {

        if (currentTimeSeconds > processTimeSeconds)
        {
            
            MakeItem();
            currentTimeSeconds -= processTimeSeconds;
        }
        
    }

    protected override bool ContinueRunning()
    {
        return true;
    }


    protected override Recipe GetRecipe()
    {
        return recipes[0];
    }
}
