using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBubble : MonoBehaviour
{
    [SerializeField] private Image goodsImage;
    [SerializeField] private TextMeshProUGUI countText;

    public void Setup(KeyValuePair<Goods, int> goodsToBuy) {
        goodsImage.sprite = goodsToBuy.Key.displayImage;
        countText.text = "x " + goodsToBuy.Value;
    }
}
