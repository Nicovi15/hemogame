using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objectif : MonoBehaviour
{
    public string desc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void updateDone()
    {
        if (transform.parent.GetComponent<ObjectifMultiple>())
            transform.parent.GetComponent<ObjectifMultiple>().updateObj();
        else if (transform.parent.GetComponent<ListeObjectif>())
            transform.parent.GetComponent<ListeObjectif>().updateObj();
    }

    public abstract bool isDone();

    public virtual string toString() {

        return desc + " " + (isDone() ? " (Terminé)" : "");
    }
    
}
