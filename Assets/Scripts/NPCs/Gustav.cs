using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gustav : Interactable
{
    // Update is called once per frame
    public override void interact(Player player)
    {
        switch (player.stage) 
        {
            case 2:
                player.playDialogue("OddJobs_Speak_To_Gustav", this.transform.name);
                break;
            case 5:
                player.playDialogue("OddJobs_Return_To_Gustav", this.transform.name);
                break;
            default:
                Debug.Log("Not in the right stage for Gustav");
                break;
        }
    }
}
