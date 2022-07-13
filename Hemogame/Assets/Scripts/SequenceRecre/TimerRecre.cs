using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerRecre : MonoBehaviour
{
    [SerializeField]
    public float timer;

    [SerializeField]
    public float retard = 1;

    [SerializeField]
    TextMeshProUGUI chrono;

    float unit = 0;

    bool enCours = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enCours)
            return;

        unit += Time.deltaTime;
        if(unit > 1)
        {
            if (timer >= 0)
                timer--;
            else
                retard ++;
            unit = 0;
        }

        float temps = timer >= 0 ? timer : retard;

        float minutes = Mathf.Floor(temps / 60);
        float secondes = Mathf.RoundToInt(temps % 60);
        string minutesAffi;
        string secodesAffi;

        if(minutes < 10)
            minutesAffi = " " + minutes.ToString();
        else
            minutesAffi = minutes.ToString();


        if (secondes < 10)
            secodesAffi = "0" + Mathf.RoundToInt(secondes).ToString();
        else
            secodesAffi = secondes.ToString();

        if(timer >= 0)
            chrono.text = minutesAffi + ":" + secodesAffi;
        else
        {
            chrono.color = Color.red;
            chrono.text = "Retard : " + minutesAffi + ":" + secodesAffi;
        }
    }

    public void startTimer()
    {
        enCours = true;
    }

    public void endTimer()
    {
        enCours = false;
    }
}
