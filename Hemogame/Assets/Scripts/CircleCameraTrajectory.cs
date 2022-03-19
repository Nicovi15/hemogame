using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCameraTrajectory : MonoBehaviour
{
    [SerializeField]
    LineRenderer cercleRenderer;

    public int numSegments;
    public float width;
    public float radius;

    public Transform center;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cercleRenderer.startWidth = width;
        cercleRenderer.endWidth = width;
        cercleRenderer.positionCount = numSegments + 1;
        cercleRenderer.useWorldSpace = false;

        float deltaTheta = (float)(2.0 * Mathf.PI) / numSegments;
        float theta = 0f;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            //Vector3 pos = center.position + new Vector3(x, 0, z);
            Vector3 pos = new Vector3(x, 0, z);
            cercleRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}

