using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prof : MonoBehaviour, IInteractable
{
    [SerializeField]
    DialogueObject dialoguePresentationObjectifs;

    [SerializeField]
    DialogueObject dialogueObjectifsTermines;

    [SerializeField]
    DialogueObject dialogueMauvaisBallon;

    [SerializeField]
    DialogueObject dialogueMauvaisCerceau;

    [SerializeField]
    DialogueObject dialogueDebutEntrainement;

    [SerializeField]
    DialogueObject yes;

    List<DialogueObject> debriefDialogues;

    DialogueObject currentDialogue;

    [SerializeField]
    ListeObjectif LO;

    [SerializeField]
    ArmoireObjectif armoireCerceau;

    [SerializeField]
    ArmoireObjectif armoireBallon;

    [SerializeField]
    ObjectDescription bonCerceau;

    [SerializeField]
    ObjectDescription bonBallon;

    [SerializeField]
    DialogueUI dialogueUI;
    
    [SerializeField]
    GameManager2 GM;


    bool debrief = false;
    bool finDebrief = false;
    public void Interact(PlayerFPMovement player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == currentDialogue)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.showDialogue(currentDialogue);
    }

    public void Interact(PickObjects player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == currentDialogue)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.showDialogue(currentDialogue);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDialogue = dialoguePresentationObjectifs;
    }

    // Update is called once per frame
    void Update()
    {
        if (debrief)
            return;

        if (LO.isDone())
            currentDialogue = dialogueObjectifsTermines;
        else
            currentDialogue = dialoguePresentationObjectifs;
    }

    public void lancerEntrainement()
    {
        Debug.Log("c'est parti");
        StartCoroutine(waitThenSwitch(1.5f));
    }

    IEnumerator waitThenSwitch(float t)
    {
        yield return new WaitForSeconds(t);
        GM.switchScene(1);
    }

    public void lancerDebrief()
    {
        debrief = true;

        debriefDialogues = new List<DialogueObject>();

        if (armoireBallon.selectedObject.GetComponent<PickableObject>().data != bonBallon)
            debriefDialogues.Add(dialogueMauvaisBallon);

        if (armoireCerceau.selectedObject.GetComponent<PickableObject>().data != bonCerceau)
            debriefDialogues.Add(dialogueMauvaisCerceau);

        Debrief();
        
    }

    public void Debrief()
    {
        if (finDebrief)
        {
            lancerEntrainement();
            return;
        }

        if (debriefDialogues.Count > 0)
        {
            currentDialogue = debriefDialogues[debriefDialogues.Count - 1];
            debriefDialogues.RemoveAt(debriefDialogues.Count - 1);
        }
        else
        {
            currentDialogue = dialogueDebutEntrainement;
            finDebrief = true;
        }

        StartCoroutine(lancerDialogue());
    }

    IEnumerator lancerDialogue()
    {
        while(dialogueUI.IsOpen)
        {
            yield return null;
        }

        dialogueUI.showDialogue(currentDialogue);
        Debrief();
    }
}
