using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public int totalCurrency;
    public int day;
    public int numberOfEvils = 0;
    public SerializableDictionary<Goods, int> goodsWithPrice = new SerializableDictionary<Goods, int>();
    public List<Box> deliveredItem = new List<Box>();
    public List<SerializableDictionary<float, Item>> storageItem = new List<SerializableDictionary<float, Item>>();
    public List<Item> hangedItem = new List<Item>();
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void NewGame() {
        SaveSystem.Delete();
    }

    public void SaveGame() {
        SaveSystem.Save(this);
    }

    public GameData LoadGame() {
        GameData data = SaveSystem.Load();
        if (data != null)
        {
            totalCurrency = data.totalCurrency;
            day = data.day;
            numberOfEvils = data.numberOfEvils;
            goodsWithPrice = data.goodsWithPrice;
            deliveredItem = data.deliveredItem;
            storageItem = data.storageItem;
            hangedItem = data.hangedItem;
        }   

        return data;
    }
}
