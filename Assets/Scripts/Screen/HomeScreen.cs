using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScene;
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

    public void CloseGame() {
        Application.Quit();
    }
}
