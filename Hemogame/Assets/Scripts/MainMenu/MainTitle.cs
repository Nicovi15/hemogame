using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTitle : MonoBehaviour
{
    [SerializeField]
    string titre;

    [SerializeField]
    float tailleNormale;

    [SerializeField]
    float tailleGrande;

    [SerializeField]
    float tailleTresGrande;

    [SerializeField]
    float dureeOversize;

    TextMeshProUGUI tm;

    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMeshProUGUI>();
        StartCoroutine(effetOversizeCharacter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator effetOversizeCharacter()
    {
        int pos = 0;
        int prevPos = titre.Length - 1;
        int nextPos = 1;

        while (true)
        {
            string prevElt = "<size=" + tailleGrande.ToString() + "%>" + titre[prevPos]; //+ "<size=" + tailleNormale.ToString() + "%>";
            string elt = "<size=" + tailleTresGrande.ToString() + "%>" + titre[pos]; //+ "<size=" + tailleNormale.ToString() + "%>";
            string nextElt = "<size=" + tailleGrande.ToString() + "%>" + titre[nextPos] + "<size=" + tailleNormale.ToString() + "%>";

            string newTitle = "";

            if (pos == 0)
            {
                newTitle = elt + nextElt + titre.Substring(2, 6) + nextElt;
            }
            else if (pos == titre.Length - 1)
            {
                newTitle = nextElt + titre.Substring(1, 6) + prevElt + elt;
            }
            else
            {
                newTitle = titre.Substring(0, pos - 1) + prevElt + elt + nextElt + titre.Substring(nextPos + 1, titre.Length - nextPos - 1);
            }

            //string newTitle = titre.Remove(pos, 1).Insert(pos, elt);
            tm.text = newTitle;

            pos = pos + 1 == titre.Length ? 0 : pos + 1;
            prevPos = pos == 0 ? titre.Length - 1 : pos - 1;
            nextPos = pos + 1 == titre.Length ? 0 : pos + 1;

            yield return new WaitForSeconds(dureeOversize);
        }
    }
}
