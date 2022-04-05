using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LancerBallon : MonoBehaviour
{
    [SerializeField]
    Image jauge;

    public float forceJauge;

    [SerializeField]
    float vitesseJauge;

    public bool jaugeage;

    public int dirJauge;

    public float tropFaible;
    public float tropFort;

    public GameObject BallonPrefab;

    [SerializeField]
    GameObject HUD;

    Ballon bal;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(defilementJauge());
        //spawnNewBallon();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && jaugeage)
        {
            StartCoroutine(latenceAvantLancer());
        }

        /*
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(defilementJauge());
        }
        */
    }

    IEnumerator defilementJauge()
    {
        HUD.SetActive(true);

        forceJauge = 0;
        dirJauge = 1;
        jaugeage = true;

        while (jaugeage)
        {
            forceJauge += dirJauge * vitesseJauge * Time.deltaTime;

            forceJauge = Mathf.Clamp(forceJauge, 0, 100);

            if (forceJauge >= 100 || forceJauge<= 0)
                dirJauge = -dirJauge;

            jauge.fillAmount = forceJauge / 100;

            yield return null;
        }
    }

    IEnumerator latenceAvantLancer()
    {
        jaugeage = false;
        yield return new WaitForSeconds(0.2f);

        lancer();

    }

    public void lancer()
    {
        jaugeage = false;

        if (forceJauge < tropFaible)
            bal.lancer("faible");
        else if (forceJauge > tropFort)
            bal.lancer("fort");
        else
            bal.lancer("normal");

        HUD.SetActive(false);
    }

    public void pretAlancer()
    {
        StartCoroutine(defilementJauge());
    }

    public void spawnNewBallon()
    {
        bal = Instantiate(BallonPrefab, this.transform).GetComponent<Ballon>();
        bal.lb = this;

    }
}
