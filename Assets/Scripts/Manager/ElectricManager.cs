using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricManager : MonoBehaviour
{
    public static ElectricManager instance;
    private List<Electric> electricList = new List<Electric>();
    private int totalCost = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }

    public void AddElectric(Electric electric) {
        electricList.Add(electric);
    }

    public int SaveCost() {
        foreach (var item in electricList)
        {
            totalCost += item.OnSaveCost();
        }
        return totalCost;
    }
}
