using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public string EventDescription;
}

public class KillEvent : GameEvent
{
    public string mobName;

    public KillEvent(string name)
    {
        mobName = name;
    }
}

public class TalkToNpcEvent : GameEvent
{
    public string npcName; 
    public TalkToNpcEvent(string nm) 
    {
        npcName = nm;
    }

    public bool checkNpcCondition()
    {
        //check some sort of npc condition other than name
        return true;
    }
}
