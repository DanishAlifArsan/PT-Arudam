using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private UpgradeList upgradeList;
    [SerializeField] private RectTransform canvas;
    private List<Upgradable> listUpgradables = new List<Upgradable>();

    public void Setup() {
        listUpgradables = UpgradeManager.instance.upgradableObject;

        for (int i = 0; i < listUpgradables.Count; i++)
        {
            UpgradeList instantiatedUpgradeList = Instantiate(upgradeList, canvas);
            instantiatedUpgradeList.Setup(listUpgradables[i]);
        }
    }
}
