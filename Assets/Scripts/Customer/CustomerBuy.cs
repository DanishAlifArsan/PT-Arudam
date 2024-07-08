using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuy : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.dialogueBubbleUI.SetActive(true);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        if (customer.isWalking)
        {
            customer.isBuying = true;
            stateManager.SwitchState(customer, stateManager.walk);
        }
    }
}
