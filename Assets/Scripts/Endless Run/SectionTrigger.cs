using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public List<GameObject> groundSection;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Wall")) {
            GameObject random = groundSection[Random.Range(0, groundSection.Count)];

            Instantiate(random, new Vector3(18f, -1.331278f, -2.9f), Quaternion.identity);
        }
    }
}
