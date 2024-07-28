using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaleManager : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    [SerializeField] private Table table;
    public static SaleManager instance;
    public bool isTransaction = false;
    private Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
    private Dictionary<Goods, int> goodsPlaced = new Dictionary<Goods, int>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void StartTransaction(int totalPrice) {
        calculator.gameObject.SetActive(true);
        calculator.StartCalculator(totalPrice, GeneratePaid(totalPrice));
        isTransaction = true;
    }

    private int GeneratePaid(int totalPrice) {
        int[] numbers = {1000, 2000, 5000, 10000};
        return totalPrice + numbers[Random.Range(0, 4)] * Random.Range(1,3);
    }

    public void TransactionFinish() {
        isTransaction = false;
        calculator.StopCalculator();
    }

    public int PaidAmount() {
        return calculator.answer;
    }

    public void SetupTable(int width, int height) {
        table.SetupTable(width, height);
    }

    public void PlaceItem(Dictionary<Goods, int> goodsToBuy, Item item) {
        table.PlaceItem(item);
        this.goodsToBuy = goodsToBuy;
        if (goodsPlaced.ContainsKey(item.goods))
        {
            goodsPlaced[item.goods] += 1;
        } else {
            goodsPlaced.Add(item.goods, 1);
        }     
    }

    public bool CheckIsTableEmpty() {
        return table.CheckIsTableEmpty();
    }

    public void RemovePlacedGoods(Item item) {
        goodsPlaced.Remove(item.goods);
    }
    
    public void EmptyTable() {
        table.EmptyTable();
        goodsPlaced.Clear();
    }

    public void DisableInteract() {
        table.DisableInteract();
    }

    public bool IsGridNull() {
        return table.IsGridNull();
    }

    public bool CompareItem() {
        return goodsPlaced.Count == goodsToBuy.Count && !goodsPlaced.Except(goodsToBuy).Any();
    }
}
