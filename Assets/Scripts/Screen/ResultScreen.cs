using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    [SerializeField] private TextMeshProUGUI taxAmountText;
    [SerializeField] private TextMeshProUGUI electricBillAmountText;
    [SerializeField] private TextMeshProUGUI resultAmountText;
    private void OnEnable() {
        dayText.text = "Hari "+TimeManager.instance.currentDay.ToString();
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
        } else {
            SaveManager.instance.totalCurrency = result;
        }
    } 

    public void SaveGame() {
        SaveManager.instance.day = TimeManager.instance.currentDay+1;
        SaveManager.instance.SaveGame();
    }
}
