using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoAssisRecre : MonoBehaviour
{
    [SerializeField]
    bool gauche;

    [SerializeField]
    bool droite;

    // Start is called before the first frame update
    void Start()
    {
        if (gauche)
            GetComponent<Animator>().SetTrigger("Gauche");
        else if(droite)
            GetComponent<Animator>().SetTrigger("Droite");
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
