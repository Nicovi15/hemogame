using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionAccidentDissec : MonoBehaviour
{
    public bool isHemo;

    public BinomeDissection currentBinome;

    [SerializeField]
    TMP_Text textLabel;

    TypeWritterEffect twe;

    [SerializeField]
    [TextArea]
    string question;

    [SerializeField]
    DialogueUI DI;

    [SerializeField]
    GameObject reponses;

    [SerializeField]
    DialogueObject brephemo;

    [SerializeField]
    DialogueObject brepnonhemo;

    [SerializeField]
    DialogueObject mrephemo;

    [SerializeField]
    DialogueObject mrepnonhemo;

    [SerializeField]
    Color couleurProf;

    Coroutine co;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        reponses.SetActive(false);
        ecrireQuestion();
    }

    public void ecrireQuestion()
    {
        if(!isHemo)
            co = StartCoroutine(StepThroughString(currentBinome.nom + question));
        else
            co = StartCoroutine(StepThroughString("Tom" + question));
    }

    IEnumerator StepThroughString(string dialogue)
    {
        twe = GetComponent<TypeWritterEffect>();

        yield return RunTypingEffect(dialogue);

        textLabel.text = dialogue;

        yield return new WaitForSeconds(0.5f);

        afficherRep();
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

    public void afficherRep()
    {
        reponses.SetActive(true);
    }

    public void reponse1()
    {
        DI.setSpeaker("Professeur", couleurProf);
        if (isHemo)
            DI.showDialogue(brephemo);
        else
            DI.showDialogue(mrepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse2()
    {
        DI.setSpeaker("Professeur", couleurProf);
        if (isHemo)
            DI.showDialogue(mrephemo);
        else
            DI.showDialogue(brepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse3()
    {
        DI.setSpeaker("Professeur", couleurProf);
        if (isHemo)
            DI.showDialogue(mrephemo);
        else
            DI.showDialogue(mrepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse4()
    {
        DI.setSpeaker("Professeur", couleurProf);
        if (isHemo)
            DI.showDialogue(mrephemo);
        else
            DI.showDialogue(mrepnonhemo);
        gameObject.SetActive(false);
    }

}
