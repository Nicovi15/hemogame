using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    string text;

    [SerializeField]
    float highlightSize;

    [SerializeField]
    float coefAnim;

    [SerializeField]
    Color normalColor;

    [SerializeField]
    Color highlightColor;

    [SerializeField]
    UnityEvent function;

    TextMeshProUGUI TM;

    TMP_Text tm_text;

    Mesh mesh;

    Vector3[] vertices;

    bool isHighlight = false;


    // Start is called before the first frame update
    void Start()
    {
        TM = GetComponent<TextMeshProUGUI>();
        tm_text = GetComponent<TMP_Text>();

        //function.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHighlight)
            return;


        tm_text.ForceMeshUpdate();
        mesh = tm_text.mesh;
        vertices = mesh.vertices;

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.unscaledTime + i);

            vertices[i] = vertices[i] + offset * coefAnim;
        }

        mesh.vertices = vertices;
        tm_text.canvasRenderer.SetMesh(mesh);
    }

    public void PointerEnter()
    {
        TM.color = highlightColor;
        TM.text = "<size=" + highlightSize.ToString() + "%>"+text;
        isHighlight = true;
    }

    public void PointerExit()
    {
        TM.color = normalColor;
        TM.text = text;
        isHighlight = false;
    }

    public void PointerClick()
    {
        function.Invoke();
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 1.8f));
    }

    private void OnEnable()
    {
        if (TM == null)
            return;

        TM.color = normalColor;
        TM.text = text;
        isHighlight = false;
    }

}
