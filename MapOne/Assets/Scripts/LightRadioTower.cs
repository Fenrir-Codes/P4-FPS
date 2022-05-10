using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadioTower : MonoBehaviour
{
    public Light radioLight;


    void Start()
    {
        StartCoroutine(onOffLightRadioTower());
    }

    IEnumerator onOffLightRadioTower()
    {

        while (true)
        {
            radioLight.enabled = false;
            yield return new WaitForSeconds(5f);
            radioLight.enabled = true;
            yield return new WaitForSeconds(1f);
            radioLight.enabled = false;
        }
    }
}
