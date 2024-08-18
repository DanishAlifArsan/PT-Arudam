using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopList shopList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject warning;
    [SerializeField] private TextMeshProUGUI warningMessage;
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

    private void OnButtonClick(int index, int totalPrice) {
        if (!DeliveryManager.instance.CanCheckout())
        {
            WarningMessage("Phone Storage Full");
        } else if (!CurrencyManager.instance.CanBuy(totalPrice)) {
            WarningMessage("Phone No Money");
        } else if (TimeManager.instance.NightHour())
        {
            WarningMessage("Phone Cant Deliver");
        } else {
            WarningMessage("Phone Success");
            DeliveryManager.instance.StartDelivery(listGoods[index].prefab);
            CurrencyManager.instance.RemoveCurrency(totalPrice);
        }
    }

    private void WarningMessage(string message) {
        warning.SetActive(true);
        warningMessage.text = LocalizationManager.Localize(message);
    }

    public void CloseWarning() {
        warning.SetActive(false);
    }
}
