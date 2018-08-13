using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private Terrain terrain;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float padding = 1000;

    void Start() { terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>(); }

    void Update()
    {
        //bound check X-axis
        if (transform.position.x >= terrain.terrainData.size.x - padding) transform.position = new Vector3(-transform.position.x + 50, transform.position.y, transform.position.z);
        if (transform.position.x <= (-terrain.terrainData.size.x) + padding) transform.position = new Vector3(-(transform.position.x) - 50, transform.position.y, transform.position.z);

        //bound check z-axis
        if (transform.position.z >= terrain.terrainData.size.z - padding) transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z + 50);
        if (transform.position.z <= (-terrain.terrainData.size.z) + padding) transform.position = new Vector3(transform.position.x, transform.position.y, -(transform.position.z) - 50);

    }
}