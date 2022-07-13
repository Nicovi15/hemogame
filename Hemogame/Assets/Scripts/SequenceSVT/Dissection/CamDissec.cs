using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDissec : MonoBehaviour
{
    public SystemeDecoupe SD;
    public GameObject UiDissec;
    public GameObject UiNote;
    public GameObject UiPlace;
    public GameObject schemasTab;

    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject questionAccident;

    [SerializeField]
    DialogueUI DI;

    public PlaceFleur pf;

    public void TriggerNote()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        anim.SetTrigger("goToNote");
    }

    public void TriggerDissec()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        anim.SetTrigger("goToDissec");
    }

    public void TriggerProf()
    {
        //if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
        //    return;

        anim.SetTrigger("goToProf");
    }

    public void debutDissecToNote()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        UiDissec.SetActive(false);
        UiPlace.SetActive(false);
        SD.hideOutils();
    }

    public void finDissecToNote()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        UiNote.SetActive(true);
    }


    public void DebutNotetoDissec()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        UiNote.SetActive(false);
        if (SD.isDissec)
            SD.resetImage();
        else
            SD.resetPlace();
        SD.hideOutils();
    }

    public void finNoteToDissec()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        if (SD.isDissec)
            UiDissec.SetActive(true);
        else
            UiPlace.SetActive(true);
        SD.showOutils();
    }

    public void finProfToDissec()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        //UiDissec.SetActive(true);
        //SD.showOutils();
        SD.lancementDissec();
        schemasTab.SetActive(false);
    }

    public void debutProfToDissec()
    {
        if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
            return;

        SD.hideOutils();
    }

    public void debutDissecToProf()
    {
        UiDissec.SetActive(false);
        UiPlace.SetActive(false);
        SD.hideOutils();
    }

    public void finDissecToProf()
    {
        //if (DI.IsOpen || questionAccident.activeSelf || (pf != null && !pf.canMove))
        //    return;

        SD.dialogueFin();
    }



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
