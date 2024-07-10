using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<CustomerAI> customerList;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int numberOfSameCustomer;
    private float spawnTimer = 0;
    public Transform homePoint;
    public Transform cashierPoint;
    public List<CustomerAI> customerQueue = new List<CustomerAI>();
    public static CustomerManager instance;
    public bool isSpawned = false;

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
            for (int i = 0; i < numberOfSameCustomer; i++)
            {  
                CustomerAI instantiatedCustomer =  Instantiate(item, homePoint.position, Quaternion.identity);
                instantiatedCustomer.cashierPoint = cashierPoint;
                instantiatedCustomer.homePoint = homePoint;
                instantiatedCustomer.gameObject.SetActive(false);
                customerQueue.Add(instantiatedCustomer);
            }
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

    public Dictionary<Goods, int> SetGoodsToBuy() {
        Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
        for (int i = 0; i < SetNumberOfGoods(); i++)
        {
            goodsToBuy.Add(ItemManager.instance.listGoodsOnSale[i], SetAmountOfGoods());
        }
        return goodsToBuy;
    }

    private int SetNumberOfGoods() {
        return Random.Range(1, ItemManager.instance.listGoodsOnSale.Count + 1);
    }

    private int SetAmountOfGoods() {
        return Random.Range(1,4);
    }

    private void SpawnCustomer() {
        int index = Random.Range(0, customerQueue.Count);
        customerQueue[index].gameObject.SetActive(true);
    }
}
