using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.SetGoodsToBuy();
        customer.dialogueBubbleUI.SetActive(true);
        CustomerManager.instance.currentCustomer = customer;
        SaleManager.instance.SetupTable(customer.goodsToBuy.Values.Max(), customer.goodsToBuy.Count);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        customer.isBuying = true;
        if (customer.isPaying)
        {
            customer.ClearGoodsToBuy();
            stateManager.SwitchState(customer, stateManager.pay);
        } else if(customer.isWalking) {   
            customer.ClearGoodsToBuy(); 
            if (!SaleManager.instance.CheckIsTableEmpty())
            {
                MinigameManager.instance.EndlessRun();
            }
            SaleManager.instance.EmptyTable();
            CustomerManager.instance.currentCustomer = null;
            stateManager.SwitchState(customer, stateManager.walk);
        }
    }
}
