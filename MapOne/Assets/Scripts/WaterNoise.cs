using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNoise : MonoBehaviour
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
            Debug.Log("Splash");
            audioSource.PlayOneShot(triggerSound, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (triggerSound != null)
        {
            Debug.Log("Stop Splash");
            audioSource.Stop();
        }

    }
}
