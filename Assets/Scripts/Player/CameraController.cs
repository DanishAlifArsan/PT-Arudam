using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxYRotation;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxXRotation;
    [SerializeField] private float minXRotation;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform transformZAxis;
    
    private void Update()
    {
        float newRotationY =  transform.localRotation.eulerAngles.y + rotationSpeed * Input.GetAxis("Mouse X");
        float newRotationZ =  transformZAxis.localRotation.eulerAngles.z + rotationSpeed * Input.GetAxis("Mouse Y");
        newRotationZ = Mathf.Clamp(newRotationZ, minXRotation, maxXRotation);
        newRotationY = Mathf.Clamp(newRotationY, minYRotation, maxYRotation);
        transform.localRotation = Quaternion.Euler(0, newRotationY, 0);
        transformZAxis.localRotation = Quaternion.Euler(0, -110, newRotationZ);
    }
}
