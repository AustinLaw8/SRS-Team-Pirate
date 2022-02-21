using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToNpcTask: Task
{
    
    public string npcName; //or NPC name
    public override string getDescription()
    {
        return $"Talk to {npcName}";
    } 

    public override void initialize(){
        base.initialize();
        EventManager.Instance.AddListener<TalkToNpcEvent>(OnTalk); //add function later
    }

    private void OnTalk(TalkToNpcEvent evnt){
        if(evnt.npcName == npcName){ //replace with namecheck or other methods to check npc
            curAmount++;
            checkTasks();
        }
    }

   
}
