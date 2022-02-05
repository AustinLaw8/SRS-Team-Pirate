using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : ScriptableObject
{
    // Start is called before the first frame update
    public string taskDescription;
    public int reqAmount;
    public bool completed{get; protected set;}
    public int curAmount;
    public TaskCompletionEvent completionEvent;

    public void initialize() {
        completed = false;
        curAmount = 0;
        completionEvent = new TaskCompletionEvent();
    }

    public bool checkTasks() {
        if (curAmount >= reqAmount) {
            complete();
            return true;
        }
        else return false;
    }

    void complete() {
        completed = true;
        completionEvent.Invoke(this);
    }

    public string getDescription() {
        return taskDescription;
    }

    

    
}

public class TaskCompletionEvent: UnityEvent<Task> {}
