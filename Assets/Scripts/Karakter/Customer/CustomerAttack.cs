using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAttack : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
       customer.anim.SetTrigger("battle");
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
        
    }
}
