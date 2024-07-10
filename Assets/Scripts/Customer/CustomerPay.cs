using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPay : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.isWalking = false;
        SaleManager.instance.StartTransaction(customer.CountTotalPrice());
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
