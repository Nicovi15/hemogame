using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBilan : MonoBehaviour
{
    [SerializeField]
    GameObject titre;

    [SerializeField]
    GameObject boutonRejouer;

    [SerializeField]
    GameObject boutonMenu;

    [SerializeField]
    TextBilan textSport;

    [SerializeField]
    TextBilan textRecrea;

    [SerializeField]
    TextBilan textSVT;

    [SerializeField]
    FinSeqBallon finBallon;

    [SerializeField]
    FinSeqRecre finRecre;

    [SerializeField]
    FinSeqSVT finSVT;

    [SerializeField]
    GestionJaugeHemo GJH;

    [SerializeField]
    Image jaugeMorale;

    [SerializeField]
    Image jaugePhysique;

    [SerializeField]
    float vitesseJauge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void debBilan()
    {

        StartCoroutine(derouleBilan());
    }

    IEnumerator derouleBilan()
    {
        titre.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(remplirJaugeMorale());
        StartCoroutine(remplirJaugePhysique());

        string textBallon = "";
        if (finBallon.fin == (int)FinSeqBallon.Cas.PartEtNonBless)
            textBallon = "Tom a particip� au cours de sport et ne s'est pas fait mal.";
        else if (finBallon.fin == (int)FinSeqBallon.Cas.PartEtBless)
            textBallon = "Tom a particip� au cours de sport et s'est fait mal.";
        else
            textBallon = "Tom n'a pas particip� au cours de sport.";

        textSport.ecrireText(textBallon);

        while (textSport.isRunning)
            yield return null;

        string textRecre = "";
        if (finRecre.blesser)
            textRecre += "Il est tomb� dans la cours de r�cr�ation. ";
        else
            textRecre += "Il n'est pas tomb� dans la cours de r�cr�ation. ";

        if (finRecre.retard)
            textRecre += "Il est arriv� en retard.";
        else
            textRecre += "Il n'est pas arriv� en retard.";

        textRecrea.ecrireText(textRecre);

        while (textRecrea.isRunning)
            yield return null;

        string textTP = "Il a r�alis� l'activit� de SVT avec " + finSVT.binome+" et ";

        if (finSVT.blesserBinome)
            textTP += "il l'a bless�(e) lors de la manipulation. ";
        else
            textTP += "il ne l'a pas bless�(e) lors de la manipulation. ";

        if (finSVT.blesserTom)
            textTP += "Tom s'est bless� durant le cours. ";
        else
            textTP += "Tom ne s'est pas bless� durant le cours.";

        textSVT.ecrireText(textTP);

        while (textSVT.isRunning)
            yield return null;

        yield return new WaitForSeconds(1);

        boutonRejouer.SetActive(true);
        boutonMenu.SetActive(true);
    }

    IEnumerator remplirJaugePhysique()
    {
        float a = 0;

        while(a < GJH.jauges.physique)
        {
            a += vitesseJauge * Time.deltaTime;
            jaugePhysique.fillAmount = a / 100;
            yield return null;
        }
        jaugePhysique.fillAmount = GJH.jauges.physique / 100f;
    }

    IEnumerator remplirJaugeMorale()
    {
        float a = 0;

        while (a < GJH.jauges.morale)
        {
            a += vitesseJauge * Time.deltaTime;
            jaugeMorale.fillAmount = a / 100;
            yield return null;
        }
        jaugeMorale.fillAmount = GJH.jauges.morale / 100f;
    }

}