using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaper : MonoBehaviour
{
    [SerializeField] private List<GameObject> newsPaperScene; 
    [SerializeField] private HomeScreen homeScreen;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip clickSound;
    private bool isInteract = false;
    private int activeNewspaper;

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1) ) {
                HandleClose(activeNewspaper);
            }
        }
    }

    public void ShowScene(int index) {
        AudioManager.instance.PlaySound(paperSound);
        newsPaperScene[index].SetActive(true);
        isInteract = true;
        activeNewspaper = index;
    }
    private void HandleClose(int index) {
        if (index < 2)
        {
            RemoveScene(index);
        } else {
            ReturnHome();
        }
    }
    private void RemoveScene(int index) {
        AudioManager.instance.PlaySound(clickSound);
        newsPaperScene[index].SetActive(false);
        isInteract = false;
    }

    private void ReturnHome() {
        homeScreen.LoadScene(0);
    }

    
}
