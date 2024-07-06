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
    public Action<int> OnButtonClick;

    private int index;
    
    public void Setup(Goods item, int index) {    
        goodsImage.sprite = item.displayImage;
        nameText.text = item.name;
        priceText.text = item.buyPrice.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        this.index = index;
    }

    public void Buy() {
        OnButtonClick.Invoke(index);
    }
}
