using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorFollow : MonoBehaviour
{
    [SerializeField]
    Canvas myCanvas;

    [SerializeField]
    GameObject normal;

    [SerializeField]
    GameObject prendre;

    [SerializeField]
    GameObject interact;

    [SerializeField]
    GameObject parler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
    }

    public void setNormal()
    {
        hide();
        normal.SetActive(true);
    }

    public void setPrendre()
    {
        hide();
        prendre.SetActive(true);
    }

    public void setInteract()
    {
        hide();
        interact.SetActive(true);
    }

    public void setParler()
    {
        hide();
        parler.SetActive(true);
    }

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    public void hide()
    {
        normal.SetActive(false);
        prendre.SetActive(false);
        interact.SetActive(false);
        parler.SetActive(false);
    }
}
