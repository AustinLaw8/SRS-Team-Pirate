using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    [SerializeField] public int id;
    [SerializeField] public float Cooldown;
    [SerializeField] public Player player;
    public float currentCooldown = 0;

    public Ability(int _id, float _Cooldown, Player _player)
    {
        this.id = _id;
        this.Cooldown = _Cooldown;
        this.player = _player;
    }
    public bool onCooldown()
    {
        if (currentCooldown <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void putOnCD()
    {
        player.putOnCooldown(this);
    }

    //Function to be overwritten when creating child class
    //Depends on Actor class (whatever we call enemies)(add this back in once implemented)
    public virtual void action() { }


    public int getID() { return this.id; }
    public float getCooldown() { return this.Cooldown; }
    public Player getPlayer() { return this.player; }


}

class Test : Ability
{

    public Test(int _id, float _Cooldown, Player _player) : base(_id, _Cooldown, _player)
    {
        id = _id;
        Cooldown = _Cooldown;
        player = _player;
    }
    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD");}
        else
        {
            Debug.Log("we testing");
            putOnCD();
        }

    }
}