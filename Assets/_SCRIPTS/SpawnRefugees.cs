using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRefugees : MonoBehaviour {
    [SerializeField]
    private int MaxRefugees = 4000;

    public Terrain terrain;

    private GameObject refugee;

    private int CurrentRefugees = 0;

    // Use this for initialization
    void Start () {
        Vector3 terrainPos = terrain.transform.position;

        for (float i = terrainPos.x; i < terrain.terrainData.size.x / 2; i += 8)
        {
            for (float j = terrainPos.z; j < terrain.terrainData.size.z / 2; j += 8)
            {
                float terrainWorldHeight = terrain.terrainData.GetHeight((int)i, (int)j);

                float curTerrainPos = terrain.SampleHeight(new Vector3(i, terrainWorldHeight, j));

                if (curTerrainPos >= 8.0f && curTerrainPos < 9.5f && CurrentRefugees < MaxRefugees && Random.Range(1, 100) <= 5)
                {
                    refugee = Instantiate(Resources.Load("refugee"), new Vector3(i, curTerrainPos + 1.5f, j), Quaternion.identity) as GameObject;

                    CurrentRefugees++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {}

    public int getSpawnedRefugees()
    {
        return CurrentRefugees;
    }
}
