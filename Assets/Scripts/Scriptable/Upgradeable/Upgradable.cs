using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    public int id;
    public string itemName;
    public Sprite displayImage;
    public int level;
    public List<int> upgradePrices;
    public List<GameObject> upgradeObjects;
    public int currentlevel = 0;
}
