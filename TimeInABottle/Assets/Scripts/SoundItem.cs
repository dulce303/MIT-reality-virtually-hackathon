using UnityEngine;
using VRTK;
using System.Collections;
using System.Collections.Generic;

public class SoundItem : VRTK_InteractableObject
{
    public AudioSource sound;
    public double maxVolume = 1;


    private float fadeInterval = 0.05f;
    public float fadeSpeed = 0.02f;

    public override void StartUsing(GameObject usingObject)
    {
        base.StartUsing(usingObject);
        Debug.logger.Log("Using SoundItem: " + name);
        sound.Play();
    }

    protected override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
    }

    public void FadeIn()
    {
        sound.volume = fadeSpeed;
        sound.Play();
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        while (sound.volume < maxVolume)
        {
            sound.volume += fadeSpeed;
            yield return new WaitForSeconds(fadeInterval);
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        while (sound.volume > 0f)
        {
            sound.volume -= 0.01f;
            yield return new WaitForSeconds(fadeInterval);
        }
        sound.Stop();
    }

}
