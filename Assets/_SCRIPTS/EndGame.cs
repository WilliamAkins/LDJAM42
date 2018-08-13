using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    private ResourceList resourceList;
    private Image fade;

    private float lerpTime = 0.0f;

    private TextMeshProUGUI title;

    private bool alreadyUpdated = false;

    private float savedRefugees = 0;
    private float percent = 0;

    private Button returnToMenu;

    private string[] messages = new string[5];

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Ending the game...");

        fade = GameObject.Find("EndGameCanvas/Fade").GetComponent<Image>();

        if (resourceList == null)
            resourceList = GameObject.FindGameObjectWithTag("boat").GetComponent<ResourceList>();

        GameObject.Find("UI").SetActive(false);

        returnToMenu = gameObject.transform.Find("EndGameCanvas/Fade/btnMenu").GetComponent<Button>();
        returnToMenu.onClick.AddListener(goToMenu);

        GameObject.FindGameObjectWithTag("boat").GetComponent<Movement>().enabled = false;

        messages[0] = "You didn't even save anyone?! What on Earth are you doing? Why do we bring you all the way out here anyway if you're just going to go home...";
        messages[1] = "A poor effort... At least you tried, I guess?";
        messages[2] = "Barely half, lots still perished. You need to do better!";
        messages[3] = "You saved over half. I'm sure you did your best, but it might've not been good enough!";
        messages[4] = "You saved a lot of butts today! You should be proud, and we all thank you for your effort!";
    }

    // Update is called once per frame
    void Update ()
    {
        if (alreadyUpdated == false)
        {
            alreadyUpdated = true;

            float totalRefugees = GameObject.Find("GameManager").GetComponent<SpawnRefugees>().getSpawnedRefugees();
            savedRefugees = resourceList.getTotalRefugees();
            percent = Mathf.Floor((savedRefugees / totalRefugees) * 100);

            gameObject.transform.Find("EndGameCanvas/Fade/Title").GetComponent<TextMeshProUGUI>().text = "You saved " + percent.ToString() + "% of the refugees!";

            int msgID = 0;
            if (fade.color.a == 1.0f)
            {
                if (savedRefugees == 0)
                {
                    msgID = 0;
                }
                else if (percent <= 25)
                {
                    msgID = 1;
                }
                else if (percent <= 50)
                {
                    msgID = 2;
                }
                else if (percent <= 75)
                {
                    msgID = 3;
                }
                else if (percent <= 100)
                {
                    msgID = 4;
                }
            }
            gameObject.transform.Find("EndGameCanvas/Fade/EndMsg").GetComponent<TextMeshProUGUI>().text = messages[msgID];
        }

        lerpTime += 0.25f * Time.deltaTime;

        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, Mathf.Lerp(0.0f, 1.0f, lerpTime));
    }

    private void goToMenu()
    {
        Debug.Log("Returning to menu...");
        SceneManager.LoadScene("MainMenu");
    }
}