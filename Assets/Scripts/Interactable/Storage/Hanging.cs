using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanging : Interactable
{
    [SerializeField] private Vector3 itemPos;
    private Item hangedItem;
    public int id;

    public void GenerateFromSave(Item item) {
        if (item != null)
        {
            Item instantiatedItem = Instantiate(item, itemPos + transform.position, Quaternion.identity, transform);    
            instantiatedItem.hanging = this;
            instantiatedItem.isOnBox = false;
            instantiatedItem.goods = ItemManager.instance.SetGoods(item.id);
            hangedItem = instantiatedItem;
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        Interactable itemInHand = broadcaster.itemInHand;
        if (itemInHand != null && hangedItem == null && ItemManager.instance.isHangAble(itemInHand.itemType))
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
                hangedItem = item;
                item.transform.localPosition = itemPos;
                item.transform.localRotation = Quaternion.identity;
                item.hanging = this;
                item.isOnBox = false;
                ItemManager.instance.GenerateList(item);
                ItemManager.instance.UpdateHangable(id, item.goods.itemPrefab);
            }
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && hangedItem == null && ItemManager.instance.isHangAble(item.itemType)){
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Hang");
        }
    }

    public void RemoveItem(Item item) {
        item.hanging = null;
        hangedItem = null;
        ItemManager.instance.UpdateHangable(id, null);
    }
}
