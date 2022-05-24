using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionJaugeHemo : MonoBehaviour
{
    [SerializeField]
    public JaugesHemo jauges;

    [SerializeField]
    GameObject graphic;

    [SerializeField]
    Image jaugePhysique;

    [SerializeField]
    Image jaugeMorale;

    [SerializeField]
    float vitesseJauge;

    public bool modifMorale = false;
    public bool modifPhysique = false;

    // Start is called before the first frame update
    void Start()
    {
        //addPhysiqueGraph(25, 1, 0, 0);
        //addMoraleGraph(-25, 1, 0, 0);
        jauges.physique = 50;
        jauges.morale = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addPhysique(int x)
    {
        jauges.physique += x;
        jauges.physique = Mathf.Clamp(jauges.physique, 0, 100);
    }

    public void addMorale(int x)
    {
        jauges.morale += x;
        jauges.morale = Mathf.Clamp(jauges.morale, 0, 100);
    }

    public void addPhysiqueGraph(int x, float scale, float posX, float posY)
    {
        int newValue = jauges.physique + x;
        newValue = Mathf.Clamp(newValue, 0, 100);
        graphic.SetActive(true);
        RectTransform rt = graphic.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(posX, posY);
        rt.localScale = new Vector3(scale, scale, 1);

        StartCoroutine(modifJaugePhysique(newValue));
    }

    public IEnumerator modifJaugePhysique(int newValue)
    {
        modifPhysique = true;

        float currentValue = jauges.physique;
        jaugePhysique.fillAmount = currentValue / 100;

        int step = newValue > currentValue ? 1 : -1;

        while(step == 1 ? currentValue < newValue : currentValue > newValue)
        {
            currentValue += step * vitesseJauge * Time.deltaTime;
            jaugePhysique.fillAmount = currentValue / 100;
            yield return null;
        }

        jauges.physique = newValue;
        jaugePhysique.fillAmount = jauges.physique / 100f;

        modifPhysique = false;

        yield return new WaitForSeconds(1);

        if(!modifPhysique && !modifMorale)
            graphic.SetActive(false);
    }


    public void addMoraleGraph(int x, float scale, float posX, float posY)
    {
        int newValue = jauges.morale + x;
        newValue = Mathf.Clamp(newValue, 0, 100);
        graphic.SetActive(true);
        RectTransform rt = graphic.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(posX, posY);
        rt.localScale = new Vector3(scale, scale, 1);

        StartCoroutine(modifJaugeMorale(newValue));
    }

    public IEnumerator modifJaugeMorale(int newValue)
    {
        modifMorale = true;

        float currentValue = jauges.morale;
        jaugeMorale.fillAmount = currentValue / 100;

        int step = newValue > currentValue ? 1 : -1;

        while (step == 1 ? currentValue < newValue : currentValue > newValue)
        {
            currentValue += step * vitesseJauge * Time.deltaTime;
            jaugeMorale.fillAmount = currentValue / 100;
            yield return null;
        }

        jauges.morale = newValue;
        jaugeMorale.fillAmount = jauges.morale / 100f;

        modifMorale = false;

        yield return new WaitForSeconds(1);

        if (!modifPhysique && !modifMorale)
            graphic.SetActive(false);
    }

}
