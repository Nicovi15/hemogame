using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRecre : MonoBehaviour
{
    [SerializeField]
    GameManagerRecre GM;

    [SerializeField]
    Vector3 infirmeriePos;

    bool infirmerie = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInfirmerie()
    {
        infirmerie = true;
        transform.position = infirmeriePos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("finish");
        if (infirmerie)
            GM.finishInfirmerie();
        else
            GM.finish();

        gameObject.SetActive(false);
    }
}
