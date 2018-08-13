using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRadius : MonoBehaviour {
    public int Segments = 50;
    public float xradius = 5;
    public float yradius = 5;
    LineRenderer line;

    public void PrepareRadius(GameObject GameobjectPosition)
    {
        line = GameobjectPosition.GetComponent<LineRenderer>();
        //line.SetVertexCount(Segments + 1);
        line.positionCount = (Segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    // Update is called once per frame
    public void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (Segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / Segments);
        }
    }

}
