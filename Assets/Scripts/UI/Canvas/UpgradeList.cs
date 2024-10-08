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
    [SerializeField] private AudioClip clickSound;
    private int index;
    private Upgradable upgradable;
    
    public void Setup(Upgradable item) {   
        upgradable = item; 
        nameText.text = item.itemName;    
        this.index = item.id;
        Refresh(item);
    }

    private void Refresh(Upgradable item) {
        int level = item.currentlevel;
        goodsImage.sprite = level < item.level? item.displayImage[level] : item.displayImage[item.level-1];
        levelText.text = level < item.level? "Level "+ (level+1).ToString() : "Level Max";
        priceText.text = level < item.level? item.upgradePrices[level].ToString("C", CultureInfo.CreateSpecificCulture("id-ID")) : "N/A";

        if (level >= item.level)
        {   
            buttonUpgrade.interactable = false;
        }
    }

    public void Upgrade() {
        AudioManager.instance.PlaySound(clickSound);
        UpgradeManager.instance.Upgrade(index);
        Refresh(upgradable);
    }
}
