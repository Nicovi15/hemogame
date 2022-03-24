using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoireBallon : Interactable
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interact()
    {
        anim.SetTrigger("Inter");
    }
}
