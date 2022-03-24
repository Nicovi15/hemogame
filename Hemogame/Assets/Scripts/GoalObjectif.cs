using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalObjectif : Objectif
{
    [SerializeField]
    GameManager2 GM;

    [SerializeField]
    ObjectDescription goalData;

    PickableObject objPlace;

    public bool estOccupe;
    public bool done;

    public bool cantBeMove;

    public bool catObj;

    public string categorie;

    [SerializeField]
    float dist;

    public MeshRenderer MR;
    Collider col;

    public string actionDesc;

    // Start is called before the first frame update
    void Start()
    {
        MR = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && GM.rightHand.childCount > 0 )
        {
            PickableObject objetTenu = GM.rightHand.GetChild(0).GetComponent<PickableObject>();
            if (objetTenu.data.categorie == categorie || objetTenu.data == goalData)
            {
                if (Vector3.Distance(objetTenu.transform.position, transform.position) < dist)
                {
                    MR.enabled = true;
                }
                else
                {
                    MR.enabled = false;
                }
            }
        }
        else if (!done && GM.PO.pickedObject)
        {
            if (GM.PO.pickedObject.GetComponent<PickableObject>().data == goalData || GM.PO.pickedObject.GetComponent<PickableObject>().data.categorie == categorie)
            {
                if (Vector3.Distance(GM.PO.pickedObject.transform.position, transform.position) < dist)
                {
                    MR.enabled = true;
                }
                else
                {
                    MR.enabled = false;
                }
            }
        }
        else
        {
            MR.enabled = false;
        }


        if (estOccupe)
            if (objPlace.isHolded)
                leverObjectif();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!estOccupe && other.CompareTag("PickObject") && !other.GetComponent<PickableObject>().isHolded && (other.GetComponent<PickableObject>().data.categorie == categorie || other.GetComponent<PickableObject>().data == goalData))
        {
            Debug.Log(other.name);
            placerObjectif(other.gameObject);
        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickObject"))
        {
            leverObjectif();
        }
    }
    */

    public void placerObjectif(GameObject obj)
    {
        obj.transform.position = this.transform.position;
        obj.transform.rotation = this.transform.rotation;
        objPlace = obj.GetComponent<PickableObject>();
        Rigidbody objBody = obj.GetComponent<Rigidbody>();
        objBody.isKinematic = true;
        if (catObj)
            done = true;
        else if (obj.GetComponent<PickableObject>().data == goalData)
            done = true;
        estOccupe = true;
        this.gameObject.layer = 2;
        col.enabled = false;
        //obj.GetComponent<Collider>().enabled = true;
        StartCoroutine(reenableCollider(obj.GetComponent<Collider>()));
        
        if (cantBeMove)
            obj.layer = 0;

        updateDone();
    }

    public void leverObjectif()
    {
        done = false;
        estOccupe = false;
        objPlace = null;
        this.gameObject.layer = 9;
        col.enabled = true;

        updateDone();
    }

    public override bool isDone()
    {
        return done;
    }

    public string getActionDesc()
    {
        return "";
    }

    IEnumerator reenableCollider(Collider c)
    {
        float t = 0.1f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        c.enabled = true;
    }

}
