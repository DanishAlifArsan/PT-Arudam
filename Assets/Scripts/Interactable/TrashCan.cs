using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, Interactable
{
    public void OnCancel(ItemInteract broadcaster)
    {
        
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand != null)
        { 
            Destroy(broadcaster.itemInHand.gameObject);
        }
    }
}
