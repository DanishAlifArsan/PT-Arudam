using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    [SerializeField] private List<GameObject> newsPaperScene; 

    public void ShowScene(int index) {
        newsPaperScene[index].SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RemoveScene(int index) {
        newsPaperScene[index].SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
