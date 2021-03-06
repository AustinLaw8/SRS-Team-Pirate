using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private static GameObject pauseCanvasInstance;
    [SerializeField] private GameObject pb;
    [SerializeField] private GameObject pauseWindow;

    // Start is called before the first frame update
    void Awake()
    {

        Time.timeScale = 1;

        if (pauseCanvasInstance != null && pauseCanvasInstance != this)
        {
            Destroy(this.transform.parent.gameObject);
        } else {
            pauseCanvasInstance = this.transform.parent.gameObject;
        }

        pb = GameObject.Find("PauseButton");
        pb.GetComponent<Button>().onClick.AddListener(delegate
        {

            Debug.Log(SceneManager.GetActiveScene());

            // Switch player control on/off?
            if (Player.MyPlayer.isControllable()) {
                Player.MyPlayer.revokeControl();
            } else {
                Player.MyPlayer.returnControl();
            }
            Player.MyPlayer.paused = !(Player.MyPlayer.paused);


            // Pause scene entirely?
            if (Time.timeScale == 1) {
                Time.timeScale = 0;
            } else {
                Time.timeScale = 1;
            }

            // Pause window activates
            pauseWindow.SetActive(!pauseWindow.activeSelf);

            // Things in questWindow need to become active
        });
    }

}
