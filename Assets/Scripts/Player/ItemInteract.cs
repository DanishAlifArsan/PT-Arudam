using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public CameraController controller;
    public Transform playerHand;
    public Transform itemHand;
    [SerializeField] private float pickupRange;
    public Transform itemInHand;
    public bool canInteract = true;
    public GameObject canvas;
    private Camera cam;
    private bool hasHighlighted = false;

    private void Awake() {
        cam = Camera.main;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickupRange))
        {
            
            Interactable item = hit.collider.GetComponent<Interactable>();
            if (Input.GetMouseButtonDown(0) && canInteract) {
                Interact(item);
            }
        }
    }

    private void Interact(Interactable item) {
        if (item != null)
        {
            item.OnInteract(this);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (hasHighlighted) return;

        if (other.CompareTag("Item"))
        {
            hasHighlighted = true;
            Interactable item = other.GetComponent<Interactable>();
            item.ToggleHighlight(true);
            // if (Input.GetMouseButtonDown(0) && canInteract) {
            //     Interact(item);
            // } else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)) {   //buat hp doang
            //     Cancel(item);
            // }
        } else if (other.CompareTag("Storage"))
        {
            Interactable item = other.GetComponent<Interactable>();
            if(itemInHand != null) {
                hasHighlighted = true;
                item.ToggleHighlight(true);
            } else {
                hasHighlighted = false;
                item.ToggleHighlight(false);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Item"))
        {
            Interactable item = other.GetComponent<Interactable>();
            item.ToggleHighlight(false);
            hasHighlighted = false;
        } else if (other.CompareTag("Storage"))
        {
            Interactable item = other.GetComponent<Interactable>();
            item.ToggleHighlight(false);
            hasHighlighted = false;
        }
    }
}