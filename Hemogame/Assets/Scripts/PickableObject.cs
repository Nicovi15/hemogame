using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public ObjectDescription data;

    [SerializeField]
    Outline outline;


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
        transform.rotation = Quaternion.Euler(data.holdingRot);
    }

    public float hold()
    {
        transform.rotation = Quaternion.Euler(data.holdingRot);
        transform.localPosition = data.holdingPos;

        return data.holdingRadius;
    }
    public void drop()
    {
        transform.rotation = Quaternion.Euler(data.holdingRot.x, transform.rotation.y, data.holdingRot.z);

    }
}
