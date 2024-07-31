using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyAmountText;
    [SerializeField] private TextMeshProUGUI taxAmountText;
    [SerializeField] private TextMeshProUGUI electricBillAmountText;
    [SerializeField] private TextMeshProUGUI resultAmountText;

    public void CountMoneyResult(int money, int tax, int electricBill) {
        moneyAmountText.text = money.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        taxAmountText.text = tax.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        electricBillAmountText.text = electricBill.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
        int result = money - (tax + electricBill);
        resultAmountText.text = result.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
    } 
}
