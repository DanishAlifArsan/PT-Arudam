using System;
using UnityEngine;

public class Money : Interactable
{
    [SerializeField] private MoneyManager manager;
    public int value;
    public bool isAbleToInteract = true;

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (SaleManager.instance.isTransaction)
        {
            if (isAbleToInteract)
            {
                manager.CountPayment(this);
            } else {
                manager.ConfirmPayment();
                ToggleHighlight(broadcaster.centerIndicator, false);
            }
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (SaleManager.instance.isTransaction)
        {
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }

    public void SetName(String name) {
        highlight.highlightName = name;
    }
}
