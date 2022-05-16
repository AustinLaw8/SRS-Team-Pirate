using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{   
    [SerializeField] private string transitionTo;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player") {
            Debug.Log("Switching to scene: " + transitionTo);
            SceneManager.LoadScene(transitionTo, LoadSceneMode.Single);
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName(transitionTo));
        }
    }
}
