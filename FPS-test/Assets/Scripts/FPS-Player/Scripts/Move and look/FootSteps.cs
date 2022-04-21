using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    // This script have to be attached on tha player object.


    Movement movement;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.isWalking == true)
        {
            audioSource.pitch = Random.Range(0.6f, 1.2f);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
         
        }
        else
        {
            audioSource.Stop();
        }
    }
}
