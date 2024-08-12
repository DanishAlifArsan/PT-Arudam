using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ItemInteract player;
    [SerializeField] private int minTax;
    [SerializeField] private int maxTax;
    [SerializeField] private ResultScreen resultScreen;
    [SerializeField] private GameObject blinking;
    [SerializeField] private Volume volume;
    [SerializeField] private TextMeshProUGUI dayText;
    public static GameManager instance;
    private Vignette vignette;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    private void Start() {
        vignette = GetVignette();
        blinking.SetActive(false);
        vignette.intensity.value = 0.25f;
        dayText.text = "Hari "+TimeManager.instance.currentDay.ToString();
    }

    private void Update() {
        if (TimeManager.instance.Midnight())
        {
            EndDay();
        }
        if (TimeManager.instance.NightHour())
        {
            blinking.SetActive(true);
            vignette.intensity.value = 0.5f;
        }
    }

    public void EndDay() {
        resultScreen.gameObject.SetActive(true);
        player.controller.enabled = false;
        player.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        int money = CurrencyManager.instance.totalCurrency; 
        int tax = Random.Range(minTax, maxTax) * 1000;
        int electricBill = ElectricManager.instance.SaveCost();
        resultScreen.CountMoneyResult(money, tax, electricBill);   // todo buat kodingan untuk mengatur listrik
    }

    private Vignette GetVignette() {
        volume.sharedProfile.TryGet<Vignette>(out var vignette);
        return vignette;
    }
}
