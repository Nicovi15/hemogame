using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunCanvas : MonoBehaviour
{
    [SerializeField]
    PlayerRecre PR;

    [SerializeField]
    RectTransform vitesseAiguille;

    [SerializeField]
    RectTransform flecheDir;

    [SerializeField]
    Image flecheImage;

    [SerializeField]
    Transform capsuleDir;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Color couleur1;

    [SerializeField]
    Color couleur2;

    [SerializeField]
    Color couleur3;

    [SerializeField]
    RectTransform coeurVie;

    float coeurScale;

    [SerializeField]
    float dureeEntreBatttements;

    [SerializeField]
    float dureeBattementUp;

    [SerializeField]
    float dureeBattementDown;

    float t = 0;
    bool enBattement = false;

    float battement = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vitesseAiguille.rotation = Quaternion.Euler(0, 0, 90 - 180 * (PR.currentMoveSpeed / PR.moveSpeedMax));

        flecheDir.rotation = Quaternion.Euler(0, 0, -capsuleDir.rotation.eulerAngles.y);

        float d = Vector3.Dot(playerTransform.forward, capsuleDir.transform.forward);

        if (d > 0.75)
            flecheImage.color = couleur1;
        else if (d > 0)
            flecheImage.color = couleur2;
        else
            flecheImage.color = couleur3;

        if (PR.currentVie > 0)
            coeurScale = PR.currentVie / PR.vieMax;
        else
            coeurScale = 0.05f;


        coeurVie.localScale = new Vector3(coeurScale + battement, coeurScale + battement, 1);

        t += Time.deltaTime;
        if(t > dureeEntreBatttements && !enBattement)
        {
            StartCoroutine(battre());
        }

    }

    IEnumerator battre()
    {
        enBattement = true;
        battement = 0.05f;

        yield return new WaitForSeconds(dureeBattementUp);

        battement = 0;

        yield return new WaitForSeconds(dureeBattementDown);

        battement = 0.05f;

        yield return new WaitForSeconds(dureeBattementUp);

        battement = 0;
        enBattement = false;
        t = 0;

        yield return null;
    }

    private void OnDisable()
    {
        enBattement = false;
    }
}
