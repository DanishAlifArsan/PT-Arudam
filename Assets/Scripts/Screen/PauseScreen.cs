using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseScene;
    [SerializeField] private ItemInteract player;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip clickSound;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.PlaySound(paperSound);
            Time.timeScale = 0;
            pauseScene.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.controller.enabled = false;
            player.enabled = false;
        }
    }

    public void Continue() {
        AudioManager.instance.PlaySound(clickSound);
        Time.timeScale = 1;
        pauseScene.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.enabled = true;
        player.controller.enabled = true;
    }
}
