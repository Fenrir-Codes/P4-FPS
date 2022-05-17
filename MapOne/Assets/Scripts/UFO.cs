using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    // The lights on UFO turning on and off with a timeDelay on 0.05f
    [SerializeField] private GameObject[] pointLights;

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
                //Debug.Log("Lights off!");
                i.GetComponent<Light>().enabled = false;
                for (int j = 0; j < pointLights.Length; j++)
                {
                    pointLights[j].SetActive(true);
                    yield return new WaitForSeconds(.05f);
                }
                i.GetComponent<Light>().enabled = true;

                //Debug.Log("Lights on!");
            }
        }
    }


}
