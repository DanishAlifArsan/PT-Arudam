using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Storage : Interactable
{
    [SerializeField] private int storageSize;
    public int id;
    public SerializableDictionary<float, Item> itemDictionary = new SerializableDictionary<float, Item>();

    public SerializableDictionary<float, Item> GenerateStorageFromSave(SerializableDictionary<float, Item> dictionary) {
        itemDictionary = dictionary;
        foreach (var item in itemDictionary)
        {
            Debug.Log(item.Value +","+item.Key);
            if (item.Value != null)
            {
                AddItem(item.Value, item.Key);
            }
        }
        return itemDictionary;
    }

    public SerializableDictionary<float, Item> GenerateNewStorage() {
        float storageLength = 0.206f + 0.167f;
        float columnLength = storageLength / storageSize;
        for (int i = 0; i < storageSize; i++)
        {
            float itemPosX = columnLength * i - 0.167f;

            itemDictionary.Add(itemPosX, null);
        } 
        return itemDictionary;
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        Interactable itemInHand = broadcaster.itemInHand;
        if (itemInHand != null && itemDictionary.ContainsValue(null) && ItemManager.instance.IsHoldItem(itemInHand.itemType))
        {
            //masukkan item ke storage         
            Item item;

            switch (itemInHand.itemType)
            {
                case ItemType.Box:
                    var box = broadcaster.itemInHand.GetComponent<Box>();
                    if (box.itemStack.Count > 0)
                    {   
                        item = box.TakeItem();
                        item.goods = ItemManager.instance.SetGoods(item.id);
                    } else {
                        item = null;
                    }
                    break;
                case ItemType.Goods:
                    item = broadcaster.itemInHand.GetComponent<Item>();
                    broadcaster.itemInHand = null;
                    break;
                default:
                    item = null;
                    break;
            }

            if (item != null) {
                AddItem(item);
            }
        }
    }

    private void AddItem(Item item) {
        for (int i = 0; i < storageSize; i++)
        {
            if (itemDictionary[itemDictionary.ElementAt(i).Key] == null)
            {
                Vector3 itemPos = new Vector3(itemDictionary.ElementAt(i).Key,-0.159f,0);

                item.transform.parent = transform;
                item.transform.localPosition = itemPos;
                item.transform.localRotation = Quaternion.identity;
                item.storage = this;
                item.isOnBox = false;
                itemDictionary[itemDictionary.ElementAt(i).Key] = item;
                AddToList(item);
                break;      
            }
        }
    }

    private void AddItem(Item item, float pos) {
        Vector3 itemPos = new Vector3(pos,-0.159f,0);
        Item instantiatedItem = Instantiate(item, itemPos, Quaternion.identity, transform);
        Debug.Log(instantiatedItem);
        instantiatedItem.storage = this;            
        instantiatedItem.isOnBox = false;
        itemDictionary[pos] = instantiatedItem;
        AddToList(instantiatedItem);
    }

    private void AddToList(Item item) {
        ItemManager.instance.GenerateList(item);
        ItemManager.instance.GenerateList(id, itemDictionary);
    }

    public void RemoveItem(Item item) {
        itemDictionary[item.transform.localPosition.x] = null;
        item.storage = null;
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && itemDictionary.ContainsValue(null) && ItemManager.instance.IsHoldItem(item.itemType)){
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }
}
