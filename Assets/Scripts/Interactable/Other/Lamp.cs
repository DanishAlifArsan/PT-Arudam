using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lamp : Interactable
{
    [SerializeField] private Light lamp;
    private bool isOn = false;
    private void Awake() {
        lamp.gameObject.SetActive(false);
    }
    public override void OnInteract(ItemInteract broadcaster)
    {
        isOn = !isOn;
        lamp.gameObject.SetActive(isOn);
        highlight.highlightName = isOn? "Matikan": "Nyalakan";
        ToggleHighlight(broadcaster.centerIndicator, true);
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status);
    }
}
