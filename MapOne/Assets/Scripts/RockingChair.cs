using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockingChair : MonoBehaviour
{
    // Making the rocking chair move when player enter trigger

    public GameObject rockingChair;

    public bool playerNearChair;

    void Start()
    {
        playerNearChair = false;
        //rockingChair.SetActive(false);
    }

    // void Update()
    // {
    //     if (playerNearChair)
    //     {
    //         rockingChair.SetActive(!rockingChair.activeSelf);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(startRocking());
            rockingChair.SetActive(true);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animation>().Play("rockingChair");

        }

    }

    // private void OnTriggerExit(Collider other) {

    //         gameObject.GetComponent<Animation>().Stop("rockingChair");
    // }

    // public IEnumerator startRocking()
    // {
    //     rockingChair.SetActive(true);
    //     gameObject.GetComponent<AudioSource>().Play();
    //     gameObject.GetComponent<Animation>().Play("rockingChair");
    //     yield return new WaitForSeconds(16f);
    // }

}
