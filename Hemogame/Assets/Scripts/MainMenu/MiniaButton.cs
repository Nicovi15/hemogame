using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MiniaButton : MonoBehaviour
{
    [SerializeField]
    string titreScene;

    [SerializeField]
    TextMeshProUGUI selectedText;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color highlightColor;

    [SerializeField]
    UnityEvent function;

    [SerializeField]
    Image triangle;


    void Start()
    {

    }

    public void PointerEnter()
    {
        triangle.color = highlightColor;
        selectedText.text = titreScene;
    }

    public void PointerExit()
    {
        triangle.color = normalColor;
        selectedText.text = "";
    }

    public void PointerClick()
    {
        function.Invoke();
    }

    public void loadScene(int index)
    {
        //SceneManager.LoadScene(index);
        //StartCoroutine(loadAsync(index));
        SceneManager.LoadSceneAsync(index);
    }

    //IEnumerator loadAsync(int index)
    //{
    //    AsyncOperation op = SceneManager.LoadSceneAsync(index);
    //
    //    while (!op.isDone)
    //    {
    //        yield return null;
    //    }
    //}
}
