using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<Upgradable> upgradableObject;
    [SerializeField] private Upgrade upgradeShop;
    public static UpgradeManager instance;
    public List<int> upgradedList = new List<int>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        GameData data = SaveManager.instance.LoadGame();
        if (data != null)
        {
            GenerateUpgradableFromSave(data.upgradedList);
        } else {
            GenerateNewUpgradable();
        }   
        upgradeShop.Setup();
    }

    private void GenerateUpgradableFromSave(List<int> list) {
        upgradedList = list;
        for (int i = 0; i < upgradableObject.Count; i++)
        {
            int level = upgradedList[i]-1;
            upgradableObject[i].id = i;
            if (level >= 0)
            {
                upgradableObject[i].currentlevel = upgradedList[i];
                upgradableObject[i].upgradeObjects[level].SetActive(true);
            }
        }
    }

    private void GenerateNewUpgradable() {
        for (int i = 0; i < upgradableObject.Count; i++)
        {
            upgradableObject[i].id = i;
            upgradedList.Add(0);
        }
    }

    public void Upgrade(int id) {
        int level = upgradableObject[id].currentlevel;
        if (level < upgradableObject[id].level)
        {
            upgradableObject[id].currentlevel++;
            ClearObject(upgradableObject[id]);
            upgradableObject[id].upgradeObjects[level].SetActive(true);
            upgradedList[id] = level+1;
            CurrencyManager.instance.RemoveCurrency(upgradableObject[id].upgradePrices[level]);
        }
    }

    private void ClearObject(Upgradable upgradable) {
        foreach (var item in upgradable.upgradeObjects)
        {
            item.SetActive(false);
        }
    }
}
