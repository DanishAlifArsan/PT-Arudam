using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donation : Interactable
{
    [SerializeField] private int minDonation;
    [SerializeField] private int maxDonation;
    public int donationAmount = 0;
    private bool isDonate = false;

    private void Start() {
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            donationAmount = data.donation;
        }
    }

    private void Update() {
        if (isDonate)
        {
            if (!SaleManager.instance.isTransaction) {
                isDonate = false;
            } 
            return;
        }

        if (SaleManager.instance.isTransaction && !isDonate)
        {
            int donate = Random.Range(minDonation, maxDonation) * 1000;
            donationAmount += donate;
            isDonate = true;
            SaveManager.instance.donation = donationAmount;
        }    
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (donationAmount > 0)
        {
            CurrencyManager.instance.AddCurrency(donationAmount);
            donationAmount = 0;
            ToggleHighlight(broadcaster.centerIndicator, false);
        }
    }

    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (donationAmount > 0)
        {
            ToggleHighlight(broadcaster.centerIndicator, status);
        }
    }

}
