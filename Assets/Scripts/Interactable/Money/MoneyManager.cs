using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private List<Money> moneys = new List<Money>();
    private int totalValue = 0;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Transform paymentPoint;
     [SerializeField] private AudioClip paySound;

    private void Start() {
        moneyText.text = "";
    }

    public void CountPayment(Money money) {
        var temp = Instantiate(money, paymentPoint.position, Quaternion.identity,paymentPoint);
        temp.isAbleToInteract = false;
        temp.SetName("Interact Return");
        moneys.Add(temp);
        totalValue += money.value;
        moneyText.text = totalValue.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
    }

    public void ConfirmPayment() {
        if (SaleManager.instance.PaidAmount() == totalValue)
        {
            AudioManager.instance.PlaySound(paySound);
            CurrencyManager.instance.AddCurrency(totalValue);
            SaleManager.instance.TransactionFinish();
        }
        
        foreach (var item in moneys)
        { 
            Destroy(item.gameObject);
        }
        moneys.Clear();
        totalValue = 0;
        moneyText.text = "";
    }
}
