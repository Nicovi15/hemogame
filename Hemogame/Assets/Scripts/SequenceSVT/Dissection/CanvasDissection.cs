using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDissection : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    public string titre;

    [SerializeField]
    public GameObject image;

    [SerializeField]
    public GameObject ciseaux;

    [SerializeField]
    public GameObject pinces;

    [SerializeField]
    public GameObject scalpel;

    [SerializeField]
    public int coupMax;

    [SerializeField]
    public List<GameObject> correction;

    [SerializeField]
    public bool isOrdered;

    [SerializeField]
    public List<GameObject> emplacementDoigts;

    public bool canChange;

    public float tempsAvantChangement = 4;
    float cdChange = 0;

    GameObject currentEmplacement = null;


    // Start is called before the first frame update
    void Start()
    {
        pickNewEmplacement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canChange)
            return;

        if (cdChange < tempsAvantChangement)
            cdChange += Time.deltaTime;
        else
        {
            cdChange = 0;
            pickNewEmplacement();
        }
    }

    public virtual void achievement()
    {
        Debug.Log("Fini");
    }

    public void pickNewEmplacement()
    {
        if (emplacementDoigts.Count == 0)
            return;

        List<GameObject> tirage = new List<GameObject>(emplacementDoigts);

        if (currentEmplacement != null)
        {
            currentEmplacement.SetActive(false);
            tirage.Remove(currentEmplacement);
        }       
        currentEmplacement = tirage[Random.Range((int)0, (int)tirage.Count)];
        currentEmplacement.SetActive(true);
    }

}
