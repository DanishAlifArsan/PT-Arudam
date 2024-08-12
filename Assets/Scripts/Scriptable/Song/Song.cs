using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Song")]
public class Song : ScriptableObject
{
    public string title;
    public string artist;
    public AudioClip music;
}
