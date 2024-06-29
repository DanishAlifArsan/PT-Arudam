public interface Interactable
{
    void OnInteract(ItemInteract broadcaster);
    void OnCancel(ItemInteract broadcaster);
}
