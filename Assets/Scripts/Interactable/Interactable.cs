using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Highlight highlight;
    public abstract void OnInteract(ItemInteract broadcaster);

    public void ToggleHighlight(bool status, InteractIndicator indicator) {
        highlight?.ToggleHighlight(status, indicator);
    }

    public void EnableHighlight(bool status) {
        if (highlight != null)
        {
            highlight.isAbleToHighlight = status;
        }
    }
}
