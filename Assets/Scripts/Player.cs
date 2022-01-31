using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Player : MonoBehaviour
{

    [SerializeField] private float speed=3f;
    [SerializeField] private List<Ability> abilities;
    [SerializeField] public AbilityManager abilityManager;
    
    private Rigidbody2D rb;
    private float x_vel;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        abilities.Add(new Test(0,3,this));
    }

    void Start()
    {
        x_vel = 0;
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

        /**
         * float yInput = input.Get<Vector2>().y;
         * if (yInput > 0) { 
         *  jump()
         * } elif (yInput < 0) {
         *    crouch() or dropdownplatform()
         * } else {
         *      do something? or do nothing. 
         * }
         **/
    }

    
    public void FixedUpdate()
    {
        rb.velocity = new Vector2(x_vel, rb.velocity.y);
    }

}
