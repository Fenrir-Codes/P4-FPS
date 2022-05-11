using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public bool playerNearLight; // Check if the player is in trigger

    public GameObject turnOnTxt; // Helper text

    public GameObject barnLight;

    void Start()
    {
        playerNearLight = false;
        turnOnTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearLight && Input.GetKeyDown(KeyCode.E))
        {
            barnLight.SetActive(!barnLight.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play("lightSwitch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // If player is in trigger zone
        {
            //turnOnTxt.SetActive(true);
            StartCoroutine(showHelpTxt());
            playerNearLight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            turnOnTxt.SetActive(false);
            //StartCoroutine(showHelpTxt());
            playerNearLight = false;
        }
    }

    public IEnumerator showHelpTxt()
    {
        //turnOnTxt.SetActive(false);
        turnOnTxt.SetActive(true);
        yield return new WaitForSeconds(2f);
        turnOnTxt.SetActive(false);
    }
}
