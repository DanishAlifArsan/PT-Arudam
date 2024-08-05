using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Display display;
    public List<Goods> listGoods = new List<Goods>(); 
    public List<Goods> listGoodsOnSale = new List<Goods>(); 
    public static ItemManager instance;
    public SerializableDictionary<Goods, int> goodsWithPrice = new SerializableDictionary<Goods, int>();

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

    public void GenerateList(Goods goods, int price) {
        display.GenerateList(goods, price);
        listGoodsOnSale = display.GetGoodsOnSale();
    }

    public bool IsHoldItem(ItemType type) {
        return type.Equals(ItemType.Goods) || type.Equals(ItemType.Box);
    }
}
