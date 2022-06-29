using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecre : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float hauteur;

    [SerializeField]
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, hauteur, target.position.z);
        transform.position += transform.up * yOffset;
    }
}
