using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WantedPoster : Interactable
{
    [SerializeField] private GameObject posterCanvas;
    private bool isInteract = false;
    private ItemInteract broadcaster;
    private Renderer rend;
    private Animator anim;
    private void Start() {
        rend = GetComponent<Renderer>();
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
       ToggleHighlight(broadcaster.centerIndicator, status);
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        this.broadcaster = broadcaster;
        rend.enabled = false;
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        broadcaster.SetIndicator(true,"Kembali");
        ToggleHighlight(broadcaster.centerIndicator, false);
        posterCanvas.SetActive(true);
        isInteract = true;
    }

    private void ClosePoster() {
        ToggleHighlight(broadcaster.centerIndicator, true);
        rend.enabled = true;
        broadcaster.canInteract = true;
        broadcaster.controller.enabled = true;
        broadcaster.SetIndicator(false);
        posterCanvas.SetActive(false);
        isInteract = false;
    }
}
