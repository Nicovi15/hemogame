using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [SerializeField]
    GameObject playerFP;

    [SerializeField]
    GameObject camGlobal;

    [SerializeField]
    public PickObjects PO;

    [SerializeField]
    public Transform rightHand;

    public bool fpController;


    // Start is called before the first frame update
    void Start()
    {
        fpController = true;
        playerFP.SetActive(fpController);
        camGlobal.SetActive(!fpController);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
            switchPlayerMode();
    }

    void switchPlayerMode()
    {
        fpController = !fpController;
        playerFP.SetActive(fpController);
        camGlobal.SetActive(!fpController);
    }

    public void interruptToInteract()
    {
        playerFP.SetActive(false);
        camGlobal.SetActive(false);
    }

    public void resume()
    {
        playerFP.SetActive(fpController);
        camGlobal.SetActive(!fpController);
    }



}
