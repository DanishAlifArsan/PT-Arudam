using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override void OnHighlight(ItemInteract broadcaster, bool status)
    {
        if (TimeManager.instance.NightHour() && broadcaster.itemInHand == null)
        {
            ToggleHighlight(broadcaster.centerIndicator, status, "Interact Bed");
        }
    }

    public override void OnInteract(ItemInteract broadcaster)
    {
        if (TimeManager.instance.NightHour() && broadcaster.itemInHand == null)
        {
            GameManager.instance.EndDay();
        }
    }
}
