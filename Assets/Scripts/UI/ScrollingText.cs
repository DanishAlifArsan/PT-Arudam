using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float textSpeed;
    private string currentText;
    public bool isCalling;

    public static ScrollingText instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void Show(String text) {
        if (isCalling)
        {
            return;
        }

        isCalling = true;
        currentText = text;
        StartCoroutine(DisplayText());
    }

    public void Close() {
        isCalling = false;
        text.text = "";
        StopAllCoroutines();
    }

    private IEnumerator DisplayText() {
        text.text = "";

        foreach (char c in currentText.ToCharArray())
        {
            text.text += c;

            yield return new WaitForSeconds(0.1f/textSpeed);
        }

        yield return new WaitForSeconds(0.2f/textSpeed);
        Close();
    }
}
