using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    // This script have to be attached on tha player object.

    Movement movement;  // MOVEMENT SCRIPT
    private AudioSource audioSource;
    private AudioSource stoneSteps;
    public Transform cube;
    private string floorTag;

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
        stoneSteps = cube.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            //This is the colliders tag
            floorTag = hit.collider.tag;
            Debug.Log(floorTag);
        }

        if (movement.isWalking == true)
        {
            audioSource.pitch = Random.Range(0.6f, 1.2f);
            if (!audioSource.isPlaying && floorTag == "ForestFloor")
            {
                audioSource.Play();
                stoneSteps.Stop();
            }
            if (!stoneSteps.isPlaying && floorTag == "Stone")
            {
                audioSource.Stop();
                stoneSteps.Play();
            }
        }
        else
        {
            audioSource.Stop();
            stoneSteps.Stop();
        }

    }
}
