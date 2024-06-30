using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingShop : MonoBehaviour
{
    [SerializeField] private float interval;
    [SerializeField] private Calculator calculator;
    public bool isTransaction = false;
    private float cooldown;

    public static TestingShop instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    private void Start() {
        cooldown = interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransaction) {
            return;
        }

        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            isTransaction = true;
            calculator.gameObject.SetActive(true);
            int price = GeneratePrice();
            int amount = GenerateAmount();
            calculator.StartCalculator(price, amount, GeneratePaid(price, amount));
        }
    }

    private int GeneratePrice() {
        int[] numbers = {1000, 2000, 5000, 7000};
        return numbers[Random.Range(0, 4)];
    }
    private int GenerateAmount() {
        return Random.Range(1,5);
    }
    private int GeneratePaid(int price, int amount) {
        int[] numbers = {1000, 2000, 5000, 10000};
        return (price * amount) + numbers[Random.Range(0, 4)] * Random.Range(1,3);
    }

    public void TransactionFinish() {
        isTransaction = false;
        cooldown = interval;
        calculator.StopCalculator();
    }

    public int PaidAmount() {
        return calculator.answer;
    }
}
