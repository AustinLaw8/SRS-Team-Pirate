using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject mySpawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") {
            Instantiate(mySpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
