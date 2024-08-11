using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCurrency;
    public int day;
    public int numberOfEvils;
    public int donation = 0;
    public SerializableDictionary<Goods, int> goodsWithPrice = new SerializableDictionary<Goods, int>();
    public List<Box>  deliveredItem = new List<Box>();
    public List<SerializableDictionary<float, Item>> storageItem = new List<SerializableDictionary<float, Item>>();
    public List<Item> hangedItem = new List<Item>();
    public List<int> upgradedList = new List<int>();

    public GameData(SaveManager manager) {
        totalCurrency = manager.totalCurrency;
        day = manager.day;
        numberOfEvils = manager.numberOfEvils;
        donation = manager.donation;
        goodsWithPrice = manager.goodsWithPrice;
        deliveredItem = manager.deliveredItem;
        storageItem = manager.storageItem;
        hangedItem = manager.hangedItem;
        upgradedList = manager.upgradedList;
    }
}
