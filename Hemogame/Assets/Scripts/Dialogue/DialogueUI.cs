using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBox;

    [SerializeField]
    TMP_Text textLabel;

    [SerializeField]
    TMP_Text speakerLabel;

    [SerializeField]
    DialogueObject testDialogue;

    ResponseHandler responseHandler;

    TypeWritterEffect twe;

    Coroutine co;

    public bool IsOpen { get; private set; }


    private void Start()
    {
        twe = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        //showDialogue(testDialogue);
       
    }

    public void showDialogue(DialogueObject dob)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        co = StartCoroutine(StepThroughDialogue(dob));
    }

    public void showDialogue(String s)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        co = StartCoroutine(StepThroughString(s));
    }

    internal void startDialogue()
    {
        showDialogue(testDialogue);
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    IEnumerator StepThroughString(string dialogue)
    {

        yield return RunTypingEffect(dialogue);

        textLabel.text = dialogue;

        yield return null;

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && Time.timeScale > 0.5f /*Input.GetKeyDown(KeyCode.Space)*/);

        CloseDialogueBox();
    }

    IEnumerator StepThroughDialogue(DialogueObject dob)
    {
        for(int i = 0; i < dob.Dialogue.Length; i++)
        {
            string dialogue = dob.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dob.Dialogue.Length - 1 && dob.HasResponses)
                break;

            yield return null;

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) && Time.timeScale > 0.5f /*Input.GetKeyDown(KeyCode.Space)*/);
        }

        if (dob.HasResponses)
            responseHandler.ShowResponses(dob.Responses);
        else
            CloseDialogueBox();
    }

    IEnumerator RunTypingEffect(string dialogue)
    {
        twe.Run(dialogue, textLabel);
        while (twe.IsRunning)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0.5f /*Input.GetKeyDown(KeyCode.Space)*/)
            {
                twe.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void stopDialogue()
    {
        //IsOpen = true;
        //dialogueBox.SetActive(true);
        StopCoroutine(co);
    }

    public void setSpeaker(string nom, Color couleur)
    {
        speakerLabel.text = nom;
        speakerLabel.color = couleur;
    }

}
