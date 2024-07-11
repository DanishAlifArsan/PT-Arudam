using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to do: benerin kode biar performa bagus
public class Table : MonoBehaviour
{
    Grid grid;
    public void SetupTable(int width, int height) {
        grid = new Grid(width, height);
    }

    public void PlaceItem(Item item) {
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                if (grid[i,j].Value == null)
                {
                    grid[i, j] = new KeyValuePair<Vector2, Item>( grid[i, j].Key, item);
                    item.transform.parent = transform;
                    item.table = this;
                    item.transform.localPosition = new Vector3(grid[i, j].Key.y, 0f, grid[i, j].Key.x) ;
                    item.transform.localRotation = Quaternion.identity;
                    item.EnableHighlight(true);
                    break;
                }
            }
        }
    }

    public void RemoveItem(Item item) {
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                if (grid[i,j].Value == item)
                {
                    grid[i,j] = new KeyValuePair<Vector2, Item>(grid[i,j].Key, null);
                    item.table = null;
                    break;
                }
            }
        }
    }

    public bool IsGridNull() {
        for (int i = 0; i < grid.width; i++)
        {
            for (int j = 0; j < grid.height; j++)
            {
                if (grid[i,j].Value != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
}

class Grid{
    private static KeyValuePair<Vector2, Item>[,] _grid;
    public int width;
    public int height;
    public Grid(int width, int height) {
        this.width = width;
        this.height = height;
        _grid = new KeyValuePair<Vector2, Item>[width,height];

        float xLength = 0.18f + 0.5f;
        float xColumnLength = xLength / width;

        float yLength =  1.3f - 0.6f;
        float yColumnLength = yLength /height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = new Vector2(xColumnLength * i - 0.5f , yColumnLength * j + 0.6f);
                _grid[i,j] = new KeyValuePair<Vector2, Item>(pos, null);
            }
        }
    }

    public KeyValuePair<Vector2, Item> this[int i, int j]
    {
        get
        {
            return _grid[i, j];
        }
        set
        {
            _grid[i, j] = value;
        }
    }
}


