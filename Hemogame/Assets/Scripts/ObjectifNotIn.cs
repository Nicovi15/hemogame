using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifNotIn : Objectif
{
    public List<GameObject> objDangereux;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickObject") && other.GetComponent<PickableObject>().data.valid)
        {
            objDangereux.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickObject") && other.GetComponent<PickableObject>().data.valid)
        {
            objDangereux.Remove(other.gameObject);
        }
    }

    public override bool isDone()
    {
        return objDangereux.Count == 0;
    }
}
