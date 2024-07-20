using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Customer")]
public class Customer : ScriptableObject
{
    public float walkSpeed;
    public float patience;
    public int maxNumberOfGoods = 1;
    public int buyAmountPerGoods = 1;
    public CustomerAI prefab;
    public bool isEvil = false;
}
