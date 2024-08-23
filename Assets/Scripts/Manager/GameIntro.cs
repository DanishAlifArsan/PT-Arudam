using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameIntro : MonoBehaviour
{
    [SerializeField] private ItemInteract player;
    [SerializeField] private WantedPoster poster;
    [SerializeField] private PoliceAI police;

    //spawn polisi di toko
    public void SpawnPolice() {
        police.MoveToShop();
    }

    //dialog pertama
    public void Greeting() {
        ScrollingText.instance.Show("Greeting");
    }

    //dialog kedua
    public void Intro() {
        ScrollingText.instance.Show("Intro");
    }

    //dialog ketiga
    public void Closing() {
        ScrollingText.instance.Show("Closing");
    }

    //membuka poster
    public void ShowPoster() {
        police.anim.SetTrigger("poster");
        poster.OnInteract(player);
    }

    //menjalankan police ai
    public void MovePolice() {
        police.BackToHome();
    }
}
