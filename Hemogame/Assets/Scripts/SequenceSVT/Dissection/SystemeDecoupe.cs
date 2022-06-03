using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemeDecoupe : MonoBehaviour
{
    [SerializeField]
    public CanvasDissection currentCanvasPrefab;

    [SerializeField]
    public CanvasDissection currentCanvas;

    [SerializeField]
    public GameObject currentImage;

    [SerializeField]
    public GameObject currentOutils;

    [SerializeField]
    Mouse mouse;

    public int nbCoupe;

    public string outilsName = "ciseaux";

    public bool isFailed = false;

    [SerializeField]
    TextMeshProUGUI titre;

    [SerializeField]
    TextMeshProUGUI nbCoup;


    // Start is called before the first frame update
    void Start()
    {
        startSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSequence()
    {
        //selectCiseaux();
        //resetImage();
        //nbCoupe = currentCanvas.coupMax;

        //currentCanvas = Instantiate(currentCanvasPrefab);

        resetImage();
    }


    public void resetImage()
    {
        //Destroy(currentImage);
        //currentImage = Instantiate(currentCanvas.image);
        //mouse.image = currentImage;
        //nbCoupe = currentCanvas.coupMax;
        //mouse.canCoupe = true;
        //showOutils();
        if(currentCanvas != null)
            Destroy(currentCanvas.gameObject);
        currentCanvas = Instantiate(currentCanvasPrefab);
        currentCanvas.gameObject.SetActive(true);
        currentImage = currentCanvas.image;
        mouse.image = currentImage;
        nbCoupe = currentCanvas.coupMax;
        mouse.canCoupe = true;
        titre.text = currentCanvas.titre;
        nbCoup.text = nbCoupe.ToString();

        currentOutils = null;

        if (outilsName == "ciseaux")
            selectCiseaux();
        else if (outilsName == "pinces")
            selectPinces();
        else if (outilsName == "scalpel")
            selectScalpel();

        isFailed = false;
    }

    public void selectCiseaux()
    {
        if (currentOutils != null && outilsName == "ciseaux")
            return;

        if(currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.ciseaux;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "ciseaux";
    }

    public void selectPinces()
    {
        if (currentOutils != null && outilsName == "pinces")
            return;

        //Destroy(currentOutils);
        //currentOutils = Instantiate(currentCanvas.pinces);
        //currentOutils.SetActive(mouse.canCoupe);
        if (currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.pinces;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "pinces";
    }

    public void selectScalpel()
    {
        if (currentOutils != null && outilsName == "scalpel")
            return;

        //Destroy(currentOutils);
        //currentOutils = Instantiate(currentCanvas.scalpel);
        //currentOutils.SetActive(mouse.canCoupe);
        if (currentOutils != null)
            currentOutils.SetActive(false);
        currentOutils = currentCanvas.scalpel;
        currentOutils.SetActive(mouse.canCoupe);
        outilsName = "scalpel";
    }

    public void hideOutils()
    {
        currentOutils.SetActive(false);
    }

    public void showOutils()
    {
        currentOutils.SetActive(true);
    }

    public void proceedCoupe(GameObject pin1, GameObject pin2)
    {
        nbCoupe--;
        nbCoup.text = nbCoupe.ToString();
        if (nbCoupe <= 0)
        {
            mouse.canCoupe = false;
            hideOutils();
        }

        if (!isFailed)
        {

            if (!currentCanvas.isOrdered)
            {
                GameObject aSuppr = null;

                foreach(GameObject o in currentCanvas.correction)
                {
                    GameObject coup11 = o.transform.GetChild(0).gameObject;
                    GameObject coup12 = o.transform.GetChild(1).gameObject;

                    if ((pin1 == coup11 && pin2 == coup12) || (pin2 == coup11 && pin1 == coup12))
                    {
                        aSuppr = o;
                        Debug.Log("good ! ");
                        break;
                    }
                }

                if (aSuppr != null)
                    currentCanvas.correction.Remove(aSuppr);
                else
                    isFailed = true;
                
            }
            else
            {
                GameObject coup11 = currentCanvas.correction[0].transform.GetChild(0).gameObject;
                GameObject coup12 = currentCanvas.correction[0].transform.GetChild(1).gameObject;

                if ((pin1 == coup11 && pin2 == coup12) || (pin2 == coup11 && pin1 == coup12))
                {
                    currentCanvas.correction.Remove(currentCanvas.correction[0]);
                    Debug.Log("good ! ");
                }
                else
                    isFailed = true;
            }

            if(currentCanvas.correction.Count <= 0)
            {
                currentCanvas.achievement();
            }
        }

        
    }



}
