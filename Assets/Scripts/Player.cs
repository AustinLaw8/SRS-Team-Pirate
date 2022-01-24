using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] 

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    void Awake() 
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void OnMove(InputValue input)
    {
        Vector2 direction = input.Get<Vector2>();
        // TODO: Temporary; this should (probably) only set x velocity, we will do jumping separately
        rb.velocity = direction;
    }


}
