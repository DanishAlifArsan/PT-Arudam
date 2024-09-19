using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cashier : Interactable
{
    public override void OnInteract(ItemInteract broadcaster)
    {
        CustomerAI currentCustomer = CustomerManager.instance.currentCustomer;
        if (currentCustomer != null && !SaleManager.instance.isTransaction) {
            currentCustomer.OnInteract(broadcaster);
        }
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        CustomerAI currentCustomer = CustomerManager.instance.currentCustomer;
        if (currentCustomer != null && !SaleManager.instance.isTransaction) {
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Place");
        }
    }
}
