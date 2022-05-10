using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public bool playerNearLight; // Chech if the player is in trigger

    public GameObject turnOnTxt; // Helper text

    public GameObject lightSwitch;

    void Start()
    {
        playerNearLight = false;
        turnOnTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
