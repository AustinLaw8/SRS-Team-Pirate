using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 3f;

    private Rigidbody2D rb;
    private float x_vel;

    // Variables for checking if Player is grounded
    private bool grounded;
    private BoxCollider2D groundedBox;
    [SerializeField] private LayerMask groundMask;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        foreach (BoxCollider2D bx in this.GetComponents<BoxCollider2D>())
            if (bx.isTrigger)
                groundedBox = bx;
        x_vel = 0;
    }

    void Start()
    {
    }

    public void OnMove(InputValue input)
    {
        x_vel = input.Get<Vector2>().x * speed;

        // Handles direction character is facing
        if (x_vel > 0)
        {
            // Character should face to the right
        }
        else if (x_vel < 0)
        {
            // Character should face the left
        }


        float yInput = input.Get<Vector2>().y;
        if (yInput > 0)
        {
            if (groundedBox.IsTouchingLayers(groundMask.value))
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        else if (yInput < 0)
        {
            // crouch() or dropdownplatform()
        }
        else
        {
            // do something? probably not
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(x_vel, rb.velocity.y);
    }

}
