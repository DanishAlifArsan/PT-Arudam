using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Collider standingCollider;
    [SerializeField] Collider crouchCollider;
    [SerializeField] private float crouchDuration;
    private Rigidbody rb;
    private bool isJump, isCrouch;
    private void Start() {
        isCrouch = false;
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        if (IsGrounded())
        {
            isJump = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() && !isCrouch)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            isJump = true;
        }   

        if (Input.GetKeyDown(KeyCode.DownArrow) && IsGrounded() && !isJump)
        {
            StartCoroutine(Crouch());
        } 
    }

    bool IsGrounded() {
        return Physics.CheckSphere(groundCheck.position, .1f, groundLayer);
    }

    private IEnumerator Crouch() {
        standingCollider.enabled = false;
        crouchCollider.enabled = true;
        isCrouch = true;
        yield return new WaitForSeconds(crouchDuration);
        standingCollider.enabled = true;
        crouchCollider.enabled = false;
        isCrouch = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            Debug.Log("Hit Obstacle");
        }
    }
}
