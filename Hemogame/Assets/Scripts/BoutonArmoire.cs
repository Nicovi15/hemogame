using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoutonArmoire : MonoBehaviour
{
    public GameObject pickObjectPrefab;

    public ArmoireBallon AB;

    public bool isSelected;

    public Color normalColor;
    public Color selectedColor;

    Image im;

    // Start is called before the first frame update
    void Start()
    {
        im = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clique()
    {
        if (isSelected)
            return;

        AB.unselectBouton();
        select();
        AB.setSelectedObj(pickObjectPrefab);
    }

    public void select()
    {
        isSelected = true;
        im.color = selectedColor;
    }

    public void unselect()
    {
        isSelected = false;
        im.color = normalColor;
    }
}
