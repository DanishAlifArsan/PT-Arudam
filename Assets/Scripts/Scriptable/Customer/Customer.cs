using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Customer")]
public class Customer : ScriptableObject
{
    public float health = 10;
    public float walkSpeed;
    public float acceleration;
    public float patience;
    public int maxNumberOfGoods = 1;
    public int buyAmountPerGoods = 1;
    public CustomerAI prefab;
    public Sprite battleSprite;
    public AudioClip happySound;
    public AudioClip angrySound;
    public bool isEvil = false;
}
