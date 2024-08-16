using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Donation : Interactable
{
    [SerializeField] private int minDonation;
    [SerializeField] private int maxDonation;
    [SerializeField] private TextMeshProUGUI donationText;
    public int donationAmount = 0;
    private bool isDonate = false;

    private void Start() {
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            donationAmount = data.donation;
        }
        SetText();
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
            SetText();
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
            SetText();
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

    private void SetText() {
        donationText.text = donationAmount.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
    }
}
