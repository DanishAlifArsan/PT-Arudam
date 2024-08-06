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
        ScrollingText.instance.Show("Tangkap dia jangan sampai kabur");
    }
    private void StartBattle(CustomerAI currentCustomer) {
        BattleManager.instance.battledCustomer = currentCustomer;
        battleDirector.Play();
        ScrollingText.instance.Show("Hati-hati dia bersenjata");
    }
    private void StartPolice() {
        catchDirector.Play();
        ScrollingText.instance.Show("Laporan diterima segera menuju lokasi");
        police.StartChasing();
        SaveManager.instance.numberOfEvils += 1;
    }
}
