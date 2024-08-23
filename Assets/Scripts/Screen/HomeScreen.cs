using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Setting settingScreen;
    [SerializeField] private GameObject  creditScreen;
    [SerializeField] private bool inGame = false;

    private void Awake() {
        Time.timeScale = 1;
        if (inGame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        Setup();
    }

    private void Setup() {
        LocalizationManager.Read();
        float volume =  PlayerPrefs.GetFloat("volume", 0f);
        settingScreen.volumeSlider.value = volume;
        settingScreen.SetVolume(volume);

        int lang =  PlayerPrefs.GetInt("lang", 0);
        settingScreen.langDropdown.value = lang;
        settingScreen.SetLanguage(lang);
    }

    public void LoadScene(int sceneId) {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    private IEnumerator LoadSceneAsync(int sceneId) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScene.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    
    public void ShowSetting(bool status) {
        settingScreen.gameObject.SetActive(status);    
    }

    public void ShowCredit(bool status) {
        creditScreen.gameObject.SetActive(status);
    }

    public void CloseGame() {
        Application.Quit();
        
         #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
