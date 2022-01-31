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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(collision.gameObject);
            Debug.Log("hit!");
        }
    }

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
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
