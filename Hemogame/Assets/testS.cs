using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testS : MonoBehaviour
{
    public GameObject target;

    Vector3 lastPosFrame;

    public bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && isClicked)
        {
            Vector3 delta = Input.mousePosition - lastPosFrame;
            lastPosFrame = Input.mousePosition;

            Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            target.transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.1f, axis) * target.transform.rotation;
        }
    }

    public void eyah()
    {

    }

    public void pointerDown()
    {
        isClicked = true;
        lastPosFrame = Input.mousePosition;
    }

    public void pointerUp()
    {
        isClicked = false;
    }
}
