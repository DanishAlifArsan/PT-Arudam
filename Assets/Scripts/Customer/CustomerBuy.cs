using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.dialogueBubbleUI.SetActive(true);
        SaleManager.instance.SetupTable(customer.goodsToBuy.Values.Max(), customer.goodsToBuy.Count);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (customer.isWalking)
        {
            // customer.isBuying = true;
            // stateManager.SwitchState(customer, stateManager.walk);
            // stateManager.SwitchState(customer, stateManager.pay);
        }
    }
}
