using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeNoise : MonoBehaviour
{
    public AudioClip triggerSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerSound != null)
        {
            Debug.Log("HORN");
            audioSource.PlayOneShot(triggerSound, 2f);
        }
    }
}
