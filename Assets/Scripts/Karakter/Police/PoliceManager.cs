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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    
    public void StartChasing(CustomerAI currentCustomer) {
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0:
                StartPolice();
                break;
            case 1:
                StartBattle(currentCustomer);
                break;
            case 2:   
                StartEndlessRun(currentCustomer);
                break;       
        }
        // StartBattle(currentCustomer);
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
    private void StartPolice() {
        catchDirector.Play();
        ScrollingText.instance.Show("Call Police 5");
        police.StartChasing();
        SaveManager.instance.numberOfEvils += 1;
    }
}
