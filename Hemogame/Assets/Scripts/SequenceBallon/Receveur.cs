using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receveur : MonoBehaviour
{
    [SerializeField]
    string nom;

    public int filePos;

    public Vector3 recevPos;

    public float step;

    Animator anim;

    [SerializeField]
    float speed;

    [SerializeField]
    int maxFile;

    public bool isHemo;

    [SerializeField]
    LancerBallon lb;

    [SerializeField]
    Color col;

    [SerializeField]
    [TextArea]
    string messageNormal;

    [SerializeField]
    [TextArea]
    string messageFort;

    [SerializeField]
    [TextArea]
    string messageFaible;

    [SerializeField]
    GameObject canvasMess;

    [SerializeField]
    GameManagerSequenceBallon GM;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void avancer()
    {
        if(filePos == 0)
        {
            anim.enabled = true;
            anim.SetTrigger("retour");

        }
        else
        {
            StartCoroutine(deplacement());
        }

        if(filePos-1 ==0 )
            lb.currentReceveur = this;
    }

    public void finRetour()
    {
        anim.enabled = false;
        filePos = maxFile;
    }

    IEnumerator deplacement()
    {
        Vector3 posInit = transform.position;
        Vector3 posDest = posInit + new Vector3(step, 0, 0);

        float t = 0;

        while (t < step)
        {
            t += speed * Time.deltaTime;
            transform.position = posInit +new Vector3(t, 0, 0);

            yield return null;
        }

        transform.position = posDest;
        filePos--;
    }

    public bool resultatTropFaible(Ballon bal)
    {
        lb.addEchauf(-5f);
        MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
        mf.showString(messageFaible, nom, col);

        return true;
    }

    public bool resultatTropFort(Ballon bal)
    {
        if (isHemo && bal.type == GM.basketData)
        {
            lb.addEchauf(-5f);
            MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
            mf.showString(messageFort, nom, col);

            GM.lancementSequenceChoix();

            return false;
        }
        else
        {
            lb.addEchauf(-5f);
            MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
            mf.showString(messageFort, nom, col);

            return true;
        }
        
    }

    public bool resultatNormal(Ballon bal)
    {
        lb.addEchauf(10);
        MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
        mf.showString(messageNormal, nom, col);

        return true;
    }

}
