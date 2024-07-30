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
    public Interactable itemInHand;
    public bool canInteract = true;
    public GameObject canvas;
    private Camera cam;
    private bool hasHighlighted = false;
    public InteractIndicator centerIndicator;
    public InteractIndicator sideIndicator;

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
            Interactable item = other.GetComponent<Interactable>();
            item.OnHighlight(this, true);
            hasHighlighted = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Item"))
        {
            Interactable item = other.GetComponent<Interactable>();
            item?.OnHighlight(this, false);
            hasHighlighted = false;
        }
    }

    public void SetIndicator(bool status, string name = "") {
        sideIndicator.gameObject.SetActive(status);
        sideIndicator.SetName(name);
    }
}