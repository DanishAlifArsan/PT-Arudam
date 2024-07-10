using UnityEngine;

public class Money : Interactable
{
    [SerializeField] private MoneyManager manager;
    public int value;
    public bool isAbleToInteract = true;

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (isAbleToInteract && SaleManager.instance.isTransaction)
        {
            manager.CountPayment(this);
        } else {
            manager.ConfirmPayment();
        }
    }
}
