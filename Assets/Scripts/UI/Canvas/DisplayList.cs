using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayList : MonoBehaviour
{
    [SerializeField] private Image goodsImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceInput;
    [SerializeField] private AudioClip clickSound;
    public Func<int, int, bool, int> OnButtonClick;

    private int index;
    
    public void Setup(Goods item, int index) {    
        goodsImage.sprite = item.displayImage;
        nameText.text = item.name;
        priceInput.text = item.sellPrice.ToString();
        this.index = index;
    }

    public void SetPrice(bool isPlus) {
        AudioManager.instance.PlaySound(clickSound);
        priceInput.text = OnButtonClick.Invoke(index, int.Parse(priceInput.text), isPlus).ToString();
    }
}
