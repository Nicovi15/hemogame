using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource AS;

    public SettingsSave settings;

    public float dureeFadeIn;

    public float dureeFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        //AS = GetComponent<AudioSource>();
        //playFadeMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playMusic()
    {
        updateVolume();
        AS.volume = settings.audioVolume;
        AS.Play();
    }

    public void stopMusic()
    {
        updateVolume();
        AS.Stop();
    }

    public void playFadeMusic()
    {
        updateVolume();
        StartCoroutine(fadeIn());
    }

    public void stopFadeMusic()
    {
        updateVolume();
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn()
    {
        float t = 0;
        AS.Play();
        while (t < dureeFadeIn)
        {
            t += Time.deltaTime;
            AS.volume = Mathf.Lerp(0, settings.audioVolume, t / dureeFadeIn);
            yield return null;
        }
        AS.volume = settings.audioVolume;
    }

    IEnumerator fadeOut()
    {
        float t = 0;
        while (t < dureeFadeOut)
        {
            t += Time.deltaTime;
            AS.volume = Mathf.Lerp(settings.audioVolume, 0, t / dureeFadeOut);
            yield return null;
        }
        AS.volume = settings.audioVolume;
        AS.Stop();
    }


    public void updateVolume()
    {
        AS.volume = settings.audioVolume;
    }

}
