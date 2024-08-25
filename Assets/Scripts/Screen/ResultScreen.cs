using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    [SerializeField] private TextMeshProUGUI taxAmountText;
    [SerializeField] private TextMeshProUGUI electricBillAmountText;
    [SerializeField] private TextMeshProUGUI resultAmountText;
    [SerializeField] private HomeScreen homeScreen;
    [SerializeField] private NewsPaper newsPaper;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip clickSound;
    private Action onContinueButton;
    private void OnEnable() {
        AudioManager.instance.PlaySound(paperSound);
        string day = LocalizationManager.Localize("Menu Result") + " ";
        dayText.text = day+TimeManager.instance.currentDay.ToString();
    }

    public void CountMoneyResult(int money, int tax, int electricBill) {
        moneyAmountText.text = money.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        taxAmountText.text = tax.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        electricBillAmountText.text = electricBill.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        int result = money - (tax + electricBill);
        resultAmountText.text = result.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));

        if (result <= 0)
        {
            //gameover karena bangkrut
            SaveManager.instance.NewGame();
            onContinueButton += BadEnding;
        } else if (TimeManager.instance.Ending())
        {
            //gameover karena hari selesai
            SaveManager.instance.NewGame();
            onContinueButton += GoodEnding;
        } else {
            //lanjut hari
            SaveManager.instance.totalCurrency = result;
            onContinueButton += ContinueGame;
            SaveGame();
        }
    } 

    private void SaveGame() {
        SaveManager.instance.deliveredItem = DeliveryManager.instance.deliveredItem;
        SaveManager.instance.day = TimeManager.instance.currentDay+1;
        SaveManager.instance.goodsWithPrice = ItemManager.instance.goodsWithPrice;
        SaveManager.instance.storageItem = ItemManager.instance.storageItem;
        SaveManager.instance.hangedItem = ItemManager.instance.hangedItem;
        SaveManager.instance.upgradedList = UpgradeManager.instance.upgradedList;
        SaveManager.instance.SaveGame();
    }

    private void ContinueGame() {
        AudioManager.instance.PlaySound(clickSound);
        homeScreen.LoadScene(1);
    }
    private void GoodEnding() {
        newsPaper.ShowScene(2);
    }
    private void BadEnding() {
        newsPaper.ShowScene(3);
    }

    public void Continue() {
        onContinueButton.Invoke();
    }
}
