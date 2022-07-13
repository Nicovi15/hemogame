using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IGMenu : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    [SerializeField]
    GameObject panelComment;

    CursorLockMode CLM;
    public bool cursorVisible;

    public bool isOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel.activeSelf)
                disablePanel();
            else
                enablePanel();
        }
    }

    public void disablePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        isOpened = false;
        Cursor.visible = cursorVisible;
        Cursor.lockState = CLM;
    }

    public void enablePanel()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        CLM = Cursor.lockState;
        cursorVisible = Cursor.visible;
        isOpened = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void commentJouer()
    {
        panel.SetActive(false);
        panelComment.SetActive(true);
    }

    public void boutonRetour()
    {
        panel.SetActive(true);
        panelComment.SetActive(false);
    }

    public void retourMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitter()
    {
        Application.Quit();
    }
    

}
