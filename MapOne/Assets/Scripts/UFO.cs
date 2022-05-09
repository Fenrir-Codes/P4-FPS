using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public GameObject[] pointLights;

    void Start()
    {
        StartCoroutine(shiftingLights());
    }

    IEnumerator shiftingLights()
    {

        foreach (GameObject i in pointLights)
        {
            Debug.Log("Lights off!");
            i.GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(.5f);
            i.GetComponent<Light>().enabled = true;
            Debug.Log("Lights on!");
        }
    }
}
