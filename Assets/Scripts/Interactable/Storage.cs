using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Storage : MonoBehaviour, Interactable
{
    [SerializeField] private int storageSize;
    private List<Item> items = new List<Item>();
    public void OnCancel(ItemInteract broadcaster)
    {
        
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand != null &&  items.Count < storageSize)
        {
            //masukkan item ke storage
            var box = broadcaster.itemInHand.GetComponent<Box>();
            Item item;
            if (box != null) {
                if (box.itemStack.Count > 0)
                {   
                    item = box.itemStack.Pop();
                } else {
                    item = null;
                }
            } else {
                item = broadcaster.itemInHand.GetComponent<Item>();
                broadcaster.itemInHand = null;
            }

            if (item != null) {
                AddItem(item);
            }
        }
    }

    private void AddItem(Item item) {
        //masih error. ganti dengan grid system dan dictionary
        float storageLength = 0 + 0.206f;
        float columnLength = storageLength / storageSize;

        float itemPosX = columnLength * items.Count;

        Vector3 itemPos = new Vector3(itemPosX,-0.083f,0);

        item.transform.parent = transform;
        item.transform.localPosition = itemPos;
        item.transform.localRotation = Quaternion.identity;
        item.storage = this;
        items.Add(item);
    }

    public void RemoveItem(Item item) {
        item.storage = null;
        items.Remove(item);
    }
}
