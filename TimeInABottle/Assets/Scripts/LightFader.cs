using UnityEngine;
using System.Collections;

public class LightFader : MonoBehaviour {

    public Light targetLight;

    public double maxIntensity = 6;


    private float fadeInterval = 0.01f;
    public float fadeSpeed = 1f;

    // Use this for initialization
    void Start () {
	    if(targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }
	}
	
    public void FadeIn()
    {
        if (targetLight == null)
            return;

        targetLight.intensity = fadeSpeed;
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        while (targetLight.intensity < maxIntensity)
        {
            targetLight.intensity += fadeSpeed;
            yield return new WaitForSeconds(fadeInterval);
        }
    }

    public void FadeOut()
    {
        if (targetLight == null)
            return;

        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        while (targetLight.intensity > 0f)
        {
            targetLight.intensity -= 0.01f;
            yield return new WaitForSeconds(fadeInterval);
        }
    }
}
