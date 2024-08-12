using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI ArtistText;
    public Action<AudioClip> OnButtonClick;
    private AudioClip music;

    public void Setup(Song song) {
        titleText.text = song.title;
        ArtistText.text = song.artist;
        music = song.music;
    }

    public void PlaySong() {
        OnButtonClick.Invoke(music);
    }
}
