using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private Item[] itemList;
    public Stack<Item> itemStack = new Stack<Item>();
    private void Start() {
        foreach (var item in itemList)
        {
            itemStack.Push(item);
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand == null) {
            ToggleHighlight(broadcaster.centerIndicator, false);
            transform.SetParent(broadcaster.playerHand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;  
            broadcaster.itemInHand = this;
        }   
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (broadcaster.itemInHand == null)
        {   
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }
}
