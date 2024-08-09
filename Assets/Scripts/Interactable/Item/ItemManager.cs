using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Display display;
    public List<Storage> storageList;
    [SerializeField] private List<Hanging> hanggingList;
    public List<Goods> listGoods = new List<Goods>(); 
    public List<Goods> listGoodsOnSale = new List<Goods>(); 
    public static ItemManager instance;
    public SerializableDictionary<Goods, int> goodsWithPrice = new SerializableDictionary<Goods, int>();
    public List<SerializableDictionary<float, Item>> storageItem = new List<SerializableDictionary<float, Item>>();
    public List<Item> hangedItem = new List<Item>(); 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            goodsWithPrice = data.goodsWithPrice;
            foreach (var item in goodsWithPrice)
            {
                GenerateList(item.Key, item.Value);
            }
            GenerateStorageFromSave(data.storageItem);
            GenerateHangedFromSave(data.hangedItem);
        } else {
            GenerateNewStorage();
            GenerateNewHanged();
        }
    }

    private void GenerateStorageFromSave(List<SerializableDictionary<float, Item>> list) {
        for (int i = 0; i < storageList.Count; i++)
        {
            storageList[i].id = i;
            storageItem.Add(storageList[i].GenerateStorageFromSave(list[i]));
        }
    }

    private void GenerateNewStorage() {
        for (int i = 0; i < storageList.Count; i++)
        {
            storageList[i].id = i;
            storageItem.Add(storageList[i].GenerateNewStorage());   
        }
    }

    private void GenerateHangedFromSave(List<Item> list) {
        hangedItem = list;
        for (int i = 0; i < hangedItem.Count; i++)
        {
            hanggingList[i].id = i;
            hanggingList[i].GenerateFromSave(hangedItem[i]);
        }
    }

    private void GenerateNewHanged() {
        for (int i = 0; i < hanggingList.Count; i++)
        {
            hanggingList[i].id = i;
            hangedItem.Add(null);
        }
    }

    public Goods SetGoods(int id) {
        int index = listGoods.FindIndex(a => a.id == id);
        return listGoods[index];
    }

    public void GenerateList(Item item) {
        display.GenerateList(item);
        listGoodsOnSale = display.GetGoodsOnSale();
    }   
    public void UpdateList(int storageId, SerializableDictionary<float, Item> itemDictionary) {
        
        storageItem[storageId] = itemDictionary;
    }   
    public void UpdateHangable(int id, Item item) {
        hangedItem[id] = item;
    }   

    public void GenerateList(Goods goods, int price) {
        display.GenerateList(goods, price);     
        listGoodsOnSale = display.GetGoodsOnSale();
    }

    public bool IsHoldItem(ItemType type) {
        return type.Equals(ItemType.Goods) || type.Equals(ItemType.Box);
    }

    public bool isHangAble(ItemType type) {
        return type.Equals(ItemType.HangingGoods) || type.Equals(ItemType.HangingBox);
    }

    public bool isDiscardable(ItemType type) {
        return IsHoldItem(type) || isHangAble(type);
    }
}
