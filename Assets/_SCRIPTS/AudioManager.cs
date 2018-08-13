using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    List<AudioClip> clips = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> PickUpRefugee = new List<AudioClip>();

    [SerializeField]
    AudioSource boatSource = null;
    AudioSource boatSource2 = null;

    [SerializeField]
    AudioSource portSource = null;    
    
    int currentClip = 0;
	// Use this for initialization
	void Start () {
        if (boatSource == null)
            boatSource = GameObject.FindGameObjectWithTag("boat").GetComponent<AudioSource>();
        if (boatSource2 == null)
            boatSource2 = GameObject.Find("Boaty McBoatface/Camera").GetComponent<AudioSource>();
        if (portSource == null)
            portSource = GameObject.FindGameObjectWithTag("Port").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale == 0)
        {
            if(boatSource.isPlaying) boatSource.Pause();
        }
        if(Time.timeScale == 1)
        {
            if (!boatSource.isPlaying) boatSource.Play();
        }
	}

    public void setSound(int i)
    {
        boatSource.clip = clips[i];
        boatSource.loop = true;
        boatSource.volume = 0.3f;
        boatSource.Play();
        currentClip = i;
    }

    public void PlayWelcomeSound()
    {
        portSource.clip = clips[2];
        portSource.Play();
    }

    public void PlayPickUpRefugeeNoise()
    {
        boatSource2.clip = PickUpRefugee[Random.Range(0, 6)];
        boatSource2.pitch = Random.Range(0.5f, 1.5f);
        boatSource2.loop = false;
        boatSource2.Play();
    }

    public void PlayClip(int i)
    {
        boatSource2.PlayOneShot(clips[i]);

    }

    public int getCurrentClip()
    {
        return currentClip;
    }
}
