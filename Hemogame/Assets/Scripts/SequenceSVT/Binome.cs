using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binome : MonoBehaviour
{
    [SerializeField]
    string nom;

    [SerializeField]
    float minPhysique;

    [SerializeField]
    float minMorale;

    [SerializeField]
    GameManagerSVT GM;

    [SerializeField]
    DialogueObject diagAccept;

    [SerializeField]
    DialogueObject diagRefuse;

    [SerializeField]
    Color couleur;

    [SerializeField]
    QuestionBinome qb;

    bool refus = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GM.DI.IsOpen && refus)
        {
            qb.afficherRep();
            refus = false;
        }
    }

    public void click()
    {
        if (GM.DI.IsOpen)
            return;

        qb.cacherRep();

        if (GM.jauges.physique > minPhysique && GM.jauges.morale > minMorale)
        {
            foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
            {
                if (responseEvents.DialogueObject == diagAccept)
                {
                    GM.DI.AddResponseEvents(responseEvents.Events);
                    break;
                }
            }

            GM.DI.showDialogue(diagAccept);
            GM.DI.setSpeaker(nom, couleur);

        }

        else
        {
            GM.DI.showDialogue(diagRefuse);
            GM.DI.setSpeaker(nom, couleur);
            refus = true;
        }
            
    }

    public void acceptBin()
    {
        GM.binome = nom;
        GM.lancerActi();
    }
    public void refuserBin()
    {
        qb.afficherRep();
    }
}
