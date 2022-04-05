using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransiMEP : MonoBehaviour
{
    [SerializeField]
    DialogueUI diag;

    Animator anim;

    public bool isOut;

    bool transiDone = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetTrigger("idle");
        StartCoroutine(lireTexte());

        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator lireTexte()
    {
        yield return new WaitForSeconds(3);

        anim.SetTrigger("ouverture");
    }

    public void triggerOut()
    {
        anim.SetTrigger("out");
        isOut = true;
    }

    public void triggerDialogue()
    {
        diag.startDialogue();
        //this.gameObject.SetActive(false);
    }


    public void triggerFermeture()
    {
        anim.SetTrigger("fermeture");
        isOut = false;
        //StartCoroutine(loadScene());
    }

    public void finFermeture()
    {
        transiDone = true;
        SceneManager.LoadScene(1);
    }

    IEnumerator loadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            yield return null;
        }

        while (!transiDone)
        {
            yield return null;
        }

        
    }
}
