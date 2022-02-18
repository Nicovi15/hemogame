using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Objects;

    [SerializeField]
    GameObject ValidObjects;

    [SerializeField]
    GameObject NoValidSelected;

    [SerializeField]
    GameObject CanvasPanelObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getObjects()
    {
        return Objects;
    }

    public GameObject getValidObjects()
    {
        return ValidObjects;
    }

    public GameObject getNoValidSelected()
    {
        return NoValidSelected;
    }

    public GameObject getCanvasPanelObject()
    {
        return CanvasPanelObject;
    }
}
