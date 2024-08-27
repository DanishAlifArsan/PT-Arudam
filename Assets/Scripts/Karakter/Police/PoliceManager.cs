using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager instance;
    [SerializeField] private PoliceAI police;
    [SerializeField] private PlayableDirector endlessrunDirector;
    [SerializeField] private PlayableDirector battleDirector;
    [SerializeField] private PlayableDirector catchDirector;
    [SerializeField] private NewsPaper newsPaper;
    private CustomerAI evilCustomer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    
    public void StartChasing(CustomerAI currentCustomer) {
        // kalau endless run jadi
        // int random = Random.Range(0,3);
        // switch (random)
        // {
        //     case 0:
        //         StartPolice(currentCustomer);
        //         break;
        //     case 1:
        //         StartBattle(currentCustomer);
        //         break;
        //     case 2:   
        //         StartEndlessRun(currentCustomer);
        //         break;       
        // }
        //
        // kalau endless run gak jadi
        int random = Random.Range(0,2);
        switch (random)
        {
            case 0:
                StartPolice(currentCustomer);
                break;
            case 1:
                StartBattle(currentCustomer);
                break;     
        }
        //
    }

    private void StartEndlessRun(CustomerAI currentCustomer) {
        EndlessRunManager.instance.chasedCustomer = currentCustomer;
        endlessrunDirector.Play();
        ScrollingText.instance.Show("Call Police 3");
    }
    private void StartBattle(CustomerAI currentCustomer) {
        currentCustomer.Battle();
        BattleManager.instance.battledCustomer = currentCustomer;
        battleDirector.Play();
        ScrollingText.instance.Show("Call Police 4");
    }
    private void StartPolice(CustomerAI currentCustomer) {
        catchDirector.Play();
        ScrollingText.instance.Show("Police Thanks");
        evilCustomer = currentCustomer;
    }

    public void AfterCatching() {
        catchDirector.Stop();
        CustomerManager.instance.DespawnCustomer(evilCustomer);
        SaleManager.instance.EmptyTable();
        int getMoney = SaleManager.instance.GetReturnedItemPrice();
        CurrencyManager.instance.AddCurrency(getMoney);
        SaveManager.instance.numberOfEvils += 1;
        newsPaper.ShowScene(0);
    }
}
