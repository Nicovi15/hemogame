using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifMultiple : Objectif
{
    public List<Objectif> listeInit;

    public List<Objectif> objDone;

    public int howManyDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(toString());
    }

    public void updateObj()
    {
        objDone.Clear();
        foreach(Objectif o in listeInit)
        {
            if (o.isDone())
                objDone.Add(o);    
        }

        updateDone();
    }

    public override bool isDone()
    {
        return objDone.Count >= howManyDone;
    }

    public override string toString()
    {
        return desc + " " + objDone.Count + "/" + howManyDone + (isDone() ? " (Terminé)":"");
    }

}
