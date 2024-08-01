using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameIntro : MonoBehaviour
{
    [SerializeField] private ItemInteract player;
    [SerializeField] private WantedPoster poster;
    [SerializeField] private NavMeshAgent police;
    [SerializeField] private Transform policeHome;
    [SerializeField] private Transform policePoint;

    //spawn polisi di toko
    public void SpawnPolice() {
        police.SetDestination(policePoint.position);
    }

    //dialog pertama
    public void Greeting() {
        ScrollingText.instance.Show("Selamat pagi pak");
    }

    //dialog kedua
    public void Intro() {
        ScrollingText.instance.Show("Akhir akhir ini maling sandal sedang berkeliaran di sekitar kampung sini");
    }

    //dialog ketiga
    public void Closing() {
        ScrollingText.instance.Show("Tolong lapor jika melihat seseorang yang ciri cirinya kayak gini");
    }

    //membuka poster
    public void ShowPoster() {
        poster.OnInteract(player);
    }

    //menjalankan police ai
    public void MovePolice() {
        police.SetDestination(policeHome.position);
    }
}
