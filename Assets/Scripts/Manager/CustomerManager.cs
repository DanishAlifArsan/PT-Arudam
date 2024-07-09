using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private List<CustomerAI> customerList;
    [SerializeField] private Transform homePoint;
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
            instantiatedCustomer.gameObject.SetActive(false);
        }
    }

    private Goods SetGoodsToBuy() {
        return ItemManager.instance.listGoodsOnSale[Random.Range(0, ItemManager.instance.listGoodsOnSale.Count-1)];
    }

    private int SetNumberOfGoods() {
        return Random.Range(1,3);
    }
}
