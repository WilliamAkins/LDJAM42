using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
    public Button PlayGame;
    public Button ExitGame;

    void Start()
    {
        PlayGame.onClick.AddListener(Play);
        ExitGame.onClick.AddListener(Exit);
    }
    //RefuelAddOne.onClick.AddListener(RefuelShipOne);

    void Play()
    {
        SceneManager.LoadScene("trial");
    }

    void Exit()
    {
        Application.Quit();
    }

}
