using UnityEngine;
using VRTK;
using System.Collections;

public class GrabTarget : MonoBehaviour {

    public AudioSource nearAudio;

	// Use this for initialization
	void Start () {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(DoTriggerPressed);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoTriggerPressed(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("object grabbed");
        nearAudio.Play();
    }
}
