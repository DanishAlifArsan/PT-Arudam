using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public string highlightName;
    public void ToggleHighlight(InteractIndicator indicator, bool val)
    {
        if (val)
        {
            indicator?.gameObject.SetActive(true);
            indicator?.SetName(highlightName);
        }
        else
        {
            indicator?.gameObject.SetActive(false);
            indicator?.SetName("");
        }
    }
}
