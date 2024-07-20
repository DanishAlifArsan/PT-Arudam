using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerWalk : IState
{
    private Vector3 destination;
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.isWalking = true;
        customer.dialogueBubbleUI.SetActive(false);
        if (customer.isBuying)
        {
            destination = customer.homePoint.position;
        } else {
            destination = customer.cashierPoint.position;
        }
        customer.agent.SetDestination(destination);
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
       float dist = customer.agent.remainingDistance;
       if (dist!=Mathf.Infinity && customer.agent.pathStatus == NavMeshPathStatus.PathComplete && customer.agent.remainingDistance == 0)
       {
            customer.isWalking = false;
            if (customer.isBuying)
            {
                if (customer.isEvil)
                {
                    CustomerManager.instance.DespawnCustomer(customer);
                    return;
                }
                stateManager.SwitchState(customer, stateManager.idle);
            } else {
                stateManager.SwitchState(customer, stateManager.buy);
            }
       }
    }
}
