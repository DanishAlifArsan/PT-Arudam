using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopList shopList;
    [SerializeField] private RectTransform canvas;
    private List<Goods> listGoods;
    [SerializeField] private Transform packagePoint;
    int totalPrice = 0;

    private void Awake() {
        listGoods = ItemManager.instance.listGoods;

        for (int i = 0; i < listGoods.Count; i++)
        {
            ShopList instantiatedShopList =  Instantiate(shopList, canvas);
            totalPrice = listGoods[i].buyPrice * listGoods[i].amountOnBox + listGoods[i].deliveryPrice;
            instantiatedShopList.Setup(listGoods[i], i, totalPrice);
            instantiatedShopList.OnButtonClick += OnButtonClick;
        }
    }

    private void OnButtonClick(int index) {
        if (DeliveryManager.instance.CanCheckout() && CurrencyManager.instance.CanBuy(totalPrice))
        {
            DeliveryManager.instance.StartDelivery(listGoods[index].prefab);
            CurrencyManager.instance.RemoveCurrency(totalPrice);
        }
    }
}
