using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkToNpcTask: Task
{
    
    public string NpcName; //or NPC name
    public override string getDescription()
    {
        return $"Talk to {NpcName}";
    } 

    public override void initialize(){
        base.initialize();
        EventManager.Instance.AddListener<TalkToNpcEvent>(OnTalk); //add function later
    }

    private void OnTalk(TalkToNpcEvent evnt){
        if(true){ //replace with namecheck or other methods to check npc
            curAmount++;
            checkTasks();
        }
    }

   
}
