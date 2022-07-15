using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRecre : MonoBehaviour
{
    [SerializeField]
    TimerRecre TR;

    [SerializeField]
    PlayerRecre PR;

    [SerializeField]
    GameObject RunCanvas;

    [SerializeField]
    DialogueUI DI;

    [SerializeField]
    Color colorTom;

    [SerializeField]
    Color colorAlex;

    [SerializeField]
    DialogueObject introRecre;

    [SerializeField]
    DialogueObject introRecre2;

    [SerializeField]
    DialogueObject moinsViteDiag;

    [SerializeField]
    DialogueObject goodFinRecre;

    [SerializeField]
    DialogueObject infirmFinRecre;

    [SerializeField]
    DialogueObject badFinRecre;

    [SerializeField]
    GestionJaugeHemo jauges;

    [SerializeField]
    TransiRecre transi;

    [SerializeField]
    GameObject questionAccident;

    [SerializeField]
    FinishRecre fr;

    AudioSource AS;

    int perteMoral = 10;

    string toDo = "";

    [SerializeField]
    FinSeqBallon finBallon;

    [SerializeField]
    List<DialogDebRecre> diags;


    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        RunCanvas.SetActive(false);

        int tauxPhysique = jauges.jauges.physique;

        if (tauxPhysique > 66)
            PR.setViesMax(3);
        else if (tauxPhysique > 33)
            PR.setViesMax(2);
        else
            PR.setViesMax(1);

    }

    // Update is called once per frame
    void Update()
    {
        if (toDo != "" && !DI.IsOpen)
        {
            if (toDo == "lancerPartie")
            {
                lancerPartie();
                toDo = "";
            }
            else if (toDo == "intro2")
            {
                AS.Play();
                DI.setSpeaker("Alex", colorAlex);
                DI.showDialogue(introRecre2);
                toDo = "lancerPartie";
            }
            else if (toDo == "finPartie")
            {
                if (TR.retard > 1)
                {
                    if (TR.retard > 60)
                        perteMoral = -30;
                    else if (TR.retard > 60)
                        perteMoral = -20;
                    else
                        perteMoral = -10;

                    DI.setSpeaker("Tom", colorTom);
                    DI.showDialogue(badFinRecre);

                    toDo = "retard";
                    return;
                }

                transi.triggerFermeture();
                toDo = "";
            }
            else if(toDo == "retard")
            {
                jauges.addMorale(perteMoral);
                //StartCoroutine(lancerTransiFin());
                toDo = "fin";
            }
            else if(toDo == "fin")
            {
                StartCoroutine(lancerTransiFin(1));
            }
        }
    }

    public void dialogDebut()
    {
        //  //AS.Play();
        //  DI.setSpeaker("Tom", colorTom);
        //  DI.showDialogue(introRecre);
        //  //toDo = "lancerPartie";

        StartCoroutine(diagDebutCor());
        
    }

    IEnumerator diagDebutCor()
    {
        DialogDebRecre d = diags[finBallon.fin];

        StartCoroutine(d.startDialogue());

        while (d.isRunning)
            yield return null;

        toDo = "lancerPartie";
    }


    public void lancerPartie()
    {
        RunCanvas.SetActive(true);
        TR.startTimer();
        PR.lancerPartie();
    }

    public void finishInfirmerie()
    {
        RunCanvas.SetActive(false);
        TR.endTimer();
        PR.finPartie();

        //Debug.Log("infirm");

        DI.setSpeaker("Tom", colorTom);
        DI.showDialogue(infirmFinRecre);
        toDo = "finPartie";
    }

    public void finish()
    {
        RunCanvas.SetActive(false);
        TR.endTimer();
        PR.finPartie();

        DI.setSpeaker("Tom", colorTom);
        DI.showDialogue(goodFinRecre);
        toDo = "finPartie";

    }

    public void lancerQuestionAccident()
    {
        StartCoroutine(quesAccident());
    }


    IEnumerator quesAccident()
    {
        RunCanvas.SetActive(false);
        TR.endTimer();
        PR.finPartie();

        questionAccident.SetActive(true);

        fr.setInfirmerie();
        
        while (questionAccident.activeSelf)
            yield return null;

        while (DI.IsOpen)
            yield return null;

        DI.setSpeaker("Tom", colorTom);
        DI.showDialogue(moinsViteDiag);

        while (DI.IsOpen)
            yield return null;

        PR.seBlesse();
        RunCanvas.SetActive(true);
        TR.startTimer();
    }

    IEnumerator lancerTransiFin(float seconde = 2)
    {
        yield return new WaitForSeconds(seconde);
        transi.triggerFermeture();
    }
}
