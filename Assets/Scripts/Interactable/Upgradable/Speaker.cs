using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using YoutubePlayer;

public class Speaker : Electric
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject musicUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SongList songList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private List<Song> musics;
    [SerializeField] private Button playButton;
    [SerializeField] private Button StopButton;
    private ItemInteract broadcaster;
    private bool isInteract = false;
    private bool isOn = false;

    private void OnEnable() {
        playButton.onClick.AddListener( delegate {PlayMusic();});
        StopButton.onClick.AddListener( delegate {StopMusic();});
        GenerateList();
    }

    private void OnDisable() {
        isOn = false; 
    }

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CloseSpeaker();
            }
        }
        OnCountCost(isOn);
        playButton.interactable = !isOn;
        StopButton.interactable = isOn;
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        musicUI.SetActive(true);
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

     private void CloseSpeaker() {
        broadcaster.SetIndicator(false);
        musicUI.SetActive(false);
        director.Stop();
        ToggleHighlight(broadcaster.centerIndicator, true);
        broadcaster.canvas.SetActive(true);
        broadcaster.controller.enabled = true;
        broadcaster.canInteract = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInteract = false;
    }

    private void GenerateList() {
        foreach (var item in musics)
        {
            SongList instantiatedSongList = Instantiate(songList, canvas);
            instantiatedSongList.Setup(item);
            instantiatedSongList.OnButtonClick += OnButtonClick;
        }
    }

    private void OnButtonClick(AudioClip music) {
        audioSource.clip = music;
        audioSource.Play();
        isOn = true;
    }

    private void PlayMusic() {
        if (audioSource.clip != null)
        { 
            audioSource.Play();
            isOn = true;
        }
    }

    private void StopMusic() {
        audioSource.Stop();
        isOn = false;
    }
}
