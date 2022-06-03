using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSVT : MonoBehaviour
{

    [SerializeField]
    public DialogueUI DI;

    [SerializeField]
    DialogueObject diagDebut;

    [SerializeField]
    public JaugesHemo jauges;

    [SerializeField]
    GameObject questionBinome;

    [SerializeField]
    DialogueObject diagFin;

    public Color couleurProf;

    public string binome;

    string toDo = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toDo != "" && !DI.IsOpen)
        {
            if (toDo == "choixBinome")
            {
                questionBinome.SetActive(true);
                toDo = "";
            }
            else if (toDo == "lancerActi")
            {
                changerScene();
            }
            else if(toDo == "changerScene")
            {
                SceneManager.LoadScene(3);
                toDo = "";
            }
        }
    }

    public void dialogDebut()
    {
        toDo = "choixBinome";
        DI.showDialogue(diagDebut);
    }

    public void lancerActi()
    {
        toDo = "lancerActi";
        questionBinome.SetActive(false);
        
    }

    public void changerScene()
    {
        toDo = "changerScene";
        DI.setSpeaker("Professeur", couleurProf); 
        DI.showDialogue(diagFin);
    }

}
