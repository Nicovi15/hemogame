using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDebugSwitchScene : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            menu.SetActive(!menu.activeSelf);
    }

    public void lancerScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
