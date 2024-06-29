using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, Interactable
{
    public void OnCancel(ItemInteract broadcaster)
    {
        transform.SetParent(null);
        broadcaster.isItemInHand = false;
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        transform.SetParent(broadcaster.playerHand);
        transform.localPosition = Vector3.zero;
        broadcaster.isItemInHand = true;
    }
}
