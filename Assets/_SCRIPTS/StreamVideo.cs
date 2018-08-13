using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
    public RawImage ri;
    public VideoPlayer vp;


	// Use this for initialization
	void Start () {
        StartCoroutine(PlayVideo());
	}

    IEnumerator PlayVideo()
    {
        vp.Prepare();
        WaitForSeconds sec = new WaitForSeconds(1);
        while(!vp.isPrepared)
        {
            yield return sec;
            break;
        }
        ri.texture = vp.texture;
        vp.Play();
    }
	
}
