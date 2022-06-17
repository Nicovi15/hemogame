using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutonValidePlace : MonoBehaviour
{
    [SerializeField]
    Image im;

    [SerializeField]
    SystemeDecoupe SD;

    public Color validColor;
    public Color nonvalidColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SD.currentPlace != null)
            if (SD.currentPlace.canValide)
                im.color = validColor;
            else
                im.color = nonvalidColor;
    }
}
