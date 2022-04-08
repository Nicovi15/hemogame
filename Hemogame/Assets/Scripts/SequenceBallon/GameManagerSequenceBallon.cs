using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSequenceBallon : MonoBehaviour
{
    [SerializeField]
    public ObjectDescription mousseData;

    [SerializeField]
    public ObjectDescription basketData;

    [SerializeField]
    public ObjectDescription footData;

    [SerializeField]
    List<Receveur> receveurs;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void avancerFile()
    {
        foreach (Receveur r in receveurs)
            r.avancer();
    }

    public void lancementSequenceChoix() {



    }

}
