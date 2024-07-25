using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMove : MonoBehaviour
{
   [SerializeField] private float moveSpeed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * moveSpeed, Space.World);
    }
}
