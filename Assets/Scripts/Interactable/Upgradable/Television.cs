using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;
using YoutubePlayer;

public class Television : Interactable
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject tvUI;
    [SerializeField] private TMP_InputField videoInput;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private PrepareYoutubeVideo prepare;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    private ItemInteract broadcaster;
    private bool isInteract = false;

    private void Start() {
        videoInput.onEndEdit.AddListener(delegate {PrepareVideo();});
    }

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CloseTV();
            }
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        tvUI.SetActive(true);
        this.broadcaster = broadcaster;
        broadcaster.SetIndicator(true,"Kembali");
        director.Play();
        ToggleHighlight(broadcaster.centerIndicator, false);
        broadcaster.canvas.SetActive(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status);
    }

     private void CloseTV() {
        broadcaster.SetIndicator(false);
        tvUI.SetActive(false);
        director.Stop();
        ToggleHighlight(broadcaster.centerIndicator, true);
        broadcaster.canvas.SetActive(true);
        broadcaster.controller.enabled = true;
        broadcaster.canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteract = false;
    }

    public void PrepareVideo() {
        string url = videoInput.text;
        if (!String.IsNullOrEmpty(url))
        {
            youtubePlayer.youtubeUrl = url;
            prepare.Prepare();
        }   
    }

    public void StopVideo() {
        videoPlayer.Stop();
    }
}
