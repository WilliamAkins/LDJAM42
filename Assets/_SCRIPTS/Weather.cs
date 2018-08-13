using UnityEngine;

/// <summary>
/// Handles generating weathers in game as a form of hinderance
/// </summary>
public class Weather : MonoBehaviour
{
    /// <summary>
    /// Number of weather objects to initialise
    /// </summary>
    [SerializeField]
    private int noOfWeather = 5;

    /// <summary>
    /// Local reference to the water map so its size can be obtained and used for RNG calcs
    /// </summary>
    private Transform water;

    /// <summary>
    /// 
    /// </summary>
    private Terrain terrain;

    /// <summary>
    /// Array of prefabs for possible weathers
    /// </summary>
    [SerializeField]
    private GameObject[] weatherPrefabs;

	void Start()
    {
        //catch if no weather prefabs are specified
        if (weatherPrefabs.Length == 0)
        {
            Debug.Log("Cannot generate weather: no prefabs specified");
            return;
        }

        //get local ref of water transform
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<Transform>();

        //get local ref of terrain 
        terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>();

        //generate weather objects
        for (int i = 0; i < noOfWeather; i++)
        {
            //randomise weather object's position
            Vector3 randPos = new Vector3(Random.Range(-(water.transform.localScale.x / 2), (terrain.terrainData.size.x / 2)), -2, Random.Range(-(terrain.terrainData.size.x / 2), (terrain.terrainData.size.x / 2)));

            //randomly select weather prefab
            GameObject selectedPrefab = weatherPrefabs[Random.Range(0, weatherPrefabs.Length)];

            //initialise prefab
            GameObject obj = Instantiate(selectedPrefab, randPos, Quaternion.Euler(90, 0, 0));

            //make new object a child of water
            obj.transform.SetParent(water);
        }
    }
}