using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatRecre : MonoBehaviour
{
    SkinnedMeshRenderer SMR;

    [SerializeField]
    Material baseMat;

    // Start is called before the first frame update
    void Start()
    {
        SMR = GetComponent<SkinnedMeshRenderer>();
        SMR.material = baseMat;
        Material m = SMR.material;
        m.SetColor("_BaseColor", Color.HSVToRGB(Random.Range(0f,1f),1,1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
