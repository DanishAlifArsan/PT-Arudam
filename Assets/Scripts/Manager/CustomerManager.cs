using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<CustomerAI> customerList;
    public Transform homePoint;
    public Transform cashierPoint;
    public Queue<CustomerAI> customerQueue = new Queue<CustomerAI>();
    public static CustomerManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        foreach (var item in customerList)
        {
            CustomerAI instantiatedCustomer =  Instantiate(item, homePoint.position, Quaternion.identity);
            instantiatedCustomer.cashierPoint = cashierPoint;
            instantiatedCustomer.homePoint = homePoint;
            // instantiatedCustomer.gameObject.SetActive(false); //pindah ke setup queue
        }
    }

    public Dictionary<Goods, int> SetGoodsToBuy() {
        Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
        for (int i = 0; i < SetNumberOfGoods(); i++)
        {
            goodsToBuy.Add(ItemManager.instance.listGoodsOnSale[i], SetAmountOfGoods());
        }
        return goodsToBuy;
    }

    private int SetNumberOfGoods() {
        return Random.Range(1, ItemManager.instance.listGoodsOnSale.Count);
    }

    private int SetAmountOfGoods() {
        return Random.Range(1,3);
    }
}
