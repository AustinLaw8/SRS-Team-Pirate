using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeekSpawner : MonoBehaviour
{

    [SerializeField] public GameObject myProjectile;
    [SerializeField] public double firingHz = 1;
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
            Instantiate(myProjectile, gameObject.transform.position, Quaternion.identity);
            currTime = 0;
        }
    }
}
