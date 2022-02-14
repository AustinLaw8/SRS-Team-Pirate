using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] public List<Ability> abilities = new List<Ability> { };
    [SerializeField] private float jumpForce = 3f;

    private Rigidbody2D rb;
    private float x_vel;
    private Vector2 facing;
    [SerializeField] private LayerMask interactableMask;
    private ContactFilter2D interactableFilter;
    // Variables for checking if Player is grounded
    private bool grounded;
    private BoxCollider2D groundedBox;
    private BoxCollider2D interactBox;
    [SerializeField] private LayerMask groundMask;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        abilities.Add(new Test1(this));
        abilities.Add(new Test2(this));
        abilities.Add(new Test3(this));
        abilities.Add(new Test4(this));
        foreach (BoxCollider2D bx in this.GetComponents<BoxCollider2D>())
        {
            if (bx.isTrigger)
            {
                groundedBox = bx;
            }
            else
            {
                interactBox = bx;
            }
        }

        interactableFilter = new ContactFilter2D();
        interactableFilter.layerMask = interactableMask;
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
            facing = Vector2.right;
        }
        else if (x_vel < 0)
        {
            facing = Vector2.left;
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

    public void OnInteract(InputValue input)
    {
        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D temp = new ContactFilter2D();
        temp.layerMask = groundMask;
        interactBox.OverlapCollider(temp.NoFilter(), results);
        foreach (var e in results)
        {
            Debug.Log(e.transform.name);
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(x_vel, rb.velocity.y);
        foreach (var ability in abilities)
        {
            ability.currentCooldown -= Time.deltaTime;
            if (ability.currentCooldown < 0)
            {
                ability.currentCooldown = 0;
            }
        }
    }

    public void OnUseAbility1(InputValue input) { abilities[0].action(); }

    public void OnUseAbility2(InputValue input) { abilities[1].action(); }

    public void OnUseAbility3(InputValue input) { abilities[2].action(); }

    public void OnUseAbility4(InputValue input) { abilities[3].action(); }

    public void putOnCooldown(Ability ability)
    {
        if (!abilities.Contains(ability))
        {
            return;
        }
        else
        {
            ability.currentCooldown = ability.getCooldown();
        }
    }
}
