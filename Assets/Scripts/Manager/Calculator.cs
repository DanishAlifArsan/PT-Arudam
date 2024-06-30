using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerText;
    public int answer = 0;

    private void OnEnable() {
        questionText.text = "";
        answerText.text = "";
    }

    public void StartCalculator(int price, int amount, int paid) {
        answer = paid - (price * amount);
        questionText.text = paid.ToString("G") + "-" + "(" + price.ToString("G") + "x" + amount.ToString("G") + ")";
        answerText.text = answer.ToString("G");
    }

    public void StopCalculator() {
        answer = 0;
        gameObject.SetActive(false);
    }
}
