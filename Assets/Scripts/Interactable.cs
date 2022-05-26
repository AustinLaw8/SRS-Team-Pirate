using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable]
// public class InteractEvent : UnityEvent<Player>
// {
// }

public abstract class Interactable : MonoBehaviour
{
    public bool inRange;

    public abstract void interact(Player player);
}
