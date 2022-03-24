using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelObjet : MonoBehaviour
{
    public float xOffset;
    public float yOffset;

    RectTransform rt;
    int coefX;
    int coefY;

    ObjectDescription data;

    bool isSelected;

    [SerializeField]
    TextMeshProUGUI titreTM;

    [SerializeField]
    TextMeshProUGUI descriptionTM;

    [SerializeField]
    GameObject imageWarning;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();

        Vector2 mousePos = Input.mousePosition;
        if (mousePos.x < Screen.width / 2)
            coefX = 1;
        else
            coefX = -1;
        if (mousePos.y < Screen.height / 2)
            coefY = 1;
        else
            coefY = -1;

        adjustPos();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 mousePos = Input.mousePosition;

        float x = mousePos.x + coefX * xOffset;

        float y = mousePos.y + coefY * yOffset;

        if (x + rt.rect.width / 2 > Screen.width)
            x = Screen.width - rt.rect.width / 2;
        else if (x - rt.rect.width / 2 < 0)
            x = rt.rect.width / 2;

        if (y + rt.rect.height/2 > Screen.height)
            y = Screen.height - rt.rect.height / 2;
        else if (y - rt.rect.height / 2 < 0)
            y = rt.rect.height / 2;

    */
        adjustPos();
    }

    public void setData(ObjectDescription od)
    {
        data = od;

        titreTM.text = data.nameObject;
        descriptionTM.gameObject.SetActive(data.isDescribed);
        descriptionTM.text = data.description;

    }

    public void setMessage(string mes)
    {
        titreTM.text = mes;
        descriptionTM.gameObject.SetActive(false);
        imageWarning.SetActive(false);
    }

    public void showOnlyName()
    {
        descriptionTM.gameObject.SetActive(false);
        imageWarning.SetActive(false);
    }

    public void setIsSelected(bool s)
    {
        isSelected = s;
        imageWarning.SetActive(isSelected);
    }

    void adjustPos()
    {
        Vector2 mousePos = Input.mousePosition;

        float x = mousePos.x + coefX * xOffset;

        float y = mousePos.y + coefY * yOffset;

        if (x + rt.rect.width / 2 > Screen.width)
            x = Screen.width - rt.rect.width / 2;
        else if (x - rt.rect.width / 2 < 0)
            x = rt.rect.width / 2;

        if (y + rt.rect.height / 2 > Screen.height)
            y = Screen.height - rt.rect.height / 2;
        else if (y - rt.rect.height / 2 < 0)
            y = rt.rect.height / 2;

        rt.position = new Vector3(x, y, 0);
    }
}
