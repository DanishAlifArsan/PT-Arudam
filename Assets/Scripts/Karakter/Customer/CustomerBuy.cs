using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public bool isRunning;
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        isRunning = false;
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
            if (!SaleManager.instance.CheckIsTableEmpty() && !isRunning)
            {
                EndlessRunManager.instance.StartRunning(false);
                isRunning = true;
            } else {
                SaleManager.instance.EmptyTable();
                stateManager.SwitchState(customer, stateManager.walk);
            }
        }
    }
}
