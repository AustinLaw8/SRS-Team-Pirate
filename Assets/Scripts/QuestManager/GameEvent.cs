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
