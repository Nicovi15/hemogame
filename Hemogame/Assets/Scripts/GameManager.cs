using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool findingPhase;

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
        findingPhase = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transiVerif()
    {
        findingPhase = false;
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

    public void setCFCtarget(GameObject go)
    {
        CFC.target = go.transform;
    }
}
