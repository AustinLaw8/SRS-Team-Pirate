using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public bool isPartOfQuest; // Only on if NPC has something to do with quest

    public void Talk(){
        // Do talking stuff

        Debug.Log($"Talking to {npcName}");
        
        if(!isPartOfQuest) return;
        EventManager.Instance.QueueEvent(new TalkToNpcEvent(npcName));
    }
}
