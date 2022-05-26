using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] 
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyMeleeProjectile : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float range = 5.0f; // Auto disappears when exceeding range
    [SerializeField] private float speed = 1.0f;
    //[SerializeField] private float initVertForce = 0.0f;
    private SpriteRenderer sp;
    private float dir = -1.0f;

    private float distTrav = 0.0f;

    // Use to get source enemy transform?
    // Generated by enemy and passes in?
    [SerializeField] private GameObject sourceEnemy;

    //private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Only 1 player
        //player = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        Vector3 projectileToPlayer = transform.position - Player.MyPlayer.transform.position;
        /*
        if (projectileToPlayer.x > 0) {
            dir = -1;
        } else {
            dir = 1;
        }
        */
    }

    // Upon contact, deal damage and explode
    // Set player collider to be a trigger?
    /*
    void OnTriggerStay2D(Collider2D playerTarget) 
    {
        //Debug.Log("on player");
        if (playerTarget.gameObject.tag == "Player") {
            Damageable playerDamageHub = playerTarget.gameObject.GetComponent<Damageable>();
            if (playerDamageHub != null) {
                playerDamageHub.applyDamage(1);
            }
            Destroy(gameObject);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        // Vector2 transform.position
        // player.transform.position
        Vector3 projectileToPlayer = transform.position - Player.MyPlayer.transform.position;
        // Translate, and maybe rotate(?)
        if (distTrav >= range) {
            Destroy(gameObject);
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, transform.position - projectileToPlayer, step);

        sp.flipX = (projectileToPlayer.x < 0);

        distTrav += step;
    }
}
