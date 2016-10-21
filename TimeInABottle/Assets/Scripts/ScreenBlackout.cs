using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScreenBlackout : Fader {

    private Image img;

	// Use this for initialization
	void Start () {
        img = GetComponentInChildren<Image>();
        setter = SetOpacity;
	}
	
    public void SetOpacity(float value)
    {
        Color color = img.color;
        color.a = value;
        img.color = color;
    }
}
