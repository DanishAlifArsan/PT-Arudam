using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject destroyedObject;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("People"))
        {
            if (destroyedObject != null) {
                destroyedObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
