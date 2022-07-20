using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    public SettingsSave settings;
    public Slider audioSlide;
    public Slider camSlide;
    public MouseLook ML;

    public AudioPlayer AP;

    // Start is called before the first frame update
    void Start()
    {
        audioSlide.value = settings.audioVolume;
        camSlide.value = settings.camSen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateVolume()
    {
        settings.audioVolume = audioSlide.value;
        AP.updateVolume();
    }

    public void updateCam()
    {
        settings.camSen = camSlide.value;
        ML.updateSens();
    }
}
