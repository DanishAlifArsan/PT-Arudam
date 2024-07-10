using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleManager : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    public static SaleManager instance;
    public bool isTransaction = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void StartTransaction(int totalPrice) {
        calculator.gameObject.SetActive(true);
        calculator.StartCalculator(totalPrice, GeneratePaid(totalPrice));
        isTransaction = true;
    }

    private int GeneratePaid(int totalPrice) {
        int[] numbers = {1000, 2000, 5000, 10000};
        return totalPrice + numbers[Random.Range(0, 4)] * Random.Range(1,3);
    }

    public void TransactionFinish() {
        isTransaction = false;
        calculator.StopCalculator();
    }

    public int PaidAmount() {
        return calculator.answer;
    }
}
