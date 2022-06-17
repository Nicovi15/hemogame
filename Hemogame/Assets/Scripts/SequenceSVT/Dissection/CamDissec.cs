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

    public void TriggerNote()
    {
        anim.SetTrigger("goToNote");
    }

    public void TriggerDissec()
    {
        anim.SetTrigger("goToDissec");
    }

    public void TriggerProf()
    {
        anim.SetTrigger("goToProf");
    }

    public void debutDissecToNote()
    {
        UiDissec.SetActive(false);
        UiPlace.SetActive(false);
        SD.hideOutils();
    }

    public void finDissecToNote()
    {
        UiNote.SetActive(true);
    }


    public void DebutNotetoDissec()
    {
        UiNote.SetActive(false);
        if (SD.isDissec)
            SD.resetImage();
        else
            SD.resetPlace();
        SD.hideOutils();
    }

    public void finNoteToDissec()
    {
        if (SD.isDissec)
            UiDissec.SetActive(true);
        else
            UiPlace.SetActive(true);
        SD.showOutils();
    }

    public void finProfToDissec()
    {
        //UiDissec.SetActive(true);
        //SD.showOutils();
        SD.lancementDissec();
        schemasTab.SetActive(false);
    }

    public void debutProfToDissec()
    {
        SD.hideOutils();
    }

    public void finDissecToProf()
    {
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
