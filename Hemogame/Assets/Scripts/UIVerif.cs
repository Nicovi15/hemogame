using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIVerif : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    [SerializeField]
    TextMeshProUGUI titre;

    [SerializeField]
    Button bEx;

    [SerializeField]
    Button bBg;

    [SerializeField]
    Button bLeft;

    [SerializeField]
    Button bRight;

    [SerializeField]
    Button bTerminer;

    [SerializeField]
    TextMeshProUGUI texte;

    public bool isEx;

    public int index;

    public ClickObject currentObjet = null;

    public List<ClickObject> objetsValides;

    public GameObject inspect;

    public Transform posInspect;

    public LayerMask layerInspect;

    public testS t;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        for(int i = 0; i < GM.getValidObjects().transform.childCount; i++)
        {
            objetsValides.Add(GM.getValidObjects().transform.GetChild(i).gameObject.GetComponent<ClickObject>());
        }

        index = 0;
        isEx = true;
        currentObjet = objetsValides[index];

        inspect = Instantiate(currentObjet.gameObject, posInspect.position, new Quaternion());
        inspect.transform.parent = null;
        inspect.layer = LayerMask.NameToLayer("Test");
        t.target = inspect;
        GM.setCFCtarget(currentObjet.gameObject);
        bTerminer.interactable = false;

        if (currentObjet.isSelected)
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrectSelected;
        else
        {
            currentObjet.outline.enabled = true;
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrect;
        }

        currentObjet.outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObjet != null)
        {
            titre.text = currentObjet.getData().nameObject;

            if (isEx)
                texte.text = currentObjet.getData().explication;
            else
                texte.text = currentObjet.getData().bonGeste;

            if (isEx)
            {
                bEx.interactable = false;
                bBg.interactable = true;
            }
            else
            {
                bEx.interactable = true;
                bBg.interactable = false;
            }

            if (index == 0)
                bLeft.interactable = false;
            else
                bLeft.interactable = true;

            if (index == objetsValides.Count - 1)
                bRight.interactable = false;
            else
                bRight.interactable = true;

            if (index == objetsValides.Count - 1)
                bTerminer.interactable = true;
        }
    }


    public void clickBex()
    {
        isEx = true;
    }

    public void clickBbg()
    {
        isEx = false;
    }

    public void clickLeft()
    {
        index--;
        if (index == 0)
            bLeft.interactable = false;
        updateIndex();
    }

    public void clickRight()
    {
        index++;
        if (index == objetsValides.Count - 1)
            bRight.interactable = false;
        updateIndex();
    }

    public void updateIndex()
    {
        currentObjet.outline.OutlineMode = Outline.Mode.OutlineVisible;

        if (currentObjet.isSelected)
            currentObjet.outline.OutlineColor = currentObjet.couleurSelected;
        else
        {
            currentObjet.outline.enabled = false;
        }

        isEx = true;
        currentObjet = objetsValides[index];

        Destroy(inspect);
        inspect = Instantiate(currentObjet.gameObject, posInspect.position, new Quaternion());
        inspect.transform.parent = null;
        inspect.layer = LayerMask.NameToLayer("Test");
        t.target = inspect;
        GM.setCFCtarget(currentObjet.gameObject);

        if (currentObjet.isSelected)
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrectSelected;
        else
        {
            currentObjet.outline.enabled = true;
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrect;
        }

        currentObjet.outline.OutlineMode = Outline.Mode.OutlineAll;

    }
}
