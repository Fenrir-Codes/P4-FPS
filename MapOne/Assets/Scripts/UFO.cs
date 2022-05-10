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

        while (true)
        {
            foreach (GameObject i in pointLights)
            {
                Debug.Log("Lights off!");
                i.GetComponent<Light>().enabled = false;
                Debug.Log("Lights on!");
                pointLights[0].SetActive(true);
                yield return new WaitForSeconds(.05f);
                pointLights[1].SetActive(true);
                yield return new WaitForSeconds(.05f);
                pointLights[2].SetActive(true);
                yield return new WaitForSeconds(.05f);
                pointLights[3].SetActive(true);
                yield return new WaitForSeconds(.05f);
                pointLights[4].SetActive(true);
                i.GetComponent<Light>().enabled = true;
            }
        }
    }


}
