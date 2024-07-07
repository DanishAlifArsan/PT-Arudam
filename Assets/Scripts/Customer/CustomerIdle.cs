public class CustomerIdle : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
       if (customer.isWalking)
       {
            stateManager.SwitchState(customer, stateManager.walk);
       }
    }
}
