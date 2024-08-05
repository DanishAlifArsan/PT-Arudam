using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public int totalCurrency;
    public int day;
    public int numberOfEvils = 0;
    public SerializableDictionary<Transform, Box> deliveredItem = new SerializableDictionary<Transform, Box>();
    public SerializableDictionary<Transform, Item> storageItem = new SerializableDictionary<Transform, Item>();
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
            deliveredItem = data.deliveredItem;
            storageItem = data.storageItem;
        }   

        return data;
    }
}
