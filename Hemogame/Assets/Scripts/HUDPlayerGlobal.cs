using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDPlayerGlobal : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    ListeObjectif LO;

    [SerializeField]
    GameObject panelObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = LO.toString();
    }

    public void OnOffPanel()
    {
        panelObj.SetActive(!panelObj.activeSelf);
    }
}
