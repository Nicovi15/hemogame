using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemeDecoupe : MonoBehaviour
{
    [SerializeField]
    public CanvasDissection currentCanvasPrefab;

    [SerializeField]
    public CanvasDissection currentCanvas;

    [SerializeField]
    public GameObject currentImage;

    [SerializeField]
    public GameObject currentOutils;

    [SerializeField]
    Mouse mouse;

    public int nbCoupe;

    public string outilsName = "ciseaux";

    public bool isFailed = false;

    [SerializeField]
    TextMeshProUGUI titre;

    [SerializeField]
    TextMeshProUGUI nbCoup;

    [SerializeField]
    public GameObject ciseaux;

    [SerializeField]
    public GameObject pinces;

    [SerializeField]
    public GameObject scalpel;

    [SerializeField]
    public GameObject emplacementNote;

    [SerializeField]
    public GameObject currentNote;

    [SerializeField]
    public List<CanvasDissection> CanvasPrefab;

    public DialogueUI DI;

    public DialogueObject dialogDebut;
    public DialogueObject dialogBon;
    public DialogueObject dialogMauvais;
    public DialogueObject dialogFin;
    public DialogueObject dialogFinProf;
    public Color couleurProf;

    public int index = 0;

    string toDo = "";

    public CamDissec cd;

    [SerializeField]
    BinomeDissection currentBinome;

    [SerializeField]
    TextMeshPro titreNote;

    [SerializeField]
    TransiDissec transi;

    // Start is called before the first frame update
    void Start()
    {
        startSequence();
        //cd.TriggerDissec();
    }

    // Update is called once per frame
    void Update()
    {
        if(toDo != "" && !DI.IsOpen)
        {

            if(toDo == "next")
            {
                resetImage();
            }
            else if (toDo == "reset")
            {
                resetImage();
            }
            else if (toDo == "fin")
            {
                cd.TriggerProf();
            }
            else if(toDo == "debut")
            {
                cd.TriggerDissec();
            }
            else if (toDo == "finProf")
            {
                transi.triggerFermeture();
            }

            toDo = "";
        }
    }

    public void startSequence()
    {
        //selectCiseaux();
        //resetImage();
        //nbCoupe = currentCanvas.coupMax;

        //currentCanvas = Instantiate(currentCanvasPrefab);

        resetImage();
    }


    public void resetImage()
    {
        //Destroy(currentImage);
        //currentImage = Instantiate(currentCanvas.image);
        //mouse.image = currentImage;
        //nbCoupe = currentCanvas.coupMax;
        //mouse.canCoupe = true;
        //showOutils();
        if(currentCanvas != null)
            Destroy(currentCanvas.gameObject);
        currentCanvas = Instantiate(currentCanvasPrefab);
        currentCanvas.gameObject.SetActive(true);
        currentImage = currentCanvas.image;
        mouse.image = currentImage;
        nbCoupe = currentCanvas.coupMax;
        mouse.canCoupe = true;
        titre.text = currentCanvas.titre;
        nbCoup.text = nbCoupe.ToString();

        currentOutils = null;

        if (outilsName == "ciseaux")
            selectCiseaux();
        else if (outilsName == "pinces")
            selectPinces();
        else if (outilsName == "scalpel")
            selectScalpel();

        isFailed = false;

        if(currentNote != null)
            Destroy(currentNote);
        if (index == 0)
            currentNote = Instantiate(currentBinome.noteFleur1);
        else if (index == 1)
            currentNote = Instantiate(currentBinome.noteFleur2);
        else
            currentNote = Instantiate(currentBinome.noteFleur3);

        currentNote.transform.SetParent(emplacementNote.transform);
        currentNote.transform.localPosition = new Vector3();
        currentNote.transform.localRotation = new Quaternion();
        currentNote.transform.localScale = new Vector3(1, 1, 1);
        titreNote.text = "Notes de " + currentBinome.nom;

    }

    public void selectCiseaux()
    {
        if (currentOutils != null && outilsName == "ciseaux")
            return;

        if(currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.ciseaux;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "ciseaux";
        ciseaux.SetActive(true);
        scalpel.SetActive(false);
        pinces.SetActive(false);
    }

    public void selectPinces()
    {
        if (currentOutils != null && outilsName == "pinces")
            return;

        //Destroy(currentOutils);
        //currentOutils = Instantiate(currentCanvas.pinces);
        //currentOutils.SetActive(mouse.canCoupe);
        if (currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.pinces;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "pinces";
        ciseaux.SetActive(false);
        scalpel.SetActive(false);
        pinces.SetActive(true);
    }

    public void selectScalpel()
    {
        if (currentOutils != null && outilsName == "scalpel")
            return;

        //Destroy(currentOutils);
        //currentOutils = Instantiate(currentCanvas.scalpel);
        //currentOutils.SetActive(mouse.canCoupe);
        if (currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.scalpel;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "scalpel";
        ciseaux.SetActive(false);
        scalpel.SetActive(true);
        pinces.SetActive(false);
    }

    public void hideOutils()
    {
        currentOutils.SetActive(false);
    }

    public void showOutils()
    {
        currentOutils.SetActive(true);
    }

    public void proceedCoupe(GameObject pin1, GameObject pin2)
    {
        nbCoupe--;
        nbCoup.text = nbCoupe.ToString();
        if (nbCoupe <= 0)
        {
            mouse.canCoupe = false;
            hideOutils();
        }

        if (!isFailed)
        {

            if (!currentCanvas.isOrdered)
            {
                GameObject aSuppr = null;

                foreach(GameObject o in currentCanvas.correction)
                {
                    GameObject coup11 = o.transform.GetChild(0).gameObject;
                    GameObject coup12 = o.transform.GetChild(1).gameObject;

                    if ((pin1 == coup11 && pin2 == coup12) || (pin2 == coup11 && pin1 == coup12))
                    {
                        aSuppr = o;
                        Debug.Log("good ! ");
                        break;
                    }
                }

                if (aSuppr != null)
                    currentCanvas.correction.Remove(aSuppr);
                else
                    isFailed = true;
                
            }
            else
            {
                GameObject coup11 = currentCanvas.correction[0].transform.GetChild(0).gameObject;
                GameObject coup12 = currentCanvas.correction[0].transform.GetChild(1).gameObject;

                if ((pin1 == coup11 && pin2 == coup12) || (pin2 == coup11 && pin1 == coup12))
                {
                    currentCanvas.correction.Remove(currentCanvas.correction[0]);
                    Debug.Log("good ! ");
                }
                else
                    isFailed = true;
            }

            if(currentCanvas.correction.Count <= 0)
            {
                currentCanvas.achievement();
                nextCanvas();
            }
        }

        if(nbCoupe <= 0 && isFailed)
        {
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogMauvais);
            toDo = "reset";
        }

        
    }

    public void nextCanvas()
    {
        index++;
        if(index < CanvasPrefab.Count)
        {
            currentCanvasPrefab = CanvasPrefab[index];
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogBon);
            toDo = "next";
        }
        else
        {
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogFin);
            toDo = "fin";
        }
    }


    public void dialogueDebut()
    {
        DI.setSpeaker("Professeur", couleurProf);
        DI.showDialogue(dialogDebut);
        toDo = "debut";
    }

    public void dialogueFin()
    {
        DI.setSpeaker("Professeur", couleurProf);
        DI.showDialogue(dialogFinProf);
        toDo = "finProf";
    }



}
