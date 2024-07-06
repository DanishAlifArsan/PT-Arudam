using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopList shopList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private List<Goods> listGoods = new List<Goods>();
    [SerializeField] private Transform packagePoint;
    public bool isAnyPackage = false;
    public static Shop instance;

    private void Awake() {
         if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        for (int i = 0; i < listGoods.Count; i++)
        {
            ShopList instantiatedShopList =  Instantiate(shopList, canvas);
            instantiatedShopList.Setup(listGoods[i], i);
            instantiatedShopList.OnButtonClick += OnButtonClick;
        }
    }

    private void OnButtonClick(int index) {
        if (!isAnyPackage)
        {
            Instantiate(listGoods[index].prefab,packagePoint.position, Quaternion.identity);
            isAnyPackage = true;
        }
    }
}
