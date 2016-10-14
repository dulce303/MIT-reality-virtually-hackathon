using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class SceneManager : MonoBehaviour {

    public VRTK_InteractGrab controller1;
    public VRTK_InteractGrab controller2;

    public LightFader stereoLight;
    public SoundItem stereo;

    public AudioSource WilkeAudio;

    public AudioSource MomAudio;

    public GameObject visualizer;

    bool init = false;

    private float scaleTime = 1f;
    public static SceneManager instance;

    // Use this for initialization
    void Start () {

        instance = this;
        visualizer.SetActive(false);

        stereo.ready = false;

        WilkeAudio.time = 38f;
        WilkeAudio.Play();
        StartCoroutine(DropRing(12f / scaleTime));
    }

    IEnumerator DropRing(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject grabObject = controller1.GetGrabbedObject();
        if (grabObject != null)
        {
            controller1.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
            controller1.ForceRelease();

        }

        grabObject = controller2.GetGrabbedObject();
        if (grabObject != null) {
            controller2.GetComponent<VRTK_ObjectAutoGrab>().enabled = false;
            controller2.ForceRelease();
        }


        StartCoroutine(PlayMom(1f));
    }

    IEnumerator PlayMom(float time)
    {
        yield return new WaitForSeconds(time);

        MomAudio.Play();

        StartCoroutine(StopMom(7f));
    }

    IEnumerator StopMom(float time)
    {
        yield return new WaitForSeconds(time);
        
        stereoLight.FadeIn();
        stereo.ready = true;
    }
}
