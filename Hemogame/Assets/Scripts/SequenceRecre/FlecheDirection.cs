using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlecheDirection : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Transform finishTransform;

    [SerializeField]
    Transform capsule;

    [SerializeField]
    Transform fleche;

    [SerializeField]
    Vector3 flecheOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        capsule.transform.position = playerTransform.position;
        capsule.transform.LookAt(finishTransform);

        fleche.transform.position = playerTransform.position + flecheOffset;
        fleche.transform.rotation = Quaternion.Euler(capsule.transform.rotation.eulerAngles);

    }
}
