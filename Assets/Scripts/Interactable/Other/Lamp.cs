using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lamp : Interactable
{
    [SerializeField] private Light lamp;
    [SerializeField] private Transform saklar;
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
        if (isOn)
        {
            saklar.transform.localRotation = Quaternion.Euler(-100f, 0, 90);
        } else {
            saklar.transform.localRotation = Quaternion.Euler(-90f, 0, 90);
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status);
    }
}
