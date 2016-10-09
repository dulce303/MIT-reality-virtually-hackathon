using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK.Highlighters;

public class AudioZone : MonoBehaviour {

    public SoundItem source;

    private Dictionary<string, object> highlighterOptions;

    void Start()
    {
        highlighterOptions = new Dictionary<string, object>();
        highlighterOptions.Add("resetMainTexture", true);
    }

	void OnTriggerEnter(Collider other)
    {
        var itemSound = other.GetComponent<SoundItem>();
        if(itemSound != null && source != itemSound)
        {
            Debug.Log("AudioZone triggered: " + other.name);

            source = itemSound;
            itemSound.FadeIn();
        }

        var highlight = other.GetComponent<VRTK_BaseHighlighter>();
        if (highlight != null)
        {
            highlight.Initialise(null, highlighterOptions);
        }

        var focusLight = other.GetComponent<FocusLight>();
        if(focusLight != null)
        {
            focusLight.Activate();
        }
    }

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
    }
}
