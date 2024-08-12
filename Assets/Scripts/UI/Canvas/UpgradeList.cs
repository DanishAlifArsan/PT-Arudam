using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeList : MonoBehaviour
{
    [SerializeField] private Image goodsImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buttonUpgrade;
    private int index;
    private Upgradable upgradable;
    
    public void Setup(Upgradable item) {   
        upgradable = item; 
        goodsImage.sprite = item.displayImage;
        nameText.text = item.itemName;    
        this.index = item.id;
        Refresh(item);
    }

    private void Refresh(Upgradable item) {
        int level = item.currentlevel;
        levelText.text = level < item.level? "Level "+ (level+1).ToString() : "Level Max";
        priceText.text = level < item.level? item.upgradePrices[level].ToString("C", CultureInfo.CreateSpecificCulture("id-ID")) : "N/A";

        if (level >= item.level)
        {   
            buttonUpgrade.interactable = false;
        }
    }

    public void Upgrade() {
        UpgradeManager.instance.Upgrade(index);
        Refresh(upgradable);
    }
}
