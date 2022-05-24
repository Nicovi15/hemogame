using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receveur : MonoBehaviour
{
    [SerializeField]
    public string nom;

    public int filePos;

    public Vector3 recevPos;

    public float step;

    Animator anim;

    [SerializeField]
    float speed;

    [SerializeField]
    public int maxFile;

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

    [SerializeField]
    public Vector3 posFinFile;

    [SerializeField]
    GestionJaugeHemo jauges;

    [SerializeField]
    public float tropFaible;

    [SerializeField]
    public float tropFort;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (filePos == 0)
        {
            GM.currentReceveur = this;
            lb.setCurrentReceveur(this);
        }
            

        if(isHemo)
        {
            if (jauges.jauges.physique >= 50)
                tropFort += 10;
            else
                tropFort -= 20;
        }
            
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

        if(filePos-1 == 0)
        {
            lb.setCurrentReceveur(this);
            GM.currentReceveur = this;
        }
            
    }

    public void finRetour()
    {
        anim.enabled = false;
        StartCoroutine(placer());
    }

    IEnumerator placer()
    {
        yield return new WaitForSeconds(0.01f);
        transform.position = posFinFile;
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
        lb.addEchauf(-1f);
        MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
        mf.showString(messageFaible, nom, col);

        return true;
    }

    public bool resultatTropFort(Ballon bal)
    {
        if (isHemo /* && bal.type == GM.basketData*/ || (!GM.blesse && (int)Random.Range(0,4) == 0))
        {
            lb.addEchauf(-1f);
            MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
            mf.showString(messageFort, nom, col);

            GM.lancementSequenceChoix();

            return false;
        }
        else
        {
            lb.addEchauf(-1f);
            MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
            mf.showString(messageFort, nom, col);

            return true;
        }
        
    }

    public bool resultatNormal(Ballon bal)
    {
        lb.addEchauf(6);
        //lb.addEchauf(50);
        MessageFeedback mf = Instantiate(canvasMess).GetComponent<MessageFeedback>();
        mf.showString(messageNormal, nom, col);

        if (isHemo)
            jauges.addPhysiqueGraph(7,0.7f,-600,0);

        return true;
    }

    public void sortir()
    {
        Destroy(this.gameObject);

    }

}
