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
        for (int i = 0; i < storageSize; i++)
        {
            float pos = dictionary.ElementAt(i).Key;
            Item item = dictionary.ElementAt(i).Value;
            
            itemDictionary.Add(pos, item);
            if (item != null)
            {
                Vector3 itemPos = new Vector3(pos,-0.159f,0.225f);
                Item instantiatedItem = Instantiate(item, itemPos + transform.position, Quaternion.identity, transform);
                instantiatedItem.goods = ItemManager.instance.SetGoods(item.id);
                instantiatedItem.storage = this;            
                instantiatedItem.isOnBox = false;
            }
        }
        return itemDictionary;
    }

    public SerializableDictionary<float, Item> GenerateNewStorage() {
        float storageLength = 0.206f + 0.167f;
        float columnLength = storageLength / storageSize;
        for (int i = 0; i < storageSize; i++)
        {
            float value = columnLength * i - 0.167f;
            float itemPosX =  (float) System.Math.Round(value,2);

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
                Vector3 itemPos = new Vector3(itemDictionary.ElementAt(i).Key,-0.159f,0.225f);

                item.transform.parent = transform;
                item.transform.localPosition = itemPos;
                item.transform.localRotation = Quaternion.identity;
                item.storage = this;
                item.isOnBox = false;
                itemDictionary[itemDictionary.ElementAt(i).Key] = item.goods.itemPrefab;
                AddToList(item);
                break;      
            }
        }
    }

    private void AddToList(Item item) {
        ItemManager.instance.GenerateList(item);
        ItemManager.instance.UpdateList(id, itemDictionary);
    }

    public void RemoveItem(Item item) {
        float value = item.transform.localPosition.x;
        itemDictionary[(float) System.Math.Round(value,2)] = null;
        item.storage = null;
        ItemManager.instance.UpdateList(id, itemDictionary);
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        Interactable item = broadcaster.itemInHand;
        if (item != null && itemDictionary.ContainsValue(null) && ItemManager.instance.IsHoldItem(item.itemType)){
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Place");
        }
    }
}
