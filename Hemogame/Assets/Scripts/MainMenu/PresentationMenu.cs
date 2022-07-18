using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PresPerso
{
    public string nom;
    [TextArea]
    public string desc;
    public Material mat;

}
public class PresentationMenu : MonoBehaviour
{
    public int currentIndex;

    public List<PresPerso> persos;

    public TextMeshProUGUI textNom;

    public TextMeshProUGUI textDesc;

    public SkinnedMeshRenderer MR;

    public GameObject boutonGauche;

    public GameObject boutonDroit;


    // Start is called before the first frame update
    void Start()
    {
        updateWithIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateWithIndex()
    {
        textNom.text = persos[currentIndex].nom;
        textDesc.text = persos[currentIndex].desc;
        MR.material = persos[currentIndex].mat;

        if (currentIndex == 0)
            boutonGauche.SetActive(false);
        else if (currentIndex == persos.Count - 1)
            boutonDroit.SetActive(false);
        else
        {
            boutonGauche.SetActive(true);
            boutonDroit.SetActive(true);
        }
    }

    public void boutonFleche(int i)
    {
        currentIndex += i;
        updateWithIndex();
    }
}
