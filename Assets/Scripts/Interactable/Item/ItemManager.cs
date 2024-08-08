using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Display display;
    public List<Storage> storageList;
    public List<Goods> listGoods = new List<Goods>(); 
    public List<Goods> listGoodsOnSale = new List<Goods>(); 
    public static ItemManager instance;
    public SerializableDictionary<Goods, int> goodsWithPrice = new SerializableDictionary<Goods, int>();
    public List<SerializableDictionary<float, Item>> storageItem = new List<SerializableDictionary<float, Item>>();

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
        } else {
            GenerateNewStorage();
        }
    }

    private void GenerateStorageFromSave(List<SerializableDictionary<float, Item>> list) {
        // storageItem = list;
        for (int i = 0; i < storageList.Count; i++)
        {
            storageList[i].id = i;
            // storageList[i].GenerateStorageFromSave(list[i]);
            // storageItem.Add(items);
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

    public void GenerateList(Goods goods, int price) {
        display.GenerateList(goods, price);     
        listGoodsOnSale = display.GetGoodsOnSale();
    }

    public bool IsHoldItem(ItemType type) {
        return type.Equals(ItemType.Goods) || type.Equals(ItemType.Box);
    }
}
