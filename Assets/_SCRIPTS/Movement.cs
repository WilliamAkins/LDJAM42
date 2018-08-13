using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float maxMovementSpeed = 35.0f;
    

    [SerializeField]
    private float throttleAcceleration = 0.02f;
    

    [SerializeField]
    ResourceList resources = null;

    AudioManager audio = null;

    public float fuelEfficiency = 2f;

    private Vector3 rotation = Vector3.zero;

    private float movementSpeed = 0.0f;

    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private float maxHeight = -0;

    private Quaternion desiredRotate;

    private bool skipCheck = false;

    /// <summary>
    /// Local reference to game terrain
    /// </summary>
    private Terrain terrain;

    /// <summary>
    /// How far the pirate can sample terrain height to avoid beaching 
    /// </summary>
    [SerializeField]
    private float maxSampleDistance = 100;

    /// <summary>
    /// Maximum angle of correction the pirates take when approaching collision
    /// </summary>
    [SerializeField]
    private float correctionValue = 90;

    /// <summary>
    /// Used as a cooldown when a boat hitting land flip occurs
    /// </summary>
    private float untilNextFlip = 0;

    public float getRotation()
    {
        return rotationSpeed;
    }

    private void Start()
    {
        if (resources == null)
        {
            resources = GetComponent<ResourceList>();
        }
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();

        if (audio == null)
            audio = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {

        movementSpeed = Mathf.Clamp(movementSpeed, 0.0f, maxMovementSpeed);

        //increase or decrease throttle
        float v = Input.GetAxisRaw("Throttle");
        if (v <= -0.1 && resources.getCurrentFuelResources() > 0)
        {
            resources.CurrentFuelResources -= (Time.fixedDeltaTime / fuelEfficiency);
            //resources.setCurrentFuelResources(resources.getCurrentFuelResources() - (Time.fixedDeltaTime / fuelEfficiency));
            movementSpeed -= v * throttleAcceleration;
            if(audio.getCurrentClip() != 0)
            {
                audio.setSound(0);
            }
            
        }
        else
        {
            if (audio.getCurrentClip() != 1)
            {
                audio.setSound(1);
            }
            movementSpeed -= throttleAcceleration;
        }
        transform.Translate(0, 0, movementSpeed * Time.fixedDeltaTime);
        //increase or decrease the rotation
        rotation = new Vector3(0, Input.GetAxisRaw("Horizontal"), 0);
        rotation *= rotationSpeed;
        
        transform.Rotate(rotation);
        
        if (untilNextFlip > 0f)
        {
            if (untilNextFlip >= 4f) transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotate, 2f * Time.fixedDeltaTime);
            untilNextFlip -= Time.fixedDeltaTime;
        }

        if (transform.position.y >= maxHeight && untilNextFlip <= 0f && !skipCheck)
        {
            Vector3 leftOfShip = transform.position + new Vector3(-maxSampleDistance, 0, 0);
            Vector3 rightOfShip = transform.position + new Vector3(maxSampleDistance, 0, 0);

            Debug.Log(terrain.SampleHeight(leftOfShip) + " : " + terrain.SampleHeight(rightOfShip));

            if (terrain.SampleHeight(leftOfShip) > terrain.SampleHeight(rightOfShip))
            {
                desiredRotate = Quaternion.Euler(transform.rotation.eulerAngles) * Quaternion.Euler(new Vector3(0, correctionValue, 0));
                untilNextFlip = 5f;
            }
            else if (terrain.SampleHeight(leftOfShip) < terrain.SampleHeight(rightOfShip))
            {
                desiredRotate = transform.rotation * Quaternion.Euler(new Vector3(0, -correctionValue, 0));
                untilNextFlip = 5f;
            }
        }

            //if (untilNextFlip > 0f)
            //{
            //    if (untilNextFlip >= 4f) transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotate, 2f * Time.fixedDeltaTime);
            //    untilNextFlip -= Time.fixedDeltaTime;
            //}

            //if (transform.position.y >= maxHeight && untilNextFlip <= 0f && !skipCheck)
            //{
            //    transform.Translate(0, 0, -movementSpeed * Time.fixedDeltaTime);
            //    desiredRotate = Quaternion.Euler(transform.rotation.eulerAngles) * Quaternion.Euler(new Vector3(0, 180, 0));
            //    desiredRotate = Quaternion.Euler(0, desiredRotate.eulerAngles.y, 0);
            //    untilNextFlip = 5f;
            //}

            //Debug.Log("throttle = " + rotation);
    }

    public bool GetCheckStatus()
    {
        return skipCheck;
    }

    public void SetCheckStatus(bool nStatus)
    {
        skipCheck = nStatus;
    }
}