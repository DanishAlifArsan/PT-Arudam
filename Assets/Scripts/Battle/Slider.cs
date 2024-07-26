using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 start;
    [SerializeField] private Vector3 end;
    [SerializeField] private Health health;
    private bool colliderFlag = false;
    private Status sliderStatus;

    public enum Status
    {
        Full,
        Half,
        Miss,
    }

    private void FixedUpdate() {
        float pinPosX = transform.localPosition.x + Time.deltaTime * speed;
        transform.localPosition = new Vector2(pinPosX, transform.localPosition.y);

        if (transform.localPosition.x > end.x)
        {
            ResetPosition();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && colliderFlag){
            ResetPosition();
        }
    }

    private void ResetPosition() {
        health.Damage(sliderStatus);
        transform.localPosition = start;
        colliderFlag = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
        switch (other.tag)
           {
            case "Full":
                sliderStatus = Status.Full;
                colliderFlag = true;
                break;
            case "Half":
                sliderStatus = Status.Half;
                colliderFlag = true;
                break;
            default:
                sliderStatus = Status.Miss;
                colliderFlag = true;
                break;
           }
    }

    private void OnTriggerExit2D(Collider2D other) {
        colliderFlag = false;
    }
}
