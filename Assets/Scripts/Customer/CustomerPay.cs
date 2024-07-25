using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPay : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        SaleManager.instance.DisableInteract();
        customer.dialogueBubbleUI.SetActive(false);
        customer.isWalking = false;
        SaleManager.instance.StartTransaction(customer.CountTotalPrice());
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (!SaleManager.instance.isTransaction)
        {
            SaleManager.instance.EmptyTable();
            CustomerManager.instance.currentCustomer = null;
            stateManager.SwitchState(customer, stateManager.walk);
        }
    }
}
