
using UnityEngine;

public interface IInteractable
{
    public bool IsInteractable { get; set; }
    public void HoverInteract();
    public void ExitInteract();
    public void Interact();
    public Transform GetTransform();
}