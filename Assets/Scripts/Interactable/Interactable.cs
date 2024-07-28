using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Highlight highlight;
    public abstract void OnInteract(ItemInteract broadcaster);
    public abstract void OnHighlight(ItemInteract broadcaster, bool status);
    public ItemType itemType;

    public void ToggleHighlight(InteractIndicator indicator, bool status) {
        highlight?.ToggleHighlight(indicator, status); 
    }
}
