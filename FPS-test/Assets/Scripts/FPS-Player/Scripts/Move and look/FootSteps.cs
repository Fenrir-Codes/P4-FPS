using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    // This script have to be attached on tha player object.

    Movement movement;  // MOVEMENT SCRIPT
    private AudioSource defaultAudio;
    private AudioSource StoneEffect;
    public Transform StoneSurface;
    public Transform defaultSurface;
    private string Tag;

    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<Movement>();
        defaultAudio = GetComponent<AudioSource>();
        StoneEffect = StoneSurface.GetComponent<AudioSource>();
}

    // Update is called once per frame
    void Update()
    {
        playEffects();
    }

    void playEffects()
    {
        defaultAudio.pitch = Random.Range(0.4f, 1.5f);

        if (movement.isWalking == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Tag = hit.collider.tag;
                Debug.Log(Tag);

                if (!defaultAudio.isPlaying && Tag == "Gravel")
                {
                    defaultAudio.Play();
                    StoneEffect.Stop();
                }
                if (!StoneEffect.isPlaying && Tag == "Stone")
                {
                    defaultAudio.Stop();
                    StoneEffect.Play();
                }

            }
        }
        else
        {
            defaultAudio.Stop();
            StoneEffect.Stop();
        }
    }
}
