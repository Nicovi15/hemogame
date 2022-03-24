using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public ObjectDescription data;

    [SerializeField]
    Outline outline;

    public bool isHolded;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        outline.enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void mouseOver()
    {
        outline.enabled = true;
    }

    public void mouseLeave()
    {
        outline.enabled = true;
    }

    public void hover()
    {
        outline.enabled = true;
    }

    public void unhover()
    {
        outline.enabled = false;
    }

    public void pick()
    {
        isHolded = true;
        transform.rotation = Quaternion.Euler(data.holdingRot);
    }

    public void unpick()
    {
        isHolded = false;
    }

    public float hold()
    {
        
        //transform.Rotate(data.holdingRotPos, Space.Self);
        transform.localPosition = data.holdingPos;
        transform.localEulerAngles = data.holdingRotPos;
        isHolded = true;

        return data.holdingRadius;
    }
    public void drop()
    {
        isHolded = false;
        transform.rotation = Quaternion.Euler(data.holdingRot.x, transform.rotation.y, data.holdingRot.z);

    }

    public void poser()
    {
        isHolded = false;
    }

    public ObjectDescription getData()
    {
        return data;
    }
}
