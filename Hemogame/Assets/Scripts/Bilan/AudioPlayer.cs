using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource AS;

    public float volume;

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
        AS.volume = volume;
        AS.Play();
    }

    public void stopMusic()
    {
        AS.Stop();
    }

    public void playFadeMusic()
    {
        StartCoroutine(fadeIn());
    }

    public void stopFadeMusic()
    {
        StartCoroutine(fadeOut());
    }

    IEnumerator fadeIn()
    {
        float t = 0;
        AS.Play();
        while (t < dureeFadeIn)
        {
            t += Time.deltaTime;
            AS.volume = Mathf.Lerp(0, volume, t / dureeFadeIn);
            yield return null;
        }
        AS.volume = volume;
    }

    IEnumerator fadeOut()
    {
        float t = 0;
        while (t < dureeFadeOut)
        {
            t += Time.deltaTime;
            AS.volume = Mathf.Lerp(volume, 0, t / dureeFadeOut);
            yield return null;
        }
        AS.volume = volume;
        AS.Stop();
    }


}
