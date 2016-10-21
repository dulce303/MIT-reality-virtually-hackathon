using UnityEngine;
using System.Collections;
using System;

public class Fader : MonoBehaviour {
    
    public float minVal = 0f;
    public float maxVal = 1f;
    public float duration = 2f;
    public Action<float> setter;
    
    protected float curVal;
    protected float fadeInterval = 0.01f;

    protected Action fadeInCallback;
    protected Action fadeOutCallback;

    public void FadeIn(Action callback = null)
    {
        if (setter == null)
            return;

        fadeInCallback = callback;
        curVal = minVal;
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        while (curVal < maxVal)
        {                   
            curVal += Time.deltaTime / duration;
            setter(curVal);
            yield return null;
        }

        if(fadeInCallback != null)
            fadeInCallback();
    }

    public void FadeOut(Action callback = null)
    {
        if (setter == null)
            return;

        fadeOutCallback = callback;
        curVal = maxVal;
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        while (curVal > minVal)
        {
            curVal -= Time.deltaTime / duration;
            setter(curVal);
            yield return null;
        }

        if(fadeOutCallback != null)
            fadeOutCallback();
    }
}
