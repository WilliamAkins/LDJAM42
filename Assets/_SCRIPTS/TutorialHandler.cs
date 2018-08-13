using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialHandler : MonoBehaviour {
    [SerializeField]
    private ResourceList boatResources = null;
    [SerializeField]
    private Toggle rotate = null;
    [SerializeField]
    private Toggle throttle = null;
    [SerializeField]
    private Toggle refugee = null;
    [SerializeField]
    private Toggle dropoff = null;
    [SerializeField]
    private TextMeshProUGUI complete = null;
    // Use this for initialization
    void Start () {
        if (rotate == null) rotate = GameObject.Find("Rotate").GetComponent<Toggle>();
        if (throttle == null) throttle = GameObject.Find("Throttle").GetComponent<Toggle>();
        if (refugee == null) refugee = GameObject.Find("Refugee").GetComponent<Toggle>();
        if (dropoff == null) dropoff = GameObject.Find("Dropoff").GetComponent<Toggle>();
        if (complete == null) complete = GameObject.Find("Complete").GetComponent<TextMeshProUGUI>();
        complete.enabled = false;
        if (boatResources == null) boatResources = GameObject.FindGameObjectWithTag("boat").GetComponent<ResourceList>();

	}
	
	// Update is called once per frame
	void Update () {
        rotated();
        throttled();
        refugees();
        dropoffCheck();
        completed();

    }

    public void EndTutorial()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    private void rotated()
    {
        if(Input.GetAxis("Horizontal") >= 0.2 || Input.GetAxis("Horizontal") <= -0.2)
        {
            rotate.isOn = true;

            ColorBlock cb = rotate.colors;
            cb.disabledColor = new Color(0, 1, 0);
            rotate.colors = cb;
        }
    }

    private void throttled()
    {
        if(Input.GetAxis("Throttle") <= -0.2)
        {
            throttle.isOn = true;
            ColorBlock cb = throttle.colors;
            cb.disabledColor = new Color(0, 1, 0);
            throttle.colors = cb;
        }
    }

    private void refugees()
    {
        if(boatResources.getCurrentAmountOfRefugees() != 0)
        {
            refugee.isOn = true;
            ColorBlock cb = refugee.colors;
            cb.disabledColor = new Color(0, 1, 0);
            refugee.colors = cb;
        }
    }
    private void dropoffCheck()
    {
        if(refugee.isOn == true && boatResources.getCurrentAmountOfRefugees() == 0)
        {
            dropoff.isOn = true;
            ColorBlock cb = dropoff.colors;
            cb.disabledColor = new Color(0, 1, 0);
            dropoff.colors = cb;
        }
    }

    private void completed()
    {
        if(refugee.isOn && dropoff.isOn && throttle.isOn && rotate.isOn)
        {
            complete.enabled = true;
        }
    }
    
}
