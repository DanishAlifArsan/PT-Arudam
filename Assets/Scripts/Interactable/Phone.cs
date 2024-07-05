using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Phone : Interactable
{
    [SerializeField] private GameObject phoneScreen;
    [SerializeField] private List<GameObject> apps;

    private Stack<GameObject> backStack = new Stack<GameObject>();
    public override void OnInteract(ItemInteract broadcaster)
    {
        EnableHighlight(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        phoneScreen.SetActive(true);
        OpenApp(0);
    }

    public void OpenApp(int index) {
        apps[index].SetActive(true);
        backStack.Push(apps[index]);
        Debug.Log(backStack.Peek());
    }

    public void CloseApp() {
        GameObject poppedApp = backStack.Pop();
        Debug.Log(poppedApp);
        poppedApp.SetActive(false);

        if (backStack.Count <= 0) {
           return;
        }
    }

    public override void OnCancel(ItemInteract broadcaster)
    {
        EnableHighlight(true);
        while (backStack.Count > 0) {
            CloseApp();
        }
        broadcaster.canInteract = true;
        broadcaster.controller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        phoneScreen.SetActive(false);
    }
}
