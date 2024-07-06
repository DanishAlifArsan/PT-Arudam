using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private int startingCurrency;
    private int totalCurrency = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    private void Start()
    {
        AddCurrency(startingCurrency);
    }

    public void AddCurrency(int value) {
        totalCurrency += value;
        Updatecurrency();
    }

    public void RemoveCurrency(int value){
        totalCurrency -= value;
        Updatecurrency();
    }

    private void Updatecurrency() {
        currencyText.text = totalCurrency.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
    }

    public bool CanBuy(int cost) {
        return totalCurrency - cost >= 0;
    }
}
