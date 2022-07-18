using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    Color jaugeVert;

    [SerializeField]
    Color jaugeJaune;

    [SerializeField]
    Color jaugeRouge;

    [SerializeField]
    AudioPlayer AP;

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
        AP.playFadeMusic();
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
            textBallon = "Tom a participé au cours de sport et ne s'est pas fait mal.";
        else if (finBallon.fin == (int)FinSeqBallon.Cas.PartEtBless)
            textBallon = "Tom a participé au cours de sport et s'est fait mal.";
        else
            textBallon = "Tom n'a pas participé au cours de sport.";

        textSport.ecrireText(textBallon);

        while (textSport.isRunning)
            yield return null;

        string textRecre = "";
        if (finRecre.blesser)
            textRecre += "Il est tombé dans la cours de récréation. ";
        else
            textRecre += "Il n'est pas tombé dans la cours de récréation. ";

        if (finRecre.retard)
            textRecre += "Il est arrivé en retard.";
        else
            textRecre += "Il n'est pas arrivé en retard.";

        textRecrea.ecrireText(textRecre);

        while (textRecrea.isRunning)
            yield return null;

        string textTP = "Il a réalisé l'activité de SVT avec " + finSVT.binome+" et ";

        if (finSVT.blesserBinome)
            textTP += "il l'a blessé(e) lors de la manipulation. ";
        else
            textTP += "il ne l'a pas blessé(e) lors de la manipulation. ";

        if (finSVT.blesserTom)
            textTP += "Tom s'est blessé durant le cours. ";
        else
            textTP += "Tom ne s'est pas blessé durant le cours.";

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

            if (jaugePhysique.fillAmount >= 0.66)
                jaugePhysique.color = jaugeVert;
            else if (jaugePhysique.fillAmount >= 0.33)
                jaugePhysique.color = jaugeJaune;
            else
                jaugePhysique.color = jaugeRouge;
        }
        jaugePhysique.fillAmount = GJH.jauges.physique / 100f;

        if (jaugePhysique.fillAmount >= 0.66)
            jaugePhysique.color = jaugeVert;
        else if (jaugePhysique.fillAmount >= 0.33)
            jaugePhysique.color = jaugeJaune;
        else
            jaugePhysique.color = jaugeRouge;
    }

    IEnumerator remplirJaugeMorale()
    {
        float a = 0;

        while (a < GJH.jauges.morale)
        {
            a += vitesseJauge * Time.deltaTime;
            jaugeMorale.fillAmount = a / 100;

            if (jaugeMorale.fillAmount >= 0.66)
                jaugeMorale.color = jaugeVert;
            else if (jaugeMorale.fillAmount >= 0.33)
                jaugeMorale.color = jaugeJaune;
            else
                jaugeMorale.color = jaugeRouge;

            yield return null;
        }
        jaugeMorale.fillAmount = GJH.jauges.morale / 100f;

        if (jaugeMorale.fillAmount >= 0.66)
            jaugeMorale.color = jaugeVert;
        else if (jaugeMorale.fillAmount >= 0.33)
            jaugeMorale.color = jaugeJaune;
        else
            jaugeMorale.color = jaugeRouge;
    }

    public void retourMenu()
    {
        AP.stopFadeMusic();
        SceneManager.LoadScene(0);
    }

    public void rejouer()
    {
        resetFins();
        AP.stopFadeMusic();
        SceneManager.LoadScene(1);
    }

    public void resetFins()
    {
        GJH.jauges.physique = 50;
        GJH.jauges.morale = 60;

        finBallon.fin = (int)FinSeqBallon.Cas.NonPart;

        finRecre.blesser = false;
        finRecre.retard = false;

        finSVT.binome = "";
        finSVT.blesserBinome = false;
        finSVT.blesserTom = false;

    }

}
