using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PriceList : Interactable
{
    [SerializeField] private PlayableDirector director;
    private bool isInteract = false;
    private ItemInteract broadcaster;

    private void Update() {
        if (isInteract)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseList();
            }
        }
    }
    public override void OnInteract(ItemInteract broadcaster)
    {
        this.broadcaster = broadcaster;
        director.Play();
        ToggleHighlight(false);
        EnableHighlight(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }

    private void CloseList() {
        director.Stop();
        EnableHighlight(true);
        broadcaster.controller.enabled = true;
        broadcaster.canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteract = false;
    }
}
