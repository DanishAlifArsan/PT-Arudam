using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<Customer> customerList;
    [SerializeField] private List<Customer> evilCustomerList;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int spawnCount;
    private float spawnTimer = 0;
    // public Transform homePoint;
    public List<Transform> homePoint;
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
        Customer evilCustomer = evilCustomerList[Random.Range(0, evilCustomerList.Count)];
        Transform evilSpawnPoint = homePoint[Random.Range(0, homePoint.Count)];
        CustomerAI intantiatedEvilCustomer = Instantiate(evilCustomer.prefab, evilSpawnPoint.position, Quaternion.identity, transform.parent);
        SetupCustomer(intantiatedEvilCustomer, evilCustomer, evilSpawnPoint);

        for (int i = 0; i < spawnCount; i++)
        {
            Customer customer = customerList[Random.Range(0, customerList.Count)];
            Transform spawnPoint = homePoint[Random.Range(0, homePoint.Count)];
            CustomerAI instantiatedCustomer =  Instantiate(customer.prefab, spawnPoint.position, Quaternion.identity, transform.parent);
            SetupCustomer(instantiatedCustomer, customer, spawnPoint);
        }
    }

    private void Update() {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0 && !isSpawned && customerQueue.Count > 0)
        {
            SpawnCustomer();
            spawnTimer = spawnInterval;
        }
    }

    private void SetupCustomer(CustomerAI instantiatedCustomer, Customer origin, Transform homePoint) {
        instantiatedCustomer.cashierPoint = cashierPoint;
        instantiatedCustomer.homePoint = homePoint;
        instantiatedCustomer.agent.speed = origin.walkSpeed;
        instantiatedCustomer.agent.acceleration = origin.acceleration;
        instantiatedCustomer.health = origin.health;
        instantiatedCustomer.waitDuration = origin.patience;
        instantiatedCustomer.maxNumberOfGoods = origin.maxNumberOfGoods;
        instantiatedCustomer.buyAmountPerGoods = origin.buyAmountPerGoods;
        instantiatedCustomer.battleSprite = origin.battleSprite;
        instantiatedCustomer.speak.happySound = origin.happySound;
        instantiatedCustomer.speak.angrySound = origin.angrySound;
        instantiatedCustomer.isEvil = origin.isEvil;
        instantiatedCustomer.gameObject.SetActive(false);
        customerQueue.Add(instantiatedCustomer);
    }

    public Dictionary<Goods, int> SetGoodsToBuy(int maxNumberOfGoods, int buyAmountPerGoods) {
        Dictionary<Goods, int> goodsToBuy = new Dictionary<Goods, int>();
        int numberOfGoods = SetNumberOfGoods(maxNumberOfGoods);
        List<Goods> randomGoods = SetGoodsToBuy(ItemManager.instance.listGoodsOnSale, numberOfGoods);
        for (int i = 0; i < numberOfGoods; i++)
        {
            goodsToBuy.Add(randomGoods[i], SetAmountOfGoods(buyAmountPerGoods));
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

    private List<Goods> SetGoodsToBuy(List<Goods> list, int k) {
        List<Goods> collection = list;
        int n = collection.Count;
        for (int i = 0; i < k; i++)
        {
            int j = Random.Range(i, n - 1);
            Goods temp = collection[i];
            collection[i] = collection[j];
            collection[j] = temp;
        }
        return collection;
    }

    private int SetAmountOfGoods(int buyAmountPerGoods) {
        return Random.Range(1,buyAmountPerGoods+1);
    }

    private void SpawnCustomer() {
        int index = Random.Range(0, customerQueue.Count);
        customerQueue[index].gameObject.SetActive(true);
    }

    public void DespawnCustomer(CustomerAI customer) {
        Destroy(customer.gameObject);
        customerQueue.Remove(customer);
    }
}
