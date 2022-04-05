using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] 

public class TargetReticle : MonoBehaviour
{

    private Rigidbody2D rb;
    
    [SerializeField] private float range = 6.0f;

    // Big issue = probably buggy when multiple enemies under reticle
    private Damageable enemyToHit;

    public Damageable getEnemyToHit() { 
        return enemyToHit;
    }

    // Use to get player transform
    // Use inspector to add player
    [SerializeField] private GameObject player;

    void Awake()
    {
        // Use inspector to add player? 
        // But can hardcode here if applicable
    }

    // Collision or trigger?
    // Trigger enemy vulnerable to being basic attacked
    private void OnTriggerStay2D(Collider2D enemyTarget) 
    {
        if (enemyTarget.gameObject.tag == "Enemy") {
            // Debug.Log("Aimed at enemy");

            // Player and enemyTarget.attachedRigidbody need to be 2D, which SHOULD happen
            Vector2 playerToEnemy = enemyTarget.attachedRigidbody.transform.position - player.transform.position;
            // Use enemy mask, maybe can control range of attack with Raycast(pos1,pos2,RANGE,mask) ?
            LayerMask enemyMask = LayerMask.GetMask("Enemy");
            //int enemyMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, playerToEnemy, 100, enemyMask);

            //If something was hit
            if (hit.collider != null)
            {
                //If the object hit is less than or equal to 6 units away from this object.
                if (hit.distance <= range)
                {
                    Damageable enemyDamageHub = enemyTarget.gameObject.GetComponent<Damageable>();
                    //Debug.Log("Enemy in range, dist:" + hit.distance + " point:" + hit.point);
                    if (enemyDamageHub != null) {
                        //Debug.Log("Got damageable class");
                        enemyToHit = enemyDamageHub;
                    }
                } else {
                    //Debug.Log("Enemy outside 6.0f, dist:" + hit.distance);
                }
            }
        }
    }

    // Using OnTriggerStay instead of OnTriggerEnter makes this method unnecessary
    private void OnTriggerExit2D(Collider2D enemyTarget)
    {
        if (enemyTarget.gameObject.tag == "Enemy") {
            // Trigger enemy not able to be basic attacked
            //Debug.Log("Deselected from enemy");
            enemyToHit = null;
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        transform.position = mousePosition;
    }

}