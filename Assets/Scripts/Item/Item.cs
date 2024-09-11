using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    public ItemSO ItemSO { get => itemSO; }

    bool isGrabbable = true;
    public bool IsGrabbable { get => isGrabbable; set => isGrabbable = value; }

    public void Interact()
    {

    }
}
