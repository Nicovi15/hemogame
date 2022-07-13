using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChoix : MonoBehaviour
{
    [SerializeField]
    GameObject canvasMess;

    [SerializeField]
    GameManagerSequenceBallon GM;

    [SerializeField]
    GameObject Question;

    [SerializeField]
    Color couleurProf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void afficherMess(string messageNormal)
    {
        MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
        mf.setXpos(-600);
        mf.showString(GM.currentReceveur.nom + messageNormal, "Ludovic", couleurProf);
    }

    public void afficherSang()
    {
        GM.afficherSaignement();
    }

    public void afficherQuestion()
    {
        Question.SetActive(true);
    }

    public void retourBallon()
    {
        GM.retourSequenceBallon();
        GM.cacherSaignement();
    }
}
