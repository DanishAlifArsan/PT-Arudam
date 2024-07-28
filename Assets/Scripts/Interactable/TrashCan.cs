using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Interactable
{
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && ItemManager.instance.IsHoldItem(item.itemType))
        {
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && ItemManager.instance.IsHoldItem(item.itemType))
        { 
            Destroy(broadcaster.itemInHand.gameObject);
            broadcaster.itemInHand = null;
            ToggleHighlight(broadcaster.centerIndicator, false);
        }
    }
}
