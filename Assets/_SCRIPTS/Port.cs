using UnityEngine;

/// <summary>
/// Handles everything relating to user interaction with ports
/// </summary>
public class Port : MonoBehaviour
{
    /// <summary>
    /// Internal player reference
    /// </summary>
    private Rigidbody playerRB;

    /// <summary>
    /// reference to canvas
    /// </summary>
    public GameObject RefuelCanvas;

    /// <summary>
    /// Radius in which the port can be interacted with and/or automatically guides ships in
    /// </summary>
    [SerializeField]
    private float serviceRadius = 100f;

    /// <summary>
    /// Radius in which the player is considered docked into the port and the menu can be shown
    /// </summary>
    [SerializeField]
    private float embarkRadius = 25f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float maxSpeed = 5f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float maxRotate = 2f;

    private int counter = 0;
    public AudioManager AM;

    private bool shipInPort = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.GetChild(0).position, serviceRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.GetChild(0).position, embarkRadius);
    }

    void Start()
    {
        playerRB = GameObject.FindGameObjectWithTag("boat").GetComponent<Rigidbody>();
        if (AM == null) GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        //calc distance between ship and port (magnitude of diff between both vectors)
        Vector3 direction = playerRB.position - transform.GetChild(0).position;
        float distance = direction.magnitude;
        Movement playerMovement = playerRB.GetComponent<Movement>();

        //guide player if they are within service radius but not within embarkation radius
        if (distance <= serviceRadius && distance > (embarkRadius - 5) && Input.GetAxis("Horizontal") == 0 && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            playerMovement.SetCheckStatus(true);

            //cancel any rigidbody velocity so it does not interrupt with the docking process ***NEED TO FIND BETTER SOLUTION***
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;

            playerRB.transform.rotation = Quaternion.Slerp(playerRB.transform.rotation, Quaternion.LookRotation(transform.GetChild(0).position - playerRB.transform.position), maxRotate * Time.fixedDeltaTime);
            playerRB.transform.rotation = Quaternion.Euler(0, playerRB.transform.rotation.y, 0);
            playerRB.transform.position += playerRB.transform.forward * maxSpeed * Time.fixedDeltaTime;
        }

        if (distance > serviceRadius) playerMovement.SetCheckStatus(false);

        //port menu trigger
        if (distance <= embarkRadius)
        {
            RefuelCanvas.SetActive(true);
            if (!shipInPort) AM.PlayWelcomeSound();
            shipInPort = true;

        }
        else
        {
            RefuelCanvas.SetActive(false);
            shipInPort = false;
        }


    }
}



/// <summary>
/// 
/// </summary>
//public class Port : MonoBehaviour
//{
//   Collider[] NearbyShips;
//   Collider[] NearbyShips;

//   int LayerMask = (1 << 9);

//   private int Segments = 50;
//   private float xradius = 3;
//   private float yradius = 3;

//   LineRenderer line;


//   void Start()
//   {
//       line = gameObject.GetComponent<LineRenderer>();

//       line.positionCount = (Segments + 1);
//       line.useWorldSpace = false;
//       CreatePoints();
//   }

//   // Update is called once per frame
//   void Update () {
//       NearbyShips = Physics.OverlapSphere(transform.position, 2.0f, LayerMask);
//       for (int i = 0; i < NearbyShips.Length; i++)
//           Debug.Log(NearbyShips[i]);
//}

//   public void CreatePoints()
//   {
//       float x;
//       float z;

//       float angle = 20f;

//       for (int i = 0; i < (Segments + 1); i++)
//       {
//           x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
//           z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

//           //line.startColor = Color.red;
//           //line.endColor = Color.green;
//           line.SetPosition(i, new Vector3(x, 0, z));

//           angle += (360F / Segments);
//       }
//   }
//}