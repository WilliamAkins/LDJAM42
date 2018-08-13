using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour {

    [SerializeField]
    GameObject boat;

    private float rotationSensitivity = 1f;

    [SerializeField]
    float distance = 50f;
    // Use this for initialization
    void Start () {
        transform.position = boat.transform.position + new Vector3(0, distance, -distance);
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(boat.transform.position, Vector3.up, (Input.GetAxis("Mouse X") * 100) * Time.deltaTime * rotationSensitivity);
    }

    public void setSensitivity(float i)
    {
        rotationSensitivity = i;
    }
}
