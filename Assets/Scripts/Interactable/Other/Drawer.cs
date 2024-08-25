using System.Collections;
using System.Collections.Generic;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;

public class Drawer : Interactable
{
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    private Animator anim;
    public bool isPulled = false;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        
        ToggleHighlight(broadcaster.centerIndicator, status, Indicator());
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        base.OnInteract(broadcaster);
        isPulled = !isPulled;
        anim.SetBool("isPulled", isPulled);
        ToggleHighlight(broadcaster.centerIndicator, true, Indicator());
    }

    private string Indicator() {
        string open = LocalizationManager.Localize("Interact Open");
        string close = LocalizationManager.Localize("Interact Close");
        return isPulled? close: open;
    }

    public void Open() {
        AudioManager.instance.PlaySound(openSound);
    }
    public void Close() {
        AudioManager.instance.PlaySound(closeSound);
    }
}
