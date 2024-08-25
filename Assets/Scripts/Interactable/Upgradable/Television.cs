using System;
using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;

public class Television : Electric
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject tvUI;
    [SerializeField] private TMP_InputField videoInput;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button StopButton;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private PrepareYoutubeVideo prepare;
    [SerializeField] private YoutubePlayer.YoutubePlayer youtubePlayer;
    [SerializeField] private PlayVideo playVideo;
    private ItemInteract broadcaster;
    private bool isInteract = false;
    private bool isOn = false;

    private void OnEnable() {
        prepare.youtubePlayer = youtubePlayer;
        playVideo.videoPlayer = videoPlayer;
        
        videoInput.onEndEdit.AddListener(delegate {PrepareVideo();});
        PlayButton.onClick.AddListener(delegate {PlayVideo();});
        StopButton.onClick.AddListener(delegate {StopVideo();});

        ElectricManager.instance.AddElectric(this);
    }

    private void OnDisable() {
        StopVideo();
    }

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CloseTV();
            }
        }

        OnCountCost(isOn);
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        base.OnInteract(broadcaster);
        tvUI.SetActive(true);
        this.broadcaster = broadcaster;
        string indicator = LocalizationManager.Localize("Menu Back");
        broadcaster.SetIndicator(true,indicator);
        director.Play();
        ToggleHighlight(broadcaster.centerIndicator, false, "Interact Tv");
        broadcaster.canvas.SetActive(false);
        broadcaster.controller.enabled = false;
        broadcaster.canInteract = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInteract = true;
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        ToggleHighlight(broadcaster.centerIndicator, status, "Interact Tv");
    }

     private void CloseTV() {
        broadcaster.SetIndicator(false);
        tvUI.SetActive(false);
        director.Stop();
        ToggleHighlight(broadcaster.centerIndicator, true, "Interact Tv");
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

    public void PlayVideo() {
        playVideo.Play();
        isOn = true;
    }

    public void StopVideo() {
        videoPlayer.Stop();
        isOn = false;
    }
}
