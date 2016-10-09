using UnityEngine;
using VRTK;
using System.Collections;

public class HandController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<VRTK_InteractGrab>().ControllerGrabInteractableObject += new ObjectInteractEventHandler(DoInteractGrab);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoInteractGrab(object sender, ObjectInteractEventArgs e)
    {
        var msg = "Controller grabbed " + e.target.name;
        Debug.Log(msg);
        GameManager.instance.messageLog.text = msg;

        e.target.GetComponent<AudioSource>().Play();
    }
}
