using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeak : MonoBehaviour
{
    public AudioClip happySound;
    public AudioClip angrySound;

    public void Happy() {
        if (happySound != null)
        {
            AudioManager.instance.PlaySound(happySound);
        }
    }
    public void Angry() {
        if (angrySound != null)
        {
            AudioManager.instance.PlaySound(angrySound);
        }
    }
}
