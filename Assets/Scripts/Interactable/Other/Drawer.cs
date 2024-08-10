using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : Interactable
{
    private Animator anim;
    public bool isPulled = false;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        
        ToggleHighlight(broadcaster.centerIndicator, status);
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        isPulled = !isPulled;
        highlight.highlightName = isPulled? "Tutup": "buka";
        anim.SetBool("isPulled", isPulled);
        ToggleHighlight(broadcaster.centerIndicator, true);
    }
}
