using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class Phone : Interactable
{
    [SerializeField] private GameObject phoneScreen;
    [SerializeField] private MeshRenderer phoneObject;
    [SerializeField] private List<GameObject> apps;

    private Stack<GameObject> backStack = new Stack<GameObject>();
    private ItemInteract broadcaster;
    private bool isInteract = false;

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1) ) {
                ClosePhone();
            }
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (SaleManager.instance.isTransaction) return;
        
        base.OnInteract(broadcaster);
        this.broadcaster = broadcaster;
        string indicator = LocalizationManager.Localize("Cancel Phone");
        broadcaster.SetIndicator(true,indicator);
        ToggleHighlight(broadcaster.centerIndicator, false, "Interact Phone");
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        phoneScreen.SetActive(true);
        phoneObject.gameObject.SetActive(false);
        OpenApp(0);
        isInteract = true;
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

    public void ClosePhone() {
        ToggleHighlight(broadcaster.centerIndicator, true, "Interact Phone");
        while (backStack.Count > 0) {
            CloseApp();
        }
        broadcaster.SetIndicator(false);
        broadcaster.canInteract = true;
        broadcaster.controller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        phoneScreen.SetActive(false);
        phoneObject.gameObject.SetActive(true);
        broadcaster = null;
        isInteract = false;
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (SaleManager.instance.isTransaction) return;
        ToggleHighlight(broadcaster.centerIndicator, status, "Interact Phone");
    }
}
