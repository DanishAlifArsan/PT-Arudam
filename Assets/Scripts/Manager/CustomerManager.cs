using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<Customer> customerList;
    [SerializeField] private float spawnInterval;
    private float spawnTimer = 0;
    public Transform homePoint;
    public Transform cashierPoint;
    public List<CustomerAI> customerQueue = new List<CustomerAI>();
    public static CustomerManager instance;
    public bool isSpawned = false;
    public CustomerAI currentCustomer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        spawnTimer = spawnInterval;

        foreach (var item in customerList)
        {
            CustomerAI instantiatedCustomer =  Instantiate(item.prefab, homePoint.position, Quaternion.identity);
            instantiatedCustomer.cashierPoint = cashierPoint;
            instantiatedCustomer.homePoint = homePoint;
            instantiatedCustomer.agent.speed = item.walkSpeed;
            instantiatedCustomer.agent.acceleration = item.walkSpeed;
            instantiatedCustomer.waitDuration = item.patience;
            instantiatedCustomer.maxNumberOfGoods = item.maxNumberOfGoods;
            instantiatedCustomer.buyAmountPerGoods = item.buyAmountPerGoods;
            instantiatedCustomer.isEvil = item.isEvil;
            instantiatedCustomer.gameObject.SetActive(false);
            customerQueue.Add(instantiatedCustomer);
        }
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && !isSpawned)
        {
            SpawnCustomer();
            spawnTimer = spawnInterval;
        }
    }

    public Dictionary<Goods, int> SetGoodsToBuy(int maxNumberOfGoods, int buyAmountPerGoods) {
        Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
        for (int i = 0; i < SetNumberOfGoods(maxNumberOfGoods); i++)
        {
            goodsToBuy.Add(ItemManager.instance.listGoodsOnSale[i], SetAmountOfGoods(buyAmountPerGoods));
        }
        return goodsToBuy;
    }

    private int SetNumberOfGoods(int maxNumberOfGoods) {
        int goodsCount = ItemManager.instance.listGoodsOnSale.Count;
        if (goodsCount < maxNumberOfGoods) {
            return Random.Range(1, goodsCount + 1);
        } else {
             return Random.Range(1, maxNumberOfGoods + 1);
        }
    }

    private int SetAmountOfGoods(int buyAmountPerGoods) {
        return Random.Range(1,buyAmountPerGoods+1);
    }

    private void SpawnCustomer() {
        int index = Random.Range(0, customerQueue.Count);
        customerQueue[index].gameObject.SetActive(true);
    }
}
