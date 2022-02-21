using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class KillingTask : Task
{
   
    public string mobName;

    public override string getDescription(){
        return $"Kill {mobName}s!";
    }

    public override void initialize(){
        base.initialize();
        EventManager.Instance.AddListener<KillEvent>(OnKill);
    }

    private void OnKill(KillEvent evnt){
        if(evnt.mobName == mobName){ 
            curAmount++;
            checkTasks();
        }
    }
}
