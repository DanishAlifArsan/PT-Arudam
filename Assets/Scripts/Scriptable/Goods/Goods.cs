using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Goods")]
public class Goods : ScriptableObject
{
    public int id;
    public string goodsName;
    public Sprite displayImage;
    public int sellPrice;
    public int buyPrice;
    public int deliveryPrice;
    public int amountOnBox;
    public Box prefab;
    public Item itemPrefab;
}
