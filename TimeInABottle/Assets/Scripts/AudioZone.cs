using UnityEngine;
using System.Collections;

public class AudioZone : MonoBehaviour {

    public SoundItem source;

    void Start()
    {
    }

	void OnTriggerEnter(Collider other)
    {
        var itemSound = other.GetComponent<SoundItem>();
        if(itemSound != null && source != itemSound && !itemSound.played)
        {
            Debug.Log("AudioZone triggered: " + other.name);

            source = itemSound;
            itemSound.FadeIn();
        }
    }
    /*
    void OnTriggerExit(Collider other)
    {
        var itemSound = other.GetComponent<SoundItem>();
        if (itemSound != null && itemSound == source)
        {
            Debug.Log("AudioZone left: " + other.name);

            source = null;
            itemSound.FadeOut();
        }

        var focusLight = other.GetComponent<FocusLight>();
        if (focusLight != null)
        {
            focusLight.Deactivate();
        }
    }*/
}
