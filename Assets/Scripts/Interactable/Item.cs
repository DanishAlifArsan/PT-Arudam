using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public Goods goods;
    public int id;
    public Storage storage;
    private void Start() {
        EnableHighlight(false);
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (storage != null && broadcaster.itemInHand == null) {
            storage.RemoveItem(this);
            transform.SetParent(broadcaster.itemHand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            broadcaster.itemInHand = transform;
            EnableHighlight(false);
        }
    }
}
