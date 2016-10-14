using UnityEngine;
using UnityEngine.Events;
using VRTK;
using VRTK.UnityEventHelper;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(VRTK_InteractableObject_UnityEvents))]
public class SoundItem : VRTK_InteractableObject
{
    private AudioSource sound;

    public bool useVisualizer = false;
    public double maxVolume = 1;

    public bool played = false;
    public bool ready = true;
    public float delay = 0f;
    public float duration = 0f;

    private float fadeInterval = 0.05f;
    public float fadeSpeed = 0.02f;
    
    protected override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
        
        InteractableObjectGrabbed += new InteractableObjectEventHandler(OnObjectGrabbed);
        InteractableObjectUsed += new InteractableObjectEventHandler(OnObjectUsed);
    }

    void OnObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log(name + " was grabbed");

        PlaySound();
    }

    void OnObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log(name + " was used");

        PlaySound();
    }

    public void PlaySound()
    {
        played = true;

        FadeIn();

        if(useVisualizer)
        {
            SceneManager.instance.visualizer.SetActive(true);
        }

        if(duration > 0f)
        {
            StartCoroutine(TimeOutSound(duration));
        }
    }

    IEnumerator TimeOutSound(float time)
    {
        yield return new WaitForSeconds(time);

        FadeOut();
    }

    public void FadeIn(float startTime = 0f)
    {
        if (sound == null)
            return;

        sound.volume = fadeSpeed;
        sound.time = startTime + delay;
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
        if (sound == null)
            return;

        SceneManager.instance.visualizer.SetActive(false);

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
