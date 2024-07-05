using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Storage : Interactable
{
    [SerializeField] private int storageSize;

    private Dictionary<float, Item> itemDictionary;

    private void Awake() {
        float storageLength = 0.206f + 0.167f;
        float columnLength = storageLength / storageSize;

        itemDictionary = new Dictionary<float, Item>();
        for (int i = 0; i < storageSize; i++)
        {
            float itemPosX = columnLength * i - 0.167f;

            itemDictionary.Add(itemPosX, null);
        } 
    }
    
    public override void OnCancel(ItemInteract broadcaster)
    {
        
    }


    public override void OnInteract(ItemInteract broadcaster)
    {
        if (broadcaster.itemInHand != null && itemDictionary.ContainsValue(null))
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
        for (int i = 0; i < storageSize; i++)
        {
            Debug.Log(itemDictionary[itemDictionary.ElementAt(i).Key]);
            if (itemDictionary[itemDictionary.ElementAt(i).Key] == null)
            {
                Vector3 itemPos = new Vector3(itemDictionary.ElementAt(i).Key,-0.159f,0);

                item.transform.parent = transform;
                item.transform.localPosition = itemPos;
                item.transform.localRotation = Quaternion.identity;
                item.storage = this;
                item.EnableHighlight(true);
                itemDictionary[itemDictionary.ElementAt(i).Key] = item;
                break;      
            }
        }
    }

    public void RemoveItem(Item item) {
        itemDictionary[item.transform.localPosition.x] = null;
        Debug.Log(item.transform.localPosition.x);
        Debug.Log(itemDictionary[itemDictionary.ElementAt(0).Key]);
        item.storage = null;
    }
}
