using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuttable2D : MonoBehaviour
{
    [SerializeField]
    private bool _shareVertices = false;

    [SerializeField]
    private bool _smoothVertices = false;

    public bool correct = false;
    Vector3 normal;
    float dist;
    Rigidbody rb;

    public bool ShareVertices
    {
        get
        {
            return _shareVertices;
        }
        set
        {
            _shareVertices = value;
        }
    }

    public bool SmoothVertices
    {
        get
        {
            return _smoothVertices;
        }
        set
        {
            _smoothVertices = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (correct && other.GetComponent<Cuttable2D>())
        {
            this.transform.position -= normal * dist;
            Destroy(rb);
            this.GetComponent<MeshCollider>().isTrigger = false;
            correct = false;
            this.normal = new Vector3();
            this.dist = 0;
        }
            
    }

    public void correctionNormal(Vector3 normal, float dist)
    {
        this.GetComponent<MeshCollider>().isTrigger = true;
        correct = true;
        this.normal = normal;
        this.dist = dist * 2;
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("hey2");
    //}
}
