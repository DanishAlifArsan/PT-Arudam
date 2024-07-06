using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject priceDisplay;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PriceList priceList;

    private List<string> items = new List<string>();
    public static ItemManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

    }

    public void GenerateList(Item item) {
        if (!items.Contains(item.itemName))
        {
            items.Add(item.itemName);
            Instantiate(priceDisplay, canvas);
        }

        //ditambahkan scriptable object untuk menampilkan detail setiap item ke price list
    }
}
