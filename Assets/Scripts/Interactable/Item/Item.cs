using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public Goods goods;
    public int id;
    public Storage storage;
    public Table table;
    public Hanging hanging;
    public bool isOnBox = true;
    public bool canInteract = true;

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (!isOnBox && broadcaster.itemInHand == null && canInteract) {
            if (storage != null )
            {
                storage.RemoveItem(this);
            }
            if (table != null)
            {
                table.RemoveItem(this);    
            }
            if (hanging != null)
            {
                hanging.RemoveItem(this);
            }

            transform.SetParent(broadcaster.itemHand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            broadcaster.itemInHand = this;
            ToggleHighlight(broadcaster.centerIndicator, false);
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (!isOnBox && broadcaster.itemInHand == null && canInteract)
        {
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }
}
