using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionsAccident : MonoBehaviour
{
    [SerializeField]
    GameManagerSequenceBallon GM;

    [SerializeField]
    TMP_Text textLabel;

    TypeWritterEffect twe;

    [SerializeField]
    [TextArea]
    string question;

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
        co = StartCoroutine(StepThroughString(GM.currentReceveur.nom + question));
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
        if(GM.currentReceveur.isHemo)
            GM.resultatChoix(brephemo);
        else
            GM.resultatChoix(mrepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse2()
    {
        if (GM.currentReceveur.isHemo)
            GM.resultatChoix(mrephemo);
        else
            GM.resultatChoix(brepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse3()
    {
        if (GM.currentReceveur.isHemo)
            GM.resultatChoix(mrephemo);
        else
            GM.resultatChoix(mrepnonhemo);
        gameObject.SetActive(false);
    }

    public void reponse4()
    {
        if (GM.currentReceveur.isHemo)
            GM.resultatChoix(mrephemo);
        else
            GM.resultatChoix(mrepnonhemo);
        gameObject.SetActive(false);
    }


}
