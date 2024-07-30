using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionTrigger : MonoBehaviour
{
    public List<GameObject> groundSection;
    [SerializeField] private GameObject enemySection;
    [SerializeField] private ProgressBar progressBar;
    private GameObject sectionToGenerate;
    private bool endOfRun = false;
    private void Update() {
        if(progressBar.CheckDistance() && !endOfRun) {
            sectionToGenerate = enemySection;
            endOfRun = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Wall")) 
        {
            if (!endOfRun)
            {
                sectionToGenerate = groundSection[Random.Range(0, groundSection.Count)];
            }

            Instantiate(sectionToGenerate, new Vector3(20f, -1.331278f, -2.9f), Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("People"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
