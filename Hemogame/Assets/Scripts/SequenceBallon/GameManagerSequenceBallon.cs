using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSequenceBallon : MonoBehaviour
{
    [SerializeField]
    public ObjectDescription mousseData;

    [SerializeField]
    public ObjectDescription basketData;

    [SerializeField]
    public ObjectDescription footData;

    [SerializeField]
    public List<Receveur> receveurs;

    [SerializeField]
    public Receveur TomReceveur;

    [SerializeField]
    GameObject camPlayer;

    [SerializeField]
    GameObject camChoix;

    [SerializeField]
    GameObject HUDechauf;

    [SerializeField]
    LancerBallon lb;

    [SerializeField]
    DialogueUI DI;

    [SerializeField]
    DialogueObject diagDebut;

    [SerializeField]
    DialogueObject diagFin;

    [SerializeField]
    TransiBallon transi;

    [SerializeField]
    GameObject questionParticip;

    [SerializeField]
    GestionJaugeHemo Jauges;

    public Receveur currentReceveur;

    string toDo = "";

    public bool blesse = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toDo != "" && !DI.IsOpen)
        {
            if(toDo == "retourEchauf")
            {
                if (!currentReceveur.isHemo)
                    blesse = true;
                camChoix.GetComponent<Animator>().SetTrigger("Travel");
                receveurs.Remove(currentReceveur);
                currentReceveur.GetComponent<Animator>().enabled = true;
                currentReceveur.GetComponent<Animator>().SetTrigger("sortir");

                foreach (Receveur r in receveurs)
                {
                    r.maxFile--;
                    r.posFinFile += new Vector3( r.step, 0, 0);
                }
            }
            else if(toDo == "debuterBallon")
            {
                //HUDechauf.SetActive(true);
                //lb.spawnNewBallon();
                questionParticip.SetActive(true);
            }
            else if(toDo == "fin")
            {
                transi.triggerFermeture();
            }


            toDo = "";
        }
    }

    public void avancerFile()
    {
        foreach (Receveur r in receveurs)
            r.avancer();
    }

    public void lancementSequenceChoix() {

        camChoix.SetActive(true);
        camPlayer.GetComponent<Camera>().enabled = false;
        camPlayer.GetComponent<AudioListener>().enabled = false;
        camChoix.GetComponent<Animator>().SetTrigger("Travel");
        HUDechauf.SetActive(false);

    }

    public void resultatChoix(DialogueObject dialogue)
    {
        toDo = "retourEchauf";
        DI.showDialogue(dialogue);
    }

    public void retourSequenceBallon()
    {

        camPlayer.GetComponent<Camera>().enabled = true;
        camPlayer.GetComponent<AudioListener>().enabled = true;
        camChoix.SetActive(false);
        lb.spawnNewBallon();
        HUDechauf.SetActive(true);

    }

    public void dialogDebut()
    {
        toDo = "debuterBallon";
        DI.showDialogue(diagDebut);
    }

    public void dialogFin()
    {
        StartCoroutine(afficherDialogFin());
    }

    IEnumerator afficherDialogFin()
    {
        yield return new WaitForSeconds(1);
        DI.showDialogue(diagFin);
        toDo = "fin";
    }

    IEnumerator lancerJeu(float t)
    {
        yield return new WaitForSeconds(t);
        HUDechauf.SetActive(true);
        lb.spawnNewBallon();
    }

    public void TomParticipe()
    {
        questionParticip.SetActive(false);
        Jauges.addMoraleGraph(25, 1, 0, 0);
        Jauges.addPhysiqueGraph(5, 1, 0, 0);
        StartCoroutine(lancerJeu(3));
    }

    public void TomParticipePas()
    {
        questionParticip.SetActive(false);
        Jauges.addMoraleGraph(-20, 1, 0, 0);
        Jauges.addPhysiqueGraph(-15, 1, 0, 0);

        receveurs.Remove(TomReceveur);
        Destroy(TomReceveur.gameObject);
        foreach (Receveur r in receveurs)
        {
            r.maxFile--;
            r.posFinFile += new Vector3(r.step, 0, 0);
        }


        StartCoroutine(lancerJeu(3));
    }

}
