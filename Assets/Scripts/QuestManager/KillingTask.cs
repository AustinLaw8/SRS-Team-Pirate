using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingTask : Task
{
    public string mobName;

    public override string getDescription(){
        return $"Kill {mobName}s!";
    }

    public override void initialize(){
        base.initialize();
        // QuestManager.Instance.AddListener<KillEvent>(OnKill);
    }

    private void OnKill(){
        // if(event.name == mobName){
        //     curAmount++;
        //     Evaluate();
        // }
    }
}
