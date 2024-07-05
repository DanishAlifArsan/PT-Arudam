using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Highlight highlight;
    public abstract void OnInteract(ItemInteract broadcaster);
    public abstract void OnCancel(ItemInteract broadcaster);    // buat hp doang

    public void ToggleHighlight(bool status) {
        highlight?.ToggleHighlight(status);
    }

    public void EnableHighlight(bool status) {
        if (highlight != null)
        {
            highlight.isAbleToHighlight = status;
        }
    }
}
