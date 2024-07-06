using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake() {
         if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);
    }
}
