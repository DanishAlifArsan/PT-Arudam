using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public string highlightName;
    public void ToggleHighlight(InteractIndicator indicator, bool val, string name, bool localize = true)
    {
        if (localize)
        {
            highlightName = LocalizationManager.Localize(name);
        }
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
