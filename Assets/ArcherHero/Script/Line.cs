using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lr;

    void Awake()
    {
      lr=  GetComponent<LineRenderer>();
    }
    public void RenderLine(Vector3 startPoint,Vector3 endPoint)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;
        lr.SetPositions(points); 
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
