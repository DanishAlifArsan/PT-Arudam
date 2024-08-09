using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndlessRunManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector endlessrunDirector;
    [SerializeField] private GameObject endlessRunObject;
    [SerializeField] private GameObject shopObject;
    [SerializeField] private SpriteRenderer chaseEffect;
    [SerializeField] private FieldMove startingPlatform;
    [SerializeField] private ProgressBar progressBar;
    public static EndlessRunManager instance;
    private FieldMove initialPlatform;
    private bool isPolice;
    public CustomerAI chasedCustomer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void StartRunning(bool isPolice) {
        this.isPolice = isPolice;
        chaseEffect.enabled = true;
        initialPlatform = Instantiate(startingPlatform, new Vector3(-1.05f, -1.329018f, -2.87f) + endlessRunObject.transform.position, Quaternion.identity, endlessRunObject.transform);
        initialPlatform.enabled = false;
        progressBar.Setup();
        endlessrunDirector.Play();
    }

    public void SetupEndlessRun() {
        shopObject.SetActive(false);
        endlessRunObject.SetActive(true);
    }

    public void MovePlatform() {
        initialPlatform.enabled = true;
    }

    public void SwitchScene(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void EndlessRunEnd(bool isSuccess) {
        GameObject[] activePlatforms = GameObject.FindGameObjectsWithTag("Platform");
        if (activePlatforms.Length > 0)
        {
            foreach (var item in activePlatforms)
            {
                Destroy(item);
            }
        }

        endlessrunDirector.Stop();
        shopObject.SetActive(true);
        endlessRunObject.SetActive(false);
        chaseEffect.enabled = false;
        SaleManager.instance.EmptyTable();
        CustomerManager.instance.DespawnCustomer(chasedCustomer);
        
        if (isSuccess)
        {
            // kalau ketangkap
            if (isPolice)
            {
                 SaveManager.instance.numberOfEvils += 1;
            }
            int getMoney = SaleManager.instance.GetReturnedItemPrice();
            CurrencyManager.instance.AddCurrency(getMoney);
        } else {
            // kalau kabur
            Debug.Log("Kabur");
        }
    }
}
