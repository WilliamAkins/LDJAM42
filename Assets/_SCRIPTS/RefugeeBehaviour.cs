using TMPro;
using UnityEngine;

public class RefugeeBehaviour : MonoBehaviour {

    private GameObject boat;

    [SerializeField]
    private float MovementSpeed = 16.0f;

    private bool movingToPlayer = false;
    private bool movingToLand = false;
    private bool callingOut = false;

    private Vector3 originalPos;
    private Quaternion originalRot;

    [SerializeField]
    private float RefugeeLifeTime = 10.0f;

    [SerializeField]
    private string[] callouts;

    private float curLifeTime;

    // Use this for initialization
    void Start ()
    {
        curLifeTime = RefugeeLifeTime;

        boat = GameObject.FindGameObjectWithTag("boat");

        //store the original position and rotation
        originalPos = transform.position;
        originalRot = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (movingToPlayer && !movingToLand)
        {
            //keeps the refugee looking at the player
            transform.LookAt(boat.transform);
            transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

            transform.position = Vector3.MoveTowards(transform.position, boat.transform.position, MovementSpeed * Time.deltaTime);

            curLifeTime -= 1.0f * Time.deltaTime;

            //attempt to make the refugee call out
            if (!callingOut && Random.Range(0, 4) == 1 && callouts.Length > 1)
            {
                callingOut = true;
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = callouts[Random.Range(0, callouts.Length)];
            }
            else callingOut = true;

            transform.GetChild(0).GetChild(0).LookAt(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());
            transform.GetChild(0).GetChild(0).rotation = transform.GetChild(0).GetChild(0).rotation * Quaternion.Euler(new Vector3(0, 180, 0));

            if (curLifeTime <= 0.0f)
            {
                movingToPlayer = false;
                movingToLand = true;
                callingOut = false;
                transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().text = "";
            }
        }
        else if(movingToLand)
        {
            returnToLand();
        }
    }

    private void returnToLand()
    {
        transform.LookAt(originalPos);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, originalPos, MovementSpeed * Time.deltaTime);
       
        //if the refugee is in its original position, then reset its rotation
        if (transform.position == originalPos)
        {
            transform.rotation = originalRot;

            //reset some values
            movingToLand = false;
            curLifeTime = RefugeeLifeTime;
        }
    }

    public void setMovementToBoat(bool newMovingToPlayer)
    {
        movingToPlayer = newMovingToPlayer;
    }

    public bool getMovementToBoat()
    {
        return movingToPlayer;
    }

    public void destroyRefugee()
    {
        Destroy(gameObject);
    }
}
