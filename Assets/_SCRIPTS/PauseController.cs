using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    [SerializeField]
    PauseSystem pauseSystem = null;
	// Use this for initialization
	void Start () {
        if (pauseSystem == null)
            pauseSystem = GameObject.Find("GameManager").GetComponent<PauseSystem>();

    }
	
	public void QuitGame()
    {
        Application.Quit();
    }

    public void unPause()
    {
        pauseSystem.unPauseGame();
    }
}
