﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class SceneManager : MonoBehaviour {

    public VRTK_InteractGrab controller1;
    public VRTK_InteractGrab controller2;

    public LightFader stereoLight;
    public SoundItem stereo;

    public GameObject visualizer;

    bool init = false;

    private float scaleTime = 1f;

    // Use this for initialization
    void Start () {

        visualizer.SetActive(false);

        stereo.enabled = false;
        StartCoroutine(DropRing(50f / scaleTime));

    }
	
	// Update is called once per frame
	void Update () {
        
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

        stereoLight.FadeIn();
        stereo.enabled = true;

        StartCoroutine(PlayStereo(10f / scaleTime));
    }

    IEnumerator PlayStereo(float time)
    {
        yield return new WaitForSeconds(time);

        stereo.FadeIn();
        visualizer.SetActive(true);

        StartCoroutine(StopStereo(20f));
    }

    IEnumerator StopStereo(float time)
    {
        yield return new WaitForSeconds(time);

        stereo.FadeOut();
        visualizer.SetActive(false);
    }
}
