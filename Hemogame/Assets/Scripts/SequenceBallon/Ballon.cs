using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public float tropFaible;
    public float tropFort;
    public Receveur rec;

    Animator anim;
    public LancerBallon lb;

    public ObjectDescription type;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lancer(string force)
    {
        anim.SetTrigger(force);
    }

    public void pretAlancer()
    {
        lb.pretAlancer();
    }

    public void finLancerFaible()
    {
        if(rec.resultatTropFaible(this))
            StartCoroutine(tempFin());
    }

    public void finLancerFort()
    {
        if(rec.resultatTropFort(this))
            StartCoroutine(tempFin());
    }

    public void finLancerNormal()
    {
        if(rec.resultatNormal(this))
            StartCoroutine(tempFin());
    }

    
    IEnumerator tempFin()
    {
        anim.SetTrigger("fin");
        yield return new WaitForSeconds(0.5f);
        lb.spawnNewBallon();
        Destroy(this.gameObject);
    }



}
