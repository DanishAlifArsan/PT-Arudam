using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Phone : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject phoneScreen;
    [SerializeField] private List<GameObject> apps;

    private Stack<GameObject> backStack = new Stack<GameObject>();
    public void OnInteract(ItemInteract broadcaster)
    {
        broadcaster.controller.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        phoneScreen.SetActive(true);
        OpenApp(0);
    }

    public void OpenApp(int index) {
        apps[index].SetActive(true);
        backStack.Push(apps[index]);
    }

    public void CloseApp() {
        GameObject poppedApp = backStack.Pop();
        poppedApp.SetActive(false);

        if (backStack.Count <= 0) {
           return;
        }
    }

    public void OnCancel(ItemInteract broadcaster)
    {
        broadcaster.controller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        phoneScreen.SetActive(false);
    }
}
