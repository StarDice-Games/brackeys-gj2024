using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO itemSO;
    private Vector2 gridPosition;

    public ItemSO ItemSO { get => itemSO; }

    public void Interact()
    {

    }
}
