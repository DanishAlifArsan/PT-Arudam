using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI answerText;
    public int answer = 0;

    public void StartCalculator(int totalPrice, int paid) {
        answer = paid - totalPrice;
        questionText.text = paid.ToString("G") + "-" + totalPrice.ToString("G");
        answerText.text = answer.ToString("G");
    }

    public void StopCalculator() {
        answer = 0;
        gameObject.SetActive(false);
    }
}
