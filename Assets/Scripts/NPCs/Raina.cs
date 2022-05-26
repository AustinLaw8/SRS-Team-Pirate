using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raina : Interactable
{
    // Update is called once per frame
    public override void interact(Player player)
    {
        switch (player.stage) 
        {
            case 4:
                player.playDialogue("OddJobs_Speak_To_Raina", this.transform.name);
                break;
            default:
                Debug.Log("Not in the right stage for Raina");
                break;
        }
    }
}
