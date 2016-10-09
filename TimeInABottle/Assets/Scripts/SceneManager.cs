using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class SceneManager : MonoBehaviour {

    public VRTK_InteractGrab controller1;
    public VRTK_InteractGrab controller2;

    public LightFader stereoLight;
    public SoundItem stereo;

    bool init = false;

    private float scaleTime = 1;

    // Use this for initialization
    void Start () {


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

    }
}
