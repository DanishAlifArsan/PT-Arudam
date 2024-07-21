using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("jump");
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
        }    
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Hit Obstacle");
        }
    }
}
