using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    public static PoliceManager instance;
    [SerializeField] private PoliceAI police;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    
    public void StartChasing() {
        int random = Random.Range(0,3);
        switch (random)
        {
            case 0:
                ScrollingText.instance.Show("Laporan diterima segera menuju lokasi");
                police.StartChasing();
                break;
            case 1:
                StartCoroutine(StartBattle());
                break;
            case 2:   
                StartCoroutine(StartEndlessRun());
                break;       
        }
    }

    private IEnumerator StartEndlessRun() {
        ScrollingText.instance.Show("Tangkap dia jangan sampai kabur");
        yield return new WaitUntil(() => !ScrollingText.instance.isCalling);
        MinigameManager.instance.EndlessRun();
    }
    private IEnumerator StartBattle() {
        ScrollingText.instance.Show("Hati-hati dia bersenjata");
        yield return new WaitUntil(() => !ScrollingText.instance.isCalling);
        MinigameManager.instance.Battle();
    }
}
