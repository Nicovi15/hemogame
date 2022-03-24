using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawObjectifList : MonoBehaviour
{
    [SerializeField]
    TextMeshPro text;

    [SerializeField]
    ListeObjectif LO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = LO.toString();
    }
}
