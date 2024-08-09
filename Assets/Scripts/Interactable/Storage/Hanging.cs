using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : Interactable
{
    [SerializeField] private Vector3 itemPos;
    public override void OnInteract(ItemInteract broadcaster)
    {
        Interactable itemInHand = broadcaster.itemInHand;
        if (itemInHand != null && transform.childCount == 0 && ItemManager.instance.isHangAble(itemInHand.itemType))
        {
            //masukkan item ke storage         
            Item item;

            switch (itemInHand.itemType)
            {
                case ItemType.HangingBox:
                    var box = broadcaster.itemInHand.GetComponent<Box>();
                    if (box.itemStack.Count > 0)
                    {   
                        item = box.TakeItem();
                        item.goods = ItemManager.instance.SetGoods(item.id);
                    } else {
                        item = null;
                    }
                    break;
                case ItemType.HangingGoods:
                    item = broadcaster.itemInHand.GetComponent<Item>();
                    broadcaster.itemInHand = null;
                    break;
                default:
                    item = null;
                    break;
            }

            if (item != null) {
                item.transform.parent = transform;  
                item.transform.localPosition = itemPos;
                item.transform.localRotation = Quaternion.identity;
                item.hanging = this;
                item.isOnBox = false;
                ItemManager.instance.GenerateList(item);
            }
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && transform.childCount == 0 && ItemManager.instance.isHangAble(item.itemType)){
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }

    public void RemoveItem(Item item) {
        item.hanging = null;
    }
}
