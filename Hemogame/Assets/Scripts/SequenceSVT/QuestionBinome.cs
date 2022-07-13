using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionBinome : MonoBehaviour
{

    [SerializeField]
    GameManagerSVT GM;

    [SerializeField]
    TMP_Text textLabel;

    TypeWritterEffect twe;

    [SerializeField]
    [TextArea]
    string question;

    [SerializeField]
    GameObject reponses;


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
        StartCoroutine(StepThroughString(question));
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

            if (Input.GetMouseButtonDown(0) && Time.timeScale > 0.5f/*Input.GetKeyDown(KeyCode.Space)*/)
            {
                twe.Stop();
            }
        }
    }

    public void afficherRep()
    {
        reponses.SetActive(true);
    }

    public void cacherRep()
    {
        reponses.SetActive(false);
    }
}
