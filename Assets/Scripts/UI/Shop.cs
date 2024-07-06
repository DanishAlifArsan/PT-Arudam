using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopList shopList;
    [SerializeField] private RectTransform canvas;
    private List<Goods> listGoods;
    [SerializeField] private Transform packagePoint;

    private void Awake() {
        listGoods = ItemManager.instance.listGoods;

        for (int i = 0; i < listGoods.Count; i++)
        {
            ShopList instantiatedShopList =  Instantiate(shopList, canvas);
            instantiatedShopList.Setup(listGoods[i], i);
            instantiatedShopList.OnButtonClick += OnButtonClick;
        }
    }

    private void OnButtonClick(int index) {
        if (!ItemManager.instance.isAnyPackage)
        {
            // ubah ke aktifin logic tukang paket
            Instantiate(listGoods[index].prefab,packagePoint.position, Quaternion.identity);
            ItemManager.instance.isAnyPackage = true;
        }
    }
}
