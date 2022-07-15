using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    GameObject panelMain;

    [SerializeField]
    GameObject panelChapitres;

    [SerializeField]
    GameObject panelComment;

    [SerializeField]
    JaugesHemo jauges;

    [SerializeField]
    GameObject hiddenPanel;

    private void Start()
    {
        Time.timeScale = 1;
        jauges.physique = 50;
        jauges.morale = 60;
    }
    public void buttonJouer()
    {
        //SceneManager.LoadScene(1);
        SceneManager.LoadSceneAsync(1);
    }

    public void buttonRetour()
    {
        panelChapitres.SetActive(false);
        panelComment.SetActive(false);
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

    public void buttonQuitter()
    {
        Application.Quit();
    }

    public void showHiddenPanel()
    {
        hiddenPanel.SetActive(true);
    }
}
