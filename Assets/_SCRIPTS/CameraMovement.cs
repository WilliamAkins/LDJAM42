using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    GameObject boat;
    [SerializeField]
    Transform target;

    [SerializeField]
    float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    float rotationSensitivity = 10f;

    [SerializeField]
    float zoomSensitivity = 5000f;

    [SerializeField]
    float distance = 50f;

    Movement shipMove = null;

    // Use this for initialization
    void Start () {
        boat = GameObject.FindGameObjectWithTag("boat");
        target.gameObject.GetComponent<RotationObject>().setSensitivity(rotationSensitivity);

        transform.position = boat.transform.position + new Vector3(0, distance, -distance);

        shipMove = this.transform.parent.GetComponent<Movement>();

    }
	
	// Update is called once per frame
	void Update () {
        // Define a target position above and behind the target transform
        transform.LookAt(boat.transform.position);

        if (Input.GetButton("Mouse Down") || ControllerCheck())
            transform.RotateAround(boat.transform.position, Vector3.up, (Input.GetAxis("Mouse X") * 100) * Time.deltaTime * rotationSensitivity);

        if (Vector3.Distance(transform.position, boat.transform.position) >= 35 
            && Vector3.Distance(transform.position, boat.transform.position) <= 550)
            transform.Translate(Vector3.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity) * Time.deltaTime);

        if(Vector3.Distance(transform.position, boat.transform.position) <= 35 && Input.GetAxis("Mouse ScrollWheel") < -0.2)
        {
            transform.Translate(Vector3.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity) * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, boat.transform.position) >= 550 && Input.GetAxis("Mouse ScrollWheel") > 0.2)
        {
            transform.Translate(Vector3.forward * (Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity) * Time.deltaTime);
        }

        //MAKE GAMEPAD VERSION HERE


        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
    }

    private void FixedUpdate()
    {
        float input = Input.GetAxis("Horizontal");
        if (input != 0)
        {
            input *= shipMove.getRotation();
            transform.RotateAround(boat.transform.position, Vector3.up, -input);
        }
    }

    public bool ControllerCheck()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    return true;
                }
            }
        }
        return false;
    }
}
