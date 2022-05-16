using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] 

public class EnemyChaseProjectile : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] private float range = 5.0f; // Auto disappears when exceeding range
    [SerializeField] private float speed = 1.0f;

    private float distTrav = 0.0f;

    // Use to get source enemy transform?
    // Generated by enemy and passes in?
    [SerializeField] private GameObject sourceEnemy;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Only 1 player
        player = GameObject.Find("Player");
    }

    // Upon contact, deal damage and explode
    // Set player collider to be a trigger?
    private void OnTriggerStay2D(Collider2D playerTarget) 
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

    // Update is called once per frame
    void Update()
    {
        // Vector2 transform.position
        // player.transform.position
        // Translate, and maybe rotate(?)
        Vector2 projectileToPlayer = transform.position - player.transform.position;
        if (distTrav >= range) {
            Destroy(gameObject);
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        distTrav += step;
    }
}
