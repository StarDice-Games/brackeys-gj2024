
using UnityEngine;

public interface IInteractable
{
    public bool IsInteractable { get; set; }
    public bool IsCompleted();
    public void HoverInteract();
    public void ExitInteract();
    public void Interact();
    public Transform GetTransform();
}