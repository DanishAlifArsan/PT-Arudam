using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indicatorText;
    
    public void SetName(string name) {
        indicatorText.text = name;
    }
}
