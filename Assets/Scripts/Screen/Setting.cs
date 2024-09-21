using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public UnityEngine.UI.Slider volumeSlider;
    public TMP_Dropdown langDropdown;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioClip paperSound;

    private int language;
    private float volume;
    private void Awake() {
        langDropdown.onValueChanged.AddListener(delegate {selectvalue(langDropdown);});
    }
    private void OnEnable() {
        AudioManager.instance.PlaySound(paperSound);
    }

    private void selectvalue(TMP_Dropdown dropdown)
    {
        language = dropdown.value;
    }

    public void SetVolume(float _volume) {
        volume = _volume;
        audioMixer.SetFloat("volume",_volume);
    }

    public void SetLanguage(int index) {
        language = index;
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

    public void SaveSetting() {
        SetVolume(volume);
        SetLanguage(language);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("lang", langDropdown.value);
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
