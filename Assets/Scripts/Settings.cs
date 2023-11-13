using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetMaster(Slider volume)
    {
        audioMixer.SetFloat("Master", volume.value);
    }
    public void SetMusic(Slider volume)
    {
        audioMixer.SetFloat("Music", volume.value);
    }
    public void SetSFX(Slider volume)
    {
        audioMixer.SetFloat("SFX", volume.value);
    }
}
