using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{   
    // Update is called once per frame
    public void OnClick ()
    {
        SceneManager.LoadScene("BeachZone");
    }
}
