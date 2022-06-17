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
    public PlaceFleur currentPlace;

    [SerializeField]
    public PlaceFleur currentPlacePrefab;

    [SerializeField]
    public GameObject currentImage;

    [SerializeField]
    public GameObject currentOutils;

    [SerializeField]
    public GameObject questionAccident;

    [SerializeField]
    Mouse mouse;

    public int nbCoupe;

    public string outilsName = "ciseaux";

    public bool isFailed = false;

    [SerializeField]
    TextMeshProUGUI titre;

    [SerializeField]
    TextMeshProUGUI titre2;

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

    [SerializeField]
    public List<PlaceFleur> PlacePrefab;

    public DialogueUI DI;

    public DialogueObject dialogDebut;
    public DialogueObject dialogBon;
    public DialogueObject dialogMauvais;
    public DialogueObject dialogFin;
    public DialogueObject dialogFinProf;
    public DialogueObject dialogCoupureBinome;
    public DialogueObject dialogCoupureTom;
    public DialogueObject dialogDebutDissec;
    public DialogueObject dialogMidDissec;
    public Color couleurProf;

    public int index = 0;

    string toDo = "";

    public CamDissec cd;

    [SerializeField]
    public BinomeDissection currentBinome;

    [SerializeField]
    TextMeshPro titreNote;

    [SerializeField]
    TransiDissec transi;

    public List<DoigtsDissec> doigtsCoupe;

    public GameObject hudDissec;
    public GameObject hudDoigts;

    public bool isDissec = true;

    public bool continuer = false;

    [SerializeField]
    GestionJaugeHemo jauges;

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
            else if(toDo == "debutDissec")
            {
                hudDissec.SetActive(true);
                showOutils();
                setCanChange(true);
            }
            else if(toDo == "midDissec")
            {
                hudDissec.SetActive(false);
                hideOutils();
                Destroy(currentCanvas.gameObject);
                index = 0;
                hudDoigts.SetActive(true);
                resetPlace();
                isDissec = false;
            }
            else if(toDo == "nextPlace")
            {
                resetPlace();
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

    public void resetPlace()
    {
        if (currentPlace != null)
            Destroy(currentPlace.gameObject);
        currentPlace = Instantiate(currentPlacePrefab);
        currentPlace.gameObject.SetActive(true);
        currentImage = currentPlace.image;
        mouse.image = currentImage;
        mouse.canCoupe = true;
        titre2.text = currentPlace.titre;

        //currentOutils = null;
        //
        //if (outilsName == "ciseaux")
        //    selectCiseaux();
        //else if (outilsName == "pinces")
        //    selectPinces();
        //else if (outilsName == "scalpel")
        //    selectScalpel();
        //
        //isFailed = false;

        if (currentNote != null)
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
        if(currentOutils != null)
            currentOutils.SetActive(false);
    }

    public void showOutils()
    {
        if (currentOutils != null)
            currentOutils.SetActive(true);
    }

    public IEnumerator proceedCoupe(GameObject pin1, GameObject pin2)
    {
        nbCoupe--;
        nbCoup.text = nbCoupe.ToString();

        mouse.canCoupe = false;
        hideOutils();

        if (doigtsCoupe.Count > 0)
        {
            foreach(var d in doigtsCoupe)
                d.afficheBlessure();

            //jauges.addPhysique(currentBinome.pertePhysique);
            jauges.addMorale(currentBinome.perteMorale);

            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogCoupureBinome);

            while (DI.IsOpen)
                yield return null;

            hudDissec.SetActive(false);

            questionAccident.GetComponent<QuestionAccidentDissec>().isHemo = false;
            questionAccident.GetComponent<QuestionAccidentDissec>().currentBinome = currentBinome;
            questionAccident.SetActive(true);

            while (DI.IsOpen || questionAccident.activeSelf)
                yield return null;

            hudDissec.SetActive(true);
            foreach (var d in doigtsCoupe)
                d.cacherBlessure();

            doigtsCoupe.Clear();
        }




        mouse.canCoupe = true;
        showOutils();

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
                jauges.addMorale(currentBinome.gainMorale);
            }
        }

        if(nbCoupe <= 0 && isFailed)
        {
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogMauvais);
            toDo = "reset";
            jauges.addMorale(currentBinome.perteMorale);
        }

        setCanChange(true);
        yield return null;

        
    }

    public void nextCanvas()
    {
        index++;
        if(index < CanvasPrefab.Count)
        {
            currentCanvasPrefab = CanvasPrefab[index];
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            //if (index == 1)
            //    DI.showDialogue(currentBinome.dialog1);
            //else
            DI.showDialogue(dialogBon);
            toDo = "next";
        }
        else
        {
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogMidDissec);
            toDo = "midDissec";
        }
    }

    public void nextPlace()
    {
        index++;
        if (index < PlacePrefab.Count)
        {
            currentPlacePrefab = PlacePrefab[index];
            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            //if (index == 1)
            //    DI.showDialogue(currentBinome.dialog2);
            //else
            DI.showDialogue(dialogBon);
            toDo = "nextPlace";
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

    public void setCanChange(bool b)
    {
        currentCanvas.canChange = b;
    }

    public void lancementDissec()
    {
        DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
        DI.showDialogue(dialogDebutDissec);
        toDo = "debutDissec";
        setCanChange(false);
    }

    public void validDoigts()
    {
        if (!currentPlace.canValide)
            return;

        Debug.Log("lancement coupe auto");
        currentPlace.canMove = false;
        StartCoroutine(coupeAuto());
    }

    public IEnumerator coupeAuto()
    {
        for(int i = 0; i < currentPlace.decoupes.Count; i++)
        {
            bonOutilsAuto(currentPlace.decoupesOutils[i]);

            continuer = false;

            GameObject p1 = currentPlace.decoupes[i].transform.GetChild(0).gameObject;
            GameObject p2 = currentPlace.decoupes[i].transform.GetChild(1).gameObject;

            //mouse.decoupeAuto(p1.transform.position, p2.transform.position);
            StartCoroutine(mouse.traceAuto(p1.transform.position, p2.transform.position));

            while(!continuer)
                yield return null;
        }

        Debug.Log("next place");
        nextPlace();
        jauges.addMorale(currentBinome.gainMorale);

        yield return null;
    }

    public IEnumerator proceedCoupeAuto()
    {
        if (doigtsCoupe.Count > 0)
        {
            foreach (var d in doigtsCoupe)
                d.afficheBlessure();

            jauges.addPhysique(currentBinome.pertePhysique);

            DI.setSpeaker(currentBinome.nom, currentBinome.couleur);
            DI.showDialogue(dialogCoupureTom);

            while (DI.IsOpen)
                yield return null;

            hudDoigts.SetActive(false);

            questionAccident.GetComponent<QuestionAccidentDissec>().isHemo = true;
            questionAccident.GetComponent<QuestionAccidentDissec>().currentBinome = currentBinome;
            questionAccident.SetActive(true);

            while (DI.IsOpen || questionAccident.activeSelf)
                yield return null;

            hudDoigts.SetActive(true);
            foreach (var d in doigtsCoupe)
                d.cacherBlessure();

            doigtsCoupe.Clear();
        }

        continuer = true;

    }

    public void bonOutilsAuto(string outils)
    {
        ciseaux.SetActive(false);
        scalpel.SetActive(false);
        pinces.SetActive(false);

        if (outils == "ciseaux")
            ciseaux.SetActive(true);
        else if (outils == "pinces")
            pinces.SetActive(true);
        else if (outils == "scalpel")
            scalpel.SetActive(true);

    }


}
