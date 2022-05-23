using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 

public class EnemyRandMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float jumpActivateDist = 2.4f; // After moving jumpActivateDist, doing impulse up (careful for no double jump)
    [SerializeField] private float jumpForce = 2.0f;
    [SerializeField] private float turnDist = 0.4f; // After moving turnDist, decide again to kite forward or back
    [SerializeField] private float speed = 0.8f; // Speed of travel
    [SerializeField] private float range = 4.0f; // Kite limit, manually adjust to be slightly less than actual range of missle used
    private GameObject player;
    private float turnTimerDistTrav = 0.0f;
    private float jumpTimerDistTrav = 0.0f;
    Vector3 enemyToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Only 1 player
        player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
        enemyToPlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTimerDistTrav > turnDist) {
            enemyToPlayer = transform.position - player.transform.position; 
            turnTimerDistTrav = 0.0f;
        }
        if (jumpTimerDistTrav > jumpActivateDist) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpTimerDistTrav = 0.0f;
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        if (enemyToPlayer.magnitude < range) {
            // Kite forward
            /*
            if (enemyToPlayer.x > 0) {
                // enemy has greater x than player
                transform.position = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            }
            */
            transform.position = Vector3.MoveTowards(transform.position, transform.position + enemyToPlayer, step);
        } else {
            // Kite backward
            /*
            if (enemyToPlayer.x > 0) {
                // enemy has greater x than player
                transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            }
            */
            transform.position = Vector3.MoveTowards(transform.position, transform.position - enemyToPlayer, step);
        }
        turnTimerDistTrav += step;
        jumpTimerDistTrav += step;
    }
}
