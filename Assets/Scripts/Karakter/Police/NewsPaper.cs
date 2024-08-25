using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    [SerializeField] private List<GameObject> newsPaperScene; 
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip clickSound;

    public void ShowScene(int index) {
        AudioManager.instance.PlaySound(paperSound);
        newsPaperScene[index].SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RemoveScene(int index) {
        AudioManager.instance.PlaySound(clickSound);
        newsPaperScene[index].SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
