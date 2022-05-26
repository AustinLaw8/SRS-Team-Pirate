using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassandra : Interactable
{
    // Update is called once per frame
    public override void interact(Player player)
    {
        switch (player.stage) 
        {
            case 3:
                player.playDialogue("OddJobs_Speak_To_Cassandra", "Milton");
                break;
            default:
                Debug.Log("Not in the right stage for Cassandra");
                break;
        }
    }
}
