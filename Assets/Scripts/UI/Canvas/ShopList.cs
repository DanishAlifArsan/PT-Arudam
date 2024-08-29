using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopList : MonoBehaviour
{
    [SerializeField] private Image goodsImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private AudioClip clickSound;
    public Action<int, int> OnButtonClick;

    private int index;
    private int totalPrice;
    
    public void Setup(Goods item, int index, int totalPrice) {    
        this.totalPrice = totalPrice;
        goodsImage.sprite = item.displayImage;
        nameText.text = item.goodsName;
        priceText.text = totalPrice.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        this.index = index;
    }

    public void Buy() {
        AudioManager.instance.PlaySound(clickSound);
        OnButtonClick.Invoke(index, totalPrice);
    }
}
