using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private List<Transform> pages;
    [SerializeField] private TextMeshProUGUI pageNumberText;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip paperSound;
    int currentIndex;
    Transform currentPage;

    private void OnEnable() {
        currentIndex = 0;
        FlipPage();
    }

    public void NextPage() {
        if (currentIndex >= pages.Count - 1)
        {
            gameObject.SetActive(false);
            return;
        }
        
        currentIndex++;
        FlipPage();
        currentPage.gameObject.SetActive(false);
        anim.SetTrigger("next");
    }

    public void PrevPage() {
        if (currentIndex <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        currentIndex--;
        FlipPage();
        currentPage.gameObject.SetActive(false);
        anim.SetTrigger("prev");
    }

    private void FlipPage() {
        AudioManager.instance.PlaySound(paperSound);
        int page = currentIndex +1;
        pageNumberText.text = page + "/" + pages.Count;
    }

    public void ShowPage() {
        currentPage = pages[currentIndex];
        currentPage.gameObject.SetActive(true);
        
    }

    private void OnDisable() {
        foreach (var item in pages)
        {
            item.gameObject.SetActive(false); 
        }
    }
}
