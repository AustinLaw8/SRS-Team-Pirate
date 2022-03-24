using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    [SerializeField] private int health = 1;
    private bool targetable = false;

    // Start is called before the first frame update
    void Start()
    {
        targetable = true;
    }

    public void makeVulnerable() { targetable = true; }

    public void dropTarget() { targetable = false; }

    public void applyDamage(int damage) { 
        if (!targetable) {
            return;
        }
        health -= damage; 
        if (health <= 0) {
            dropTarget();
            // Full on destroy or save for later? 
            // Destroy reduces clutter and error chance and is "logical" 
            // but removes opportunity for enemy respawn, i.e. whatever 
            // spawner mechanism needs to keep making new instances.
            Destroy(gameObject);
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
