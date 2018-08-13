using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : Torch {
    protected override void DepletedEvent()
    {
        //  Get associated player
        Player player = FindObjectOfType<Player>();
        if (player == null) return;

        player.MatchExtinguished();
    }
}
