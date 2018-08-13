using System;
using UnityEngine;

/// <summary>
/// Pirate AI and method handling
/// </summary>
public class PirateAI : MonoBehaviour
{
    /// <summary>
    /// Local reference to player object
    /// </summary>
    private Transform player;

    /// <summary>
    /// Local reference to game terrain
    /// </summary>
    private Terrain terrain;

    /// <summary>
    /// Speed the pirates traverse at
    /// </summary>
    [SerializeField]
    private float maxSpeed = 30;

    /// <summary>
    /// Speed the pirates rotate at
    /// </summary>
    [SerializeField]
    private float maxRotate = 1;
    
    /// <summary>
    /// Maximum height permitted
    /// </summary>
    [SerializeField]
    private float maxHeight = 0;

    /// <summary>
    /// How far the pirate can sample terrain height to avoid beaching 
    /// </summary>
    [SerializeField]
    private float maxSampleDistance = 100;

    /// <summary>
    /// Maximum angle of correction the pirates take when approaching collision
    /// </summary>
    [SerializeField]
    private float correctionValue = 45;

    /// <summary>
    /// Range in which the pirates can follow the player
    /// </summary>
    [SerializeField]
    private float detectionRange = 300;

    /// <summary>
    /// Minimum percentage the pirates can steal from the player
    /// </summary>
    [SerializeField]
    private float stealMin = 0.1f;

    /// <summary>
    /// Maximum percentage the pirates can steal from the player
    /// </summary>
    [SerializeField]
    private float stealMax = 0.1f;

    /// <summary>
    /// Internal countdown
    /// </summary>
    private float countDown;

    public MessageHandler mh;
    /// <summary>
    /// Directs the pirates during RandomTravel
    /// </summary>
    private Quaternion targetRotation = new Quaternion(-1, -1, -1, -1);
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.GetChild(0).position, detectionRange);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("boat").GetComponent<Transform>();
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();
        targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0, UnityEngine.Random.Range(0, 180), 0));
        mh = GameObject.Find("UI/Messages").GetComponent<MessageHandler>();
    }

    void FixedUpdate()
    {
        //calc distance between pirate and player (magnitude of diff between both vectors)
        Vector3 direction = transform.position - player.transform.position;
        float distance = direction.magnitude;

        //test if pirate is within player detection range
        if (distance <= detectionRange && countDown <= 0) Follow();
        else RandomTravel();

        //test if pirate ship is within range for a steal
        if (distance <= (detectionRange / 2))
        {
            int stealToDo = UnityEngine.Random.Range(0, 5000);
            if (stealToDo == 1000) StealFuel();
            else if (stealToDo == 2500) StealFood();
            else if (stealToDo == 4000) StealMedicine();
        }
    }

    /// <summary>
    /// Makes the pirate travel pseudorandomly
    /// </summary>
    private void RandomTravel()
    {
        CheckHeight(); //ensure the boat isn't having height problems

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, maxRotate * Time.fixedDeltaTime);
        transform.position += transform.forward * maxSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Makes the pirate pursue the player
    /// </summary>
    private void Follow()
    {
        CheckHeight(); //ensure the boat isn't having height problems

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), maxRotate * Time.fixedDeltaTime);
        transform.position += transform.forward * maxSpeed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Facilitates the piracy operation of fuel stealing
    /// </summary>
    private void StealFuel()
    {
        int toSteal = (int)(player.GetComponent<ResourceList>().getCurrentFuelResources() * UnityEngine.Random.Range(stealMin, stealMax));
        if (toSteal == 0) return;
        player.GetComponent<ResourceList>().setCurrentFuelResources(-toSteal);
        mh.AddMessage("Pirate ship stole " + toSteal + " fuel!");
        Debug.Log("Pirate ship stole " + toSteal + " fuel!");
        ReverseCourse();
    }

    /// <summary>
    /// Facilitates the piracy operation of food stealing
    /// </summary>
    private void StealFood()
    {
        int toSteal = (int)(player.GetComponent<ResourceList>().getCurrentFoodResources() * UnityEngine.Random.Range(stealMin, stealMax));
        if (toSteal == 0) return;
        player.GetComponent<ResourceList>().setCurrentFoodResources(-toSteal);

        mh.AddMessage("Pirate ship stole " + toSteal + " food!");
        ReverseCourse();
    }

    /// <summary>
    /// Facilitates the piracy operation of medicine stealing
    /// </summary>
    private void StealMedicine()
    {
        int toSteal = (int)(player.GetComponent<ResourceList>().getCurrentMedicineResources() * UnityEngine.Random.Range(stealMin, stealMax));
        if (toSteal == 0) return;
        player.GetComponent<ResourceList>().setCurrentMedicineResources(-toSteal);
        mh.AddMessage("Pirate ship stole " + toSteal + " medicine!");
        ReverseCourse();
    }

    /// <summary>
    /// Performs a series of height tests to prevent the pirate from beaching itself
    /// </summary>
    private void CheckHeight()
    {
        //at random, sample the height of the map to the left and right to rotate the boat away from land
        if (UnityEngine.Random.Range(0, 100) == 50 && countDown <= 0)
        {
            Vector3 leftOfShip = transform.position + new Vector3(-maxSampleDistance, 0, 0);
            Vector3 rightOfShip = transform.position + new Vector3(maxSampleDistance, 0, 0);

            if (terrain.SampleHeight(leftOfShip) > terrain.SampleHeight(rightOfShip)) targetRotation = targetRotation * Quaternion.Euler(new Vector3(0, correctionValue, 0));
            else if (terrain.SampleHeight(leftOfShip) < terrain.SampleHeight(rightOfShip)) targetRotation = targetRotation * Quaternion.Euler(new Vector3(0, -correctionValue, 0)); 
        }

        //if countdown is being used, maintain the countdown
        if (countDown >= 0) countDown -= Time.fixedDeltaTime;
        else if (countDown <= 0) { }

        //check if the boat has collided with land
        if (transform.position.y >= maxHeight && countDown <= 0) ReverseCourse();
    }

    /// <summary>
    /// Directs pirate to go backwards 
    /// </summary>
    private void ReverseCourse()
    {
        countDown = 10f;
        targetRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 180, 0));
    }
}