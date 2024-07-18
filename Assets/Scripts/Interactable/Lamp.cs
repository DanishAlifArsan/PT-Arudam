using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    [SerializeField] private Light lamp;
    private bool status = false;
    private void Awake() {
        lamp.gameObject.SetActive(false);
    }
    public override void OnInteract(ItemInteract broadcaster)
    {
        status = !status;
        lamp.gameObject.SetActive(status);
    }
}
