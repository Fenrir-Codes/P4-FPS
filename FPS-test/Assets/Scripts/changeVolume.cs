using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class changeVolume : MonoBehaviour
{   
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    Slider gunSlider;
    [SerializeField]
    Slider mainSlider;


    const string mainVolume = "mainVolume";
    const string sfxVolume = "sfxVolume";
    const string gunVolume = "gunVolume";

    private void Awake()
    {
        mainSlider.onValueChanged.AddListener(setMainVolume);
        sfxSlider.onValueChanged.AddListener(setSFXVolume);
        gunSlider.onValueChanged.AddListener(setGunVolume);
    }

    void setMainVolume(float value)
    {
        audioMixer.SetFloat(mainVolume,value);
    }

    void setSFXVolume(float value)
    {
        audioMixer.SetFloat(sfxVolume, value);
    }

    void setGunVolume(float value)
    {
        audioMixer.SetFloat(gunVolume, value);
    }

    //public void adjustVolume(float volume)
    //{
    //    audioMixer.SetFloat("volume", volume);
    //}
}
