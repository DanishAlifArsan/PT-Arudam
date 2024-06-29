using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public CameraController controller;
    public Transform playerHand;
    [SerializeField] private float pickupRange;
    public bool isItemInHand = false;
    private Camera cam;

    private void Awake() {
        controller = GetComponent<CameraController>();
        cam = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange))
        { 
            Debug.Log(hit.transform.gameObject);
            Interactable item = hit.collider.GetComponent<Interactable>();
            if (Input.GetMouseButtonDown(0)) {
                Interact(item);
            } else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) {
                Cancel(item);
            }
        }
    }

    private void Interact(Interactable item) {
        if (item != null)
        {
            item.OnInteract(this);
            // switch (getItemTag(hit))
            //     {
            //         case "Hold Item": HoldItemHandler(item); break;
            //         case "Stack Item": StackItemHandler(item); break;
            //         default: item.OnInteract(this); break;
            //     }
            // }
        }
    }

    private void Cancel(Interactable item) {
        item?.OnCancel(this);
    }

    // private void HoldItemHandler(Interactable item) {
    //     if (!isItemInHand)
    //     {
    //         item.OnInteract(this);
    //     } else {
    //         item.OnCancel(this);
    //     }
    // }
    // private void StackItemHandler(Interactable item) {
    //     // todo
    // }

    // private String getItemTag(RaycastHit hit) {
    //     return hit.transform.gameObject.tag;
    // }
}