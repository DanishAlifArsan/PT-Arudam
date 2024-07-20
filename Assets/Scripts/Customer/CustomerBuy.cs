using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.dialogueBubbleUI.SetActive(true);
        CustomerManager.instance.currentCustomer = customer;
        SaleManager.instance.SetupTable(customer.goodsToBuy.Values.Max(), customer.goodsToBuy.Count);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        customer.isBuying = true;
        if (customer.isPaying)
        {
            stateManager.SwitchState(customer, stateManager.pay);
        } else if(customer.isWalking) {    
            //kasih minigame kejar kejaran
            SaleManager.instance.EmptyTable();
            CustomerManager.instance.currentCustomer = null;
            stateManager.SwitchState(customer, stateManager.walk);
        }
    }
}
