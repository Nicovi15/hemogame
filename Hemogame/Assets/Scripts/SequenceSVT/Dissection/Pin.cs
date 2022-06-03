using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer SR;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color highlightColor;

    [SerializeField]
    Color selectedColor;

    bool isSelected;
    Vector3 pinPos;

    // Start is called before the first frame update
    void Start()
    {
        pinPos = SR.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSelected)
            SR.color = normalColor;
    }
    
    public void highlight()
    {
        if (isSelected)
            return;

        SR.color = highlightColor;
    }

    public void select()
    {
        isSelected = true;
        SR.color = selectedColor;
    }

    public void unselect()
    {
        isSelected = false;
        SR.color = normalColor;
    }

    public Vector3 getPinPos()
    {
        return pinPos;
    }

}
