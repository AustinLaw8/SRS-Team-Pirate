using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeekSpawner : MonoBehaviour
{

    [SerializeField] public GameObject myProjectile;
    [SerializeField] public float firingHz = 1;
    [SerializeField] public float vertOffset = 0.0f;
    double currTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        //Debug.Log(currTime);
        if (currTime > (1.0/firingHz)) {
            Instantiate(myProjectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + vertOffset, gameObject.transform.position.z), Quaternion.identity);
            currTime = 0;
        }
    }
}
