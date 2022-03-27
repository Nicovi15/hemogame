using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoireBallon : Interactable
{
    public Animator anim;
    public GameObject interactionObj;
    public GameObject camArmoire;
    public GameManager2 GM;

    public GameObject selectedObj;

    public Inspection I;

    public List<BoutonArmoire> boutons;

    public testS T;

    public ArmoireObjectif AO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    internal void unselectBouton()
    {
        foreach (BoutonArmoire b in boutons)
            b.unselect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interact()
    {
        anim.SetTrigger("Inter");
        camArmoire.SetActive(true);
        GM.interruptToInteract();
    }

    public void openMenu()
    {
        interactionObj.SetActive(true);
        if (selectedObj)
        {
            I.T = T;
            I.setObjInspect(selectedObj, selectedObj.GetComponent<PickableObject>().data);
        }

    }

    public void quitMenu()
    {
        interactionObj.SetActive(false);
        I.clearInspect();
        anim.SetTrigger("Inter");
    }

    public void stopInteract()
    {
        GM.resume();
    }

    public void setSelectedObj(GameObject go)
    {
        selectedObj = go;
        I.T = T;
        I.setObjInspect(go, go.GetComponent<PickableObject>().data);

        if (AO)
            AO.setSelectedObject(selectedObj);
    }

    public void valider()
    {
        quitMenu();
    }

}
