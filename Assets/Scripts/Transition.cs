using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{   
    [SerializeField] private string transitionTo;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 destination;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") {
            Debug.Log("Switching to scene: " + transitionTo);
            SceneManager.LoadScene(transitionTo);
            player.transform.position = destination;
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName(transitionTo));
        }
    }
}
