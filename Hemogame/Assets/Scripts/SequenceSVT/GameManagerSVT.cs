using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSVT : MonoBehaviour
{

    [SerializeField]
    public DialogueUI DI;

    [SerializeField]
    DialogueObject diagDebut;

    [SerializeField]
    public JaugesHemo jauges;

    [SerializeField]
    GameObject questionBinome;

    [SerializeField]
    DialogueObject diagFin;

    public Color couleurProf;

    public string binome;

    string toDo = "";

    [SerializeField]
    TransiSVT transi;

    [SerializeField]
    BinomeDissection currentBinome;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toDo != "" && !DI.IsOpen)
        {
            if (toDo == "choixBinome")
            {
                questionBinome.SetActive(true);
                toDo = "";
            }
            else if (toDo == "lancerActi")
            {
                changerScene();
            }
            else if(toDo == "changerScene")
            {
                //SceneManager.LoadScene(3);
                transi.triggerFermeture();
                toDo = "";
            }
        }
    }

    public void dialogDebut()
    {
        toDo = "choixBinome";
        DI.showDialogue(diagDebut);
    }

    public void lancerActi()
    {
        toDo = "lancerActi";
        questionBinome.SetActive(false);
        
    }

    public void changerScene()
    {
        toDo = "changerScene";
        DI.setSpeaker("Professeur", couleurProf); 
        DI.showDialogue(diagFin);
        BinomeDissection binomeChoisi = Resources.Load<BinomeDissection>("BinomeDissec/"+binome);
        currentBinome.nom = binomeChoisi.nom;
        currentBinome.couleur = binomeChoisi.couleur;
        currentBinome.noteFleur1 = binomeChoisi.noteFleur1;
        currentBinome.noteFleur2 = binomeChoisi.noteFleur2;
        currentBinome.noteFleur3 = binomeChoisi.noteFleur3;

        currentBinome.gainMorale = binomeChoisi.gainMorale;
        currentBinome.perteMorale = binomeChoisi.perteMorale;
        currentBinome.pertePhysique = binomeChoisi.pertePhysique;
        currentBinome.gainPhysique = binomeChoisi.gainPhysique;

        currentBinome.dialog1 = binomeChoisi.dialog1;
        currentBinome.dialog2 = binomeChoisi.dialog2;
    }

}
