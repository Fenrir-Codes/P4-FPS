using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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

    // AudioSource audioSource;
    // public AudioClip triggerSound;

    // public float fadeOutFactor = 0.08f;
    // public float fadeInFactor = 0.08f;

    // private bool fadeInOut = false;

    // void Start()
    // {
    //     audioSource = GetComponent<AudioSource>();
    //     audioSource.volume = 0.0f;
    // }

    // void Update()
    // {
    //     if (audioSource.volume <= 0.0f) { audioSource.Play(); }
    //     if (audioSource.volume >= 1.0f) { audioSource.Stop(); }

    //     if (fadeInOut == true)
    //     {
    //         if (audioSource.volume < 1.0f)
    //         {
    //             audioSource.volume += fadeInFactor * Time.deltaTime;
    //         }
    //     }

    //     if (fadeInOut == false)
    //     {
    //         if (audioSource.volume > 0.0f)
    //         {
    //             audioSource.volume -= fadeOutFactor * Time.deltaTime;
    //         }
    //     }

    // }

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         fadeInOut = true;
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         fadeInOut = false;
    //     }
    // }
}
