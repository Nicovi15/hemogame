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
    float[] vitesses;

    [SerializeField]
    float vitesseJauge;

    public bool jaugeage;

    public int dirJauge;

    public float tropFaible;
    public float tropFort;

    public GameObject BallonMoussePrefab;
    public GameObject BallonBasketPrefab;
    public GameObject BallonFootPrefab;

    [SerializeField]
    GameObject HUD;

    Ballon bal;

    public Receveur currentReceveur;

    bool isFirst = true;

    [SerializeField]
    float vitesseEchauf;

    [SerializeField]
    float echauffement;

    public bool enCours;

    public bool termine = false;

    [SerializeField]
    Image barreEchauf;

    [SerializeField]
    ChoixPlacement choixP;

    [SerializeField]
    GameManagerSequenceBallon GM;

    [SerializeField]
    Color jaugeFaible;

    [SerializeField]
    Color jaugeNormal;

    [SerializeField]
    Color jaugeFort;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(defilementJauge());
        //spawnNewBallon();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && jaugeage && Time.timeScale > 0.5f)
        {
            StartCoroutine(latenceAvantLancer());
        }

        /*
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(defilementJauge());
        }
        */

        if (echauffement >= 100)
        {
            enCours = false;
            if(!termine)
                GM.dialogFin();
            termine = true;
           
        }
            

        if (enCours)
            echauffement -= Time.deltaTime * vitesseEchauf;

        if (echauffement <= 0)
            echauffement = 0;

        barreEchauf.fillAmount = echauffement / 100f;
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

            if (forceJauge < tropFaible)
                jauge.color = jaugeFaible;
            else if (forceJauge > tropFort)
                jauge.color = jaugeFort;
            else
                jauge.color = jaugeNormal;

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
        if (!enCours)
            return;


        if (!isFirst)
            GM.avancerFile();

        isFirst = false;

        vitesseJauge = vitesses[Random.Range(0, vitesses.Length)];

        if (choixP.ballonChoisi == GM.mousseData)
            bal = Instantiate(BallonMoussePrefab, this.transform).GetComponent<Ballon>();
        else if(choixP.ballonChoisi == GM.basketData)
            bal = Instantiate(BallonBasketPrefab, this.transform).GetComponent<Ballon>();
        else if (choixP.ballonChoisi == GM.footData)
            bal = Instantiate(BallonFootPrefab, this.transform).GetComponent<Ballon>();

        bal.lb = this;
        bal.rec = currentReceveur;
        //tropFaible = bal.tropFaible;
        //tropFort = bal.tropFort;
    }

    public void addEchauf(float f)
    {
        echauffement += f;
    }

    public void setCurrentReceveur(Receveur r)
    {
        this.currentReceveur = r;
        tropFaible = r.tropFaible;
        tropFort = r.tropFort;

        if (choixP.ballonChoisi == GM.mousseData)
        {
            tropFort += 15;
            tropFaible += 3;
        }
        else if (choixP.ballonChoisi == GM.basketData)
        {
            tropFort -= 10;
            tropFaible -= 3;
        }
        else if (choixP.ballonChoisi == GM.footData)
        {
            tropFort -= 5;
            tropFaible += 5;
        }

    }
}
