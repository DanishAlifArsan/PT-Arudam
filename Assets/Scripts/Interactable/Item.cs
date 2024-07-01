using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    public Storage storage;
    public void OnCancel(ItemInteract broadcaster)
    {
        
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        if (storage != null && broadcaster.itemInHand == null) {
            transform.SetParent(broadcaster.itemHand);
            transform.localPosition = Vector3.zero;
            broadcaster.itemInHand = transform;
            storage.RemoveItem(this);
        }
    }
}
