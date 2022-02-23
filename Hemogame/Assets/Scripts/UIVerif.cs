using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIVerif : MonoBehaviour
{
    public enum MODES{VS, NVS, VNS};
    
    public MODES mode;

    [SerializeField]
    GameManager GM;

    [SerializeField]
    TextMeshProUGUI mainTitre;

    [SerializeField]
    TextMeshProUGUI score;

    [SerializeField]
    TextMeshProUGUI detailScore;

    public int bonnesRep = 0;
    public int oublis = 0;
    public int erreur = 0;
    public int tousObjets = 0;

    [SerializeField]
    TextMeshProUGUI titre;

    [SerializeField]
    Button bEx;

    [SerializeField]
    Button bBg;

    [SerializeField]
    Button bLeft;

    [SerializeField]
    Button bRight;

    [SerializeField]
    Button bTerminer;

    [SerializeField]
    TextMeshProUGUI texte;

    public bool isEx;

    public int index;
    public int maxIndex = -1;

    public ClickObject currentObjet = null;

    public List<ClickObject> objetsValidesSel;

    public List<ClickObject> objetsValidesNonSel;

    public List<ClickObject> objetsNonValideSel;

    public List<ClickObject> objets;

    public GameObject inspect;

    public Transform posInspect;

    public LayerMask layerInspect;

    public testS t;

    public int VS;
    public int NVS;
    public int VNS;

    public int max;

    public Color VScolor;
    public Color NVScolor;
    public Color VNScolor;

    public Color VScolor_outline;
    public Color NVScolor_outline;
    public Color VNScolor_outline;

    public List<Image> backgrounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        bTerminer.interactable = false;

        for (int i = 0; i < GM.getValidObjects().transform.childCount; i++)
        {
            tousObjets++;
            ClickObject current = GM.getValidObjects().transform.GetChild(i).gameObject.GetComponent<ClickObject>();
            if (current.isSelected)
                objetsValidesSel.Add(current);
            else
                objetsValidesNonSel.Add(current);
        }

        for(int i = 0; i < GM.getNoValidSelected().transform.childCount; i++)
        {
            objetsNonValideSel.Add(GM.getNoValidSelected().transform.GetChild(i).gameObject.GetComponent<ClickObject>());
        }

        foreach (var o in objetsValidesSel)
            objets.Add(o);
        foreach (var o in objetsNonValideSel)
            objets.Add(o);
        foreach (var o in objetsValidesNonSel)
            objets.Add(o);


        //VS = 0;
        //NVS = objetsValidesSel.Count;
        //VNS = NVS + objetsNonValideSel.Count;
        max = objets.Count;

        index = 0;

        updateIndex();
        //updateMode();
        /*
        isEx = true;
        
        currentObjet = objets[index];

        inspect = Instantiate(currentObjet.gameObject, posInspect.position, new Quaternion());
        inspect.transform.parent = null;
        inspect.layer = LayerMask.NameToLayer("Test");
        t.target = inspect;
        GM.setCFCtarget(currentObjet.gameObject);
        bTerminer.interactable = false;

        if (currentObjet.isSelected)
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrectSelected;
        else
        {
            currentObjet.outline.enabled = true;
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrect;
        }

        currentObjet.outline.OutlineMode = Outline.Mode.OutlineAll;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObjet != null)
        {
            titre.text = currentObjet.getData().nameObject;

            if (isEx)
                texte.text = currentObjet.getData().explication;
            else
                texte.text = currentObjet.getData().bonGeste;

            if (isEx)
            {
                bEx.interactable = false;
                bBg.interactable = true;
            }
            else
            {
                bEx.interactable = true;
                bBg.interactable = false;
            }

            if (index == 0)
                bLeft.interactable = false;
            else
                bLeft.interactable = true;

            if (index == max - 1)
                bRight.interactable = false;
            else
                bRight.interactable = true;

            if (index == max - 1)
                bTerminer.interactable = true;


            score.text = "Score : " + Mathf.Max((bonnesRep * 15 - oublis * 10 - erreur * 5), 0);

            if (maxIndex == max - 1)
            {
                detailScore.text = "Bonnes réponses : " + bonnesRep + "/" + tousObjets + " | Erreurs : " + erreur;
            }
            else
            {
                detailScore.text = "Bonnes réponses : " + bonnesRep + " | Erreurs : " + erreur;
            }
        }
    }


    public void clickBex()
    {
        isEx = true;
    }

    public void clickBbg()
    {
        isEx = false;
    }

    public void clickLeft()
    {
        index--;
        if (index == 0)
            bLeft.interactable = false;
        updateIndex();
    }

    public void clickRight()
    {
        index++;
        if (index == max - 1)
            bRight.interactable = false;
        updateIndex();
    }

    public void updateIndex()
    {
        if(currentObjet != null)
        {
            /*
            currentObjet.outline.OutlineMode = Outline.Mode.OutlineVisible;

            if (currentObjet.isSelected)
                currentObjet.outline.OutlineColor = currentObjet.couleurSelected;
            else
            {
                currentObjet.outline.enabled = false;
            }
            */
            Destroy(inspect);
        }

        isEx = true;
        currentObjet = objets[index];

        if (currentObjet.getData().valid)
        {
            currentObjet.outline.enabled = true;
            bBg.gameObject.SetActive(true);

            if (currentObjet.isSelected)
            {
                mainTitre.text = "Vérification : Bonne réponse";
                updateColor(VScolor);
                currentObjet.outline.OutlineColor = VScolor_outline;

                if (index > maxIndex)
                    bonnesRep++;
            }
            else
            {
                mainTitre.text = "Vérification : Oubli";
                updateColor(VNScolor);
                currentObjet.outline.OutlineColor = VNScolor_outline;

                if (index > maxIndex)
                    oublis++;
            }
        }
        else
        {
            mainTitre.text = "Vérification : Erreur";
            updateColor(NVScolor);
            currentObjet.outline.OutlineColor = NVScolor_outline;
            bBg.gameObject.SetActive(false);

            if (index > maxIndex)
                erreur++;
        }

        if (index > maxIndex)
            maxIndex = index;

        Quaternion q = new Quaternion();
        q.eulerAngles = currentObjet.getData().inspecRot;
        inspect = Instantiate(currentObjet.gameObject, posInspect.position, q);
        inspect.transform.position = inspect.transform.position  + currentObjet.getData().inspecPos;
        inspect.transform.localScale = currentObjet.getData().inspecScale;
        inspect.transform.parent = null;
        if (inspect.GetComponent<PathFollower>())
            inspect.GetComponent<PathFollower>().enabled = false;
        //inspect.layer = LayerMask.NameToLayer("Test");
        MoveToLayer(inspect.transform, LayerMask.NameToLayer("Test"));
        t.target = inspect;
        GM.setCFCtarget(currentObjet.gameObject);

        /*
        if (currentObjet.isSelected)
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrectSelected;
        else
        {
            currentObjet.outline.enabled = true;
            currentObjet.outline.OutlineColor = currentObjet.couleurCorrect;
        }
        */

        currentObjet.outline.OutlineMode = Outline.Mode.OutlineAll;

    }


    public void clickTerminer()
    {
        SceneManager.LoadScene(0);
    }

    public void updateMode()
    {
        if (index >= VNS && VNS != NVS && NVS != VS)
            mode = MODES.VNS;
        else if (index < VNS && index >= NVS && NVS != VS)
            mode = MODES.NVS;
        else
            mode = MODES.VS;
    }

    public void updateColor(Color c)
    {
        foreach (var b in backgrounds)
            b.color = c;
    }

    void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            MoveToLayer(child, layer);
    }
}
