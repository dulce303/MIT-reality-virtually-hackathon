using UnityEngine;
using System.Collections;

public class FocusGlow : MonoBehaviour {

    public Material mat;
    public double maxVolume = 1;
    
    private float glowInterval = 0.05f;
    public float glowSpeed = 0.02f;

    // Update is called once per frame
	void Update () {
	
	}

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    
    //TODO: fade in/out
}
