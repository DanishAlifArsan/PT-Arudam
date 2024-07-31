using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ItemInteract player;
    [SerializeField] private int tax;
    [SerializeField] private ResultScreen resultScreen;
    [SerializeField] private GameObject blinking;
    [SerializeField] private Volume volume;
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
        player.controller.enabled = false;
        player.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        resultScreen.gameObject.SetActive(true);
        int money = CurrencyManager.instance.totalCurrency; 
        resultScreen.CountMoneyResult(money, tax, 50000);   // todo buat kodingan untuk mengatur listrik
    }

    private Vignette GetVignette() {
        volume.sharedProfile.TryGet<Vignette>(out var vignette);
        return vignette;
    }
}
