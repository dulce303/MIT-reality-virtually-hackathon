using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class SceneManager : MonoBehaviour {

    public VRTK_InteractGrab controller1;
    public VRTK_InteractGrab controller2;

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

        Debug.Log("dropping");
        controller1.ForceRelease();
        controller2.ForceRelease();
    }
}
