public class CustomerIdle : IState
{
    public void EnterState(CustomerAI customer, StateManager stateManager)
    {
        customer.ClearGoodsToBuy();
        customer.SetGoodsToBuy();
        customer.dialogueBubbleUI.SetActive(false);
        if (customer.isBuying)
        {
            customer.gameObject.SetActive(false);
        }
    }

    public void UpdateState(CustomerAI customer, StateManager stateManager)
    {
       if (customer.isWalking)
       {
            stateManager.SwitchState(customer, stateManager.walk);
       }
    }
}
