using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, Interactable
{
    [SerializeField] private Item[] itemList;
    public Stack<Item> itemStack = new Stack<Item>();
    private void Start() {
        foreach (var item in itemList)
        {
            itemStack.Push(item);
        }
    }

    public void OnCancel(ItemInteract broadcaster)
    {
        
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        transform.SetParent(broadcaster.playerHand);
        transform.localPosition = Vector3.zero;
        broadcaster.itemInHand = transform;
    }
}
