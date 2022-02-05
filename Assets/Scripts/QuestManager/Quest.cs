using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct info{
        public string name;
        public string description;
        public Sprite icon;
    }

    public info information;

    [System.Serializable]
    public struct stat{
        public int xp;
        public int gold;
    }

    public stat reward = new stat {gold = 0, xp = 0};
    public bool completed {get; protected set;}
    public QuestCompletionEvent completionEvent;

    public List<Task> tasks;

    public void initialize(){
        completed = false;
        completionEvent = new QuestCompletionEvent();

        foreach (var task in tasks){
            task.initialize();
            task.completionEvent.AddListener(delegate { checkTasks(); });
        }
    }

    public void checkTasks(){
        completed = tasks.All( task => task.completed );

        if(completed){
            giveReward();
            completionEvent.Invoke(this);
            completionEvent.RemoveAllListeners();
        }
    }
    
    public void giveReward(){
        // put xp and gold into player's inventory
    }
}

public class QuestCompletionEvent : UnityEvent<Quest> {}