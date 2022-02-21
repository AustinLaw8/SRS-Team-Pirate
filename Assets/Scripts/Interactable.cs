using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool inRange;
    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(!inRange) return; // If not in range
        // if( true /* replace with condition checking for player interaction*/ ) return;  //If player not interacting

        interactAction.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;

        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;

        inRange = false;
    }
}
