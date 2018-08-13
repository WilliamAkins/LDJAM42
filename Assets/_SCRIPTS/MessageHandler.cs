using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageHandler : MonoBehaviour {

    public GameObject message = null;
    int increment = 10;
    private GameObject messageToShow;

    int StringIndex = 0;
    string[] msg;
    float timer = 5;
    float timer2 = 2;
    float messageX = 0.0f;
    float messageY = 15.0f;

    //public void Start()
    //{
    //    messageToShow = new GameObject[3];
    //    msg = new string[3];

    //}

    //public void Update()
    //{
    //    //Debug.Log(msg[increment]);
    //    if (increment > 0)
    //    {
    //        for (int i = 0; i < 3; i++)
    //        {
    //            messageToShow[i] = Instantiate(message, Vector3.zero, Quaternion.identity) as GameObject;
    //            messageToShow[i].transform.parent = GameObject.Find("UI/Messages").transform;
    //            messageToShow[i].name = "Message" + i;

    //            RectTransform messageRect = messageToShow[i].GetComponent<RectTransform>();
    //            messageRect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    //            messageRect.localPosition = new Vector3(messageX, messageY, 0.0f);

    //            messageToShow[i].GetComponent<TextMeshProUGUI>().text = msg[i];

    //            messageY += 10;
    //            GameObject delete = GameObject.Find("UI/Messages/Message" + i);
    //            Destroy(delete, 5);
    //        }
    //    }
    //}


    public void AddMessage(string msg)
    {
        timer = 5;
        messageY -= 35;
        messageToShow = Instantiate(message, Vector3.zero, Quaternion.identity) as GameObject;
        messageToShow.transform.parent = GameObject.Find("UI/Messages").transform;

        RectTransform messageRect = messageToShow.GetComponent<RectTransform>();
        messageRect.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        messageRect.localPosition = new Vector3(messageX, messageY, 0.0f);

        messageToShow.GetComponent<TextMeshProUGUI>().text = msg;

        GameObject delete = GameObject.Find("UI/Messages/message(Clone)");
        Destroy(delete, 5);
    }

}