using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<Upgradable> upgradableObject;
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
    }

    private void GenerateUpgradableFromSave(List<int> list) {
        upgradedList = list;
    }

    private void GenerateNewUpgradable() {
        for (int i = 0; i < upgradableObject.Count; i++)
        {
            upgradableObject[i].id = i;
            upgradedList.Add(0);
        }
    }

    public void Upgrade(int id, int level) {
        if (level < upgradableObject[id].level)
        {
            upgradableObject[id].upgradeObjects[level].SetActive(true);
            upgradedList[id] = level;
        }
    }
}
