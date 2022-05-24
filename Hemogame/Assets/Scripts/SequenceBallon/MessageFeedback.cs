using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageFeedback : MonoBehaviour
{

    [SerializeField]
    GameObject dialogueBox;

    [SerializeField]
    TMP_Text textSpeaker;

    [SerializeField]
    TMP_Text textLabel;

    [SerializeField]
    DialogueObject testDialogue;

    ResponseHandler responseHandler;

    TypeWritterEffect twe;

    Coroutine co;

    Animator anim;

    public bool IsOpen { get; private set; }


    private void Start()
    {
        twe = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        //CloseDialogueBox();
        //showDialogue(testDialogue);
        anim = GetComponent<Animator>();
        //showString("test", "nico", Color.blue);

    }

    public void showDialogue(DialogueObject dob)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        co = StartCoroutine(StepThroughDialogue(dob));
    }

    public void showString(string dialogue, string speaker, Color color)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        anim = GetComponent<Animator>();
        textSpeaker.text = speaker;
        textSpeaker.color = color;
        co = StartCoroutine(StepThroughString(dialogue));
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
        twe = GetComponent<TypeWritterEffect>();

        yield return RunTypingEffect(dialogue);

        textLabel.text = dialogue;

        //yield return new WaitForSeconds(0.05f);
        anim.SetTrigger("fadeout");

    }

    IEnumerator StepThroughDialogue(DialogueObject dob)
    {
        for (int i = 0; i < dob.Dialogue.Length; i++)
        {
            string dialogue = dob.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dob.Dialogue.Length - 1 && dob.HasResponses)
                break;

            yield return new WaitForSeconds(0.5f);

            //yield return new WaitUntil(() => Input.GetMouseButtonDown(0)/*Input.GetKeyDown(KeyCode.Space)*/);
        }

        /*
        if (dob.HasResponses)
            responseHandler.ShowResponses(dob.Responses);
        else
            CloseDialogueBox();
        */
    }

    IEnumerator RunTypingEffect(string dialogue)
    {
        twe.Run(dialogue, textLabel);
        while (twe.IsRunning)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0)/*Input.GetKeyDown(KeyCode.Space)*/)
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

    public void finAnim()
    {
        Destroy(this.gameObject);
    }

    public void setXpos(float newX)
    {
        RectTransform rt = dialogueBox.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(newX, rt.anchoredPosition.y);
    }

}
