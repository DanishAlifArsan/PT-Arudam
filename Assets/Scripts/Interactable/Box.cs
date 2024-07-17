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
            EnableHighlight(false);
            transform.SetParent(broadcaster.playerHand);
            transform.localPosition = Vector3.zero;
            broadcaster.itemInHand = transform;
        }   
    }
}
