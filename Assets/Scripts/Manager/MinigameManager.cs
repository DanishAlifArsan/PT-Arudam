using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector endlessrunDirector;
    [SerializeField] private PlayableDirector endlessRunEndDirector;
    [SerializeField] private PlayableDirector battleDirector;
    [SerializeField] private GameObject endlessRunObject;
    [SerializeField] private GameObject shopObject;
    [SerializeField] private SpriteRenderer chaseEffect;
    [SerializeField] private GameObject startingPlatform;
    [SerializeField] private ProgressBar progressBar;
    public static MinigameManager instance;
    private bool isPolice;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void StartRunning(bool isPolice) {
        this.isPolice = isPolice;
        endlessrunDirector.Play();
    }

    public void SetupEndlessRun() {
        GameObject[] activePlatforms = GameObject.FindGameObjectsWithTag("Platform");
        if (activePlatforms.Length > 0)
        {
            foreach (var item in activePlatforms)
            {
                Destroy(item);
            }
        }
        Instantiate(startingPlatform, new Vector3(-1.05f, -1.329018f, -2.87f), Quaternion.identity, endlessRunObject.transform);
        progressBar.Setup();
        shopObject.SetActive(false);
        endlessRunObject.SetActive(true);
    }
    
    public void EndRunning() {
        chaseEffect.enabled = true;
        endlessRunEndDirector.Play();  
    }

    public void SwitchScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void Battle() {
        battleDirector.Play();
    }

    public void EndlessRunEnd() {
        shopObject.SetActive(true);
        endlessRunObject.SetActive(false);
        chaseEffect.enabled = false;
        CustomerManager.instance.DespawnCustomer(CustomerManager.instance.currentCustomer);
        if (isPolice)
        {
            Debug.Log("Ketangkap polisi");
        } else {
            int getMoney = SaleManager.instance.GetReturnedItemPrice();
            CurrencyManager.instance.AddCurrency(getMoney);
        }
        CustomerManager.instance.currentCustomer = null;
    }
}
