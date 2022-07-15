using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBilan : MonoBehaviour
{
    TMP_Text textLabel;

    TypeWritterEffect twe;

    public bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        textLabel = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ecrireText(string text)
    {
        StartCoroutine(StepThroughString(text));
    }

    IEnumerator StepThroughString(string dialogue)
    {
        isRunning = true;

        twe = GetComponent<TypeWritterEffect>();

        yield return RunTypingEffect(dialogue);

        textLabel.text = dialogue;

        yield return new WaitForSeconds(0.5f);

        isRunning = false;
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
}
