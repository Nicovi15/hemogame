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
    DialogueObject introRecre;

    [SerializeField]
    DialogueObject goodFinRecre;

    [SerializeField]
    DialogueObject badFinRecre;

    [SerializeField]
    GestionJaugeHemo jauges;

    [SerializeField]
    TransiRecre transi;

    [SerializeField]
    GameObject questionAccident;

    AudioSource AS;

    int perteMoral = 10;

    string toDo = "";

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
            else if (toDo == "finPartie")
            {
                transi.triggerFermeture();
                toDo = "";
            }
            else if(toDo == "retard")
            {
                jauges.addMoraleGraph(perteMoral, 1, 0, 0);
                StartCoroutine(lancerTransiFin());
                toDo = "";
            }
        }
    }

    public void dialogDebut()
    {
        AS.Play();
        DI.setSpeaker("Tom", colorTom);
        DI.showDialogue(introRecre);
        toDo = "lancerPartie";
    }


    public void lancerPartie()
    {
        RunCanvas.SetActive(true);
        TR.startTimer();
        PR.lancerPartie();
    }

    public void finish()
    {
        RunCanvas.SetActive(false);
        TR.endTimer();
        PR.finPartie();

        if(TR.retard <= 0)
        {
            DI.setSpeaker("Tom", colorTom);
            DI.showDialogue(goodFinRecre);
            toDo = "finPartie";
        }
        else
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
        }
        
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
        
        while (questionAccident.activeSelf)
            yield return null;

        while (DI.IsOpen)
            yield return null;

        //////// perte jauges, dialogues, etc ????
        ///


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
