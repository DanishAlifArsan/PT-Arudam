using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCurrency;
    public int day;
    public int numberOfEvils;
    public SerializableDictionary<Transform, Box> deliveredItem = new SerializableDictionary<Transform, Box>();
    public SerializableDictionary<Transform, Item> storageItem = new SerializableDictionary<Transform, Item>();

    public GameData(SaveManager manager) {
        totalCurrency = manager.totalCurrency;
        day = manager.day;
        numberOfEvils = manager.numberOfEvils;
        deliveredItem = manager.deliveredItem;
        storageItem = manager.storageItem;
    }
}
