using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private Display display;
    public List<Goods> listGoods = new List<Goods>();
    public bool isAnyPackage = false;
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public Goods SetGoods(int id) {
        int index = listGoods.FindIndex(a => a.id == id);
        return listGoods[index];
    }

    public void GenerateList(Item item) {
        display.GenerateList(item);
    }
}
