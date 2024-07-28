using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Interactable
{
    [SerializeField] private Transform hand;
    [SerializeField] private Vector3 startingPos;
    [SerializeField] private Transform room;
    private bool isInteract = false;
    private ItemInteract broadcaster;
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
            broadcaster.SetIndicator(true,"Taruh");
            transform.SetParent(hand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            broadcaster.itemInHand = this;
            isInteract = true;
            ToggleHighlight(broadcaster.centerIndicator, false);
       }
    }

    private void CancelInteract() {
        broadcaster.SetIndicator(false);
        transform.SetParent(room);
        broadcaster.itemInHand = null;
        transform.localPosition = startingPos;
        transform.localRotation = Quaternion.identity;
        isInteract = false;
        // ToggleHighlight(broadcaster.centerIndicator, true);
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (broadcaster.itemInHand == null)
        {
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }
}
