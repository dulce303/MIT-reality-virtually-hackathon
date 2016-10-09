using UnityEngine;
using VRTK.Highlighters;
using VRTK;
using System.Collections;
using System.Collections.Generic;

public class ItemHighlighter : MonoBehaviour {

    private Dictionary<string, object> highlighterOptions;

    public VRTK_BaseHighlighter highlighter;

    // Use this for initialization
    void Start () {
        highlighterOptions = new Dictionary<string, object>();
        highlighterOptions.Add("resetMainTexture", true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate()
    {
        highlighter.Initialise(null, highlighterOptions);
        
    }
}
