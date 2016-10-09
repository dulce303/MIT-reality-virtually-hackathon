using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class SceneManager : MonoBehaviour {

    public VRTK_InteractGrab controller1;
    public VRTK_InteractGrab controller2;

    public LightFader stereoLight;

    bool init = false;

    // Use this for initialization
    void Start () {

        StartCoroutine(DropRing(5f));

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
    }
}
