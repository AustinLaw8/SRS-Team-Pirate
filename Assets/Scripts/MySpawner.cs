using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpawner : MonoBehaviour
{

    [SerializeField] public GameObject mySpawn;
    [SerializeField] public float xOffset = 0.0f;
    bool spawnedYet = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !spawnedYet) {
            Instantiate(mySpawn, new Vector3(gameObject.transform.position.x + xOffset, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            spawnedYet = true;
        }
    }
}
