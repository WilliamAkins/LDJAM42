using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceMarker : MonoBehaviour {

    public GameObject port = null;
    public GameObject boat = null;
    
	// Use this for initialization
	void Start () {
		if(boat == null)
            boat = GameObject.FindGameObjectWithTag("boat");
        

        if (port == null)
            port = GameObject.FindGameObjectWithTag("Port");
	}
	
	// Update is called once per frame
	void Update () {

        //float z = Vector3.Distance(port.transform.position, this.transform.position);
        //float x = port.transform.position.x - this.transform.position.x;
        //float y = port.transform.position.y - this.transform.position.y;
        //Debug.Log(x + " + " +  y + " + " + z);
        //float angle = Mathf.Acos((Mathf.Pow(x, 2.0f) + Mathf.Pow(y, 2.0f) - Mathf.Pow(z, 2.0f)) / (2 * x * y));
        //angle *= Mathf.Rad2Deg;
        ////if (x > 0)angle = -angle;
        //Debug.Log(angle);
        //this.transform.rotation = Quaternion.Euler(0, angle, 0);
        transform.position = boat.transform.position + new Vector3(0, -3 -boat.transform.position.y, 0);
        transform.LookAt(port.transform.position);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y + 180, 0.0f);
    }
}
