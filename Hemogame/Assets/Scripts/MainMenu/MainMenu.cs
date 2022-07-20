using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainTitle;

    [SerializeField]
    GameObject panelMain;

    [SerializeField]
    GameObject panelChapitres;

    [SerializeField]
    GameObject panelComment;

    [SerializeField]
    GameObject panelPres;

    [SerializeField]
    GameObject panelOption;

    [SerializeField]
    GameObject panelCredits;

    [SerializeField]
    JaugesHemo jauges;

    [SerializeField]
    GameObject hiddenPanel;

    [SerializeField]
    FinSeqBallon finBallon;

    [SerializeField]
    FinSeqRecre finRecre;

    [SerializeField]
    FinSeqSVT finSVT;

    [SerializeField]
    AudioPlayer AP;

    private void Start()
    {
        Time.timeScale = 1;
        jauges.physique = 50;
        jauges.morale = 60;

        finBallon.fin = (int)FinSeqBallon.Cas.NonPart;

        finRecre.blesser = false;
        finRecre.retard = false;

        finSVT.binome = "";
        finSVT.blesserBinome = false;
        finSVT.blesserTom = false;
        AP.playFadeMusic();
    }
    public void buttonJouer()
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadSceneAsync(1);
        AP.stopFadeMusic();
    }

    public void buttonRetour()
    {
        panelChapitres.SetActive(false);
        panelComment.SetActive(false);
        panelOption.SetActive(false);
        panelCredits.SetActive(false);
        panelMain.SetActive(true);
    }

    public void buttonChaptitres()
    {
        panelComment.SetActive(false);
        panelMain.SetActive(false);
        panelChapitres.SetActive(true);
    }

    public void buttonComment()
    {
        panelMain.SetActive(false);
        panelChapitres.SetActive(false);
        panelComment.SetActive(true);
    }

    public void buttonOption()
    {
        panelComment.SetActive(false);
        panelMain.SetActive(false);
        panelOption.SetActive(true);
    }

    public void buttonCredits()
    {
        panelComment.SetActive(false);
        panelMain.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void buttonQuitter()
    {
        Application.Quit();
    }

    public void showHiddenPanel()
    {
        hiddenPanel.SetActive(true);
    }

    public void buttonRetourPres()
    {
        //panelChapitres.SetActive(false);
        //panelComment.SetActive(false);
        panelPres.SetActive(false);
        mainTitle.SetActive(true);
        panelMain.SetActive(true);
    }

    public void buttonJouerToPres()
    {
        panelMain.SetActive(false);
        panelChapitres.SetActive(false);
        panelComment.SetActive(false);
        mainTitle.SetActive(false);
        panelMain.SetActive(false);
        panelPres.SetActive(true);
    }
}
