using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class Broom : Interactable
{
    [SerializeField] private Transform hand;
    [SerializeField] private Vector3 startingPos;
    [SerializeField] private Vector3 startingRotation;
    [SerializeField] private Transform room;
    public Animator animator;
    private bool isInteract = false;
    private ItemInteract broadcaster;
    private void Awake() {
        animator = GetComponent<Animator>();   
    }
    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CancelInteract();
            }
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
       if (broadcaster.itemInHand == null)
       {
            this.broadcaster = broadcaster;
            string indicator = LocalizationManager.Localize("Cancel Broom");
            broadcaster.SetIndicator(true,indicator);
            transform.SetParent(hand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            broadcaster.itemInHand = this;
            isInteract = true;
            ToggleHighlight(broadcaster.centerIndicator, false, "Interact Broom");
       }
    }

    private void CancelInteract() {
        broadcaster.SetIndicator(false);
        transform.SetParent(room);
        broadcaster.itemInHand = null;
        transform.localPosition = startingPos;
        transform.localRotation = Quaternion.Euler(startingRotation);
        isInteract = false;
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (broadcaster.itemInHand == null)
        {
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Broom");
        }
    }
}
