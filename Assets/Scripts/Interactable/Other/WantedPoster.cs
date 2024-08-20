using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class WantedPoster : Interactable
{
    [SerializeField] private GameObject posterCanvas;
    private bool isInteract = false;
    private ItemInteract broadcaster;
    [SerializeField] private Image rend;
    private Animator anim;
    private void Start() {
        anim = posterCanvas.GetComponent<Animator>();
    }
    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1) ) {
                ClosePoster();
                // anim.SetTrigger("Close")
            }
        }
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status, "Interact Poster");
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        this.broadcaster = broadcaster;
        rend.enabled = false;
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        string indicator = LocalizationManager.Localize("Cancel Phone");
        broadcaster.SetIndicator(true,indicator);
        ToggleHighlight(broadcaster.centerIndicator, false, "Interact Poster");
        posterCanvas.SetActive(true);
        isInteract = true;
    }

    private void ClosePoster() {
        rend.enabled = true;
        broadcaster.canInteract = true;
        broadcaster.controller.enabled = true;
        broadcaster.SetIndicator(false);
        posterCanvas.SetActive(false);
        isInteract = false;
    }
}
