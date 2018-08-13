using UnityEngine;

/// <summary>
/// Handles storm effects on player
/// </summary>
public class Storm : MonoBehaviour
{
    /// <summary>
    /// Local reference of the main camera
    /// </summary>
    private Transform camera;

    /// <summary>
    /// Local reference of the player
    /// </summary>
    private Rigidbody playerRB;

    /// <summary>
    /// The amount the camera shakes 
    /// </summary>
    [SerializeField]
    private float shakeAmount = 2.5f;

    /// <summary>
    /// The distance the storm affects the player
    /// </summary>
    [SerializeField]
    private float affectDistance = 165f;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        playerRB = GameObject.FindGameObjectWithTag("boat").GetComponent<Rigidbody>();
    }

    void Update()
    {
        //calc distance between ship and stormfront (magnitude of diff between both vectors)
        Vector3 direction = playerRB.position - transform.position;
        float distance = direction.magnitude;
        
        //affect storm effects on player
        if (distance <= affectDistance)
        {
            camera.localPosition = camera.localPosition + Random.insideUnitSphere * shakeAmount;

            if (Random.Range(0, 10) == 5) playerRB.transform.position = new Vector3(playerRB.transform.position.x + Random.Range(-shakeAmount / 2, shakeAmount / 2), playerRB.transform.position.y, playerRB.transform.position.z);
        }
    }
}