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
    // [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private SpriteRenderer enemySR;
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
        // battledCustomer.Battle();
        // battledCustomer.anim.SetTrigger("battle");
        // battledCustomer.enabled = false;
        // Debug.Log(battledCustomer.CurrentState());
        // battledCustomer.transform.parent = enemySpawnPoint;
        // battledCustomer.transform.position = enemySpawnPoint.position;
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
            }
            int getMoney = SaleManager.instance.GetReturnedItemPrice();
            CurrencyManager.instance.AddCurrency(getMoney);
        } else {
            if (isPolice)
            {
                // kalau polisi kalah
                int getMoney = SaleManager.instance.GetReturnedItemPrice();
                CurrencyManager.instance.AddCurrency(getMoney);
            } else {
                // kalau ternyata bukan penjahat
                int getMoney = SaleManager.instance.GetReturnedItemPrice();
                CurrencyManager.instance.RemoveCurrency(getMoney);
            }
        }
    }
}
