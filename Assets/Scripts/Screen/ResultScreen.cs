using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    [SerializeField] private TextMeshProUGUI taxAmountText;
    [SerializeField] private TextMeshProUGUI electricBillAmountText;
    [SerializeField] private TextMeshProUGUI resultAmountText;
    [SerializeField] private HomeScreen homeScreen;
    private Action<int> onContinueButton;
    private int sceneToLoad;
    private void OnEnable() {
        dayText.text = "Hari "+TimeManager.instance.currentDay.ToString();
        onContinueButton += homeScreen.LoadScene;
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
            sceneToLoad = 0;
        } else if (TimeManager.instance.Ending())
        {
            //gameover karena hari selesai
            SaveManager.instance.NewGame();
            sceneToLoad = 0;
        } else {
            //lanjut hari
            SaveManager.instance.totalCurrency = result;
            sceneToLoad = 1;
            SaveGame();
        }
    } 

    private void SaveGame() {
        SaveManager.instance.deliveredItem = DeliveryManager.instance.deliveredItem;
        SaveManager.instance.day = TimeManager.instance.currentDay+1;
        SaveManager.instance.goodsWithPrice = ItemManager.instance.goodsWithPrice;
        SaveManager.instance.storageItem = ItemManager.instance.storageItem;
        SaveManager.instance.hangedItem = ItemManager.instance.hangedItem;
        SaveManager.instance.SaveGame();
    }

    public void Continue() {
        onContinueButton.Invoke(sceneToLoad);
    }
}
