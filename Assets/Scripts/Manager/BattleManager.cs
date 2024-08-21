using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector battleDirector;
    [SerializeField] private PlayableDirector battleEndDirector;
    [SerializeField] private GameObject battleObject;
    [SerializeField] private GameObject shopObject;
    [SerializeField] private GameObject battleTransition;
    [SerializeField] private Image leftImage;
    [SerializeField] private Image rightImage;
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer enemySR;
    [SerializeField] private NewsPaper newsPaper;
    public static BattleManager instance;
    private bool isPolice;
    public CustomerAI battledCustomer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void StartBattle(bool isPolice) {
        this.isPolice = isPolice;
        battleTransition.SetActive(true);
        battleDirector.Play();
    }

    public void SetupBattle() {
        enemySR.sprite = battledCustomer.battleSprite;
        health.Setup();
        battleObject.SetActive(true);
        battleTransition.SetActive(false);
        shopObject.SetActive(false);
    }

    public void BattleEnd(bool isWin) {
        battleDirector.Stop();
        battleEndDirector.Stop();   
        leftImage.fillAmount = 1;
        rightImage.fillAmount = 1;
        shopObject.SetActive(true);
        battleObject.SetActive(false);
        // chaseEffect.enabled = false;
        SaleManager.instance.EmptyTable();
        CustomerManager.instance.DespawnCustomer(battledCustomer);

        if (isWin)
        {
            if (battledCustomer.isEvil) {
                // kalau kita menang
                SaveManager.instance.numberOfEvils += 1;
                newsPaper.ShowScene(0);
            }
            int getMoney = SaleManager.instance.GetReturnedItemPrice();
            CurrencyManager.instance.AddCurrency(getMoney);
        } else {
            if (isPolice)
            {
                // kalau polisi kalah
                int getMoney = SaleManager.instance.GetReturnedItemPrice();
                CurrencyManager.instance.AddCurrency(getMoney);
                newsPaper.ShowScene(1);
            } else {
                // kalau ternyata bukan penjahat
                int getMoney = SaleManager.instance.GetReturnedItemPrice();
                CurrencyManager.instance.RemoveCurrency(getMoney);
            }
        }
    }
}
