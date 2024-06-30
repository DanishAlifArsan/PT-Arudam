using UnityEngine;

public class Money : MonoBehaviour, Interactable
{
    [SerializeField] private MoneyManager manager;
    public int value;
    public bool isAbleToInteract = true;
    public void OnCancel(ItemInteract broadcaster)
    {
        if (isAbleToInteract)
        {
            manager.ConfirmPayment();
        }
    }

    public void OnInteract(ItemInteract broadcaster)
    {
        if (isAbleToInteract)
        {
            manager.CountPayment(this);
        }
    }
}
