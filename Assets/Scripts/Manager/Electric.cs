using UnityEngine;

public abstract class Electric : Interactable
{
    [SerializeField] protected int electricCost;
    [SerializeField] protected float costRate;
    float cooldown = 0;
    int totalCost = 0;
    public void OnCountCost(bool condition) {
        if (condition)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= costRate)
            {
                totalCost += electricCost;
                cooldown = 0;
            }
        }
    }

    public int OnSaveCost() {
        return totalCost;
    }
}
