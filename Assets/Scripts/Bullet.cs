using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] 

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveSpeed = 10;

    void Awake()
    {

    }

    // Collision or trigger?
    private void OnTriggerEnter2D(Collider2D enemyTarget)
    {
        if (enemyTarget.gameObject.tag == "Enemy") {
            // Trigger enemy vulnerable to being basic attacked
            Debug.Log("Aimed at enemy");
        }
    }

    private void OnTriggerExit2D(Collider2D enemyTarget)
    {
        if (enemyTarget.gameObject.tag == "Enemy") {
            // Trigger enemy not able to be basic attacked
            Debug.Log("Deselected from enemy");
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        transform.position = mousePosition;
    }

    /*

    // Adjust the speed for the application.
    public float speed = 1.0f;

    // The target position.
    private Vector3 target;

    void Update()
    {
        // Grab mouse values and place on the target.
        target = Mouse.current.position.ReadValue();

        // Move our position a step closer to the target.
        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            
        }
    }
    */

}
