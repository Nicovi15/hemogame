using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    [SerializeField]
    public Outline outline;

    [SerializeField]
    Color couleurSurvol;

    [SerializeField]
    public Color couleurSelected;

    [SerializeField]
    Color couleurSurvolSelected;

    [SerializeField]
    public Color couleurCorrect;

    [SerializeField]
    public Color couleurCorrectSelected;

    [SerializeField]
    ObjectDescription data;

    public bool isSelected = false;

    public bool isValid;

    public GameObject prefabPanel;

    public GameObject panel;

    GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        isSelected = false;
        outline.enabled = false;

        isValid = data.valid;

        if (isValid)
            transform.SetParent(GM.getValidObjects().transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
    }

    private void OnMouseEnter()
    {
        if (GM.findingPhase)
        {
            //Debug.Log("je survole : " + this.name);
            outline.enabled = true;
            if (isSelected)
                outline.OutlineColor = couleurSurvolSelected;
            else
                outline.OutlineColor = couleurSurvol;

            panel = Instantiate(prefabPanel, GM.getCanvasPanelObject().transform);
            panel.SendMessage("setData", data);
            panel.SendMessage("setIsSelected", isSelected);
            /*
            RectTransform rt = panel.GetComponent<RectTransform>();
            Vector2 mousePos = Input.mousePosition;

            float x = mousePos.x;
            if(mousePos.x < Screen.width/2)
                x = mousePos.x + rt.rect.width > Screen.width ? mousePos.x - rt.rect.width : mousePos.x + rt.rect.width;
            else
                x = mousePos.x - rt.rect.width < 0 ? mousePos.x + rt.rect.width : mousePos.x - rt.rect.width;

            float y = mousePos.y;

            if (mousePos.x < Screen.height / 2)
                y = mousePos.y + rt.rect.height / 4 > Screen.height ? mousePos.y - rt.rect.height / 4 : mousePos.y + rt.rect.height / 4;
            else
                y = mousePos.y - rt.rect.height / 4 < 0 ? mousePos.y + rt.rect.height / 4 : mousePos.y - rt.rect.height / 4;

            //float x = mousePos.x + rt.rect.width;
            rt.position = new Vector3(x, y, 0);
            */
        }
    }

    private void OnMouseExit()
    {
        if (GM.findingPhase)
        {
            //Debug.Log("je ne survole plus : " + this.name);
            if (!isSelected)
                outline.enabled = false;
            else
            {
                outline.OutlineColor = couleurSelected;
            }

            Destroy(panel);
        }
    }

    private void OnMouseDown()
    {
        if (GM.findingPhase)
        {
            //Debug.Log("clique sur : " +this.name);
            isSelected = !isSelected;

            if (isSelected)
            {
                outline.OutlineColor = couleurSurvolSelected;

                if (!isValid)
                    transform.SetParent(GM.getNoValidSelected().transform);
            }
            else
            {
                outline.OutlineColor = couleurSurvol;

                if (!isValid)
                    transform.SetParent(GM.getObjects().transform);
            }

            panel.SendMessage("setIsSelected", isSelected);
        }
        
    }

    public ObjectDescription getData()
    {
        return data;
    }
}
