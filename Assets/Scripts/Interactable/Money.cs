using UnityEngine;

public class Money : MonoBehaviour, Interactable
{
    [SerializeField] private MoneyManager manager;
    public int value;
    public bool isAbleToInteract = true;
    public void OnCancel(ItemInteract broadcaster)
    {

    }

    public void OnInteract(ItemInteract broadcaster)
    {
        if (isAbleToInteract && TestingShop.instance.isTransaction)
        {
            manager.CountPayment(this);
        } else {
            manager.ConfirmPayment();
        }
    }
}
