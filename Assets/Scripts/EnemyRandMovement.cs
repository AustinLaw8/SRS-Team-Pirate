using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 

public class EnemyRandMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float turnDist = 0.4f; // After moving turnDist, decide again to kite forward or back
    [SerializeField] private float speed = 0.8f; // Speed of travel
    [SerializeField] private float range = 4.0f; // Kite limit, manually adjust to be slightly less than actual range of missle used
    private GameObject player;
    private float distTrav = 0.0f;
    Vector3 enemyToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // Only 1 player
        player = GameObject.Find("Player");
        enemyToPlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (distTrav > turnDist) {
            enemyToPlayer = transform.position - player.transform.position; 
            distTrav = 0.0f;
        }
        float step = speed * Time.deltaTime; // calculate distance to move
        if (enemyToPlayer.magnitude < range) {
            // Kite forward
            transform.position = Vector3.MoveTowards(transform.position, transform.position + enemyToPlayer, step);
        } else {
            // Kite backward
            transform.position = Vector3.MoveTowards(transform.position, transform.position - enemyToPlayer, step);
        }
        distTrav += step;
    }
}
