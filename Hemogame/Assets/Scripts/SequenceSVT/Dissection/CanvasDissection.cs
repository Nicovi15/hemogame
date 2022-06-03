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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void achievement()
    {
        Debug.Log("Fini");
    }
}
