using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectif : Objectif
{
    [SerializeField]
    GameManager2 GM;

    [SerializeField]
    ObjectDescription goalData;

    [SerializeField]
    GameObject goalObject;

    public bool estOccupe;
    public bool done;

    public bool catObj;

    public string categorie;

    [SerializeField]
    float dist;

    public MeshRenderer MR;

    public Collider col;
    public string actionDesc;

    // Start is called before the first frame update
    void Start()
    {
        if(!MR)
            MR = GetComponent<MeshRenderer>();

        if(!col)
            col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && GM.rightHand.childCount > 0)
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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!estOccupe && other.CompareTag("PickObject") && !other.GetComponent<PickableObject>().isHolded && (other.GetComponent<PickableObject>().data.categorie == categorie || other.GetComponent<PickableObject>().data == goalData))
        {
            Debug.Log(other.name);
            triggerObj(other.gameObject);
        }
    }

    public void triggerObj(GameObject obj)
    {
        Destroy(obj);
        goalObject.SetActive(true);
        MR.enabled = false;
        col.enabled = false;
        estOccupe = true;
        done = true;

        updateDone();
            
    }

    public override bool isDone()
    {
        return done;
    }

    public string getActionDesc()
    {
        return actionDesc;
    }

}
