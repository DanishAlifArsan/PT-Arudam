using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Interactable
{
    public override void OnCancel(ItemInteract broadcaster)
    {
        
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand != null)
        { 
            Destroy(broadcaster.itemInHand.gameObject);
        }
    }
}
