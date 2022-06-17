using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoigtsDissec : MonoBehaviour
{
    [SerializeField]
    GameObject blessure;

    SpriteRenderer SR;

    public bool isTom;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        if (!isTom)
            SR.color = GameObject.Find("SystemeDecoupe").GetComponent<SystemeDecoupe>().currentBinome.couleur;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void afficheBlessure()
    {
        //Debug.Log("blessure");
        blessure.SetActive(true);
    }

    public void cacherBlessure()
    {
        //Debug.Log("guéri");
        blessure.SetActive(false);
    }
}
