using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspection : MonoBehaviour
{
    public GameObject objInspect;

    public Transform posInspect;

    public testS T;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setObjInspect(GameObject go, ObjectDescription od)
    {
        clearInspect();

        Quaternion q = new Quaternion();
        q.eulerAngles = od.inspecRot;
        objInspect = Instantiate(go, posInspect);
        objInspect.transform.localRotation = q;
        objInspect.transform.localPosition = od.inspecPos;
        objInspect.transform.localScale = od.inspecScale;

        if (objInspect.GetComponent<PickableObject>())
        {
            objInspect.GetComponent<Rigidbody>().isKinematic = true;
        }

        T.target = objInspect;

        MoveToLayer(objInspect.transform, LayerMask.NameToLayer("Test"));
    }

    public void clearInspect()
    {
        if (T)
            T.target = null;

        if(objInspect)
            Destroy(objInspect);
    }

    public static void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }
}
