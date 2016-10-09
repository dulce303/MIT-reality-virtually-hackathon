using UnityEngine;
using System.Collections;

public class FocusLight : MonoBehaviour {

    public Light fLight;

    // Use this for initialization
    void Start () {
        if (fLight == null)
        {
            fLight = gameObject.AddComponent<Light>();
            fLight.color = Color.yellow;
            fLight.transform.position = new Vector3(0, 0.5f, 0);
            fLight.intensity = 0;
        }
    }
	
	public void Activate () {
        fLight.intensity = 1;
	}

    public void Deactivate()
    {
        fLight.intensity = 1;
    }
}
