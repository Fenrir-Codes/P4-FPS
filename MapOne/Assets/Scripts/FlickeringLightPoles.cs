using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLightPoles : MonoBehaviour
{
    public Light spotLight;
    public Light pointLigth;
    public bool isFlickering = false;
    public float timeDelay;

    void Update()
    {

        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        // The lower the number, the more flickering (0.01f, change this)
        timeDelay = Random.Range(0.01f, 1f);
        isFlickering = true;
        // How long the light is out

        pointLigth.enabled = false;
        spotLight.enabled = false;
        yield return new WaitForSeconds(timeDelay);
        pointLigth.enabled = true;
        spotLight.enabled = true;
        // How long between the flickering
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
