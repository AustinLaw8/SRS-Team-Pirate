using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    [SerializeField] public int id;
    [SerializeField] public float Cooldown;
    //[SerializeField] public Player player;
    public float currentCooldown = 0;

    public Ability(/*Player _player*/)
    {
        //this.player = _player;
    }
    public bool onCooldown()
    {
        return currentCooldown > 0;
    }

    public void putOnCD()
    {
        Player.MyPlayer.putOnCooldown(this);
    }

    //Function to be overwritten when creating child class
    //Depends on Actor class (whatever we call enemies)(add this back in once implemented)
    public virtual void action() { }


    public int getID() { return this.id; }
    public float getCooldown() { return this.Cooldown; }
    //public Player getPlayer() { return this.player; }


}

class Test1 : Ability
{

    public Test1(/*Player _player) : base(_player*/)
    {
        id = 0;
        Cooldown = 3;
        //player = _player;
    }
    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD");}
        else
        {
            Debug.Log("Ability 1");
            putOnCD();
        }

    }
}

class Test2 : Ability
{

    public Test2(/*Player _player) : base(_player*/)
    {
        id = 1;
        Cooldown = 5;
        //player = _player;
    }
    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD");}
        else
        {
            Debug.Log("Ability 2");
            putOnCD();
        }

    }
}

class Test3 : Ability
{

    public Test3(/*Player _player) : base(_player*/)
    {
        id = 2;
        Cooldown = 1;
        //player = _player;
    }
    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD");}
        else
        {
            Debug.Log("Ability 3");
            putOnCD();
        }

    }
}

class Test4 : Ability
{

    public Test4(/*Player _player) : base(_player*/)
    {
        id = 3;
        Cooldown = 20;
        //player = _player;
    }
    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD");}
        else
        {
            Debug.Log("Ability 4");
            putOnCD();
        }

    }
}

class BasicAttack : Ability {

    private TargetReticle basAtkRet;

    public BasicAttack(/*Player _player, */TargetReticle _basAtkRet) /*: base(_player) */
    {
        basAtkRet = _basAtkRet;
        id = 4;
        Cooldown = 0.1f; // Needs attack speed
        //player = _player;
    }

    public override void action()
    {
        if (onCooldown()) { Debug.Log("CD: BasicAttack");}
        else
        {
            //Debug.Log("BasicAttack");
            if (basAtkRet != null && basAtkRet.getEnemyToHit() != null) {
                basAtkRet.getEnemyToHit().applyDamage(1); // Changeable damage?
            }

            putOnCD();
        }
    }

}