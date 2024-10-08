using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Box : Interactable
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject idleObject;
    [SerializeField] private GameObject takenObject;
    [SerializeField] private Item[] itemList;
    public Stack<Item> itemStack = new Stack<Item>();
    private void Start() {
        foreach (var item in itemList)
        {
            itemStack.Push(item);
        }
        amountText.text = itemStack.Count + "/" + itemList.Length;
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand == null) {
            base.OnInteract(broadcaster);
            DeliveryManager.instance.TakeDelivery(transform.parent);
            ToggleHighlight(broadcaster.centerIndicator, false, "Interact Take");
            transform.SetParent(broadcaster.playerHand);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;  
            broadcaster.itemInHand = this;

            if (idleObject != null)
            {
                idleObject.SetActive(false);
                takenObject.SetActive(true);
            }
        }   
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (broadcaster.itemInHand == null)
        {   
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Take");
        }
    }

    public Item TakeItem() {
        Item item = itemStack.Pop();
        amountText.text = itemStack.Count + "/" + itemList.Length;
        return item;
    }
}
