using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject priceList;
    [SerializeField] private Transform canvas;

    public List<string> items = new List<string>();
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void GenerateList() {
        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(priceList, canvas);
            //ditambahkan scriptable object untuk menampilkan detail setiap item ke price list
        }
    }
}
