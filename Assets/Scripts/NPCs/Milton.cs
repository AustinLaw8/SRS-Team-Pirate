using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milton : Interactable
{
    // Update is called once per frame
    public override void interact(Player player)
    {
        switch (player.stage) 
        {
            case 1:
                player.playDialogue("Arrival_Speak_To_Milton", "Milton");
                break;
            default:
                Debug.Log("Not in the right stage for Milton");
                break;
        }
    }
}
