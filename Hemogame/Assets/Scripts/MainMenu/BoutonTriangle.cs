using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class BoutonTriangle : MonoBehaviour
{
    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color highlightColor;

    [SerializeField]
    UnityEvent function;

    [SerializeField]
    Image triangle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerEnter()
    {
        triangle.color = highlightColor;
    }

    public void PointerExit()
    {
        triangle.color = normalColor;
    }

    public void PointerClick()
    {
        function.Invoke();
    }

    private void OnEnable()
    {
        triangle.color = normalColor;
    }
}
