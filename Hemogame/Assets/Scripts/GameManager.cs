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

    [SerializeField]
    GameObject findUI;

    [SerializeField]
    GameObject VerifUI;

    [SerializeField]
    CameraFollowCircle CFC;

    [SerializeField]
    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transiVerif()
    {
        mainCam.rect = new Rect(0, 0, 0.6f, 1);
        findUI.SetActive(false);
        VerifUI.SetActive(true);
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
