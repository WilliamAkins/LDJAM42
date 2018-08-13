using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour {


    [SerializeField]
    GameObject pauseMenu = null;
    bool paused = false;
	// Use this for initialization
	void Start () {
        if (pauseMenu == null)
            pauseMenu = GameObject.Find("Pause Menu Sub");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                unPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
	}

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        paused = true;
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        paused = false;
    }


}
