using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    // [SerializeField] private float pageSpeed = 0.1f;
    // [SerializeField] private List<Transform> pages;
    // int currentIndex = -1;
    // private bool rotate = false;
    
    // public void NextPage() {
    //     if (rotate) return;
    //     currentIndex++;
    //     float angle= 180;
    //     if (currentIndex >= pages.Count - 1)
    //     {
    //         return;
    //     }
    //     pages[currentIndex].SetAsLastSibling();
    //     StartCoroutine(Rotate(angle, true));
    // }

    // public void PrevPage() {
    //     if (rotate) return;
    //     float angle = 0;
    //     if (currentIndex -1 <= - 1)
    //     {
    //         return;
    //     }
    //     pages[currentIndex].SetAsLastSibling();
    //     StartCoroutine(Rotate(angle, false));
    // }

    // private IEnumerator Rotate(float angle, bool forward) {
    //     float value = 0;
    //     while (true)
    //     {
    //         rotate = true;
    //         Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
    //         value += Time.deltaTime * pageSpeed;
    //         pages[currentIndex].rotation = Quaternion.Slerp(pages[currentIndex].rotation, targetRotation, value);
    //         float angle1 = Quaternion.Angle(pages[currentIndex].rotation, targetRotation);
    //         if (angle1 < 0.1f)
    //         {
    //             if (!forward)
    //             {
    //                 currentIndex--;
    //             }   
    //             rotate = false;
    //             break;
    //         }
    //     }
    //     yield return null;
    // }

    [SerializeField] private List<Transform> pages;
    [SerializeField] private Animator anim;
    int currentIndex = 0;
    Transform currentPage;

    private void OnEnable() {
        currentPage = pages[0];
        pages[0].gameObject.SetActive(true);
    }

    public void NextPage() {
        if (currentIndex >= pages.Count - 1)
        {
            gameObject.SetActive(false);
            return;
        }
        
        currentIndex++;
        anim.SetTrigger("next");
        ShowPage();
    }

    public void PrevPage() {
        if (currentIndex <= 0)
        {
            gameObject.SetActive(false);
            return;
        }

        currentIndex--;
        anim.SetTrigger("prev");
        ShowPage();
    }

    private void ShowPage() {
        currentPage.gameObject.SetActive(false);
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
