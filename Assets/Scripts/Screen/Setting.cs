using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    private void OnEnable() {
        float volume =  PlayerPrefs.GetFloat("volume", 0f);
        volumeSlider.value = volume;
    }
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume",volume);
    }

    public void SetLanguage(int index) {
        switch (index)
        {
            case 0: 
            Debug.Log("Indonesia");
            break;
            case 1:
            Debug.Log("English");
            break;
            default:
            break;
        }
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
