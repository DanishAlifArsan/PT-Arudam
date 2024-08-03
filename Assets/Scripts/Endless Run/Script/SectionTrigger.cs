using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionTrigger : MonoBehaviour
{
    public List<GameObject> groundSection;
    [SerializeField] private GameObject enemySection;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private bool isShopScene = false;
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

            if (isShopScene)
            {
                GameObject instantiatedPlatform = Instantiate (sectionToGenerate, transform.parent.position + new Vector3(20f, -1.331278f, -2.9f), Quaternion.identity);
                instantiatedPlatform.transform.parent = transform.parent;
            } else {
                Instantiate(sectionToGenerate, new Vector3(20f, -1.331278f, -2.9f), Quaternion.identity, transform.parent);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("People"))
        {
            if (isShopScene)
            {
                EndlessRunManager.instance.EndlessRunEnd(true);
                endOfRun = false;
            } else {
                SceneManager.LoadScene(0); // to do ke home screen
            }
        }
    }
}
