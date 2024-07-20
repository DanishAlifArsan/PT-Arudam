using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject groundSection;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Wall")) {
            Instantiate(groundSection, new Vector3(18f, -1.331278f, -2.9f), Quaternion.identity);
        }
    }
}
