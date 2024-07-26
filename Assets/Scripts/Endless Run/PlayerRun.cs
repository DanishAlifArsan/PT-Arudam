using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] private float crouchDuration;
    [SerializeField] private ProgressBar progressBar;
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
        playerCollider.center = new Vector3(0, -0.5f, 0);
        playerCollider.height = 1;
        isCrouch = true;
        yield return new WaitForSeconds(crouchDuration);
        playerCollider.center = Vector3.zero;
        playerCollider.height = 2;
        isCrouch = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            progressBar.DamagePlayer();
        }
    }
}
