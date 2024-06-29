using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itempickup : MonoBehaviour
{
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform playerHand;
    [SerializeField] private float pickupRange;
    private bool isItemInHand = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isItemInHand)
            {
                PickItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    void PickItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(crosshair.transform.position, crosshair.transform.right, out hit, pickupRange))
        { 
            if (hit.collider.CompareTag("Item"))
            {
                hit.transform.SetParent(playerHand);
                hit.transform.localPosition = Vector3.zero;
                isItemInHand = true;
            }
        }
    }

    void DropItem()
    {
        Transform item = playerHand.GetChild(0);
        if (item != null)
        {
            item.transform.SetParent(null);
            isItemInHand = false;
        }
    }
}