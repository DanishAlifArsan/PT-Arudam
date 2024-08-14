using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// to do: benerin bug ketika customer pergi, sementara di meja masih ada barang
public class Table : MonoBehaviour
{
    Grid grid;
    public int returnedItemPrice {get; private set;} 
    public void SetupTable(int width, int height) {
        grid = new Grid(width, height);
        returnedItemPrice = 0;
    }

    public void PlaceItem(Item item) {
        for (int i = 0; i < grid.dictionary.Count; i++)
        {
            if (grid.dictionary[grid.dictionary.ElementAt(i).Key] == null)
            {
                item.transform.parent = transform;
                item.transform.localPosition = grid.dictionary.ElementAt(i).Key;
                item.transform.localRotation = Quaternion.identity;
                item.table = this;
                // item.EnableHighlight(true);
                grid.dictionary[grid.dictionary.ElementAt(i).Key] = item;
                break;      
            }
        }
    }

    public void RemoveItem(Item item) {
        grid.dictionary[item.transform.localPosition] = null;
        item.storage = null;
        SaleManager.instance.RemovePlacedGoods(item);
    }

    public void EmptyTable() {
        foreach (var item in grid.dictionary)
        {
            if (item.Value != null)
            {
                returnedItemPrice += item.Value.goods.sellPrice;
            }
            Destroy(item.Value?.gameObject);
        }
        grid.dictionary.Clear();
    }

    public bool CheckIsTableEmpty() {
        return grid.dictionary.Values.All (x => x == null);
    }

    public void DisableInteract() {
        foreach (var item in grid.dictionary)
        {
            if (item.Value != null)
            {
                item.Value.canInteract = false;
            }
        }
    }

    public bool IsGridNull() {
        return grid.dictionary.ContainsValue(null);
    }
}

class Grid{
    public Dictionary<Vector3, Item> dictionary = new Dictionary<Vector3, Item>();
    public int width;
    public int height;
    public Grid(int width, int height) {
        this.width = width;
        this.height = height;

        float xLength = 0.286f + 0.3f; // -0.3, 0.286
        float xColumnLength = xLength / width;

        float yLength =   0.2f + 0.232f;  // -0.232, 0.2
        float yColumnLength = yLength /height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = new Vector3(yColumnLength * j - 0.232f, 0f, xColumnLength * i - 0.3f);
                dictionary.Add(pos, null);
            }
        }
    }
}
