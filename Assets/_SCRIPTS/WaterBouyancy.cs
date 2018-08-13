using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBouyancy : MonoBehaviour {
    public float UpwardForce = 9.81f; // 9.81 is the opposite of the default gravity, which is 9.81. If we want the boat not to behave like a submarine the upward force has to be higher than the gravity in order to push the boat to the surface
    private bool isInWater = false;

    [SerializeField]
    List<string> tags = new List<string>();

    List<Collider> list = new List<Collider>();

    void Start()
    {
        list = new List<Collider>();
    }
    

    void OnTriggerEnter(Collider collider)
    {
        if (tags.Contains(collider.tag) && !list.Contains(collider))
        {
            list.Add(collider);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (list.Contains(collider))
        {
            //removes it from the list
            list.Remove(collider);
        }
    }

    void FixedUpdate()
    {
        foreach (Collider c in list)
        {
            if (c != null)
            {
                Vector3 force = c.transform.up * UpwardForce;
                c.transform.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Acceleration);
            }
        }
    }
	
}
