using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiagRecre
{
    public string speaker;
    public Color colorSpeaker;
    [TextArea]
    public string diag;
    public bool sonnerie;
    
}
public class DialogDebRecre : MonoBehaviour
{

    [SerializeField]
    DialogueUI DI;

    public bool isRunning = false;

    public DiagRecre[] diags;

    AudioSource AS;
    
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator startDialogue()
    {
        isRunning = true;

        foreach (var x in diags)
        {
            DI.setSpeaker(x.speaker, x.colorSpeaker);
            //DI.showDialogue(x.diag);
            DI.showDialogue(x.diag);

            if (x.sonnerie)
                AS.Play();

            while (DI.IsOpen)
                yield return null;
        }

        isRunning = false;
    }
}
