using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int tax;
    [SerializeField] private ResultScreen resultScreen;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Update() {
        if (TimeManager.instance.Midnight())
        {
            EndDay();
        }
    }

    public void EndDay() {
        Time.timeScale = 0;
        resultScreen.gameObject.SetActive(true);
        int money = CurrencyManager.instance.totalCurrency; 
        resultScreen.CountMoneyResult(money, tax, 50000);
    }
}
