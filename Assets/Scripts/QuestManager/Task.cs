using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : ScriptableObject
{
    public string taskDescription;
    public bool completed{get; protected set;}

    public int reqAmount;
    public int curAmount;
    [HideInInspector] public TaskCompletionEvent completionEvent;

    public virtual void initialize() {
        completed = false;
        curAmount = 0;
        completionEvent = new TaskCompletionEvent();
    }

    public void checkTasks() {
        if (curAmount >= reqAmount) {
            complete();
        }
    }

    public void complete() {
        completed = true;
        completionEvent.Invoke(this);
        completionEvent.RemoveAllListeners();
    }

    public virtual string getDescription() {
        return taskDescription;
    }
    
}

public class TaskCompletionEvent: UnityEvent<Task> {}
