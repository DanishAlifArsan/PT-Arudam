using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Speaker : Interactable
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private GameObject musicUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SongList songList;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private List<Song> musics;

    private ItemInteract broadcaster;
    private bool isInteract = false;

    private void Start() {
        GenerateList();
    }

    private void Update() {
        if (isInteract)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CloseSpeaker();
            }
        }
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
    }
}
