using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeObjectif : Objectif
{
    public List<Objectif> listeInit;

    public List<Objectif> objDone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool isDone()
    {
        return listeInit.Count <= objDone.Count;
    }

    public override string toString()
    {
        string res = "";

        foreach (Objectif o in listeInit)
            res += "\n- " + o.toString();

        return res;
    }

    public void updateObj()
    {
        objDone.Clear();
        foreach (Objectif o in listeInit)
        {
            if (o.isDone())
                objDone.Add(o);
        }
    }
}
