using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoireObjectif : Objectif
{
    public bool done;

    public GameObject selectedObject;

    public bool feedBack;
    public Transform feedbackPos;
    public GameObject feedbackObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool isDone()
    {
        return selectedObject;
    }


    public void setSelectedObject(GameObject go)
    {
        selectedObject = go;
        if (feedBack)
        {
            if (feedbackObj)
                Destroy(feedbackObj);

            feedbackObj = Instantiate(go);
            feedbackObj.transform.position = feedbackPos.position;
            feedbackObj.transform.rotation = feedbackPos.rotation;
            if (feedbackObj.GetComponent<PickableObject>())
            {
                feedbackObj.GetComponent<Rigidbody>().useGravity = false;
                feedbackObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Inspection.MoveToLayer(feedbackObj.transform, 0);
            }
        }

        updateDone();
    }
}
