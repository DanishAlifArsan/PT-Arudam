using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private Image goodsImage;
    [SerializeField] private TextMeshProUGUI countText;

    public void Setup(Goods item) {
        goodsImage.sprite = item.displayImage;
        countText.text = "x " + Random.Range(1,3).ToString();
    }
}
