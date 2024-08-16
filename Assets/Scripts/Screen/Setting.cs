using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;
    public TMP_Dropdown langDropdown;
    [SerializeField] private AudioMixer audioMixer;
    private void Awake() {
        // LocalizationManager.Read();
        // LocalizationManager.Language = "English";

        langDropdown.onValueChanged.AddListener(delegate {selectvalue(langDropdown);});
    }
    private void OnEnable() {
        float volume =  PlayerPrefs.GetFloat("volume", 0f);
        volumeSlider.value = volume;
    }

    private void selectvalue(TMP_Dropdown dropdown)
    {
        SetLanguage(dropdown.value);
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume",volume);
    }

    public void SetLanguage(int index) {
        switch (index)
        {
            case 0: 
            LocalizationManager.Language = "English";
            break;
            case 1:
            LocalizationManager.Language = "Indonesia";
            break;
            default:
            break;
        }
    }

    private void OnDisable() {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("lang", langDropdown.value);
    }
}
